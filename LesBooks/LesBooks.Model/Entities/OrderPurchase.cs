using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class OrderPurchase: Order
    {
        public List<Payment> payments { get; set; }
        public List<Coupon> coupons { get; set; }

        public Adress adress { get; set; }
    }
}
