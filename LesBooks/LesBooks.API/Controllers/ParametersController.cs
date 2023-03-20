using LesBooks.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/parameters")]
    [ApiController]
    public class ParametersController : ControllerBase
    {

        [HttpGet("/card/flags")]
        public ActionResult<dynamic> Get()
        {
            List<Flag> flags = new List<Flag>();
            Flag flag = new Flag()
            {
                Id = 1,
                description = "Mastercard"
            };
            Flag flag1 = new Flag()
            {
                Id = 2,
                description = "Visa"
            };
            Flag flag2 = new Flag()
            {
                Id = 3,
                description = "HiperCard"
            };
            Flag flag3 = new Flag()
            {
                Id = 4,
                description = "Maestro"
            };
            flags.Add(flag);
            flags.Add(flag1);
            flags.Add(flag2);
            flags.Add(flag3);

            return StatusCode((int)HttpStatusCode.OK, flags);
        }
    }
}
