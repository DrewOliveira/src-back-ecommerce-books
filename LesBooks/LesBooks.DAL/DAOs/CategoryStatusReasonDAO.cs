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

        public async Task<CategoryStatusReason> GetCategoryStatusReasonByDescription(string description)
        {
            CategoryStatusReason categoryStatusReason = new CategoryStatusReason();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM category_status_reason WHERE description = @description";
                cmd.Parameters.AddWithValue("@description", description);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    categoryStatusReason.id = (int)reader["id"];
                    categoryStatusReason.description = (string)reader["description"];
                } else
                {
                    categoryStatusReason.id = 0;
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

        public async Task<CategoryStatusReason> CreateCategoryStatusReason(CategoryStatusReason categoryStatusReason)
        {
            try
            {
                string sql = "INSERT INTO category_status_reason (description) VALUES (@description); SELECT SCOPE_IDENTITY();";
                OpenConnection();
                cmd.Parameters.AddWithValue("@description", categoryStatusReason.description);

                cmd.CommandText = sql;
                categoryStatusReason.id = Convert.ToInt32(cmd.ExecuteScalar());
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
