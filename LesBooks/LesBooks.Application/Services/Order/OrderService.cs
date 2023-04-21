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
        public OrderService(IBookDAO bookDAO, ICardDAO cardDAO, ICouponDAO couponDAO, IAdressDAO adressDAO, IOrderPurchaseDAO orderPurchaseDAO, IClientDAO clientDAO, IStockDAO stockDAO)
        {
            _bookDAO = bookDAO;
            _cardDAO = cardDAO;
            _couponDAO = couponDAO;
            _adressDAO = adressDAO;
            _orderPurchaseDAO = orderPurchaseDAO;
            _clientDAO = clientDAO;
            _stockDAO = stockDAO;
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
            catch(Exception ex) 
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
    }
}
