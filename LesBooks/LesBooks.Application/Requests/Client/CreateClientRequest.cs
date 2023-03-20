using LesBooks.Model.Entities;
using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class CreateClientRequest
    {
        [Required] public string name { get; set; }
        [Required] public string gender { get; set; }
        [Required] public string cpf { get; set; }
        [Required] public User user { get; set; }
        [Required] public DateTime birth { get; set; }
        public Phone phone { get; set; }
 
        public List<CreateAdressRequest> adresses { get; set; }

        public List<CreateCardRequest> cards { get; set; }

    }
}
