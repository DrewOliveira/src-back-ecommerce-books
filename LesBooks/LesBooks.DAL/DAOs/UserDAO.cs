using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class UserDAO : Connection, IUserDAO
    {
        public User CreateUser(User user)
        {
            try
            {
                OpenConnection();
                string sql = "INSERT INTO users (email, password, typeUser) VALUES (@email, @password, @typeUser); SELECT SCOPE_IDENTITY();";

                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@typeUser", user.typeUser);

                user.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }
            return user;

        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            User user = null;
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM users WHERE id_user = @id_user";

                cmd.Parameters.AddWithValue("@id_user", user.Id);

                if (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader["id_user"],
                        email = (string)reader["email"],
                        password = (string)reader["password"],
                        typeUser = (Model.Enums.TypeUser)Convert.ToInt32(reader["typeUser"])
                    };
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }
            return user;
        }

        public User UpdateUser(User user)
        {
            try
            {
                OpenConnection();
                string sql = "UPDATE users SET email = @email, password = @password, typeUser = @typeUser WHERE id_user = @id_user";

                cmd.Parameters.AddWithValue("@id_user", user.Id);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@typeUser", user.typeUser);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }
            return user;
        }
    }
}
