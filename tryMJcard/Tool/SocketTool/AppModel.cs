using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tryMJcard.SocketToolHandler;
using tryMJcard.Tool.MessageTool;
using tryMJcard.Tool.SocketTool;

namespace tryMJcard.SocketTcpTool
{
    public class AppModel
    {
        public Echo socket;

        public MessageProcessor LoginProcessor;

        public void Send(SendParams s)
        {
            if (socket == null || socket.State == WebSocketSharp.WebSocketState.Closed || socket.State == WebSocketSharp.WebSocketState.Closing)
            {
                return;
            }
            socket.Send(s.SendToJson());
        }

        public void Send(MessageBase messageBase)
        {
            if (socket != null && socket.State == WebSocketSharp.WebSocketState.Open)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    MessageHandle.Serialize(ms, messageBase);
                    byte[] data = (object)messageBase as byte[];
                    socket.Send(data);
                }

            }
        }
    }
}
