using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Pricing
    {
        public Pricing()
        {
            
        }

        public Pricing(int id, Double minProfitMargin, Double maxProfitMargin, String description)
        {
            this.id = id;
            this.minProfitMargin = minProfitMargin;
            this.maxProfitMargin = maxProfitMargin;
            this.description = description;
        }

        public int id { get; set; }

        public Double minProfitMargin { get; set; }

        public Double maxProfitMargin { get; set; }

        public String description { get; set; }

    }
}
