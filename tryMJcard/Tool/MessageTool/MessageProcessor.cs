using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    public class MessageProcessor : MessageProcessorBase<MessageProcessor>
    {
        private int m_UserId;

        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        public override void AddListener()
        {
            //EventDispatcher.AddEventListener(NetID.Login, XW.Game.UI.Login.Init);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public override void RemoveListener()
        {
            //EventDispatcher.RemoveEventListener(NetID.Login, XW.Game.UI.Login.Init);
        }

    }
}
