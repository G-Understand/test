using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    class RedisHandler
    {
        private static ConfigurationOptions connDCS = ConfigurationOptions.Parse("127.0.0.1:6379,connectTimeout=2000");
        private static ConnectionMultiplexer redisConn;
        private static readonly object Locker = new object();
        public static ConnectionMultiplexer getRedisConn()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(connDCS);
                    }
                }
            }
            return redisConn;
        }

        public static void Start()
        {
            redisConn = getRedisConn();
            var db = redisConn.GetDatabase(0);//获取第几个库信息
            //set get


            string strKey = "123123";
            string strValue = "123123123";
            if (!db.KeyExists(strKey))
            {
                db.StringSet(strKey, strValue);
            }
            else
            {
                //db.KeyDelete(strKey, CommandFlags.HighPriority);
                db.StringSet(strKey, strValue);
            }

            var GetValue = db.StringGet(strKey);
            Console.WriteLine(strKey + ", " + db.StringGet(strKey));

            Console.ReadLine();
        }
    }
}
