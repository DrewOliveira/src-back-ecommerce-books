using LesBooks.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LesBooks.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        ICouponDAO _couponDao;
        public CouponController(ICouponDAO couponDAO)
        {
            _couponDao = couponDAO;

        }
        [HttpGet("client/{id}")]
        public async Task<ActionResult<dynamic>> GetAll (int id)
        {
            var response = this._couponDao.GetAllCouponsByClientId(id);

            if (response.Count != 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);

        }
        [HttpGet("{id}/{description}")]
        public async Task<ActionResult<dynamic>> GetAll(int id,string description)
        {
            var response = this._couponDao.GetCouponByDescription(description,id);

            if (response.Count != 0)
            {
                return StatusCode((int)HttpStatusCode.OK, response.FirstOrDefault());
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);

        }
    }
}
