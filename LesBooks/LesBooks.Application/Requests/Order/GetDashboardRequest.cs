using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class GetDashboardRequest
    {
        public string init { get; set; }
        public string end { get; set; }
        public int type { get; set; }
    }
}
