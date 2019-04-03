using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    public class EventDispatcher : IDispatcher
    {

        private Dictionary<int, List<EventHandlerFunction>> listener = new Dictionary<int, List<EventHandlerFunction>>();
        private Dictionary<int, List<DataEventHandlerFunction>> dataListners = new Dictionary<int, List<DataEventHandlerFunction>>();

        #region 添加事件监听
        public void AddEventListener(string eventName, EventHandlerFunction handler)
        {
            AddEventListener(eventName.GetHashCode(), handler, eventName);
        }

        public void AddEventListener(int eventID, EventHandlerFunction handler)
        {
            AddEventListener(eventID, handler, eventID.ToString());
        }

        private void AddEventListener(int eventID, EventHandlerFunction handler, string eventGraphName)
        {
            if (listener.ContainsKey(eventID))
            {
                listener[eventID].Add(handler);
            }
            else
            {
                List<EventHandlerFunction> handlers = new List<EventHandlerFunction>();
                handlers.Add(handler);
                listener.Add(eventID, handlers);
            }
        }

        public void AddEventListener(string eventName, DataEventHandlerFunction handler)
        {
            AddEventListener(eventName.GetHashCode(), handler, eventName);
        }

        public void AddEventListener(NetID eventID, DataEventHandlerFunction handler)
        {
            AddEventListener(eventID.getInt(), handler, eventID.ToString());
        }
        public void AddEventListener(int eventID, DataEventHandlerFunction handler)
        {

        }
        private void AddEventListener(int eventID, DataEventHandlerFunction handler, string eventGraphName)
        {
            if (dataListners.ContainsKey(eventID))
            {
                dataListners[eventID].Add(handler);
            }
            else
            {
                List<DataEventHandlerFunction> handlers = new List<DataEventHandlerFunction>();
                handlers.Add(handler);
                dataListners.Add(eventID, handlers);
            }
        }
        #endregion

        #region 删除事件监听
        public void RemoveEventListener(string eventName, EventHandlerFunction handler)
        {
            RemoveEventListener(eventName.GetHashCode(), handler, eventName);
        }

        public void RemoveEventListener(int eventID, EventHandlerFunction handler)
        {
            RemoveEventListener(eventID, handler, eventID.ToString());
        }

        public void RemoveEventListener(int eventID, EventHandlerFunction handler, string eventGraphName)
        {
            if (listener.ContainsKey(eventID))
            {
                List<EventHandlerFunction> handlers = listener[eventID];
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    listener.Remove(eventID);
                }
            }
        }

        public void RemoveEventListener(string eventName, DataEventHandlerFunction handler)
        {
            RemoveEventListener(eventName.GetHashCode(), handler, eventName);
        }

        public void RemoveEventListener(NetID eventID, DataEventHandlerFunction handler)
        {
            RemoveEventListener((int)eventID, handler, eventID.ToString());
        }
        public void RemoveEventListener(int eventID, DataEventHandlerFunction handler)
        {

        }
        public void RemoveEventListener(int eventID, DataEventHandlerFunction handler, string eventGraphName)
        {


            if (dataListners.ContainsKey(eventID))
            {
                List<DataEventHandlerFunction> handlers = dataListners[eventID];
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    dataListners.Remove(eventID);
                }
            }
        }
        #endregion

        #region 事件分发
        public void Dispatch(string eventName)
        {
            Dispatch(eventName.GetHashCode(), null, eventName);
        }

        public void Dispatch(string eventName, object data)
        {
            Dispatch(eventName.GetHashCode(), data, eventName);
        }

        public void Dispatch(int eventID)
        {
            Dispatch(eventID, null, string.Empty);
        }

        public void Dispatch(int eventID, object data)
        {
            Dispatch(eventID, data, string.Empty);
        }

        private void Dispatch(int eventID, object data, string eventName)
        {


            MessageBase e = new MessageBase(eventID, eventName, data);
            if (dataListners.ContainsKey(eventID))
            {
                List<DataEventHandlerFunction> handlers = CloneArray(dataListners[eventID]);
                int len = handlers.Count;
                for (int i = 0; i < len; i++)
                {
                    handlers[i](e);
                }
            }

            if (listener.ContainsKey(eventID))
            {
                List<EventHandlerFunction> handlers = CloneArray(listener[eventID]);
                int len = handlers.Count;
                for (int i = 0; i < len; i++)
                {
                    handlers[i]();
                }
            }

        }
        #endregion

        public void ClearEvents()
        {
            listener.Clear();
            dataListners.Clear();
        }

        private List<EventHandlerFunction> CloneArray(List<EventHandlerFunction> list)
        {
            List<EventHandlerFunction> nl = new List<EventHandlerFunction>();
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                nl.Add(list[i]);
            }

            return nl;
        }
        private List<DataEventHandlerFunction> CloneArray(List<DataEventHandlerFunction> list)
        {
            List<DataEventHandlerFunction> nl = new List<DataEventHandlerFunction>();
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                nl.Add(list[i]);
            }

            return nl;
        }
    }
}
