using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class SendRespose
   {
        public Socket socket = null;

        public bool cache = false;

        public string ip = String.Empty;

       public void AddRespose(SendParams s)
       {
            try
            {
                var dic = new Dictionary<string, object>();
                dic.Add("0", s.id.getInt());
                LogManagerSimple .Instance .Log(typeof(AppRespose), s.id.ToString());
                int index = 1;
                foreach (var m in s.list)
                {
                    dic.Add(index.ToString(), m);
                    index += 1; 
                }
                byte[] array =BufferManager.Instance.Encode(dic);
                Message message = new Message(1, 1, array.Length, array);
                if (socket != null&&socket.Connected)
                {
                    socket.Send(message.ToBytes());
                }
            }
            catch (Exception e)
            {
                LogManagerSimple .Instance.Log(typeof(AppRespose), e.Message);
             
            }
            
        }
    }
}
