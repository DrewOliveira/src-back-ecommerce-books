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
    }
}
