using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class StockDAO: Connection, IStockDAO
    {
        IStockEntryHistoryDAO _stockEntryHistoryDAO;

        public StockDAO(IStockEntryHistoryDAO  stockEntryHistoryDAO)
        {
            _stockEntryHistoryDAO = stockEntryHistoryDAO;
        }
        public Stock GetStockByBookId(int id)
        {
            Stock stock = new Stock();
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM stock WHERE book_id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    stock.id = (int)reader["id"];
                    stock.stockEntryHistory = _stockEntryHistoryDAO.GetStockEntryHistoryByStoockId(Convert.ToInt32(reader["id"]));
                    stock.costValue = Convert.ToDouble(reader["costValue"]);
                    stock.quantity = (int)reader["quantity"];
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
            return stock;
        }

        public void UpdateQuantityStockByBookId(int quantity, int book_id,bool remove)
        {
            try
            {
                OpenConnection();
                string sql = String.Format("UPDATE stock SET quantity = quantity {0} @quantity WHERE book_id = @book_id", remove ? "-" : "+");
                cmd.Parameters.AddWithValue("@book_id", book_id);
                cmd.Parameters.AddWithValue("@quantity", quantity);
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
        }
    }
}
