using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class ManageStatusBookIdRequest
    {
        [Required] public string categoryStatusReasonDescription { get; set; }
        [Required] public string manageStatusReasonDescription { get; set; }
    }
}
