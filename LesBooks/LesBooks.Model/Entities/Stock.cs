using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Stock
    {
        public int id { get; set; }

        public Double costValue { get; set; }

        public int quantity { get; set; }

        public List<StockEntryHistory> stockEntryHistory { get; set; }

    }
}
