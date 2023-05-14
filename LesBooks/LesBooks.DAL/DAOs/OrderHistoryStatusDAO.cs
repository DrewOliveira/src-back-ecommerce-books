using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class OrderHistoryStatusDAO : Connection , IOrderHistoryStatusDAO
    {
        public void CreateStatusHistory(int idStatusOrder, int idOrder, int idUser)
        {

            try
            {
                string query = "INSERT INTO orderStatusHistory (id_status_order, id_order, id_user,UpdateDate) VALUES (@idStatusOrder, @idOrder, @idUser,@UpdateDate)";
                OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@idStatusOrder", idStatusOrder);
                cmd.Parameters.AddWithValue("@idOrder", idOrder);
                cmd.Parameters.AddWithValue("@idUser", idUser == 0 ? (object)DBNull.Value : idUser);
                cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<OrderStatusHistory> getHistoryOrder(int idOrder)
        {
            List<OrderStatusHistory> historyOrder = new List<OrderStatusHistory>();

            try
            {
                string sql = "SELECT * FROM OrderStatusHistory where id_order = @id ";
                OpenConnection();
                cmd.Parameters.AddWithValue("@id", idOrder);


                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderStatusHistory history = new OrderStatusHistory();

                    history.Id = Convert.ToInt32(reader["id"]);
                    history.idOrder = Convert.ToInt32(reader["id_order"]);
                    history.idStatus = Convert.ToInt32(reader["id_status_order"]);
                    history.dateUpdate = Convert.ToDateTime(reader["UpdateDate"].ToString());


                    historyOrder.Add(history);
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

            return historyOrder;
        }
    }
}
