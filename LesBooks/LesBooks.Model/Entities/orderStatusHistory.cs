using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public int idOrder { get; set; }
        public int idStatus { get; set; }
        public int idAdm { get; set; }
        public DateTime dateUpdate { get; set; }
    }
}
