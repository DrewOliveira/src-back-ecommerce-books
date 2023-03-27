using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LesBooks.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        public CouponController()
        {

        }
        [HttpGet("{id}")]
        public void GetAll (int id)
        {

        }
    }
}
