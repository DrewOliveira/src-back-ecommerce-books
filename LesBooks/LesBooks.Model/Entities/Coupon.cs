using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Coupon
    {
        public int id { get; set; }
        public string description { get; set; }
        public double value { get; set; }

        public double active { get; set; }
        public TypeCoupon typeCoupon { get; set; }
    }
}
