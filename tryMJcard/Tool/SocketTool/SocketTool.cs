using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tryMJcard
{
    struct tcp_keepalive
    {
        ulong onoff; //是否启用Keep-Alive
        ulong keepalivetime; //多长时间后开始第一次探测（单位：毫秒）
        ulong keepaliveinterval; //探测时间间隔（单位：毫秒）
    }; 
    class SocketTool:Singleton <SocketTool>
    {
        /// <summary>
        /// 地址
        /// </summary>
        private string ip = "";

        /// <summary>
        /// 端口
        /// </summary>
        private int port = 0;

        /// <summary>
        /// 连接
        /// </summary>
        private Socket socket;

        /// <summary>
        /// 设置IP，IPV6
        /// </summary>
        private static readonly IPAddress GroupAddress = IPAddress.Parse("IP地址");

        /// <summary>
        /// 设置端口
        /// </summary>
        private const int GroupPort = 11000;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_port"></param>
        public void Init(string _ip ,int _port)
        {
            ip = _ip;
            port = _port;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// 客户端心跳KeepaLive设置与开启
        /// </summary>
        /// <param name="socket"></param>
        public void KeepaLiveClient(Socket socket)
        {
            uint dummy = 0;
            byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)15000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint)15000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
            socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }

        /// <summary>
        /// 服务端心跳KeepaLive设置与开启
        /// </summary>
        /// <param name="clientep"></param>
        public void KeepaLiveServer(IPEndPoint clientep)
        {
            TcpListener myListener = new TcpListener(IPAddress.Any, clientep.Port);
            myListener.Start();
            TcpClient newClient = myListener.AcceptTcpClient();
            newClient.Client.IOControl(IOControlCode.KeepAliveValues, BitConverter.GetBytes(60), null);
        }

        public void SetScoket_TCP()
        {
            var endPort = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPort);
            socket.Listen(10);
            var count = 0;
            while (true)
            {
                try
                {//在套接字上接收接入的连接 
                    var clientSocket = socket.Accept();
                    count++;
//                     var cmd = new CommandHandle(); // 线程池 处理接收到的消息
//                     ThreadPool.QueueUserWorkItem(cmd.Action, clientSocket);
//                     var clientep = (IPEndPoint)clientSocket.RemoteEndPoint;
                    //Loggers.Log(this.GetType(), "新的客户端连接:" + clientep.Address.ToString());
                }
                catch (Exception ex)
                {
                    //Loggers.Log(this.GetType(), ex.Message);
                }
            }
        }

        public void SetScoket_UDP()
        {
            bool done = false;
            UdpClient listener = new UdpClient();
            IPEndPoint groupEP = new IPEndPoint(GroupAddress, GroupPort);
            try
            {//IPV6，组播
                listener.JoinMulticastGroup(GroupAddress);
                listener.Connect(groupEP);
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }
                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

}
