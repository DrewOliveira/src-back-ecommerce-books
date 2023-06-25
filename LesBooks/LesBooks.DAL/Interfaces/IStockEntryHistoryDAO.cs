using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IStockEntryHistoryDAO
    {
        public List<StockEntryHistory> GetStockEntryHistoryByStoockId(int id);
        public Task<StockEntryHistory> CreateStockEntryHistory(StockEntryHistory stockEntryHistory);
    }
}
