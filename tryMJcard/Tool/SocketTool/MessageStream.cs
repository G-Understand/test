using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.SocketToolHandler
{
    public class MessageStream
    {
        private byte[] _buffer;

        private int _position;

        private int _length;

        private int _capacity;

        public MessageStream()
        {
            this._buffer = new byte[0];
            this._position = 0;
            this._length = 0;
            this._capacity = 0;
        }

        private byte ReadByte()
        {
            byte arg_2A_0;
            if (this._position < this._length)
            {
                byte[] arg_26_0 = this._buffer;
                int position = this._position;
                this._position = position + 1;
                arg_2A_0 = arg_26_0[position];
            }
            else
            {
                arg_2A_0 = 0;
            }
            return arg_2A_0;
        }

        private int ReadInt()
        {
            int num = this._position += 4;
            bool flag = num <= this._length;
            int result;
            if (flag)
            {
                result = ((int)this._buffer[num - 4] | (int)this._buffer[num - 3] << 8 | (int)this._buffer[num - 2] << 16 | (int)this._buffer[num - 1] << 24);
            }
            else
            {
                this._position = this._length;
                result = -1;
            }
            return result;
        }

        private byte[] ReadBytes(int count)
        {
            int num = this._length - this._position;
            bool flag = num > count;
            if (flag)
            {
                num = count;
            }
            bool flag2 = num <= 0;
            byte[] result;
            if (flag2)
            {
                result = null;
            }
            else
            {
                byte[] array = new byte[num];
                bool flag3 = num <= 8;
                if (flag3)
                {
                    int num2 = num;
                    while (--num2 >= 0)
                    {
                        array[num2] = this._buffer[this._position + num2];
                    }
                }
                else
                {
                    Buffer.BlockCopy(this._buffer, this._position, array, 0, num);
                }
                this._position += num;
                result = array;
            }
            return result;
        }

        public bool Read(out Message message)
        {
            message = null;
            this._position = 0;
            bool flag = this._length <= 10;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                message = new Message
                {
                    Class = this.ReadByte(),
                    Flag = this.ReadByte(),
                    Size = this.ReadInt(),
                    CommandSize = this.ReadInt()
                };
                bool flag2 = message.Size <= 0 || message.Size <= this._length - this._position;
                if (flag2)
                {
                    bool flag3 = message.Size > 0;
                    if (flag3)
                    {
                        message.Content = this.ReadBytes(message.Size);
                    }
                    this.Remove(message.Size + 10);
                    result = true;
                }
                else
                {
                    message = null;
                    result = false;
                }
            }
            return result;
        }

        private void EnsureCapacity(int value)
        {
            bool flag = value <= this._capacity;
            if (!flag)
            {
                int num = value;
                bool flag2 = num < 256;
                if (flag2)
                {
                    num = 256;
                }
                bool flag3 = num < this._capacity * 2;
                if (flag3)
                {
                    num = this._capacity * 2;
                }
                byte[] array = new byte[num];
                bool flag4 = this._length > 0;
                if (flag4)
                {
                    Buffer.BlockCopy(this._buffer, 0, array, 0, this._length);
                }
                this._buffer = array;
                this._capacity = num;
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            bool flag = buffer.Length - offset < count;
            if (flag)
            {
                count = buffer.Length - offset;
            }
            this.EnsureCapacity(this._length + count);
            Array.Clear(this._buffer, this._length, this._capacity - this._length);
            Buffer.BlockCopy(buffer, offset, this._buffer, this._length, count);
            this._length += count;
        }

        private void Remove(int count)
        {
            bool flag = this._length >= count;
            if (flag)
            {
                Buffer.BlockCopy(this._buffer, count, this._buffer, 0, this._length - count);
                this._length -= count;
                Array.Clear(this._buffer, this._length, this._capacity - this._length);
            }
            else
            {
                this._length = 0;
                Array.Clear(this._buffer, 0, this._capacity);
            }
        }
    }
    public class Message
    {
        public byte[] Content
        {
            get;
            set;
        }

        public byte Flag
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int CommandSize
        {
            get;
            set;
        }

        public byte Class
        {
            get;
            set;
        }

        public Message()
        {
        }

        public Message(byte @class, byte flag, int commandSize, byte[] content)
        {
            this.Class = @class;
            this.Flag = flag;
            this.Size = content.Length;
            this.CommandSize = commandSize;
            this.Content = content;
        }

        public byte[] ToBytes()
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(this.Class);
                binaryWriter.Write(this.Flag);
                binaryWriter.Write(this.Size);
                binaryWriter.Write(this.CommandSize);
                bool flag = this.Size > 0;
                if (flag)
                {
                    binaryWriter.Write(this.Content);
                }
                result = memoryStream.ToArray();
                binaryWriter.Close();
            }
            return result;
        }

        public Message FromBytes(byte[] buffer)
        {
            Message message = new Message();
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                BinaryReader binaryReader = new BinaryReader(memoryStream);
                message.Class = binaryReader.ReadByte();
                message.Flag = binaryReader.ReadByte();
                message.Size = binaryReader.ReadInt32();
                message.CommandSize = binaryReader.ReadInt32();
                bool flag = message.Size > 0;
                if (flag)
                {
                    message.Content = binaryReader.ReadBytes(message.Size);
                }
                binaryReader.Close();
            }
            return message;
        }
    }
}
