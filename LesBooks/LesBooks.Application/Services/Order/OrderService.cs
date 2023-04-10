using LesBooks.Application.Requests;
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
        public OrderService(IBookDAO bookDAO, ICardDAO cardDAO, ICouponDAO couponDAO, IAdressDAO adressDAO)
        {
            _bookDAO = bookDAO;
            _cardDAO = cardDAO;
            _couponDAO = couponDAO;
            _adressDAO = adressDAO;
        }

        public async Task<CreateOrderPurchaseResponse> CreateOrderPurchase(CreateOrderPurchaseRequest request)
        {
            CreateOrderPurchaseResponse response = new CreateOrderPurchaseResponse();
            double totalValueMonted = 0;

            try
            {
                List<Item> itens = new List<Item>();
                foreach (var itemRequest in request.itens)
                {
                    Item item = new Item();

                    item.quantity = itemRequest.quantity;
                    item.book = _bookDAO.GetBookById(itemRequest.book_id);
                    item.totalValue = item.book.value * itemRequest.quantity;

                    itens.Add(item);
                }

                List<Payment> payments = new List<Payment>();
                foreach (var paymentCard in request.payments)
                {
                    Payment payment = new Payment();

                    payment.value = paymentCard.value;
                    payment.aprroved = true;
                    payment.card = _cardDAO.GetCardById(paymentCard.card_id);

                    payments.Add(payment);
                }


                List<Coupon> coupons = new List<Coupon>();
                foreach (var itemCoupon in request.coupons)
                {
                    Coupon counpon = _couponDAO.GetCouponById(itemCoupon.id);

                    coupons.Add(counpon);
                }

                foreach (var item in itens)
                {
                    totalValueMonted = totalValueMonted + item.totalValue;
                }

                OrderPurchase orderPurchase = new OrderPurchase();

                orderPurchase.totalValue = totalValueMonted;
                orderPurchase.dateOrder = DateTime.Now;
                orderPurchase.items = itens;
                orderPurchase.type = Model.Enums.TypeOrder.PURCHASE;
                orderPurchase.payments = payments;
                orderPurchase.coupons = coupons;
                orderPurchase.adress = _adressDAO.GetAdressById(request.adress_delivery_id);
                orderPurchase.statusOrder = StatusOrder.PROCESSING;
            }
            catch (Exception ex)
            {
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });
            }
            return response;
        }
    }
}
