using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tryMJcard.SocketTcpTool;

namespace tryMJcard.Tool.MessageTool
{
    public abstract class MessageProcessorBase<T> where T : MessageProcessorBase<T>, new()
    {
        static private T m_Instance;
        static public T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new T();
                    m_Instance.Initialize();
                }
                return m_Instance;
            }

        }
        public MessageProcessorBase()
        { }
        private EventDispatcher m_Dispatcher;
        public EventDispatcher EventDispatcher
        {
            get
            {
                if (m_Dispatcher == null)
                {
                    m_Dispatcher = new EventDispatcher();
                }
                return m_Dispatcher;
            }
        }

        private AppModel m_Peer;
        public AppModel Peer
        {
            get
            {
                return m_Peer;
            }

            set
            {
                m_Peer = value;
            }
        }


        public virtual void Initialize()
        {

            m_Dispatcher = new EventDispatcher();
        }

        public void SetPeer(AppModel Peer)
        {
            m_Peer = Peer;
        }

        public virtual void DeInitialize()
        {
            m_Dispatcher.ClearEvents();
            m_Dispatcher = null;
        }

        public virtual void AddListener()
        {

        }

        public virtual void RemoveListener()
        {

        }

        protected void SendEvent(int msgID, System.IO.MemoryStream ms)
        {

            SendEvent(new MessageBase(msgID, ms));
        }

        protected void SendEvent(MessageBase msgBase)
        {
            if (m_Peer != null && m_Peer.socket.State == WebSocketSharp.WebSocketState.Connecting)
            {
                m_Peer.Send(msgBase);
            }
        }

        protected void SendMessage(int msgID, System.IO.MemoryStream ms)
        {

            SendMessage(new MessageBase(msgID, ms));
        }

        private void SendMessage(MessageBase msgBase)
        {

            if (m_Peer != null && m_Peer.socket.State == WebSocketSharp.WebSocketState.Open)
            {
                m_Peer.Send(msgBase);
            }
        }

        public void SendRoomMessage(int msgID, System.IO.MemoryStream ms)
        {
            if (m_Peer != null && m_Peer.socket.State == WebSocketSharp.WebSocketState.Open)
            {
//                 MessageBase msgBase = new MessageBase(msgID, ms);
//                 RoomActor roomReference = (RoomActor)RoomList.GetRoomData(m_Peer.player.roomid);
//                 roomReference.Push(msgBase);
            }
        }

    }
}
