using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class ActivationStatusReasonDAO: Connection, IActivationStatusReasonDAO
    {
        ICategoryStatusReasonDAO _activationStatusReasonDAO;

        public ActivationStatusReasonDAO(ICategoryStatusReasonDAO activationStatusReasonDAO)
        {
            _activationStatusReasonDAO = activationStatusReasonDAO;
        }

        public ActivationStatusReason GetActivationStatusReasonById(int id)
        {
            ActivationStatusReason activationStatusReason = new ActivationStatusReason();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM activation_status_reason WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    activationStatusReason.id = (int)reader["id"];
                    activationStatusReason.justification = (string)reader["justification"];
                    activationStatusReason.status = Convert.ToBoolean(reader["status"]);
                    activationStatusReason.categoryStatusReason = _activationStatusReasonDAO.GetCategoryStatusReasonById(Convert.ToInt32(reader["categoryStatusReason_id"]));
                }
                reader.Close();
            }
            catch
           {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return activationStatusReason;
        }
    }
}
