﻿using LesBooks.Application.Requests;
using LesBooks.Application.Requests.Stock;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IStockService stockService;
        public StockController(IStockService stockService)
        {
            this.stockService = stockService;   
        }
        [HttpPost("lock/")]
        public async Task<ActionResult<dynamic>> lockStock([FromBody] CreateLockRequest request)
        {
            var response = await this.stockService.CreateTemporaryBlock(request);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK,response.expireTime);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
