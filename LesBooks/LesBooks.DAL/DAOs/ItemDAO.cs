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
        IBookDAO bookDAO;
        public ItemDAO(IBookDAO bookDAO)
        {

            this.bookDAO = bookDAO;

        }

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

        public List<Item> GetAllItensByOrderId(int orders_id)
        {
            List<Item> itens = new List<Item>();

            try
            {
                string sql = "SELECT * FROM item where orders_id = @orders_id";

                OpenConnection();

                cmd.Parameters.AddWithValue("@orders_id", orders_id);
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Item item = new Item();
                    Order order = new Order();

                    order.id = orders_id;
                    item.id = (int)reader["id"];
                    item.quantity = (int)reader["quantity"];
                    item.book = bookDAO.GetBookById((int)reader["book_id"]);
                    item.totalValue = Convert.ToDouble(reader["totalValue"]);
                    //item.order = order;

                    itens.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return itens;
        }
    }
}
