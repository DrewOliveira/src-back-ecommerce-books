using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class PublisherDAO : Connection, IPublisherDAO
    {
        public Publisher GetPublisherById(int id)
        {
            Publisher publisher = new Publisher();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM publisher WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    publisher.id = (int)reader["id"];
                    publisher.description = (string)reader["description"];
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
            return publisher;
        }

    }
}
