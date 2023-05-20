﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Payment
    {
        public int id { get; set; }

        public Double value { get; set; }

        public bool aprroved { get; set; }

        public Card card { get; set; }

        public DateTime dateAproval { get; set; }
    }
}
