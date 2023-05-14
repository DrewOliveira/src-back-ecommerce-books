using LesBooks.Application;
using LesBooks.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {

        IBookService _bookService;
        IStockService _stockService;
        public BookController(IBookService bookService, IStockService stockService)
        {
            _bookService = bookService;
            _stockService = stockService;
        }


        [HttpGet()]
        public async Task<ActionResult<dynamic>> GetAllBooks()
        {
            var response = await this._bookService.GetAllBooks();

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.books);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetBook(int id)
        {
            var response = await this._bookService.GetBook(id);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, response.book);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }

        [HttpGet("{id}/stock/validate/{quantity}")]
        public async Task<ActionResult<dynamic>> ValidateStockByBookId(int id, int quantity)
        {
            var response = await this._stockService.ValidateStockByBookId(id, quantity);

            if (response.erros == null)
            {
                return StatusCode((int)HttpStatusCode.OK, new { response.quantity, response.validate });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
