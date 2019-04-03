using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.MessageTool
{
    /// <summary>
    /// 消息解析类
    /// </summary>
    public class MessageHandle
    {

        public static T Deserialize<T>(byte[] source)
        {
            using (MemoryStream ms = new MemoryStream(source))
            {
                return Deserialize<T>(ms);
            }
        }
        public static T Deserialize<T>(MemoryStream ms)
        {
            return ProtoBuf.Serializer.Deserialize<T>(ms);
        }

        public static T Deserialize<T>(object value)
        {
            byte[] source = (byte[])value;
            using (MemoryStream ms = new MemoryStream(source))
            {
                return Deserialize<T>(ms);
            }
        }

        public static void Serialize(System.IO.Stream dest, object value)
        {
            ProtoBuf.Serializer.Serialize(dest, value);
        }
    }
}
