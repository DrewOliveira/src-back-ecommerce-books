using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ReferenceConsoleRedisApp
{
    class Program
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-15814.c240.us-east-1-3.ec2.cloud.redislabs.com:15814,password=HAoZJeJ3vBYklkt0opVqnNgRD8gkk3kU");
        static async Task Main(string[] args)
        {
            var db = redis.GetDatabase();
            var batch = db.CreateBatch();
            
            batch.Execute();
        }
    }
}