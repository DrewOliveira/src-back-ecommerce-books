using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IOrderDAO
    {
        public OrderPurchase GetOrderById(int order_id);
        public Order CreateReplacement(Order order);
        public Dashboard GetDashboard(Dashboard dashboard);
    }
}
