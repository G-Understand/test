using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    public class BufferManager:Singleton <BufferManager>
    {
        /// <summary>
        /// 以字节数组的形式返回指定的 32 位有符号整数值。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private byte[] getInt32(int val)
        {
            return BitConverter.GetBytes(val);
        }

        /// <summary>
        /// 进行编码 148
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public byte[] Encode(byte[] array)
        {
            byte[] array2 = new byte[array.Length];//获取一个和穿进去的相同的长度的字节数组
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = (byte)(array[i] ^ 148);///进行编码
            }
            return array2;
        }

        /// <summary>
        /// 进行解码 148
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public byte[] Decode(byte[] array)
        {
            byte[] array2 = new byte[array.Length];//获取一个和穿进去的相同的长度的字节数组
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = (byte)(array[i] ^ 148);///将编过码的进行解码
            }
            return array2;
        }

        /// <summary>
        /// 将字典进行编码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public byte[] Encode(Dictionary<string, object> src)
        {
            byte[] contentBytesEncode = BufferManager.Instance.getContentBytesEncode(src);
            byte[] array = new byte[contentBytesEncode.Length];
            for (int i = 0; i < contentBytesEncode.Length; i++)
            {
                array[i] = (byte)(contentBytesEncode[i] ^ 148);
            }
            return array;
        }

        /// <summary>
        /// 将字节流解成字典
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public Dictionary<string, object> DecodeArray(byte[] array)
        {
            byte[] array2 = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = (byte)(array[i] ^ 148);
            }
            return BufferManager.Instance.getContentBytesDecode(array2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public byte[] getContentBytesEncode(Dictionary<string, object> src)
        {
            List<object> list = new List<object>(src.Values);
            List<byte> list2 = new List<byte>();
            int num = 0;
            byte[] array = new byte[8192000];
            foreach (object current in list)
            {
                Type type = current.GetType();
                byte[] array2 = new byte[10];
                bool flag = type == typeof(byte[]);
                if (flag)
                {
                    array2 = (current as byte[]);
                    int val = array2.Length;
                    byte[] @int = BufferManager.Instance.getInt32(val);
                    Buffer.BlockCopy(@int, 0, array, num, @int.Length);
                    num += 4;
                    array[num] = 1;
                    num++;
                    Buffer.BlockCopy(array2, 0, array, num, array2.Length);
                    num += array2.Length;
                }
                else
                {
                    bool flag2 = type == typeof(Dictionary<string, object>);
                    if (flag2)
                    {
                        byte[] contentBytesEncode = BufferManager.Instance.getContentBytesEncode(current as Dictionary<string, object>);
                        int val2 = contentBytesEncode.Length;
                        byte[] int2 = BufferManager.Instance.getInt32(val2);
                        Buffer.BlockCopy(int2, 0, array, num, int2.Length);
                        num += 4;
                        array[num] = 4;
                        num++;
                        Buffer.BlockCopy(contentBytesEncode, 0, array, num, contentBytesEncode.Length);
                        num += contentBytesEncode.Length;
                    }
                    else
                    {
                        bool flag3 = type == typeof(List<object>);
                        List<object> data = new List<object>();
                        bool osne = false;
                        if (type == typeof(List<int>))
                        {
                            List<int> ayy = current as List<int>;
                            foreach (var m in ayy)
                            {
                                data.Add(m);
                            }
                            flag3 = true;
                            osne = true;

                        }


                        if (flag3)
                        {
                            string text = string.Empty;
                            var ps = osne ? data : current;
                            foreach (object current2 in ps as List<object>)
                            {
                                text = text + current2 + ",";
                            }
                            text = text.Substring(0, text.Length - 1);
                            array2 = Encoding.UTF8.GetBytes(text);
                            int val3 = array2.Length;
                            byte[] int3 = BufferManager.Instance.getInt32(val3);
                            Buffer.BlockCopy(int3, 0, array, num, int3.Length);
                            num += 4;
                            array[num] = 5;
                            num++;
                            Buffer.BlockCopy(array2, 0, array, num, array2.Length);
                            num += array2.Length;
                        }
                        else
                        {
                            bool flag4 = type == typeof(List<Dictionary<string, object>>);
                            if (flag4)
                            {
                                int num2 = 0;
                                List<Dictionary<string, object>> list3 = current as List<Dictionary<string, object>>;
                                List<byte[]> list4 = new List<byte[]>();
                                foreach (Dictionary<string, object> current3 in list3)
                                {
                                    byte[] contentBytesEncode2 = BufferManager.Instance.getContentBytesEncode(current3);
                                    num2 += contentBytesEncode2.Length;
                                    list4.Add(contentBytesEncode2);
                                }
                                int val4 = num2 + 1;
                                byte[] int4 = BufferManager.Instance.getInt32(val4);
                                Buffer.BlockCopy(int4, 0, array, num, int4.Length);
                                num += 4;
                                array[num] = 3;
                                num++;
                                array[num] = (byte)list3.Count;
                                num++;
                                foreach (byte[] current4 in list4)
                                {
                                    Buffer.BlockCopy(current4, 0, array, num, current4.Length);
                                    num += current4.Length;
                                }
                            }
                            else
                            {
                                array2 = Encoding.UTF8.GetBytes(current.ToString());
                                int val5 = array2.Length;
                                byte[] int5 = BufferManager.Instance.getInt32(val5);
                                Buffer.BlockCopy(int5, 0, array, num, int5.Length);
                                num += 4;
                                array[num] = 2;
                                num++;
                                Buffer.BlockCopy(array2, 0, array, num, array2.Length);
                                num += array2.Length;
                            }
                        }
                    }
                }
            }
            byte[] array3 = new byte[num];
            Buffer.BlockCopy(array, 0, array3, 0, num);
            return array3;
        }
        public Dictionary<string, object> getContentBytesDecode(byte[] content)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            int num = 0;
            int num2 = content.Length;
            int i = 0;
            bool flag = content.Length == 0;
            Dictionary<string, object> result;
            if (flag)
            {
                result = new Dictionary<string, object>();
            }
            else
            {
                int num3 = BufferManager.Instance.getMsgLenData(content, i) + 5;
                byte[] array = new byte[num3];
                Buffer.BlockCopy(content, i, array, 0, num3);
                i += num3;
                dictionary.Add(num.ToString(), BufferManager.Instance.getMsgValData(array));
                num++;
                while (i < content.Length)
                {
                    num3 = BufferManager.Instance.getMsgLenData(content, i) + 5;
                    array = new byte[num3];
                    Buffer.BlockCopy(content, i, array, 0, num3);
                    i += num3;
                    dictionary.Add(num.ToString(), BufferManager.Instance.getMsgValData(array));
                    num++;
                }
                result = dictionary;
            }
            return result;
        }
        private object getMsgValData(byte[] content)
        {
            byte[] array = new byte[content.Length - 5];
            Buffer.BlockCopy(content, 5, array, 0, array.Length);
            bool flag = content[4] == 1;
            object result;
            if (flag)
            {
                result = array;
            }
            else
            {
                bool flag2 = content[4] == 2;
                if (flag2)
                {
                    result = Encoding.UTF8.GetString(array);
                }
                else
                {
                    bool flag3 = content[4] == 5;
                    if (flag3)
                    {
                        string @string = Encoding.UTF8.GetString(array);
                        result = new List<object>(@string.Split(new char[]
						{
							','
						}));
                    }
                    else
                    {
                        bool flag4 = content[4] == 4;
                        if (flag4)
                        {
                            result = BufferManager.Instance.getContentBytesDecode(array);
                        }
                        else
                        {
                            bool flag5 = content[4] == 3;
                            if (flag5)
                            {
                                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                                int num = (int)content[3];
                                array = new byte[content.Length - 4];
                                Buffer.BlockCopy(content, 4, array, 0, array.Length);
                                Dictionary<string, object> contentBytesDecode = BufferManager.Instance.getContentBytesDecode(array);
                                bool flag6 = contentBytesDecode.Values.Count == 0;
                                if (flag6)
                                {
                                    result = list;
                                }
                                else
                                {
                                    int num2 = contentBytesDecode.Values.Count / num;
                                    for (int i = 0; i < num; i++)
                                    {
                                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                        for (int j = 0; j < num2; j++)
                                        {
                                            dictionary.Add(j.ToString(), contentBytesDecode[(i * 2 + j).ToString()]);
                                        }
                                        list.Add(dictionary);
                                    }
                                    result = list;
                                }
                            }
                            else
                            {
                                result = null;
                            }
                        }
                    }
                }
            }
            return result;
        }
        private int getMsgLenData(byte[] src, int offect)
        {
            byte[] array = new byte[4];
            Buffer.BlockCopy(src, offect, array, 0, 4);
            return BitConverter.ToInt32(array, 0);
        }
    }
}
