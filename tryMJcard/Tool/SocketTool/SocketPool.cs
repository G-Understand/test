using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tryMJcard
{
    class SocketPool:Singleton <SocketPool>
    {
        public Socket socket;
        /// <summary>
        /// 启动数据
        /// </summary>

        public void Start()
        {

            new Thread(Action).Start();
        }


        private void Action()
        {
            var ip = XMLManager.Instance.ip; //string.Format("172.19.8.243");
            var port = XMLManager.Instance.port;
            var endPort = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPort);
            socket.Listen(10);
            Console.Title = DateTime.Now.ToString("yyyy-MM-dd HH:mmLss") + "===>" + ip + " : " + port;
            LogManagerSimple.Instance.Log(typeof(SocketPool), "服务器启动成功:" + endPort.Address.ToString() + "==>" + port);
            var count = 0;
            while (true)
            {
                try
                {
                    //在套接字上接收接入的连接 
                    var clientSocket = socket.Accept();

                    count++;

                    var cmd = new ClientConnection(); // 线程池 处理接收到的消息
                    ThreadPool.QueueUserWorkItem(cmd.Action, clientSocket);
                    var clientep = (IPEndPoint)clientSocket.RemoteEndPoint;
                   
                    LogManagerSimple.Instance.Log(this.GetType(), "新连接：" + clientep.Address.ToString());
                }
                catch (Exception ex)
                {
                    LogManagerSimple.Instance.Log(this.GetType(), ex.Message);
                }
            }

        }

    }
}
