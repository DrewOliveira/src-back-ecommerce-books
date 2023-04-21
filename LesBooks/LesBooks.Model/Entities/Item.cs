using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Item
    {
        public int id { get; set; }
        public int quantity { get; set; }

        public Double totalValue { get; set; }
        public Book book { get; set; }

        public Order order { get; set; }
    }
}
