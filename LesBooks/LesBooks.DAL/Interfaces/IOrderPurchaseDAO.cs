using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LesBooks.DAL.Interfaces.IOrderPurchaseDAO;

namespace LesBooks.DAL.Interfaces
{
    public interface IOrderPurchaseDAO
    {
        public OrderPurchase CreatePurchase(OrderPurchase purchase);

        public List<OrderPurchase> GetOrderPurchases(int ?client_id);
    }
}
