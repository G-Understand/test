using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class AsynTCP
    {

        public void Connt()
        {
            //定义IP地址
            IPAddress local = IPAddress.Parse("127.0,0,1");
            IPEndPoint iep = new IPEndPoint(local, 13000);
            //创建服务器的socket对象
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(20);
            server.BeginAccept(new AsyncCallback(Accept), server);
        }

        void Accept(IAsyncResult iar)
        {
            //还原传入的原始套接字
            Socket MyServer = (Socket)iar.AsyncState;
            //在原始套接字上调用EndAccept方法，返回新的套接字
            Socket service = MyServer.EndAccept(iar);
        }

        void Connect(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            try
            {
                client.EndConnect(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {

            }
        }

        public static void DoBeginAccept(TcpListener listner)
        {
            //开始从客户端监听连接
            Console.WriteLine("Waitting for a connection");
            //接收连接
            //开始准备接入新的连接，一旦有新连接尝试则调用回调函数DoAcceptTcpCliet
            listner.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpCliet), listner);
        }

        //处理客户端的连接
        public static void DoAcceptTcpCliet(IAsyncResult iar)
        {
            //还原原始的TcpListner对象
            TcpListener listener = (TcpListener)iar.AsyncState;

            //完成连接的动作，并返回新的TcpClient
            TcpClient client = listener.EndAcceptTcpClient(iar);
            Console.WriteLine("连接成功");
        }

        public void doBeginConnect(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            //开始与远程主机进行连接
            client.BeginConnect(serverIP[0], 13000, requestCallBack, client);
            Console.WriteLine("开始与服务器进行连接");
        }
        private void requestCallBack(IAsyncResult iar)
        {
            try
            {
                //还原原始的TcpClient对象
                TcpClient client = (TcpClient)iar.AsyncState;
                //
                client.EndConnect(iar);
                Console.WriteLine("与服务器{0}连接成功", client.Client.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {

            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.     
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            // Begin sending the data to the remote device.     
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.     
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.     
                StateObject state = new StateObject();
                state.workSocket = client;
                // Begin receiving the data from the remote device.     
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket     
                // from the asynchronous state object.     
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.     
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.     

                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    // Get the rest of the data.     
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.     
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.     
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.     
                StateObject state = new StateObject();
                state.workSocket = client;
                // Begin receiving the data from the remote device.     
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket     
                // from the asynchronous state object.     
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.     
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.     

                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    // Get the rest of the data.     
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.     
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.     
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void DataHandle(TcpClient client)
        {
            TcpClient tcpClient = client;
            //使用TcpClient的GetStream方法获取网络流
            NetworkStream ns = tcpClient.GetStream();
            //检查网络流是否可读
            if (ns.CanRead)
            {
                //定义缓冲区
                byte[] read = new byte[1024];
                ns.BeginRead(read, 0, read.Length, new AsyncCallback(myReadCallBack), ns);
            }
            else
            {
                Console.WriteLine("无法从网络中读取流数据");
            }
        }

        public static void myReadCallBack(IAsyncResult iar)
        {
            NetworkStream ns = (NetworkStream)iar.AsyncState;
            byte[] read = new byte[1024];
            String data = "";
            int recv;

            recv = ns.EndRead(iar);
            data = String.Concat(data, Encoding.ASCII.GetString(read, 0, recv));

            //接收到的消息长度可能大于缓冲区总大小，反复循环直到读完为止
            while (ns.DataAvailable)
            {
                ns.BeginRead(read, 0, read.Length, new AsyncCallback(myReadCallBack), ns);
            }
            //打印
            Console.WriteLine("您收到的信息是" + data);
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.     
            byte[] bytes = new Byte[1024];
            // Establish the local endpoint for the socket.     
            // The DNS name of the computer     
            // running the listener is "host.contoso.com".     
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            // Create a TCP/IP socket.     
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Bind the socket to the local     
            //endpoint and listen for incoming connections.     
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                while (true)
                {
                    // Set the event to nonsignaled state.     
                    allDone.Reset();
                    // Start an asynchronous socket to listen for connections.     
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    // Wait until a connection is made before continuing.     
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }


}
