using AngleSharp.Io;
using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Order;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
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
        public OrderService(IBookDAO bookDAO, ICardDAO cardDAO, ICouponDAO couponDAO, IAdressDAO adressDAO, IOrderPurchaseDAO orderPurchaseDAO, IClientDAO clientDAO, IStockDAO stockDAO, IOrderDAO orderDAO, IAdressService adressService)
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
        }

        public async Task<CreateOrderPurchaseResponse> CreateOrderPurchase(CreateOrderPurchaseRequest request)
        {
            List<Item> itens = new List<Item>();
            List<Payment> payments = new List<Payment>();
            List<Coupon> coupons = new List<Coupon>();
            OrderPurchase orderPurchase = new OrderPurchase();
            CreateOrderPurchaseResponse response = new CreateOrderPurchaseResponse();

            try
            {
                itens = await this.GetItens(request.itens);

                if (!this.CheckoutQuantityStock(itens))
                {
                    response.erros.Add(new Erro
                    {
                        descricao = "Quantity not possible for itens this purchase",
                        detalhes = new Exception("Stock Limit"),
                        cod = "404"
                    });

                    return response;
                }

                payments = await this.GetPayments(request.payments);
                coupons = await this.GetCoupons(request.coupons);


                orderPurchase.totalValue = this.GetTotalValue(itens, coupons);
                orderPurchase.dateOrder = DateTime.Now;
                orderPurchase.items = itens;
                orderPurchase.type = Model.Enums.TypeOrder.PURCHASE;
                orderPurchase.payments = payments;
                orderPurchase.coupons = coupons;
                orderPurchase.adress = _adressDAO.GetAdressById(request.adress_delivery_id);
                orderPurchase.client = _clientDAO.GetClientById(request.client_id);
                orderPurchase.statusOrder = StatusOrder.PROCESSING;

                _orderPurchaseDAO.CreatePurchase(orderPurchase);
                _orderDAO.CreateStatusHistory((int)orderPurchase.statusOrder,orderPurchase.id, 0);

            }
            catch (Exception ex)
            {
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });
            }

            try
            {
                this.UpdateStockQuantity(itens);
            }
            catch (Exception ex)
            {
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });
            }

            return response;
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

        private void UpdateStockQuantity(List<Item> itens)
        {
            try
            {
                foreach (var item in itens)
                {
                    _stockDAO.UpdateQuantityStockByBookId(item.quantity, item.book.id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Double GetTotalValue(List<Item> itens, List<Coupon> coupons)
        {
            double totalValueMonted = 0;

            foreach (var item in itens)
            {
                totalValueMonted = totalValueMonted + item.totalValue;
            }

            foreach (var coupon in coupons)
            {
                totalValueMonted = totalValueMonted - coupon.value;
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
                OrdersPurchaseResponse.erros.Add(new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                });
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
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });

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
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });

            }

            return response;
        }
        public async Task<ResponseBase> PatchOrder(PatchOrderRequest request)
        {

            ResponseBase response = new ResponseBase();
            try
            {
                Order order = _orderPurchaseDAO.GetOrderPurchases(request.OrderId).First();
                StatusOrder newStatus = (StatusOrder)request.statusId;
                if ((int)order.statusOrder >= (int)newStatus)
                {
                    throw new Exception("Alteração de status não disponivél para o status encaminhado."); 
                }



                _orderDAO.CreateStatusHistory(request.statusId, request.OrderId, request.admId);
            }
            catch (Exception ex)
            {
                response.erros = new List<Erro>
                {
                    new Erro(){
                        descricao = ex.Message,
                        detalhes = ex
                    }
                };
            }
            return response;
            } 
        
        
 
        public decimal DeliveryPrice(string zipcode)
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
                decimal valorFrete = distancia * 0.05m; // Exemplo: Valor fixo de R$ 0,05 por km

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
