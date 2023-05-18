using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Stock
{
    public class CreateLockRequest
    {
        public int idBooks { get; set; }
        public int quantity { get; set; }
        public int idClient { get; set; }
    }
}
