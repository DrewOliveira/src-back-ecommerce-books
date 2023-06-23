using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.DAL.DAOs;
using LesBooks.Model.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace LesBook.Monitoring
{
    public class Monitoring : Connection  , IMonitoring
    {
        
        public void InitMonitoting()
        {
            
            Task.Run(async () =>
            {
                while (true)
                {
                    await VerifyPaymentPending();
                    await VerifyOrdersProcessing();

                     
                    await Task.Delay(TimeSpan.FromSeconds(120));
                }
            });
        }
        private void updatePaymentoApproval(List<int> orders)
        {
            foreach (var order in orders){
                try
                {
                    OpenConnection();
                    int approved = new Random().Next(2);
                    string sql = "UPDATE payment SET approved = @payment, dateApproval = GETDATE() where orders_id = @orders_id";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@payment", approved);
                    cmd.Parameters.AddWithValue("@orders_id", order);
                    cmd.ExecuteNonQuery();
                }
                catch
                {

                }finally
                {
                    CloseConnection();
                }
            }

        }
        public async Task VerifyPaymentPending()
        {
           List<int> orders = new List<int>();
            
            try
            {
                string sql = "SELECT * FROM payment where dateApproval is null";
                OpenConnection();


                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(Convert.ToInt32(reader["orders_id"]));
                }
                
            }
            catch
            {

            }
            finally
            {
                CloseConnection();
                if (orders.Count != 0)
                {
                    updatePaymentoApproval(orders);
                }
            }
        }
        public async void VerifyPaymentPending(List<int> orders)
        {
            foreach(int order in orders)
            {
                List<Payment> payments = new List<Payment>();

                try
                {
                    string sql = "SELECT * FROM payment where orders_id = @orders_id";

                    OpenConnection();

                    cmd.Parameters.AddWithValue("@orders_id", order);
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        this.patchOrder(order, true);
                    } else
                    {
                        while (reader.Read())
                        {
                            Payment payment = new Payment();


                            payment.id = (int)reader["id"];
                            payment.aprroved = Convert.ToBoolean(reader["approved"]);
                            payment.value = Convert.ToDouble(reader["value"]);
                            string data = reader["dateApproval"].ToString();
                            if (!String.IsNullOrEmpty(data))
                                payment.dateAproval = Convert.ToDateTime(data);


                            payments.Add(payment);
                        }
                        if (payments.All(payment => payment.dateAproval != DateTime.MinValue))
                            payments.ForEach(payment => patchOrder(order, payment.aprroved));
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
            }
        }
        public async Task<dynamic> patchOrder(int order, bool aprroved)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(new LesBooks.Application.Requests.PatchOrderRequest
                {
                    admId = 0,
                    OrderId = order,
                    statusId = aprroved ? 2 : 3
                });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PatchAsync("https://localhost:7260/api/order", content);
            }
            return null;
        }
        public async Task VerifyOrdersProcessing()
        {
            List<int> orders = new List<int>();
            try
            {
                string sql = "SELECT * FROM orders where status_order_id = 1";

                OpenConnection();
                
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(Convert.ToInt32(reader["id"]));
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
                if (orders.Count != 0)
                {
                    VerifyPaymentPending(orders);
                }
            }
        }


    }
}