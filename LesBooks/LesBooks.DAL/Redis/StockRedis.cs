using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class StockRedis : RedisConnection , IStockRedis
    {
        private string bookKey { get; set; }
        private string blockKey { get; set; }
        private string clientKey { get; set; }
        private TimeSpan timeBlockSpan { get; set; }
        private void intitKeys(string clientId, string bookId)
        {
            bookKey = $"bloqueio:*:{bookId}";
            blockKey = $"bloqueio:{clientId}:{bookId}";
            clientKey = $"bloqueio:{clientId}:*";
        }

        public DateTime CreateTemporaryBlock(string clientId, string bookId, int quantity, int timeBlock)
        {
            intitKeys(clientId, bookId);
            TimeSpan timeBlockSpan = TimeSpan.FromMinutes(timeBlock);


            if (db.KeyExists(blockKey))
            {
                IncrementTemporaryBlock(quantity);
            }
            else
            {
                db.StringSet(blockKey, quantity);
            }
            setExpireClientBlocks(timeBlock);
            return DateTime.Now.AddMinutes(timeBlock);

        }
        public void CreateBlock(string orderId, string bookId, int quantity)
        {
            blockKey = $"bloqueio:order:{orderId}:{bookId}";
            try
            {
                db.StringSet(blockKey, quantity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void freeBlock(string orderId)
        {
            this.blockKey = $"bloqueio:order:{orderId}:*";
            var endpoints = db.Multiplexer.GetEndPoints();


            foreach (var endpoint in endpoints)
            {
                var server = db.Multiplexer.GetServer(endpoint);
                var keys = server.Keys(pattern: blockKey);
                foreach (var chave in keys)
                {
                    foreach (var key in keys)
                    {
                        db.KeyDelete(key);
                    }
                }
            }
        }

        public void IncrementTemporaryBlock(int quantity)
        {
          
            db.StringIncrement(blockKey, quantity);
        }

        private void setExpireClientBlocks(int timeBlock)
        {
            var endpoints = db.Multiplexer.GetEndPoints();

            
            foreach (var endpoint in endpoints)
            {
                var server = db.Multiplexer.GetServer(endpoint);
                var keys = server.Keys(pattern: clientKey);
                foreach (var chave in keys)
                {
                    var transaction = db.CreateTransaction();
                    foreach (var key in keys)
                    {
                        transaction.KeyExpireAsync(key, TimeSpan.FromMinutes(timeBlock));
                    }
                    
                    transaction.Execute();
                }
            }
           

        }
        public int getTemporaryBlockbyBook(string bookId)
        {

            var endpoints = db.Multiplexer.GetEndPoints();
            int quantity = 0;

            foreach (var endpoint in endpoints)
            {
                var server = db.Multiplexer.GetServer(endpoint);
                var keys = server.Keys(pattern: bookKey);
                
                foreach (var chave in keys)
                {
                    var transaction = db.CreateTransaction();
                    foreach (var key in keys)
                    {
                        quantity += Convert.ToInt32(db.StringGet(key));
                    }

                    transaction.Execute();
                }
            }
            return quantity;
        }
        public void freeTemporaryBlock(string clientId)
        {
            this.clientKey = $"bloqueio:{clientId}:*";
            var endpoints = db.Multiplexer.GetEndPoints();


            foreach (var endpoint in endpoints)
            {
                var server = db.Multiplexer.GetServer(endpoint);
                var keys = server.Keys(pattern: clientKey);
                foreach (var chave in keys)
                {
                    foreach (var key in keys)
                    {
                        db.KeyDelete(key);
                    }
                }
            }
        }
    }
}
