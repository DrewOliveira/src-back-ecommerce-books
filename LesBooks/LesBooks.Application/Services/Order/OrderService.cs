using AngleSharp.Css;
using AngleSharp.Io;
using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Order;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.Interfaces;
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
        public OrderService(IBookDAO bookDAO, ICardDAO cardDAO, ICouponDAO couponDAO, IAdressDAO adressDAO, IOrderPurchaseDAO orderPurchaseDAO, IClientDAO clientDAO, IStockDAO stockDAO, IOrderDAO orderDAO, IAdressService adressService,IOrderHistoryStatusDAO orderHistoryStatusDAO)
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

        }
        public async Task<ResponseBase> CreateOrderReplacement(CreateOrderReplacementRequest request)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                Order orderOrigin = _orderDAO.GetOrderById(request.order_Id);
                Order orderReplacement = new Order();
                orderReplacement.items = orderReplacement.items.FindAll(orderItem => request.itens.Any(item => item.id == orderItem.id));
                if (request.itens.All(requestItem => orderReplacement.items.Any(replace => replace.id == requestItem.id)))
                {
                    throw new Exception("Houve um erro no processamento da troca.");
                }
                orderReplacement.totalValue = GetTotalValue(orderReplacement.items, null, 0);
                orderReplacement.client = orderOrigin.client;
                orderReplacement.dateOrder = DateTime.Now;
                orderReplacement.type = TypeOrder.REPLACEMENT;
                orderReplacement.statusOrder = StatusOrder.REPLACEMENT;

                orderReplacement = _orderDAO.CreateReplacement(orderReplacement);
                _orderHistoryStatusDAO.CreateStatusHistory((int)orderReplacement.statusOrder, orderReplacement.id, 0);
                this.UpdateStockQuantity(orderReplacement.items, false);


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
                #endregion

                #region Valor da compra
                double deliveyPrice = DeliveryPrice(orderPurchase.adress.zipCode);
                orderPurchase.totalValue = this.GetTotalValue(orderPurchase.items, orderPurchase.coupons, deliveyPrice);
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
                    if (PaymentValidation(orderPurchase.totalValue, orderPurchase.payments))
                        throw new Exception("Ocorreu um erro com o pagamento!");
                }
                #endregion

                #region Criando novos cupons se necessário
                if(orderPurchase.totalValue < 0)
                {
                    CreateCoupon(orderPurchase.totalValue, request.client_id);
                }
                #endregion

                _orderPurchaseDAO.CreatePurchase(orderPurchase);
                _orderHistoryStatusDAO.CreateStatusHistory((int)orderPurchase.statusOrder,orderPurchase.id, 0);
                this.UpdateStockQuantity(orderPurchase.items,true);

            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };
            }

            return response;
        }
        public void CreateCoupon(double value, int client_id,TypeCoupon type = TypeCoupon.REPLACEMENT)
        {
            try
            {
                Coupon coupon = new Coupon()
                {
                    typeCoupon = type,
                    value = value * -1

                };
                _couponDAO.CreateCoupon(coupon, client_id);
            }
            catch(Exception ex)
            {

            }
        }
        private Boolean PaymentValidation(double value,List<Payment> payments)
        {
            double aux = 0;
            foreach(Payment payment in payments)
            {
                aux += payment.value;
            }
            return aux == value;
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

        private Double GetTotalValue(List<Item> itens, List<Coupon> coupons,double deliveryPrice)
        {
            double totalValueMonted = deliveryPrice;

            foreach (var item in itens)
            {
                totalValueMonted = totalValueMonted + item.totalValue;
            }

            if (coupons != null)
            {
                foreach (var coupon in coupons)
                {
                    totalValueMonted = totalValueMonted - coupon.value;
                }
            }

            return totalValueMonted;
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
                if(newStatus == StatusOrder.CHANGED)
                {
                    Coupon coupon = new Coupon()
                    {
                        value = order.totalValue,
                        typeCoupon = TypeCoupon.REPLACEMENT,
                        description = String.Format("TROCA{0}", order.totalValue)
                    };
                    _couponDAO.CreateCoupon(coupon, order.client.id);
                    this.UpdateStockQuantity(order.items, false);
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
