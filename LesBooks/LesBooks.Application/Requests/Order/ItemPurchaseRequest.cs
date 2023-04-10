using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class ItemPurchaseRequest
    {
        public int book_id { get; set; }

        public int quantity { get; set; }
    }
}
