using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public abstract class RedisConnection
    {
        protected static ConnectionMultiplexer redis;
        protected IDatabase db;
        protected string server;
        public RedisConnection()
        {
            server = "localhost:6379";
            redis = ConnectionMultiplexer.Connect(server);
            db = redis.GetDatabase();
            
        }
        


    }
}