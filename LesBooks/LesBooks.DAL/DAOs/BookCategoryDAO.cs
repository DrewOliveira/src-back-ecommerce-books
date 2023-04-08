using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LesBooks.DAL.DAOs
{
    public class BookCategoryDAO: Connection, IBookCategoryDAO
    {
        public List<Category> GetAllCategoryByBookId(int id)
        {
            List<Category> categories = new List<Category>();

            try
            {
                string sql = "select * from book_category as bc inner join category as c on c.id = bc.category_id where book_id = @book_id";
                
                OpenConnection();

                cmd.Parameters.AddWithValue("@book_id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category
                    {
                        id = Convert.ToInt32(reader["category_id"]),
                        description = reader["description"].ToString()
                    };

                    categories.Add(category);
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

            return categories;
        }
    }
}
