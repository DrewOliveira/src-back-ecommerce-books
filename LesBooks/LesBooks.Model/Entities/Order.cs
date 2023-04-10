using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Order
    {
        public int id { get; set; }

        public Double totalValue { get; set; }

        public DateTime dateOrder { get; set; }

        public List<Item> items { get; set; }

        public TypeOrder type { get; set; }

        public StatusOrder statusOrder { get; set; }
    }
}
