using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class UpdateBookRequest
    {
        [Required] public String ISBN { get; set; }
        [Required] public String barcode { get; set; }
        [Required] public String title { get; set; }
        [Required] public String edition { get; set; }
        [Required] public int pageCount { get; set; }
        [Required] public Boolean active { get; set; }
        [Required] public String synopsis { get; set; }
        [Required] public Double value { get; set; }
        [Required] public Dimension dimension { get; set; }
        [Required] public DateTime publicationYear { get; set; }
    }
}
