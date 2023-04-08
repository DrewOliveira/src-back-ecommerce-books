using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class ActivationStatusReason
    {
        public int id { get; set; }

        public String justification { get; set; }

        public Boolean status { get; set; }

        public CategoryStatusReason categoryStatusReason { get; set; }
    }
}
