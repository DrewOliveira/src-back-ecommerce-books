using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Adress
    {
        public int id { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string obs { get; set; }
        public string zipCode { get; set; }
        public Neighborhood neighborhood { get; set; }
        public TypeAdress typeAdress { get; set; }
        public TypeResidence typeResidence { get; set; }
        public TypeStreet typeStreet { get; set; }


    }
}
