using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.WindowsTool
{
    public class MD5Mn
    {
        /// <summary>
        /// 对文件流进行MD5加密
        /// </summary>
        public static string MD5Stream(Stream stream)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(stream);
            byte[] b = md5.Hash;
            md5.Clear();
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 对文件进行MD5加密
        /// </summary>
        public static string MD5Stream(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8, FileOptions.Asynchronous))
            {
                return MD5Stream(stream);
            }
            //             using (FileStream stream = File.Open(filePath, FileMode.Open))
            //             {
            //                 return MD5Stream(stream);
            //             }
        }
    }
}
