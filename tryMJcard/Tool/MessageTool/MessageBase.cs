using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    public class MessageBase : EventArgs
    {
        //private static byte MESSAGE_ID = 0;
        //private static byte MESSAGE_DATA = 1;
        new public byte[] Data
        {
            get
            {
                return m_Data as byte[];
            }
        }

        public MessageBase()
        { }


        public MessageBase(int id, object data)
            : base(id, id.ToString(), data)
        {

        }

        public MessageBase(int id, string name, object data)
            : base(id, name, data)
        {

        }


        //     public void ParseFromEventParams(Dictionary<byte, object> Parameters)
        //     {
        // 
        //         if (Parameters.ContainsKey((byte)MESSAGE_ID))
        //         {
        //             m_Id = (NetID)Parameters[(byte)MESSAGE_ID];
        //         }
        // 
        //         if (Parameters.ContainsKey((byte)MESSAGE_DATA))
        //         {
        //             m_Data = Parameters[(byte)MESSAGE_DATA];
        //         }
        // 
        //     }

        //     public void ConvertToEventParams(out Dictionary<byte, object> Parameters)
        //     {
        //         Parameters = new Dictionary<byte, object>();
        //         Parameters[MESSAGE_ID] = m_Id;
        //         Parameters[MESSAGE_DATA] = ((MemoryStream)m_Data).ToArray();
        //     }

        //     public Dictionary<byte, object> ConvertToEventParams()
        //     {
        //         Dictionary<byte, object> Parameters = new Dictionary<byte, object>();
        //         Parameters[MESSAGE_ID] = m_Id;
        //         Parameters[MESSAGE_DATA] = ((MemoryStream)m_Data).ToArray();
        //         return Parameters;
        //     }
    }
}
