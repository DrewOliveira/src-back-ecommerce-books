using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class CreateOrderReplacementRequest
    {
        public int order_Id { get; set; }
        
        public List<Item> items { get; set; }
    }
}
