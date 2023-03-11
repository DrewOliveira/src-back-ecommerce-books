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

        public Adress(int id, string street, string number, string zipCode, string obs, string identification, string neighborhood, string city, string state, string country, string typeAdress, string typeResidence, string typeStreet)
        {
            this.id = id;
            this.street = street;
            this.number = number;
            this.zipCode = zipCode;
            this.obs = obs;
            this.identification = identification;
            this.neighborhood = neighborhood;
            this.city = city;
            this.state = state;
            this.country = country;
            //typeAdress =  typeAdress;
            //typeResidence = typeResidence;
            //typeStreet = typeStreet;
        }

        public int id { get; set; }
        public string identification{ get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string obs { get; set; }
        public string zipCode { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public TypeAdress typeAdress { get; set; }
        public TypeResidence typeResidence { get; set; }
        public TypeStreet typeStreet { get; set; }


    }
}
