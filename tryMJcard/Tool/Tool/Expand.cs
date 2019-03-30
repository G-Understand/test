using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    public class ssapfjpjf
    {
        public List<ajsofj> sd = new List<ajsofj>();
        public void sssapfjpjf(ajsofj asd)
        {
            sd.Add(asd);
        }
        public ssapfjpjf()
        { }
    }
    public class ajsofj
    {
        public int saofj { get; set; }
        public string sajo { get; set; }
        public ajsofj(int s, string b,int ssss,int aaaa)
        {
            saofj = s;
            sajo = b;
        }
        public ajsofj()
        {

        }
        public void asojfio()
        {

        }
        public void asfoa()
        {

        }
    }

    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class Expand
    {
        public static int getint<T>(this T o)
        {
            int number = 0;
            try
            {
                number = (int)Convert.ChangeType(o, typeof(int));
            }
            catch (Exception ex)
            {
                return 0;
            }

            return number;
        }
    }
}
