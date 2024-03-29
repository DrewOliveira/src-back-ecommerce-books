﻿using AngleSharp.Io;
using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Order;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices;

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
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
        [HttpPost("replacement")]
        public async Task<ActionResult<dynamic>> PostReplacement([FromBody] CreateOrderReplacementRequest request)
        {
            var response = await this._orderService.CreateOrderReplacement(request);
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpPatch]
        public async Task<ActionResult<dynamic>> Patch([FromBody] PatchOrderRequest request)
        {
            
            var response = await _orderService.PatchOrder(request);
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet("client/{id}")]
        public async Task<ActionResult<dynamic>> GetAllPurchasesByClientId(int id)
        {
            var response = await this._orderService.GetOrderPurchaseByClientId(id);

            if (response.erros == null)
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
        [HttpPost("dashboard")]
        public async Task<ActionResult<dynamic>> GetDashboard([FromBody] GetDashboardRequest request)
        {
            var response = await this._orderService.GetDashboard(request);

            if (response.dashboard != null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.dashboard);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetOrderById(int id)
        {
            var response = await this._orderService.GetOrderById(id);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.order_generic);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetOrders()
        {
            var response = await this._orderService.GetOrders();

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.orders);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
