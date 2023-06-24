
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LesBooks.DAL
{
    public class ClientDAO : Connection, IClientDAO
    {
        IAdressDAO adressDAO;
        ICardDAO cardDAO;
        IUserDAO userDAO;
        public ClientDAO(IAdressDAO adressDAO, ICardDAO cardDAO, IUserDAO userDAO)
        {
            this.adressDAO = adressDAO;
            this.cardDAO = cardDAO;
            this.userDAO = userDAO;
        }

        public Client CreateClient(Client client)
        {
            try
            {
                string sql = "INSERT INTO clients (fk_id_user, name, gender, cpf, ddd, phone, birth, active) " +
                    "VALUES (@userId, @name, @gender, @cpf, @ddd, @phone, @birth, 1); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name", client.name);
                cmd.Parameters.AddWithValue("@gender", client.gender);
                cmd.Parameters.AddWithValue("@cpf", client.cpf);
                cmd.Parameters.AddWithValue("@ddd", client.phone.ddd);
                cmd.Parameters.AddWithValue("@phone", client.phone.phoneNumber);
                cmd.Parameters.AddWithValue("@birth", DateTime.Now);
//
                client.user = userDAO.CreateUser(client.user);

                cmd.Parameters.AddWithValue("@userId", client.user.Id);

                client.id = Convert.ToInt32(cmd.ExecuteScalar());
                
                foreach(Adress adress in client.adresses)
                {
                    adressDAO.CreateAdress(client.id, adress);
                }
                
                foreach(Card card in client.card)
                {
                    cardDAO.CreateCard(client.id, card);
                }

            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return client;
        }


        public void DeleteClient(Client client)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();

            string sql = "SELECT * FROM clients";
            cmd.CommandText = sql;
            OpenConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Client client = new Client();
                client.id = Convert.ToInt32(reader["id_client"]);
                client.name = reader["name"].ToString();
                client.gender = reader["gender"].ToString();
                client.cpf = reader["cpf"].ToString();
                client.birth = Convert.ToDateTime(reader["birth"].ToString());
                client.active = Convert.ToBoolean(reader["active"]);
                client.phone = new Phone()
                {
                    ddd = reader["ddd"].ToString(),
                    phoneNumber = reader["phone"].ToString(),
                    typePhone = Model.Enums.TypePhone.celular
                };
                client.user = userDAO.GetUserById(Convert.ToInt32(reader["fk_id_user"]));
                client.adresses = adressDAO.GetAllAdresss(client.id);
                client.card = cardDAO.GetAllCards(client.id);

                clients.Add(client);
            }
            reader.Close();

            return clients;
        }

        public Client GetClientById(int id)
        {
            Client client = new Client();

            string sql = "SELECT * FROM clients WHERE id_client = @id";
            cmd.CommandText = sql;
            OpenConnection();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                client = new Client();
                client.id = Convert.ToInt32(reader["id_client"]);
                client.name = reader["name"].ToString();
                client.gender = reader["gender"].ToString();
                client.cpf = reader["cpf"].ToString();
                client.birth = Convert.ToDateTime(reader["birth"].ToString());
                client.active = Convert.ToBoolean(reader["active"]);
                client.phone = new Phone()
                {
                    ddd = reader["ddd"].ToString(),
                    phoneNumber = reader["phone"].ToString(),
                    typePhone = Model.Enums.TypePhone.celular
                };
                client.user = userDAO.GetUserById(Convert.ToInt32(reader["fk_id_user"]));
                client.adresses = adressDAO.GetAllAdresss(client.id);
                client.card = cardDAO.GetAllCards(client.id);

            }
            reader.Close();

            return client;
        }

        public Client UpdateClient(Client client)
        {
            try
            {
               OpenConnection();
               string sql = "UPDATE clients SET name = @name, gender = @gender, cpf = @cpf, ddd = @ddd, phone = @phone, birth = @birth, active = @active WHERE id_client = @id";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", client.id);
                cmd.Parameters.AddWithValue("@name", client.name);
                cmd.Parameters.AddWithValue("@gender", client.gender);
                cmd.Parameters.AddWithValue("@cpf", client.cpf);
                cmd.Parameters.AddWithValue("@ddd", client.phone.ddd);
                cmd.Parameters.AddWithValue("@phone", client.phone.phoneNumber);
                cmd.Parameters.AddWithValue("@birth", DateTime.Now);
                cmd.Parameters.AddWithValue("@active", client.active);
                cmd.ExecuteNonQuery();
            }

            catch
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
