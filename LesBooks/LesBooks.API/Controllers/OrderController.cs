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
    }
}
