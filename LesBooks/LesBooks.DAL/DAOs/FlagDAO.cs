using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class FlagDAO : Connection, IFlagDAO
    {
        public List<Flag> GetAllFlags()
        {
            List<Flag> list = new List<Flag>();
            try
            {
                OpenConnection();
                string sql = "SELECT id_method, description FROM methods";
                while (reader.Read())
                {
                    Flag flag = new Flag();
                    flag.Id = (int)reader["id_method"];
                    flag.description = (string)reader["description"];
                    list.Add(flag);
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
            return list;
        }

        public Flag GetFlagById(int id)
        {
            Flag flag = null;
            try
            {
                OpenConnection();
                string sql = "SELECT id_method, description FROM methods WHERE id_method = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    flag = new Flag();
                    flag.Id = (int)reader["id_method"];
                    flag.description = (string)reader["description"];
                    
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
            return flag;
        }
    }
}
