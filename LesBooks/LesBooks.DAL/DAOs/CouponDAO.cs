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
        public List<Coupon> GetAllCouponsByClientId(int client_id)
        {
            List<Coupon> coupons = new List<Coupon>();

            try
            {
                string sql = "SELECT coupon.* FROM coupon INNER JOIN client_coupon ON coupon.id = client_coupon.coupon_id WHERE client_coupon.client_id = @client_id";

                OpenConnection();

                cmd.Parameters.AddWithValue("@client_id", client_id);
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
        public List<Coupon> GetCouponByDescription(string description,int client_id)
        {
            List<Coupon> coupons = new List<Coupon>();

            try
            {
                string sql = "SELECT coupon.* FROM coupon INNER JOIN client_coupon ON coupon.id = client_coupon.coupon_id WHERE client_coupon.client_id = @client_id and coupon.description = @description";

                OpenConnection();

                cmd.Parameters.AddWithValue("@client_id", client_id);
                cmd.Parameters.AddWithValue("@description", description);
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
        public List<Coupon> UpdateCoupon(int id_coupon ,int order_id)
        {
            List<Coupon> coupons = new List<Coupon>();

            try
            {
                string sql = "UPDATE coupon SET orders_id = @orders_id, active = 0 WHERE id_coupon = @id_coupon";

                OpenConnection();

                cmd.Parameters.AddWithValue("@id_coupon", id_coupon);
                cmd.Parameters.AddWithValue("@order_id", order_id);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


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
        public List<Coupon> CreateCoupon(Coupon coupon, int client_id)
        {
            List<Coupon> coupons = new List<Coupon>();

            try
            {
                string sql = "UPDATE coupon SET orders_id = @orders_id, active = 0 WHERE id_coupon = @id_coupon";

                OpenConnection();

                cmd.Parameters.AddWithValue("@id_coupon", coupon);
                cmd.Parameters.AddWithValue("@client_id", client_id);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


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
