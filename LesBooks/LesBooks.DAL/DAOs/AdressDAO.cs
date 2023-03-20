using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class AdressDAO : Connection, IAdressDAO
    {
        public Adress CreateAdress(int id,Adress adress)
        {
            try
            {
                OpenConnection();
                string sql = "INSERT INTO adresses (fk_id_client,street,number,zipcode,obs,identification,neighborhood,city,state,country,typeAdress,typeResidence,typeStreet)" +
                        "VALUES (@fk_id_client,@street,@number,@zipcode,@obs,@identification,@neighborhood,@city,@state,@country,@typeAdress,@typeResidence,@typeStreet)" +
                        "SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@fk_id_client", id);
                cmd.Parameters.AddWithValue("@street", adress.street);
                cmd.Parameters.AddWithValue("@identification", adress.identification);
                cmd.Parameters.AddWithValue("@state", adress.state);
                cmd.Parameters.AddWithValue("@typeResidence", ((int)adress.typeResidence));
                cmd.Parameters.AddWithValue("@typeStreet", ((int)adress.typeStreet));
                cmd.Parameters.AddWithValue("@typeAdress", ((int)adress.typeAdress));
                cmd.Parameters.AddWithValue("@city", adress.city);
                cmd.Parameters.AddWithValue("@country", adress.country);
                cmd.Parameters.AddWithValue("@neighborhood", adress.neighborhood);
                cmd.Parameters.AddWithValue("@number", adress.number);
                cmd.Parameters.AddWithValue("@obs", adress.obs);
                cmd.Parameters.AddWithValue("@zipcode", adress.zipCode);


                cmd.CommandText = sql;
                
                //cmd.ExecuteNonQuery();
                adress.id = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw;
            }
            return adress;
        }

        public void DeleteAdress(Adress adress)
        {
            throw new NotImplementedException();
        }

        public Adress GetAdressById(int id)
        {
            Adress adress = null;
            try
            {
                string sql = "SELECT id_adress, fk_id_client, street, number, zipCode, obs, identification, neighborhood, city, country, state,typeAdress,typeResidence,typeStreet FROM adresses WHERE id_adress = @Id";
                cmd.CommandText = sql;
                

                OpenConnection();
                cmd.Parameters.AddWithValue("@Id", id);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    adress = new Adress
                    {
                        id = (int)reader["id_adress"],
                        //clientId = (int)reader["fk_id_client"],
                        street = (string)reader["street"],
                        number = (string)reader["number"],
                        zipCode = (string)reader["zipCode"],
                        obs = (string)reader["obs"],
                        identification = (string)reader["identification"],
                        neighborhood = (string)reader["neighborhood"],
                        city = (string)reader["city"],
                        country = (string)reader["country"],
                        state = (string)reader["state"],
                        typeAdress = (Model.Enums.TypeAdress)Convert.ToInt32(reader["typeAdress"]),
                        typeResidence = (Model.Enums.TypeResidence)Convert.ToInt32(reader["typeResidence"]),
                        typeStreet = (Model.Enums.TypeStreet)Convert.ToInt32(reader["typeStreet"])
                    };
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
            return adress;
        }

        public List<Adress> GetAllAdresss(int id)
        {
            List<Adress> list = new List<Adress>();
            try
            {
                string sql = "SELECT id_adress,fk_id_client,street,number,zipcode,obs,identification,neighborhood,city,state,country,typeAdress,typeResidence,typeStreet  FROM adresses WHERE fk_id_client = @fk_id_client";
                cmd.CommandText = sql;
                OpenConnection();
                cmd.Parameters.AddWithValue("@fk_id_client", id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Adress adress = new Adress
                    {
                        id = (int)reader["id_adress"],
                        //clientId = (int)reader["fk_id_client"],
                        street = (string)reader["street"],
                        number = (string)reader["number"],
                        zipCode = (string)reader["zipCode"],
                        obs = (string)reader["obs"],
                        identification = (string)reader["identification"],
                        neighborhood = (string)reader["neighborhood"],
                        city = (string)reader["city"],
                        country = (string)reader["country"],
                        state = (string)reader["state"],
                        typeAdress = (Model.Enums.TypeAdress)Convert.ToInt32(reader["typeAdress"]),
                        typeResidence = (Model.Enums.TypeResidence)Convert.ToInt32(reader["typeResidence"]),
                        typeStreet = (Model.Enums.TypeStreet)Convert.ToInt32(reader["typeStreet"])
                    };
                    list.Add(adress);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection();
            }
            return list.Count == 0 ? null : list;
        }

        public Adress UpdateAdress(Adress adress)
        {
            try
            {
                OpenConnection();
                string sql = "UPDATE adresses SET street = @street, number = @number, zipCode = @zipCode, obs = @obs, identification = @identification, neighborhood = @neighborhood,state = @state, city = @city, country = @country,typeAdress = @typeAdress,typeResidence = @typeResidence,typeStreet = @typeStreet  WHERE id_adress = @id_adress";
                cmd.Parameters.AddWithValue("@id_adress", adress.id);
                cmd.Parameters.AddWithValue("@street", adress.street);
                cmd.Parameters.AddWithValue("@identification", adress.identification);
                cmd.Parameters.AddWithValue("@state", adress.state);
                cmd.Parameters.AddWithValue("@city", adress.city);
                cmd.Parameters.AddWithValue("@country", adress.country);
                cmd.Parameters.AddWithValue("@neighborhood", adress.neighborhood);
                cmd.Parameters.AddWithValue("@number", adress.number);
                cmd.Parameters.AddWithValue("@obs", adress.obs);
                cmd.Parameters.AddWithValue("@zipcode", adress.zipCode);
                cmd.Parameters.AddWithValue("@typeStreet", ((int)adress.typeStreet));
                cmd.Parameters.AddWithValue("@typeResidence", ((int)adress.typeResidence));
                cmd.Parameters.AddWithValue("@typeAdress", ((int)adress.typeAdress));

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
               

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return adress;
        }
    }
}
