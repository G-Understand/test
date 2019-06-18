using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace tryMJcard.Tool.SocketTool
{
    public class ClientSocket
    {
        private WebSocket ws = new WebSocket("ws://echo.websocket.org");
//         private Notifier nf;
//         private OnEntireData recv;

//         public ClientSocket(Dispatcher dis)
//         {
//             this.recv = dis.dispatchMsg;
//             dis.send = this.Send;
//             onEventInit();
//         }


        private void onEventInit()
        {

            // Set the WebSocket events.

//             ws.OnOpen += (sender, e) =>
//                 nf.Notify(
//                     new NotificationMessage
//                     {
//                         Header = "WebSocket Open",
//                         Body = "Connected"
//                     }
//                 );
// 
//             nf = new Notifier(recv);
//             ws.OnMessage += (sender, e) =>
//                 nf.Notify(
//                     new NotificationMessage
//                     {
//                         Header = "WebSocket Message",
//                         Body = !e.IsPing ? e.Data : "Received a ping."
//                     }
//                 );
// 
//             ws.OnError += (sender, e) =>
//                 nf.Notify(
//                     new NotificationMessage
//                     {
//                         Header = "WebSocket Error",
//                         Body = e.Message
//                     }
//                 );
// 
//             ws.OnClose += (sender, e) =>
//                 nf.Notify(
//                     new NotificationMessage
//                     {
//                         Header = String.Format("WebSocket Close ({0})", e.Code),
//                         Body = e.Reason
//                     }
//                 );

            ws.Connect();

        }

        public void Send(string msg)
        {
            if (!msg.IsNullOrEmpty())
            {
                ws.Send(msg);
            }
        }

    }

    public class NotificationMessage
    {
        public string Body
        {
            get;
            set;
        }

        public string Header
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("{0}", Body);
        }
    }
}
