using com.lib.qyhd.Scripts.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    /// <summary>
    /// 消息号
    /// </summary>
    public enum NetID
    {
        Login=1,
    }
    public class SendParams
    {
        public NetID id = NetID.Login;

        public object[] list = null;

        public SendParams(NetID _id, params object[] data)
        {
            id = _id;
            list = data;
        }
        public SendParams() { }
    }
    public class AppRespose
    {
        public Socket socket = null;

        public bool cache = false;

        public void AddRespose(SendParams s)
        {
            try
            {
                var dic = new Dictionary<string, object>();
                dic.Add("0", s.id.getInt());
                //Loggers.Log(typeof(AppRespose), s.id.ToString());
                int index = 1;
                foreach (var m in s.list)
                {
                    dic.Add(index.ToString(), m);
                    index += 1;
                }
                byte[] array = BufferManager.Instance.Encode(dic);
                Message message = new Message(1, 1, array.Length, array);
                if (socket != null)
                {
                    socket.Send(message.ToBytes());
                }
            }
            catch (Exception e)
            {
                Logger.Log(typeof(AppRespose), e.Message);
            }

        }
    }
}
