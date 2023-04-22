using LesBooks.Application.Requests;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] CreateOrderPurchaseRequest request)
        {
            var response = await this._orderService.CreateOrderPurchase(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
        [HttpPatch]
        public async Task<ActionResult<dynamic>> Patch([FromBody] PatchOrderRequest request)
        {
            
            var response = await _orderService.PatchOrder(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet("client/{id}")]
        public async Task<ActionResult<dynamic>> GetAllPurchasesByClientId(int id)
        {
            var response = await this._orderService.GetOrderPurchaseByClientId(id);

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.purchases);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet("delivery/{zipcode}")]
        public async Task<ActionResult<dynamic>> GetDeliveryPrice(string zipcode)
        {
            var response = this._orderService.DeliveryPrice(zipcode);

            if (response != 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetOrderById(int id)
        {
            var response = await this._orderService.GetOrderById(id);

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.order_generic);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetOrders()
        {
            var response = await this._orderService.GetOrders();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.orders);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
