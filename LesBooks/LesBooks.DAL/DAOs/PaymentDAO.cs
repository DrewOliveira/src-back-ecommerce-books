using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class PaymentDAO: Connection, IPaymentDAO
    {
        public Payment CreatePayment(Payment payment, int order_id)
        {
            try
            {
                string sql = "insert into payment(approved, value, card_id, orders_id)" + "values(@approved, @value, @card_id, @orders_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@approved", payment.aprroved);
                cmd.Parameters.AddWithValue("@value", payment.value);
                cmd.Parameters.AddWithValue("@card_id", payment.card.Id);
                cmd.Parameters.AddWithValue("@orders_id", order_id);

                payment.id = Convert.ToInt32(cmd.ExecuteScalar());

                return payment;
            }
            catch (Exception ex)
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
