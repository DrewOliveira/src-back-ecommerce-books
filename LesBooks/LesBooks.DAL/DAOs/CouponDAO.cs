using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class CouponDAO: Connection, ICouponDAO
    {
        public Coupon GetCouponById(int id)
        {
            Coupon coupon = new Coupon();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM coupon WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    coupon.id = (int)reader["id"];
                    coupon.description = (string)reader["description"];
                    coupon.value = Convert.ToDouble(reader["value"]);
                    coupon.active = Convert.ToBoolean(reader["active"]);
                    coupon.typeCoupon = (Model.Enums.TypeCoupon)Convert.ToInt32(reader["type_coupon_id"]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return coupon;
        }

        public List<Coupon> GetAllCouponsByOrderId(int orders_id)
        {
            List<Coupon> coupons = new List<Coupon>();

            try
            {
                string sql = "SELECT * FROM coupon where orders_id = @orders_id";

                OpenConnection();

                cmd.Parameters.AddWithValue("@orders_id", orders_id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coupon coupon = new Coupon();

                    coupon.id = (int)reader["id"];
                    coupon.description = (string)reader["description"];
                    coupon.value = Convert.ToDouble(reader["value"]);
                    coupon.active = Convert.ToBoolean(reader["active"]);
                    coupon.typeCoupon = (Model.Enums.TypeCoupon)Convert.ToInt32(reader["type_coupon_id"]);

                    coupons.Add(coupon);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return coupons;
        }
    }
}
