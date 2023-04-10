using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests.Order
{
    public class PaymentPurchaseRequest
    {
        [Required] public int card_id { get; set; }

        [Required] public Double value { get; set; }
    }
}
