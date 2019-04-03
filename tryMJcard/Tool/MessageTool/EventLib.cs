using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    /// <summary>
    /// 基础事件类型
    /// </summary>
    public static class EventLib
    {
        public delegate void EventCall(object param);
        private static Dictionary<object, EventLib.EventCall> data = new Dictionary<object, EventLib.EventCall>();
        public static void RegisterEvent(object id, EventLib.EventCall call)
        {
            if (EventLib.data.ContainsKey(id))
            {
                Dictionary<object, EventLib.EventCall> dictionary;
                (dictionary = EventLib.data)[id] = (EventLib.EventCall)Delegate.Remove(dictionary[id], EventLib.data[id]);
                EventLib.data.Remove(id);
            }
            EventLib.data.Add(id, call);
        }
        public static void RemoveEvent(object id)
        {
            if (EventLib.data.ContainsKey(id))
            {
                Dictionary<object, EventLib.EventCall> dictionary;
                (dictionary = EventLib.data)[id] = (EventLib.EventCall)Delegate.Remove(dictionary[id], EventLib.data[id]);
                EventLib.data.Remove(id);
            }
        }
        public static void Clear()
        {
            EventLib.data.Clear();
        }
        public static void SendEvent(object id, object o)
        {
            if (EventLib.data.ContainsKey(id))
            {
                EventLib.data[id](o);
            }
        }
    }
}
