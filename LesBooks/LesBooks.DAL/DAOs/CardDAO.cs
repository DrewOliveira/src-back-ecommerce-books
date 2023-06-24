using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LesBooks.DAL
{
    public class CardDAO : Connection, ICardDAO
    {
        IFlagDAO _flagDAO;
        public CardDAO(IFlagDAO flagDAO)
        {
            _flagDAO = flagDAO;
        }
        public Card CreateCard(int id,Card card)
        {
            try
            {
                string sql = "INSERT INTO cards (fk_id_client, fk_id_method, number, name, securityCode, principal, expiration) VALUES (@fk_id_client, @fk_id_method, @number, @name, @securityCode, @principal, @expiration); SELECT SCOPE_IDENTITY();";
                OpenConnection();
                cmd.Parameters.AddWithValue("@fk_id_client", id == 0 ? (object)DBNull.Value : id);
                cmd.Parameters.AddWithValue("@fk_id_method", card.flag.Id);
                cmd.Parameters.AddWithValue("@number", card.number);
                cmd.Parameters.AddWithValue("@name", card.name);
                cmd.Parameters.AddWithValue("@securityCode", card.securityCode);
                cmd.Parameters.AddWithValue("@principal", card.principal);
                cmd.Parameters.AddWithValue("@expiration", card.expiration);
                cmd.CommandText = sql;
                card.Id = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return card;
        }

        public void DeleteCard(Card card)
        {
           
            try
            {
                string sql = "DELETE FROM cards WHERE id_card = @id_card;";
                OpenConnection();
                cmd.Parameters.AddWithValue("@id_card", card.Id);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Card> GetAllCards(int id)
        {
            List<Card> cards = new List<Card>();

            try
            {
                string sql = "SELECT * FROM cards WHERE fk_id_client = @fk_id_client ";

                OpenConnection();
                cmd.Parameters.AddWithValue("@fk_id_client",id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Card card = new Card
                    {
                        Id = Convert.ToInt32(reader["id_card"]),
                        flag = _flagDAO.GetFlagById(Convert.ToInt32(reader["fk_id_method"])),
                        number = reader["number"].ToString(),
                        name = reader["name"].ToString(),
                        securityCode = reader["securityCode"].ToString(),
                        principal = Convert.ToBoolean(reader["principal"]),
                        expiration = Convert.ToDateTime(reader["expiration"])
                    };

                    cards.Add(card);
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

            return cards;
        }

        public Card GetCardById(int id)
        {
            Card card = null;

            try
            {
               string sql = "SELECT * FROM cards WHERE id_card = @id_card";

                OpenConnection();

                cmd.Parameters.AddWithValue("@id_card", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    card = new Card
                    {
                        Id = Convert.ToInt32(reader["id_card"]),
                        flag = _flagDAO.GetFlagById(Convert.ToInt32(reader["fk_id_method"])),
                        number = reader["number"].ToString(),
                        name = reader["name"].ToString(),
                        securityCode = reader["securityCode"].ToString(),
                        principal = Convert.ToBoolean(reader["principal"]),
                        expiration = Convert.ToDateTime(reader["expiration"])
                    };

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
            return card;
        }

        public Card UpdateCard(Card card)
        {
            try
            {
                string sql = "UPDATE cards SET fk_id_method = @fk_id_method, number = @number, name = @name, securityCode = @securityCode, principal = @principal, expiration = @expiration WHERE id_card = @id_card;";
                OpenConnection();
                cmd.Parameters.AddWithValue("@id_card", card.Id);
                cmd.Parameters.AddWithValue("@fk_id_method", card.flag.Id);
                cmd.Parameters.AddWithValue("@number", card.number);
                cmd.Parameters.AddWithValue("@name", card.name);
                cmd.Parameters.AddWithValue("@securityCode", card.securityCode);
                cmd.Parameters.AddWithValue("@principal", card.principal);
                cmd.Parameters.AddWithValue("@expiration", card.expiration);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return card;
        }
    }
}
