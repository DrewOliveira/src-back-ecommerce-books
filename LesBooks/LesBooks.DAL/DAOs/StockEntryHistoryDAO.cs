using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class StockEntryHistoryDAO : Connection, IStockEntryHistoryDAO
    {
        public List<StockEntryHistory> GetStockEntryHistoryByStoockId(int id)
        {
            
            List<StockEntryHistory> stockEntryHistory = new List<StockEntryHistory>();

            try
            {
                OpenConnection();
                string sql = "SELECT * FROM stock_entry_history WHERE stock_id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                

                while (reader.Read())
                {
                    StockEntryHistory stockEntryHistoryItem = new StockEntryHistory
                    {
                        id = (int)reader["id"],
                        entryDate = Convert.ToDateTime(reader["entry_date"]),
                        costValue = Convert.ToDouble(reader["costValue"]),
                        quantity = (int)reader["quantity"],
                        stockId = (int)reader["stock_id"]
                    };

                    stockEntryHistory.Add(stockEntryHistoryItem);
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
            return stockEntryHistory;
        }

        public async Task<StockEntryHistory> CreateStockEntryHistory(StockEntryHistory stockEntryHistory)
        {
            try
            {
                string sql = "INSERT INTO stock_entry_history(entry_date, quantity, costValue, stock_id) VALUES(@entry_date, @quantity, @costValue, @stock_id); SELECT SCOPE_IDENTITY();";
                OpenConnection();
                cmd.Parameters.AddWithValue("@entry_date", stockEntryHistory.entryDate);
                cmd.Parameters.AddWithValue("@quantity", stockEntryHistory.quantity);
                cmd.Parameters.AddWithValue("@costValue", stockEntryHistory.costValue);
                cmd.Parameters.AddWithValue("@stock_id", stockEntryHistory.stockId);

                cmd.CommandText = sql;
                stockEntryHistory.id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return stockEntryHistory;
        }
    }
}
