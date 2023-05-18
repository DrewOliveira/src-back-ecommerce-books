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
                string sql = "SELECT coupon.* FROM coupon INNER JOIN client_coupon ON coupon.id = client_coupon.coupon_id WHERE client_coupon.client_id = @client_id and coupon.description = @description and orders_id is null";

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
        public void UpdateCoupon(int id_coupon ,int order_id)
        {
            
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

            
        }
        public void CreateCoupon(Coupon coupon, int client_id)
        {
           
            try
            {
                string sql = "INSERT INTO coupon (description,value,active,type_coupon_id) VALUES (@description,@value,1,@type_coupon_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();

                cmd.Parameters.AddWithValue("@description", coupon);
                cmd.Parameters.AddWithValue("@value", coupon.value);
                cmd.Parameters.AddWithValue("@type_coupon_id", (int)coupon.typeCoupon);
                cmd.CommandText = sql;
                coupon.id = Convert.ToInt32(cmd.ExecuteScalar());
                AddCouponToclient(coupon.id,client_id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

        }

        private void AddCouponToclient(int coupon_id, int client_id)
        {
            try
            {
                string sql = "INSERT INTO client_coupon (client_id,coupon_id) VALUES (@client_id,@coupon_id);";

                OpenConnection();

                cmd.Parameters.AddWithValue("@client_id", coupon_id);
                cmd.Parameters.AddWithValue("@coupon_id", client_id);
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
        }

    }
}
