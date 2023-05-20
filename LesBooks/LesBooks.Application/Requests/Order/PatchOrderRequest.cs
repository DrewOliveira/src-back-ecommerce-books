using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class PatchOrderRequest
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int statusId { get; set; }
        public int admId { get; set; }
        public bool updateStock { get; set; }
    }
}
