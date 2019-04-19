using System;
using tryMJcard.SocketToolHandler;

namespace tryMJcard.Tool.MessageTool
{
    public class EventArgs : IDisposable
    {
        protected NetID m_Id;
        protected string m_Name;
        protected object m_Data;
        protected string m_Uid;
        //private IDispatcher m_Dispatcher;
        private bool m_IsStoped = false;
        private bool m_IsLocked = false;



        public NetID ID
        {
            get
            {
                return m_Id;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public string Uid
        {
            get
            {
                return m_Uid;
            }
        }

        public object Data
        {
            get
            {
                return m_Data;
            }
        }

        public bool IsStoped
        {
            get
            {
                return m_IsStoped;
            }
        }

        public bool IsLocked
        {
            get
            {
                return m_IsLocked;
            }
        }

        public EventArgs()
        { }
        public EventArgs(int id, string name, object data)
        {     //构造函数，事件参数
            m_Id = (NetID)id;
            m_Name = name;
            m_Data = data;
        }

        public void FillData(int id, string name, object data)   //所有数据
        {
            m_Id = (NetID)id;
            m_Name = name;
            m_Data = data;
        }

        //--------------------------------------
        // PUBLIC METHODS
        //--------------------------------------
        public void StopPropagation()
        {    //停止传输
            m_IsStoped = true;
        }

        public void StopImmediatePropagation()
        {  //立即停止传输
            m_IsStoped = true;
            m_IsLocked = true;
        }


        public void Dispose()     //处理
        {
        }
    }
}
