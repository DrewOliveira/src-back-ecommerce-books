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

                    if ((int)reader["type_coupon_id"] == 1)
                    {
                        coupon.typeCoupon = TypeCoupon.PROMOTIONAL;
                    } else
                    {
                        coupon.typeCoupon = TypeCoupon.REPLACEMENT;
                    }
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
    }
}
