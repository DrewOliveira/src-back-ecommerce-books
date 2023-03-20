using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class UpdateClientRequest
    {
        [Required] public int id { get; set; }
        [Required] public string name { get; set; }
        [Required] public string gender { get; set; }
        [Required] public string cpf { get; set; }
        [Required] public User user { get; set; }
        public Phone phone { get; set; }
    }
}
