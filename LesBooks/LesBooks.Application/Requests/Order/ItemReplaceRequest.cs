using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class ItemReplaceRequest
    {
        public int id { get; set; }
        public int quantity { get; set; }
    }
}
