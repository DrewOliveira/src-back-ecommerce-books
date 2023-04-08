using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class PricingDAO : Connection, IPricingDAO
    {
        public Pricing GetPricingById(int id)
        {
            Pricing pricing = new Pricing();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM pricing WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pricing.id = (int)reader["id"];
                    pricing.minProfitMargin = Convert.ToDouble(reader["minProfitMargin"]);
                    pricing.maxProfitMargin = Convert.ToDouble(reader["maxProfitMargin"]);
                    pricing.description = (string)reader["description"];
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
            return pricing;
        }
    }
}
