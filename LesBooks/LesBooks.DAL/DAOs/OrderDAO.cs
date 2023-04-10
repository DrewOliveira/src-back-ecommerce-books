using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class OrderDAO: Connection, IOrderDAO
    {
        IItemDAO itemDAO;
        IPaymentDAO paymentDAO;

        public OrderDAO(IItemDAO itemDAO, IPaymentDAO paymentDAO)
        {
            this.itemDAO = itemDAO;
            this.paymentDAO = paymentDAO;
        }

        public Client CreateClient(Client client)
        {
            try
            {
                string sql = "INSERT INTO clients (fk_id_user, name, gender, cpf, ddd, phone, birth) " +
                    "VALUES (@userId, @name, @gender, @cpf, @ddd, @phone, @birth); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name", client.name);
                cmd.Parameters.AddWithValue("@gender", client.gender);
                cmd.Parameters.AddWithValue("@cpf", client.cpf);
                cmd.Parameters.AddWithValue("@ddd", client.phone.ddd);
                cmd.Parameters.AddWithValue("@phone", client.phone.phoneNumber);
                cmd.Parameters.AddWithValue("@birth", DateTime.Now);
                //
                //client.user = userDAO.CreateUser(client.user);

                cmd.Parameters.AddWithValue("@userId", client.user.Id);

                client.id = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (Adress adress in client.adresses)
                {
                    adressDAO.CreateAdress(client.id, adress);
                }

                foreach (Card card in client.card)
                {
                    cardDAO.CreateCard(client.id, card);
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
            return client;
        }
    }

}
