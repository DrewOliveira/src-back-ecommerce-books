using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Neighborhood
    {
        public int Id { get; set; }
        public int description { get; set; }
        public City city { get; set; }
    }
}
