using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface IOrderHistoryStatusDAO
    {
        public List<OrderStatusHistory> getHistoryOrder(int idOrder);
        public void CreateStatusHistory(int idStatusOrder, int idOrder, int idUser);
    }
}
