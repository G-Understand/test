using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    class MSMQ
    {
        /// <summary>
        /// 插入列队
        /// </summary>
        public static void AddMsg()
        {
            string queuePath = ".\\Private$\\mailqueue";//路径为自定义的

            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }

            MessageQueue myQueue = new MessageQueue(queuePath);
            DateTime begin = DateTime.Now;
            for (int i = 0; i < 800; i++)
            {
                Message myMessage = new Message();

                myMessage.Body = "将数据插入到消息队列";

                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                //发生消息到队列中

                myQueue.Send(myMessage);
            }


            Console.WriteLine("消息发送成功！用时毫秒=" + (DateTime.Now - begin).TotalMilliseconds);


            Console.ReadLine();
        }

        /// <summary>
        /// 读取
        /// </summary>
        public static void Handler()
        {
            string queuePath = ".\\Private$\\mailqueue";
            MessageQueue myQueue2 = new MessageQueue(queuePath);

            myQueue2.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            for (int i = 0; i < 700; i++)
            {
                Message myMessage2 = myQueue2.Receive();// myQueue.Peek();--接收后不消息从队列中移除

                string context = myMessage2.Body.ToString();

                Console.WriteLine("消息内容：" + context);
            }
            Console.WriteLine("获取完成");

            Console.ReadLine();
        }

        /// <summary>
        /// 测试
        /// </summary>
        public static void Test()
        {
            AddMsg();
            Handler();
        }
    }
}
