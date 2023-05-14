using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LesBooks.DAL.DAOs
{
    public class OrderPurchaseDAO : Connection, IOrderPurchaseDAO
    {
        IItemDAO itemDAO;
        IPaymentDAO paymentDAO;
        IAdressDAO adressDAO;
        ICouponDAO couponDAO;
        IClientDAO clientDAO;

        public OrderPurchaseDAO(IItemDAO itemDAO, IPaymentDAO paymentDAO, IAdressDAO adressDAO, ICouponDAO couponDAO, IClientDAO clientDAO)
        {
            this.itemDAO = itemDAO;
            this.paymentDAO = paymentDAO;
            this.adressDAO = adressDAO;
            this.couponDAO = couponDAO;
            this.clientDAO = clientDAO;
        }
        public void CreateStatusHistory(int idStatusOrder, int idOrder, int idUser)
        {

            try
            {
                string query = "INSERT INTO orderStatusHistory (id_status_order, id_order, id_user,UpdateDate) VALUES (@idStatusOrder, @idOrder, @idUser,@UpdateDate)";
                OpenConnection();

                cmd.Parameters.AddWithValue("@idStatusOrder", idStatusOrder);
                cmd.Parameters.AddWithValue("@idOrder", idOrder);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void UpdateStatusOrder(int idOrder,int statusOrder)
        {
            try
            {
                string query = "UPDATE orders SET  status_order_id = @status_order_id WHERE id = @order_id ";
                OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@status_order_id", statusOrder);
                cmd.Parameters.AddWithValue("@order_id", idOrder);


                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public OrderPurchase CreatePurchase(OrderPurchase purchase)
        {
            try
            {
                string sql = "INSERT INTO orders(totalValue,dateOrder, client_id, adress_id, status_order_id, type_order_id) " +
                    "VALUES (@totalValue,@dateOrder, @client_id, @adress_id, @status_order_id, @type_order_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@totalValue", purchase.totalValue);
                cmd.Parameters.AddWithValue("@dateOrder", purchase.dateOrder);
                cmd.Parameters.AddWithValue("@client_id", purchase.client.id);
                cmd.Parameters.AddWithValue("@adress_id", purchase.adress.id);
                cmd.Parameters.AddWithValue("@status_order_id", purchase.statusOrder);
                cmd.Parameters.AddWithValue("@type_order_id", purchase.type);

                purchase.id = Convert.ToInt32(cmd.ExecuteScalar());

                this.CreateItensOrderPurchase(purchase.items, purchase.id);
                this.CreatePaymentsOrderPurchase(purchase.payments, purchase.id);
                return purchase;
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

        private void CreateItensOrderPurchase(List<Item> itens, int order_id)
        {
            try
            {
                foreach (Item item in itens)
                {
                    itemDAO.CreateItem(item, order_id);
                }
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

        private void CreatePaymentsOrderPurchase(List<Payment> payments, int order_id)
        {
            try
            {
                foreach (Payment payment in payments)
                {
                    paymentDAO.CreatePayment(payment, order_id);
                }
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
       
        public List<OrderPurchase> GetOrderPurchases(int ?client_id)
        {
            List<OrderPurchase > purchases = new List<OrderPurchase>();

            try
            {
                string sql = "SELECT * FROM orders ";
                OpenConnection();

                if (client_id != null)
                {
                    sql += "where client_id = @client_id";
                    cmd.Parameters.AddWithValue("@client_id", client_id);
                }

                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderPurchase orderPurchase = new OrderPurchase();

                    //orderPurchase.dateOrder = Convert.ToDateTime(reader["publicationYear"]); NEED FIX

                    orderPurchase.id = (int)(reader["id"]);
                    orderPurchase.totalValue = Convert.ToDouble(reader["totalValue"]);
                    orderPurchase.items = itemDAO.GetAllItensByOrderId((int)reader["id"]);
                    orderPurchase.type = Model.Enums.TypeOrder.PURCHASE;
                    orderPurchase.payments = paymentDAO.GetAllPaymentsByOrderId((int)reader["id"]);
                    orderPurchase.coupons = couponDAO.GetAllCouponsByOrderId((int)reader["id"]);
                    orderPurchase.adress = adressDAO.GetAdressById((int)reader["adress_id"]);
                    orderPurchase.client = clientDAO.GetClientById((int)reader["client_id"]);
                    orderPurchase.statusOrder = (Model.Enums.StatusOrder)Convert.ToInt32((int)reader["status_order_id"]);
                    orderPurchase.dateOrder = Convert.ToDateTime(reader["dateOrder"].ToString());
                    purchases.Add(orderPurchase);
                }

                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return purchases;
        }
    }
}
