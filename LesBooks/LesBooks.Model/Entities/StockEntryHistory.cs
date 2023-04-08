using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class StockEntryHistory
    {
        public StockEntryHistory()
        {

        }

        public StockEntryHistory(int id, DateTime entryDate, int quantity, Double costValue, int stockId)
        {
            this.id = id;
            this.entryDate = entryDate;
            this.quantity = quantity;
            this.costValue = costValue;
            this.stockId = stockId;
        }

        public int id { get; set; }

        public DateTime entryDate { get; set; }

        public int quantity { get; set; }
        public Double costValue { get; set; }

        public int stockId { get; set; }
    }
}
