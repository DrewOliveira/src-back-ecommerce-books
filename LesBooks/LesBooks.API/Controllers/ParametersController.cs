using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/Parameters")]
    [ApiController]
    public class ParametersController : ControllerBase
    {

        [HttpGet("/Card/Flags")]
        public ActionResult<dynamic> Get()
        {
            List<string> flags = new List<string>();
            flags.Add("Visa");
            flags.Add("MasterCard");
            flags.Add("Elo");
            return StatusCode((int)HttpStatusCode.OK, flags);
        }
    }
}
