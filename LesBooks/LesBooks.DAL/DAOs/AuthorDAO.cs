using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class AuthorDAO : Connection, IAuthorDAO
    {
        public Author GetAuthorById(int id)
        {
            Author author = new Author();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM author WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {                    
                    author.id = (int)reader["id"];
                    author.description = (string)reader["description"];
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
            return author;
        }
    }
}
