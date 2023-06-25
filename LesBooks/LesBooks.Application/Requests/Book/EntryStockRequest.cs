using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class EntryStockRequest
    {
        [Required] public int quantity { get; set; }
        [Required] public Double costValue { get; set; }
    }
}
