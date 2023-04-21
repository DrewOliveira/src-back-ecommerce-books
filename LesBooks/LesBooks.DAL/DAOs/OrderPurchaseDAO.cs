using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.DAOs
{
    public class OrderPurchaseDAO : Connection, IOrderPurchaseDAO
    {
        IItemDAO itemDAO;
        IPaymentDAO paymentDAO;
        IAdressDAO adressDAO;
        ICouponDAO couponDAO;

        public OrderPurchaseDAO(IItemDAO itemDAO, IPaymentDAO paymentDAO, IAdressDAO adressDAO, ICouponDAO couponDAO)
        {
            this.itemDAO = itemDAO;
            this.paymentDAO = paymentDAO;
            this.adressDAO = adressDAO;
            this.couponDAO = couponDAO;
        }

        public OrderPurchase CreatePurchase(OrderPurchase purchase)
        {
            try
            {
                string sql = "INSERT INTO orders(totalValue, client_id, adress_id, status_order_id, type_order_id) " +
                    "VALUES (@totalValue, @client_id, @adress_id, @status_order_id, @type_order_id); SELECT SCOPE_IDENTITY();";

                OpenConnection();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@totalValue", purchase.totalValue);
                cmd.Parameters.AddWithValue("@client_id", purchase.client.id);
                cmd.Parameters.AddWithValue("@adress_id", purchase.adress.id);
                cmd.Parameters.AddWithValue("@status_order_id", purchase.statusOrder);
                cmd.Parameters.AddWithValue("@type_order_id", purchase.type);

                purchase.id = Convert.ToInt32(cmd.ExecuteScalar());

                this.CreateItensOrderPurchase(purchase.items, purchase.id);
                this.CreatePaymentsOrderPurchase(purchase.payments, purchase.id);
                return purchase;
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

        private void CreateItensOrderPurchase(List<Item> itens, int order_id)
        {
            try
            {
                foreach (Item item in itens)
                {
                    itemDAO.CreateItem(item, order_id);
                }
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

        private void CreatePaymentsOrderPurchase(List<Payment> payments, int order_id)
        {
            try
            {
                foreach (Payment payment in payments)
                {
                    paymentDAO.CreatePayment(payment, order_id);
                }
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
