using LesBooks.Application.Requests;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LesBooks.API.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetAsync(int id)
        {
            var response = await this._cardService.GetCarde(id);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.card);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }


        [HttpGet("all/{id}")]
        public async Task<ActionResult<dynamic>> Get(int id)
        {
            var response = await this._cardService.ListCardes(id);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.cards);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }


        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] CreateCardRequest request)
        {
            var response = await this._cardService.CreateCard(request);
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.id);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpPut]
        public async Task<ActionResult<dynamic>> Put([FromBody] UpdateCardRequest request)
        {
            var response = await this._cardService.UpdateCard(request);
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            var response = await this._cardService.DeleteCard(id);
            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
