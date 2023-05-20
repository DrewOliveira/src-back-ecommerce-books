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
        ICardDAO cardDAO;

        public PaymentDAO(ICardDAO cardDAO)
        {

            this.cardDAO = cardDAO;

        }
        public Payment CreatePayment(Payment payment, int order_id)
        {
            try
            {
                string sql = "insert into payment(approved, value, card_id, orders_id)" + "values(@approved, @value, @card_id, @orders_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@approved", DBNull.Value);
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

        public List<Payment> GetAllPaymentsByOrderId(int orders_id)
        {
            List<Payment> payments = new List<Payment>();

            try
            {
                string sql = "SELECT * FROM payment where orders_id = @orders_id";

                OpenConnection();

                cmd.Parameters.AddWithValue("@orders_id", orders_id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Payment payment = new Payment();
                    Order order = new Order();
                    order.id = orders_id;

                    payment.id = (int)reader["id"];
                    payment.aprroved = Convert.ToBoolean(reader["approved"] == DBNull.Value ? null : reader["approved"]);
                    payment.value = Convert.ToDouble(reader["value"]);
                    payment.card = cardDAO.GetCardById((int)reader["card_id"]);
                    //payment.order = order;

                    payments.Add(payment);
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

            return payments;
        }
    }
}
