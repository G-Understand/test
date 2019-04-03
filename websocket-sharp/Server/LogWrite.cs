using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace hsAppServer
{
    public class LogWrite1
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirPath"></param>
        private static void createDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="userName">用户</param>
        /// <param name="actionUrl">请求的地址</param>
        /// <param name="msg">系统消息</param>
        public static void SystemLog(string msg)
        {
            try
            {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyyMM") + "\\SystemLog\\";
                    createDir(path);

                    string fileName = path + DateTime.Now.ToString("yyyyMMdd") + ".log";

                    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 8, FileOptions.Asynchronous))
                    {
                        Byte[] dataArray = System.Text.Encoding.Default.GetBytes(DateTime.Now + " " + msg + System.Environment.NewLine);
                        bool flag = true;
                        long slen = dataArray.Length;
                        long len = 0;
                        while (flag)
                        {
                            try
                            {
                                if (len >= fs.Length)
                                {
                                    fs.Lock(len, slen);
                                    lockDic[len] = slen;
                                    flag = false;
                                }
                                else
                                {
                                    len = fs.Length;
                                }
                            }
                            catch (Exception ex)
                            {
                                while (!lockDic.ContainsKey(len))
                                {
                                    len += lockDic[len];
                                }
                            }
                        }
                        fs.Seek(len, SeekOrigin.Begin);
                        fs.Write(dataArray, 0, dataArray.Length);
                        fs.Close();
                    }
                }
            
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

    }
}
