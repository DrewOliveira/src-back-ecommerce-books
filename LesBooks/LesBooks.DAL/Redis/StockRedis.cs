using System;
using System.Collections.Generic;
using System.Linq;
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
            bookKey = $"estoque:{bookId}";
            blockKey = $"bloqueio:{clientId}:{bookId}";
            clientKey = $"bloqueio:{clientId}:*";
        }

        public void CreateTemporaryBlock(string clientId, string bookId, int quantity, int timeBlock)
        {
            intitKeys(clientId, bookId);
            TimeSpan timeBlockSpan = TimeSpan.FromMinutes(timeBlock);


            if (db.KeyExists(blockKey))
            {
                IncrementTemporaryBlock(clientId, bookId, quantity, timeBlock);
            }
            else
            {
                db.StringSet(blockKey, quantity);
            }
            setExpireClientBlocks(timeBlock);


        }
        public void IncrementTemporaryBlock(string clientId, string bookId, int quantity, int timeBlock)
        {
            intitKeys(clientId, bookId);
            TimeSpan timeBlockSpan = TimeSpan.FromMinutes(timeBlock);
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
            blockKey = $"bloqueio:*:{bookId}";
            var blockedBooks = db.Multiplexer.GetServer(server)
                                            .Keys(pattern: blockKey)
                                            .ToArray();
            int quantity = 0;
 
            foreach(var blockedBook in blockedBooks)
            {
                quantity += Convert.ToInt32(db.StringGet(blockedBook));
            }
            
            return quantity;
        }
    }
}
