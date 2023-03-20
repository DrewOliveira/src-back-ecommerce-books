using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class UpdateAdressRequest
    {
        [Required]
        public int id{ get; set; }
     
        public string identification{ get; set; }
        [Required]
        public string street{ get; set; }
        [Required] public string number{ get; set; }
         public string obs{ get; set; }
        [Required] public string zipCode{ get; set; }
        [Required] public string neighborhood{ get; set; }
        [Required] public string city{ get; set; }
        [Required] public string state{ get; set; }
        [Required] public string country{ get; set; }
        [Required] public TypeAdress typeAdress{ get; set; }
        [Required] public TypeResidence typeResidence{ get; set; }
        [Required] public TypeStreet typeStreet{ get; set; }
    }
}
