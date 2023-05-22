using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class OrderDAO : Connection, IOrderDAO
    {
        IItemDAO itemDAO;
        IPaymentDAO paymentDAO;
        IAdressDAO adressDAO;
        ICouponDAO couponDAO;
        IClientDAO clientDAO;
        IOrderHistoryStatusDAO orderHistoryStatusDAO;

        public OrderDAO(IItemDAO itemDAO, IPaymentDAO paymentDAO, IAdressDAO adressDAO, ICouponDAO couponDAO, IClientDAO clientDAO, IOrderHistoryStatusDAO orderHistoryStatusDAO)
        {
            this.itemDAO = itemDAO;
            this.paymentDAO = paymentDAO;
            this.adressDAO = adressDAO;
            this.couponDAO = couponDAO;
            this.clientDAO = clientDAO;
            this.orderHistoryStatusDAO = orderHistoryStatusDAO;
        }
        public void UpdatestatusOrder(int idOrder,int idStatusOrder)
        {
            try
            {
                string query = "UPDATE order set id_status_order = @UpdateDate where id = @order_id ";
                OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@idStatusOrder", idStatusOrder);
                cmd.Parameters.AddWithValue("@id", idOrder);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Order CreateReplacement(Order order)
        {
            try
            {
                string sql = "INSERT INTO orders(totalValue,dateOrder, client_id, status_order_id, type_order_id) " +
                    "VALUES (@totalValue,@dateOrder, @client_id, @status_order_id, @type_order_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@totalValue", order.totalValue);
                cmd.Parameters.AddWithValue("@dateOrder", order.dateOrder);
                cmd.Parameters.AddWithValue("@client_id", order.client.id);
                cmd.Parameters.AddWithValue("@status_order_id", order.statusOrder);
                cmd.Parameters.AddWithValue("@type_order_id", order.type);

                order.id = Convert.ToInt32(cmd.ExecuteScalar());

                this.CreateItensOrder(order.items, order.id);

                
                return order;
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

        private void CreateItensOrder(List<Item> itens, int order_id)
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

        public OrderPurchase GetOrderById(int order_id)
        {
            OrderPurchase orderGeneric = null;

            try
            {
                string sql = "SELECT * FROM orders where id = @order_id";

                OpenConnection();
                cmd.Parameters.AddWithValue("@order_id", order_id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    orderGeneric= new OrderPurchase();

                    //orderPurchase.dateOrder = Convert.ToDateTime(reader["publicationYear"]); NEED FIX

                    orderGeneric.id = (int)(reader["id"]);
                    orderGeneric.totalValue = Convert.ToDouble(reader["totalValue"]);
                    orderGeneric.items = itemDAO.GetAllItensByOrderId((int)reader["id"]);
                    orderGeneric.type = (Model.Enums.TypeOrder)Convert.ToInt32(reader["type_order_id"]);
                    orderGeneric.payments = paymentDAO.GetAllPaymentsByOrderId((int)reader["id"]);
                    orderGeneric.coupons = couponDAO.GetAllCouponsByOrderId((int)reader["id"]);
                    if (!String.IsNullOrEmpty(reader["adress_id"].ToString()))
                        orderGeneric.adress = adressDAO.GetAdressById((int)reader["adress_id"]);
                    else
                        orderGeneric.adress = new Adress();
                    orderGeneric.client = clientDAO.GetClientById((int)reader["client_id"]);
                    orderGeneric.statusOrder = (Model.Enums.StatusOrder)Convert.ToInt32((int)reader["status_order_id"]);
                    orderGeneric.history = orderHistoryStatusDAO.getHistoryOrder(orderGeneric.id);
                    string data = reader["dateOrder"].ToString();
                    if (!String.IsNullOrEmpty(data))
                    orderGeneric.dateOrder = Convert.ToDateTime(data);
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

            return orderGeneric;
        }
    }
}
