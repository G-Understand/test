using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class TextFile
    {
        private static TextFile _Instance;

        public static TextFile Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TextFile();
                }
                return _Instance;
            }
        }

        private TextFile()
        {

        }
        /// <summary>
        /// 写入文件，str为内容
        /// </summary>
        /// <param name="str"></param>
        public void Wirte(string str)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyyMM") + "\\SystemLog\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string fileName = path + 123456 + ".log";
            CreateOrDeleteFile(path);
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 8, FileOptions.Asynchronous);
            Byte[] dataArray = System.Text.Encoding.Default.GetBytes(str + System.Environment.NewLine);//DateTime.Now + " " +
            long fsLength = 0;
            fsLength = fs.Length;
            fs.Seek(fsLength,SeekOrigin.Begin );
            fs.Write(dataArray, 0, dataArray.Length);
        }
        /// <summary>
        /// 创建和删除文件，path为路径
        /// </summary>
        /// <param name="path"></param>
        public void CreateOrDeleteFile(string path)
        {
            string Createpath = path;
            string DeleteStr = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            string Deletepath= System.AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DeleteStr + "\\";
            if (Directory.Exists(Deletepath))
            {
                Console.WriteLine("存在一个月前的日志文件,将其删除。");
                Directory.Delete(Deletepath,true);
            }
            if (!Directory.Exists(Createpath))
            {
                Console.WriteLine("不存在该文件，创建次文件");
                Directory.CreateDirectory(Createpath);
            }
        }
    }
}
