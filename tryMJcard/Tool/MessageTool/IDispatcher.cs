using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{

    public delegate void EventHandlerFunction();
    public delegate void DataEventHandlerFunction(MessageBase tevent);

    public interface IDispatcher
    {


        //--------------------------------------
        // ADD LISTENER'S
        //--------------------------------------

        void AddEventListener(string eventName, EventHandlerFunction handler);
        void AddEventListener(int eventID, EventHandlerFunction handler);
        void AddEventListener(string eventName, DataEventHandlerFunction handler);
        void AddEventListener(int eventID, DataEventHandlerFunction handler);


        //--------------------------------------
        // REMOVE LISTENER'S
        //--------------------------------------

        void RemoveEventListener(string eventName, EventHandlerFunction handler);
        void RemoveEventListener(int eventID, EventHandlerFunction handler);
        void RemoveEventListener(string eventName, DataEventHandlerFunction handler);
        void RemoveEventListener(int eventID, DataEventHandlerFunction handler);

        //--------------------------------------
        // DISPATCH EVENTS
        //--------------------------------------
        void Dispatch(int eventID);
        void Dispatch(int eventID, object data);
        void Dispatch(string eventName);
        void Dispatch(string eventName, object data);


        //--------------------------------------
        // CLEAR EVENTS
        //--------------------------------------

        void ClearEvents();

    }
}
