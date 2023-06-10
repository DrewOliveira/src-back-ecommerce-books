using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Order;
using LesBooks.Application.Responses;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<CreateOrderPurchaseResponse> CreateOrderPurchase(CreateOrderPurchaseRequest request);

        public Task<GetAllOrdersPurchaseByClientIdResponse> GetOrderPurchaseByClientId(int client_id);

        public Task<GetOrderByIdResponse> GetOrderById(int client_id);

        public Task<GetOrdersResponse> GetOrders();
        public double DeliveryPrice(string cep);
        public Task<ResponseBase> PatchOrder(PatchOrderRequest request);
        public Task<ResponseBase> CreateOrderReplacement(CreateOrderReplacementRequest request);
        public Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request);
    }
}
