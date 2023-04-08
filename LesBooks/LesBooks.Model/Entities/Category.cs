using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Category
    {
        public Category()
        {
            
        }

        public Category(int id, String description)
        {
            this.id = id;
            this.description = description;
        }

        public int id { get; set; }
        public string description { get; set; }
    }
}
