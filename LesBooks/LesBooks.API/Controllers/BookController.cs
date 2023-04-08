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
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        

        [HttpGet()]
        public async Task<ActionResult<dynamic>> GetAllBooks()
        {
            var response = await this._bookService.GetAllBooks();

            if (response.erros.Count == 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.books);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, response.erros);
        }
    }
}
