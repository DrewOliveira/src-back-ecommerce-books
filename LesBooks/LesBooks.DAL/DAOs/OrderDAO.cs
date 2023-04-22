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

        public OrderDAO(IItemDAO itemDAO, IPaymentDAO paymentDAO, IAdressDAO adressDAO, ICouponDAO couponDAO, IClientDAO clientDAO)
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
                    orderGeneric.adress = adressDAO.GetAdressById((int)reader["adress_id"]);
                    orderGeneric.client = clientDAO.GetClientById((int)reader["client_id"]);
                    orderGeneric.statusOrder = (Model.Enums.StatusOrder)Convert.ToInt32((int)reader["status_order_id"]);
                    orderGeneric.dateOrder = Convert.ToDateTime(reader["dateOrder"].ToString());
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
