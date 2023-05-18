using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface ICouponDAO
    {
        public Coupon GetCouponById(int id);

        public List<Coupon> GetAllCouponsByOrderId(int orders_id);
        public List<Coupon> GetAllCouponsByClientId(int client_id);
        public List<Coupon> GetCouponByDescription(string description, int client_id);
        public void UpdateCoupon(int id_coupon, int order_id);
        public void CreateCoupon(Coupon coupon, int client_id);
    }
}
