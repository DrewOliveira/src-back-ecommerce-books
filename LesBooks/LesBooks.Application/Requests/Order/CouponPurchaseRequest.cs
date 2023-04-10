using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class CouponPurchaseRequest
    {
        public int id { get; set; }
        public int typeCoupon { get; set; }
    }
}
