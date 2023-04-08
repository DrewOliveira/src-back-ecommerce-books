using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class CategoryStatusReasonDAO: Connection, ICategoryStatusReasonDAO
    {
        public CategoryStatusReason GetCategoryStatusReasonById(int id)
        {
            CategoryStatusReason categoryStatusReason = new CategoryStatusReason();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM category_status_reason WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    categoryStatusReason.id = (int)reader["id"];
                    categoryStatusReason.description = (string)reader["description"];
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
            return categoryStatusReason;
        }
    }
}
