using LesBooks.Application.Requests.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class CreateOrderPurchaseRequest
    {
        [Required] public int adress_delivery_id { get; set; }

        [Required] public int client_id { get; set; }

        [Required] public List<ItemPurchaseRequest> itens { get; set; }

        [Required] public List<CouponPurchaseRequest> coupons { get; set; }

        [Required] public List<PaymentPurchaseRequest> payments { get; set; } 
    }
}
