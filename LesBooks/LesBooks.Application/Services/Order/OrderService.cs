using AngleSharp.Css;
using AngleSharp.Io;
using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Order;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.Interfaces;
using LesBooks.EmailSender;
using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services
{
    public class OrderService : IOrderService
    {
        IBookDAO _bookDAO;
        ICardDAO _cardDAO;
        ICouponDAO _couponDAO;
        IAdressDAO _adressDAO;
        IOrderPurchaseDAO _orderPurchaseDAO;
        IClientDAO _clientDAO;
        IStockDAO _stockDAO;
        IOrderDAO _orderDAO;
        IAdressService _adressService;
        IOrderHistoryStatusDAO _orderHistoryStatusDAO;
        IStockRedis _stockRedis;
        ISender _sender;
        public OrderService(IBookDAO bookDAO, ICardDAO cardDAO, ICouponDAO couponDAO, IAdressDAO adressDAO, IOrderPurchaseDAO orderPurchaseDAO, IClientDAO clientDAO, IStockDAO stockDAO, IOrderDAO orderDAO, IAdressService adressService,IOrderHistoryStatusDAO orderHistoryStatusDAO, IStockRedis stockRedis, ISender sender)
        {
            _bookDAO = bookDAO;
            _cardDAO = cardDAO;
            _couponDAO = couponDAO;
            _adressDAO = adressDAO;
            _orderPurchaseDAO = orderPurchaseDAO;
            _clientDAO = clientDAO;
            _stockDAO = stockDAO;
            _orderDAO = orderDAO;
            _adressService = adressService;
            _orderHistoryStatusDAO = orderHistoryStatusDAO;
            _stockRedis = stockRedis;
            _sender = sender;
        }

        public async Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request)
        {
            GetDashboardResponse response = new GetDashboardResponse();
            response.dashboard = new Dashboard();
            response.dashboard.AddLabel(request.init, request.end);
            if(request.type == 1)   
                response.dashboard = _orderDAO.GetDashboardByCategory(response.dashboard);
            else
                response.dashboard = _orderDAO.GetDashboardByProduct(response.dashboard);

            return response;

        }
        public async Task<ResponseBase> CreateOrderReplacement(CreateOrderReplacementRequest request)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                Order orderOrigin = _orderDAO.GetOrderById(request.order_Id);
                Order orderReplacement = new Order();

                if (orderOrigin.items.Sum(i => i.quantity) == request.items.Sum(i => i.quantity))
                {
                    PatchOrderRequest patch = new PatchOrderRequest();
                    patch.OrderId = request.order_Id;
                    patch.statusId = (int)StatusOrder.REPLACEMENT;
                    patch.updateStock = false;
                    PatchOrder(patch);
                }

                orderReplacement.items = request.items.FindAll(orderItem => request.items.Any(item => item.id == orderItem.id));
                if (!request.items.All(requestItem => orderReplacement.items.Any(replace => replace.id == requestItem.id)))
                {
                    throw new Exception("Houve um erro no processamento da troca.");
                }

                orderReplacement.items = await GetItensReplacement(orderReplacement.items);
                orderReplacement.totalValue = GetTotalValue(orderReplacement.items, 0);
                orderReplacement.client = orderOrigin.client;
                orderReplacement.dateOrder = DateTime.Now;
                orderReplacement.type = TypeOrder.REPLACEMENT;
                orderReplacement.statusOrder = StatusOrder.REPLACEMENT;

                orderReplacement = _orderDAO.CreateReplacement(orderReplacement);
                _orderHistoryStatusDAO.CreateStatusHistory((int)orderReplacement.statusOrder, orderReplacement.id, 0);
                

            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };
            }
            return response;
        }
        public async Task<CreateOrderPurchaseResponse> CreateOrderPurchase(CreateOrderPurchaseRequest request)
        {
            OrderPurchase orderPurchase = new OrderPurchase();
            CreateOrderPurchaseResponse response = new CreateOrderPurchaseResponse();

            try
            {

                #region Obtendo dados completos da compra
                orderPurchase.adress = _adressDAO.GetAdressById(request.adress_delivery_id);
                orderPurchase.client = _clientDAO.GetClientById(request.client_id);
                orderPurchase.items = await this.GetItens(request.itens);
                orderPurchase.payments = await this.GetPayments(request.payments);
                orderPurchase.coupons = await this.GetCoupons(request.coupons);
                orderPurchase.dateOrder = DateTime.Now;
                orderPurchase.type = Model.Enums.TypeOrder.PURCHASE;
                orderPurchase.statusOrder = StatusOrder.PROCESSING;
                #endregion

                #region Valor da compra
                double deliveyPrice = DeliveryPrice(orderPurchase.adress.zipCode);
                orderPurchase.totalValue = this.GetTotalValue(orderPurchase.items, deliveyPrice);
                #endregion

                #region Validando estoque
                if (!this.CheckoutQuantityStock(orderPurchase.items))
                {
                    response.erros = new Erro
                    {
                        descricao = "Quantity not possible for itens this purchase",
                        detalhes = new Exception("Stock Limit"),
                        cod = "404"
                    };

                    return response;
                }
                #endregion

                #region Validando pagamento
                if(orderPurchase.totalValue > 0)
                {
                    if (!PaymentValidation(orderPurchase.totalValue, orderPurchase.payments, orderPurchase.coupons))
                        throw new Exception("Ocorreu um erro com o pagamento!");
                }
                #endregion
                double totalValueCoupon = 0;

                orderPurchase.coupons.ForEach((c) =>
                {
                    totalValueCoupon += c.value;
                });

                #region Criando novos cupons se necessário
                if (orderPurchase.totalValue < totalValueCoupon)
                {
                    double valueFinishTotal = totalValueCoupon - orderPurchase.totalValue;
                    CreateCoupon(valueFinishTotal, request.client_id);
                }
                #endregion

                _orderPurchaseDAO.CreatePurchase(orderPurchase);
                _orderHistoryStatusDAO.CreateStatusHistory((int)orderPurchase.statusOrder,orderPurchase.id, 0);
                foreach (Item item in orderPurchase.items)
                    _stockRedis.CreateBlock(orderPurchase.id.ToString(), item.book.id.ToString(), item.quantity);
                    _stockRedis.freeTemporaryBlock(orderPurchase.client.id.ToString());

                orderPurchase.coupons.ForEach(c => _couponDAO.UpdateCoupon(c.id, orderPurchase.id));
            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };
            }

            return response;
        }
        public void CreateCoupon(double value, int client_id, TypeCoupon type = TypeCoupon.REPLACEMENT)
        {
            try
            {
                Coupon coupon = new Coupon()
                {
                    typeCoupon = type,
                    value = value,
                    description = $"TROCA{value}"

                };
                _couponDAO.CreateCoupon(coupon, client_id);
            }
            catch(Exception ex)
            {

            }
        }
        private Boolean PaymentValidation(double value, List<Payment> payments, List<Coupon> coupons)
        {
            double aux = 0;
            foreach(Payment payment in payments)
            {
                aux += payment.value;
            }

            if (coupons != null)
            {
                foreach (var coupon in coupons)
                {
                    aux += coupon.value;
                }
            }

            return aux <= value;
        }
        private Boolean CheckoutQuantityStock(List<Item> itens)
        {
            Boolean checkout = true;

            try
            {
                foreach (var item in itens)
                {
                    Stock stock = _stockDAO.GetStockByBookId(item.book.id);

                    if (checkout && stock.quantity < item.quantity)
                    {
                        checkout = false;
                    }
                }

                return checkout;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateStockQuantity(List<Item> itens,bool remove)
        {
            try
            {
                foreach (var item in itens)
                {
                    _stockDAO.UpdateQuantityStockByBookId(item.quantity, item.book.id,remove);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Double GetTotalValue(List<Item> itens, double deliveryPrice)
        {
            double totalValueMonted = deliveryPrice;

            foreach (var item in itens)
            {
                totalValueMonted = totalValueMonted + item.totalValue;
            }


            return totalValueMonted;
        }

        private async Task<List<Item>> GetItensReplacement(List<Item> requestItens)
        {
            try
            {
                List<Item> itens = new List<Item>();
                foreach (var itemRequest in requestItens)
                {
                    Item item = new Item();

                    item.quantity = itemRequest.quantity;
                    item.book = _bookDAO.GetBookById(itemRequest.book.id);
                    item.totalValue = item.book.value * itemRequest.quantity;

                    itens.Add(item);
                }

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task<List<Item>> GetItens(List<ItemPurchaseRequest> requestItens)
        {
            try
            {
                List<Item> itens = new List<Item>();
                foreach (var itemRequest in requestItens)
                {
                    Item item = new Item();

                    item.quantity = itemRequest.quantity;
                    item.book = _bookDAO.GetBookById(itemRequest.book_id);
                    item.totalValue = item.book.value * itemRequest.quantity;

                    itens.Add(item);
                }

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task<List<Payment>> GetPayments(List<PaymentPurchaseRequest> requestPayments)
        {
            try
            {
                List<Payment> payments = new List<Payment>();
                foreach (var paymentCard in requestPayments)
                {
                    Payment payment = new Payment();

                    payment.value = paymentCard.value;
                    payment.aprroved = true;
                    payment.card = _cardDAO.GetCardById(paymentCard.card_id);

                    payments.Add(payment);
                }

                return payments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<Coupon>> GetCoupons(List<CouponPurchaseRequest> requestCoupons)
        {
            try
            {
                List<Coupon> coupons = new List<Coupon>();
                foreach (var itemCoupon in requestCoupons)
                {
                    Coupon counpon = _couponDAO.GetCouponById(itemCoupon.id);

                    coupons.Add(counpon);
                }

                return coupons;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetAllOrdersPurchaseByClientIdResponse> GetOrderPurchaseByClientId(int client_id)
        {
            GetAllOrdersPurchaseByClientIdResponse OrdersPurchaseResponse = new GetAllOrdersPurchaseByClientIdResponse();

            try
            {
                OrdersPurchaseResponse.purchases = _orderPurchaseDAO.GetOrderPurchases(client_id);
            }
            catch (Exception err)
            {
                OrdersPurchaseResponse.erros = new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                };
            }

            return OrdersPurchaseResponse;
        }

        public async Task<GetOrderByIdResponse> GetOrderById(int order_id)
        {
            GetOrderByIdResponse response = new GetOrderByIdResponse();
            try
            {
                response.order_generic = _orderDAO.GetOrderById(order_id);
            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };

            }

            return response;
        }

        public async Task<GetOrdersResponse> GetOrders()
        {
            GetOrdersResponse response = new GetOrdersResponse();
            try
            {
                response.orders = _orderPurchaseDAO.GetOrderPurchases(null);
            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };

            }

            return response;
        }

        public async Task<ResponseBase> PatchOrder(PatchOrderRequest request)
        {

            ResponseBase response = new ResponseBase();
            try
            {
                Order order = _orderDAO.GetOrderById(request.OrderId);
                StatusOrder newStatus = (StatusOrder)request.statusId;
                if (CantTransitionTo(order.statusOrder,newStatus))
                {
                    throw new Exception("Alteração de status não disponivél para o status encaminhado."); 
                }
                switch (newStatus)
                {
                    case StatusOrder.CHANGED:
                        Coupon coupon = new Coupon()
                        {
                            value = order.totalValue,
                            typeCoupon = TypeCoupon.REPLACEMENT,
                            description = String.Format("TROCA{0}", order.totalValue)
                        };
                        _couponDAO.CreateCoupon(coupon, order.client.id);
                        if (request.updateStock)
                            this.UpdateStockQuantity(order.items, false);
                        break;
                    case StatusOrder.APPROVED_REPLACEMENT:
                        _sender.SendEmail(order.client.user.email, String.Format("Pedido #{0}", order.id), order.client.name, order.id.ToString());
                        break;
                    case StatusOrder.APPROVED:
                        _stockRedis.freeBlock(order.id.ToString());
                        this.UpdateStockQuantity(order.items, true);
                        break;
                    case StatusOrder.FAILED:
                        _stockRedis.freeBlock(order.id.ToString());
                        break;

                }
                _orderPurchaseDAO.UpdateStatusOrder(request.OrderId, request.statusId);
                _orderHistoryStatusDAO.CreateStatusHistory(request.statusId, request.OrderId, request.admId);

            }
            catch (Exception ex)
            {
                response.erros = 
                    new Erro(){
                        descricao = ex.Message,
                        detalhes = ex
                    
                };
            }
            return response;
        }
        public void CreateReplacementOrder()
        {

        }
        private bool CantTransitionTo(StatusOrder currentStatus, StatusOrder newStatus)
        {
            
            if (currentStatus == StatusOrder.FAILED)
            {
               
                return true;
            }

            
            int currentStatusValue = (int)currentStatus;
            int newStatusValue = (int)newStatus;

            
            return newStatusValue <= currentStatusValue;
        }




        public double DeliveryPrice(string zipcode)
        {
            string state =  _adressService.GetAdressByCep(zipcode).Result.adress.state;
            // Distâncias em km entre São Paulo e os demais estados brasileiros
            Dictionary<string, int> distancias = new Dictionary<string, int>()
            {
                { "AC", 3600 },
                { "AL", 2200 },
                { "AP", 3100 },
                { "AM", 3100 },
                { "BA", 1700 },
                { "CE", 2600 },
                { "DF", 1000 },
                { "ES", 900 },
                { "GO", 900 },
                { "MA", 2500 },
                { "MT", 1700 },
                { "MS", 1100 },
                { "MG", 600 },
                { "PA", 2500 },
                { "PB", 2300 },
                { "PR", 400 },
                { "PE", 2400 },
                { "PI", 2500 },
                { "RJ", 400 },
                { "RN", 2500 },
                { "RS", 1200 },
                { "RO", 2700 },
                { "RR", 3400 },
                { "SC", 700 },
                { "SE", 2100 },
                { "TO", 1700 },
                { "SP", 500 }
            };

            // Verifica se o estado de destino existe na lista de distâncias
            if (distancias.ContainsKey(state))
            {
                int distancia = distancias[state];
                double valorFrete = distancia * 0.05; // Exemplo: Valor fixo de R$ 0,05 por km

                return valorFrete;
            }
            else
            {
                // Estado de destino não encontrado na lista
                throw new ArgumentException("Estado de destino inválido.");
            }
        }

    }
}
