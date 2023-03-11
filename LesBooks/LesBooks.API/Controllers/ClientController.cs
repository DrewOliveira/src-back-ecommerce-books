﻿using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LesBooks.API.Controllers
{
    

    [Route("v1/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetAsync()
        {
            var response = await this._clientService.ListClientes();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.clients );
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get(int id)
        {
            var response = await this._clientService.ListClientes();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.clients.FirstOrDefault());
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

    
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] CreateClientRequest request)
        {
            var response = await this._clientService.CreateClient(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpPut]
        public async Task<ActionResult<dynamic>> Put([FromBody] UpdateClientRequest request)
        {
            var response = await this._clientService.UpdateClient(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            var response = await this._clientService.DeleteClient(id);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
