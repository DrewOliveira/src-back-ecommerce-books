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
            server = "redis-15632.c11.us-east-1-3.ec2.cloud.redislabs.com:15632,password=123";
            redis = ConnectionMultiplexer.Connect(server);
            db = redis.GetDatabase();
            
        }
        


    }
}