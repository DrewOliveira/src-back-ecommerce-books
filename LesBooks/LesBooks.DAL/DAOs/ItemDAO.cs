using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class ItemDAO: Connection, IItemDAO
    {
        public Item CreateItem(Item item, int order_id)
        {
            try
            {
                string sql = "insert into item(quantity, totalValue, book_id, orders_id)" + "values(@quantity, @totalValue ,@book_id, @orders_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@quantity", item.quantity);
                cmd.Parameters.AddWithValue("@totalValue", item.totalValue);
                cmd.Parameters.AddWithValue("@book_id", item.book.id);
                cmd.Parameters.AddWithValue("@orders_id", order_id);

                item.id = Convert.ToInt32(cmd.ExecuteScalar());
                
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

        }
    }
}
