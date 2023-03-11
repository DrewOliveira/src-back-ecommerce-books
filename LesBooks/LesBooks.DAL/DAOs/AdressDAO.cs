using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class AdressDAO : Connection, IAdressDAO
    {
        public Adress CreateAdress(int id,Adress adress)
        {
            string sql = "INSERT (fk_id_client,street,number,zipcode,obs,identification,neighborhood,city,state,country,typeAdress,typeResidence,typeStreet)" +
                "VALUES (@fk_id_client,@street,@number,@zipcode,@obs,@identification,@neighborhood,@city,@state,@country,@typeAdress,@typeResidence,@typeStreet)";
            cmd.Parameters.AddWithValue("fk_id_client",id);
            cmd.Parameters.AddWithValue("", adress.street);
            cmd.Parameters.AddWithValue("", adress.identification);
            cmd.Parameters.AddWithValue("", adress.state);
            cmd.Parameters.AddWithValue("", adress.typeResidence);
            cmd.Parameters.AddWithValue("", adress.typeStreet);
            cmd.Parameters.AddWithValue("", adress.city);
            cmd.Parameters.AddWithValue("", adress.country);
            cmd.Parameters.AddWithValue("", adress.neighborhood);
            cmd.Parameters.AddWithValue("", adress.number);
            cmd.Parameters.AddWithValue("", adress.obs);
            // cmd.Parameters.AddWithValue("", adress.);
            return adress;
        }

        public void DeleteAdress(Adress adress)
        {
            throw new NotImplementedException();
        }

        public Adress GetAdressById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Adress> GetAllAdresss()
        {
            List<Adress> list = new List<Adress>();
            try
            {
                string sql = "SELECT id_adress,fk_id_client,street,number,zipcode,obs,identification,neighborhood,city,state,country,typeAdress,typeResidence,typeStreet FROM adresses";
                cmd.CommandText = sql;
                OpenConnection();
                reader = cmd.ExecuteReader();
                while (reader.Read()){
                    int id = Convert.ToInt32(reader[""]);
                    string street = reader["street"].ToString();
                    string number = reader["number"].ToString();
                    string zipCode = reader["zipCode"].ToString();
                    string obs = reader["obs"].ToString();
                    string identification = reader["identification"].ToString();
                    string neighborhood = reader["neighborhood"].ToString();
                    string city = reader["city"].ToString();
                    string state = reader["state"].ToString();
                    string country = reader["country"].ToString();
                    string typeAdress = reader["typeAdress"].ToString();
                    string typeResidence = reader["typeResidence"].ToString();
                    string typeStreet = reader["typeStreet"].ToString();
                    Adress adress = new Adress(id,street,number,zipCode,obs,identification,neighborhood,city,state,country,typeAdress,typeResidence,typeStreet);
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
            throw new NotImplementedException();
        }
    }
}
