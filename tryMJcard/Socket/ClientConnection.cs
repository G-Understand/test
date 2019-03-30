using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class ClientConnection
    {
        public void Action(object obj)
        {
            try
            {
                var clientSocket = obj as Socket;
                if (clientSocket == null)
                {
                    //("连接成功，但创建实例没成功，对象空指针！");
                    return;
                }
                var clientep = (IPEndPoint)clientSocket.RemoteEndPoint;
                LogManagerSimple.Instance.Log(this.GetType(), "客户端连接：" + clientep.Address + "(" + clientep.Port + ")");
                var emptyDataCount = 0;

                var messageStream = new MessageStream();

                while (true)
                {

                    try
                    {
                        const int bufLen = 1024;
                        var buffer = new byte[bufLen];

                        if (!clientSocket.Connected)
                        {
                            return;
                        }

                        var recvLen = clientSocket.Receive(buffer, 0, bufLen, SocketFlags.None);//接受到该绑定ip所发送过来的数据
                        if (recvLen <= 0)
                        {

                            emptyDataCount++;
                            // 接收 10 次空数据，默认 socket 已断开

                            if (emptyDataCount >= 10)
                            {
                                return;
                            }
                            continue;
                        }
                        //当收到数据时，空数据次数清零
                        emptyDataCount = 0;

                        messageStream.Write(buffer, 0, recvLen);
                        Message message;
                        while (messageStream.Read(out message))
                        {
                            byte[] array = new byte[message.CommandSize];
                            Buffer.BlockCopy(message.Content, 0, array, 0, message.CommandSize);
                            Dictionary<string, object> dictionary = BufferManager.Instance.DecodeArray(array);
                            LogManagerSimple.Instance.Log(this.GetType(), "接受到客户端字节:" + array.Length + "  json数据为:" + dictionary.ToJson());
                            NetID nd = (NetID)Enum.Parse(typeof(NetID), dictionary["0"].ToString());
                            string id = dictionary["1"].ToString();
                            if (string.IsNullOrEmpty(id))
                            {
                                return;
                            }
                            var appmodel = User.getAppModel(id, clientSocket);

                            if (appmodel.ip.Length < 2)
                            {
                                var clienteps = (IPEndPoint)clientSocket.RemoteEndPoint;
                                appmodel.ip = clienteps.Address.ToString();
                            }

//                             ReceiveParams r = new ReceiveParams(nd, dictionary, appmodel);
//                             EventLib.SendEvent(nd, r);
                        }
                    }
                    catch (Exception s)
                    {
                        //Loggers.Log(this.GetType(), s.StackTrace);
                    }
                }
            }
            catch (Exception e)
            {

                LogManagerSimple.Instance.Log(this.GetType(), e.StackTrace);
            }
        }

    }
}
