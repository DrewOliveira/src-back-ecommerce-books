using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IPaymentDAO
    {
        public Payment CreatePayment(Payment payment, int order_id);

        public List<Payment> GetAllPaymentsByOrderId(int orders_id);
    }
}
