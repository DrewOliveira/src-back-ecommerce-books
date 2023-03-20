using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class CreateCardRequest
    {
        [Required] public int id_client { get; set; }
        [Required] public string number { get; set; }
        [Required] public string name { get; set; }
        [Required] public string securityCode { get; set; }
        [Required] public bool pricipal { get; set; }
        [Required] public DateTime expiration { get; set; }
        [Required] public Flag flag { get; set; }
    }
}
