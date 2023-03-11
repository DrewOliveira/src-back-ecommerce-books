using LesBooks.Application.Requests;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LesBooks.API.Controllers
{
    [Route("api/Adress")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        IAdressService _adressService;
        public AdressController(IAdressService adressService)
        {
            _adressService = adressService; 
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetAsync()
        {
            var response = await this._adressService.ListAdresses();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.adress);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> Get(int id)
        {
            var response = await this._adressService.ListAdresses();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.adress.FirstOrDefault());
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }


        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] CreateAdressRequest request)
        {
            var response = await this._adressService.CreateAdress(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpPut]
        public async Task<ActionResult<dynamic>> Put([FromBody] UpdateAdressRequest request)
        {
            var response = await this._adressService.UpdateAdress(request);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            var response = await this._adressService.DeleteAdress(id);
            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
