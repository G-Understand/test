using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace tryMJcard
{
    class Test
    {
        /// <summary>
        /// 麻将测试
        /// </summary>
        public static void MaJong()
        {
            var dsaasda = HuHelper.GetShunList(5);
            //1,2,3,17,18,19,31,31,14,14,28,29,45,45    ,1,2,3,17,18,19,27,28,29,31,31,14,14,31
            //1,3,17,18,19,37,37,26,26,14,14,14,45,45   ,17,18,19,1,2,3,37,37,26,26,14,14,14,37
            //1,2,3,17,18,19,41,41,26,14,14,14,45,45    ,1,2,3,17,18,19,41,41,26,14,14,14,41,26
            //1,2,,17,,19,41,41,26,26,14,14,14,45,45,45 ,1,2,3,17,18,19,41,41,26,26,14,14,14,41
            //1,2,3,35,35,35,41,41,26,26,14,14,14,45    ,1,2,3,41,41,35,35,35,26,26,14,14,14,41
            //1,2,3,17,18,19,41,41,35,35,14,14,14,45    ,1,2,3,17,18,19,41,41,35,35,14,14,14,41
            //1,2,3,17,18,19,41,41,43,43,14,14,14,45    ,17,18,19,41,41,26,26,14,14,14,1,1,1,41
            //1,1,1,17,18,19,41,41,26,26,14,14,14,45    ,17,18,19,41,41,26,26,14,14,14,1,1,1,41
            //45, 7, 8, 6, 29, 29, 31, 31, 33, 33, 33   ,6,7,8,31,32,33,33,33,31,29,29
            //1,2,3,17,17,17,41,41,26,26,14,14,14,45    ,1,2,3,41,41,26,26,17,17,17,14,14,14,41
            List<int> handCard = new List<int>() { 45, 7, 8, 6, 29, 29, 31, 31, 33, 33, 33, 32, 32, 32 };
            //int count 
            List<int> result = HuHelper.GetMaxShun(handCard, 45);
            string keList = string.Empty;
            for (int i = 0; i < result.Count; i++)
            {
                keList += "," + result[i];
            }
            Console.WriteLine(keList);
            List<int> handcard = new List<int>();
            int[] mjhc = SetTestHandCard();

            foreach (int a in mjhc)
            {
                handcard.Add(a);
            }
            List<int> fs = handcard.FindAll(delegate(int a)
            {
                return handcard[0] == a;
            });
            var result_bool = HuHelper.ShiSanYao_bool(handcard, 45);
            Console.WriteLine(handcard.Where(d => d == 9).ToList().Count());
        }

        /// <summary>
        /// 鱼死亡概率
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        public bool ProbabilityDeath()
        {
            bool result = false;
            for (int i = 0; i < 2; i++)
            {
                
            }
            return result;
        }
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GetRandom(int minValue, int maxValue)
        {//使用Guid.NewGuid().GetHashCode()作为种子，可以确保Random在极短时间产生的随机数尽可能做到不重复
            Random rand = new Random(Guid.NewGuid().GetHashCode());//使用Guid的哈希值做种子  
            int item;
            item = rand.Next(minValue, maxValue);
            return item;
        }
        public static void Fishing()
        {
            for (int i = 0; i < 500; i++)
            {
                int random = GetRandom(0, 100);
                int number = 0;
                while (random / 100.00 < ( 1/30.00 * 0.35))
                {
                    
                }
            }
        }

        /// <summary>
        /// 正则表达式测试
        /// </summary>
        public static void  RegexText()
        {
            string asdsdsdsdsdsdsds = "18838729419";
            Regex pattern = new Regex(@"0?(13|14|15|17|18|19)[0-9]{9}", RegexOptions.IgnoreCase);//@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$"    ^[1]+[3,5]+\d{9}
            bool ddddddddsss = pattern.IsMatch(asdsdsdsdsdsdsds);
            string ddddss = pattern.Replace(asdsdsdsdsdsdsds, "");

            if (!ddddddddsss && ddddss == "")
            {
                //return;
            }
            var fircardid = string.Empty;
            fircardid = asdsdsdsdsdsdsds.Substring(0, 3);
            fircardid = fircardid.PadRight(8, '*');
            fircardid += asdsdsdsdsdsdsds.Substring(8, asdsdsdsdsdsdsds.Length - 8);
        }

        /// <summary>
        /// 类（线程）测试
        /// </summary>
        public static void ThreadTest()
        {
            LockTest dat = new LockTest("gui");
            dat.button2_Click();
            A a = new A();
            Console.WriteLine(a.a);
            a = null;
            Console.WriteLine("!!!!!!!!!!!!!!!!");
        }

        /// <summary>
        /// 数组测试
        /// </summary>
        public static void Tax_List()
        {
            List<int> a = new List<int>() { 1, 1, 1, 3, 31, 33, 35, 4 };
            List<int> b = new List<int>() { 1, 3, 4, 1, 1 };
            var exceptArr = a.Except(b);
            List<int> fengList = new List<int>() { 31, 33, 35, 37 };
            List<int> c = a.Distinct().ToList();
            if (c.Count(d => a.Count(t => t == d) == b.Count(t => t == d)) >= c.Count - 1)
            {
                Console.WriteLine(" 是 一个子集");
            }
            if (fengList.All(d => a.Contains(d)))
            {
                Console.WriteLine(111111111);
            }


            if (a.All(t => b.Any(d => d == t)))
            {
                Console.WriteLine("包含");
            }
            else
            {
                Console.WriteLine("不包含");
            }
            if (exceptArr.Any())
            {
                Console.WriteLine("samllArr 是 bigArr的一个子集");
            }
            Console.WriteLine("result");
        }

        /// <summary>
        /// 数学规则测试
        /// </summary>
        public static void RegularTest()
        {
            List<int> tax111 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> tax222 = new List<int>() { 2, 3, 4, 5, 6 };
            List<int> result1111 = new List<int>();
            for (int i = 0; i < tax111.Count; i++)
            {
                for (int a = 0; a < tax222.Count; a++)
                {
                    if (tax222[a] >= tax111[i] + 1)
                    {
                        result1111.Add(tax111[i] * 10 + tax222[a]);
                    }
                }
            }
            Console.WriteLine(result1111.ToJson());
        }

        /// <summary>
        /// 设置手牌
        /// </summary>
        /// <returns></returns>
        public static int[] SetTestHandCard()
        {
            //int[] mjhc = { 1, 2, 3, 4, 1, 6, 7, 8, 9, 5, 1, 41, 43, 45 };
            int[] mjhc = { 3, 16, 3, 3, 3, 6, 6, 6, 9, 9, 9, 16, 16, 1 };
            //int[] mjhc = { 43, 1, 1, 11, 19, 21, 29, 45, 33, 45, 41, 37, 45,31 };
            //int[] mjhc = { 43, 1, 9, 11, 29, 21, 29, 45, 33, 45, 41, 37, 45,45 };
            //int[] mjhc = { 43, 1, 9, 11, 19, 21, 29, 45, 33, 45, 41, 37 ,45};
            //int[] mjhc = { 1, 1, 4, 4, 6, 6, 8, 7, 7, 7, 11, 11, 43, 43 };
            return mjhc;
        }

        /// <summary>
        /// 文本输入 测试
        /// </summary>
        public static void WriteLineTest()
        {
            Console.WriteLine(Sign_in.Instance.Find_SignInRecord(4784341));
            Console.WriteLine("请输入你想要写人的内容：");
            TextFile.Instance.Wirte(Console.ReadLine());
            Console.WriteLine("写入完成，程序即将结束".ToJson());
            Console.WriteLine("123".getint().GetType().ToJson()); Console.WriteLine("$#^@#$%&#%^#".getInt());
        }

        /// <summary>
        /// Json  测试
        /// </summary>
        public static void JsonTest()
        {
            string sadafsad = string.Empty;
            ssapfjpjf sadfo = new ssapfjpjf();
            sadfo.sssapfjpjf(new ajsofj(1, "sdfg", 1111, 2222));
            var saod = sadfo.ToJson();
            ssapfjpjf sssa = Json.ParseJson<ssapfjpjf>(saod);
            string day = DateTime.Now.DayOfWeek.ToString();
        }

        /// <summary>
        /// 时间戳 测试
        /// </summary>
        public static void TimestampTest()
        {
            string timeeeee = "2018-9-29";
            timeeeee = Convert.ToDateTime(timeeeee).AddDays(1).ToString("yyyy-MM-dd");
            DateTime gameTime = DateTime.Now;
            var dateee = Convert.ToInt32((DateTime.Now - gameTime).TotalMinutes).ToString();
            string asdff = DateTime.Now.ToString("HHmmss");//.Replace(":", string.Empty);
            int numbseras = asdff.getint();
            string asiodjfiaopsdjfpa = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
        }

        /// <summary>
        /// bool测试
        /// </summary>
        public static void BoolTest()
        {
            List<int> chilist = new List<int>();
            List<int> penglist = new List<int>();
            List<int> hulist = new List<int>();
            hulist.Add(1);
            chilist.Add(1);
            int PengGangType = 4;
            if ((hulist.Count > 0
                   && !hulist.Contains(1)) || ((hulist.Count > 0 && !hulist.Contains(1)) && (!penglist.Contains(1) && penglist.Count > 0)) || (PengGangType == 4 && (!penglist.Contains(1) && penglist.Count > 0)))
            {
                return;
            }
        }

        /// <summary>
        /// 数字测试
        /// </summary>
        public static void IntTest()
        {
            //             RoomData room = (
            //                 from r 
            //                     in roomList 
            //                 where r.roomId == roomId 
            //                 select r
            //                 ).FirstOrDefault() ;
            string jingdu = "123456987654";
            /*var datssssssssss = jingdu.Split('|');*/
            for (int i = 0; i < jingdu.Count(); i++)
            {
                Console.WriteLine(jingdu[i]);
            }
            var shuiji = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //var data = Permutations(shuiji, 5);
            int[] IntArr = new int[] { 1, 2, 3, 4, 5 }; //整型数组
            var data = PermutationAndCombination<int>.GetCombination(IntArr, 3);
            List<int> sasasa = new List<int>();
            if (sasasa.Count != 1 || sasasa[0] != 0)
            {

            }
            int sc = 77 * 95 / 100;
            int pow = (int)Math.Pow(2, 4);
            int sdde = (int)1.9530;

        }

        /// <summary>
        /// 字符串测试
        /// </summary>
        public static void StringTest()
        {
            string url = "http://www.cfanz.cn/uploads/jpg/2013/07/13/0/XEPLd7d2C5.jpg";
            url = url.Replace("/", "++++"); ;
        }
    }

    /// <summary>
    /// 线程测试
    /// </summary>
    public class A
    {
        public int a = 5;
        public A()
        {
            if (onjiance != null && onjiance.Enabled)
            {
                onjiance.Stop();
            }
            onjiance = new System.Timers.Timer(10000);
            onjiance.Elapsed += new ElapsedEventHandler(TimerHanlderOnJianCe);
            onjiance.AutoReset = true;
            onjiance.Start();
            onjiance.Stop();
        }

        System.Timers.Timer onjiance = new System.Timers.Timer();
        private void TimerHanlderOnJianCe(object sender, ElapsedEventArgs e)
        {
            JianCe();
        }
        public void JianCe()
        {
            Console.WriteLine("依然在");
        }
        ~A()
        {
            Console.WriteLine("jieshul");
        }

        object o = new object();

        public void ReadMsg(int msg)
        {
            lock (o)
            {
                Console.WriteLine("----------------------" + msg);
            }

        }

        static object c = new object();
        public void ReadText(int msg)
        {
            lock (c)
            {
                Console.WriteLine("----------------------" + msg);
            }
        }
    }

    /// <summary>
    /// 跨线程操作UI的时候传递的参数，本文为了显示消息，所以简单的封装了一个
    /// </summary>
    public class MyEventArgs : EventArgs
    {
        public readonly string Message = string.Empty;
        public MyEventArgs(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    /// <summary>
    /// 测试类，用于测试2种锁的区别
    /// </summary>
    public class LockTest
    {
        //2个锁
        private static readonly object Locker1 = new object();
        private readonly object Locker2 = new object();

        /// <summary>
        /// 跨线程操作UI的委托和事件
        /// </summary>
        public delegate void MessageEventHandler(object sender, MyEventArgs e);
        public event MessageEventHandler MessageEvent;
        public void OnMessage(MyEventArgs e)
        {
            if (this.MessageEvent != null) MessageEvent(this, e);
        }

        //要锁的变量，通过它可以看出2种锁在不同情况下的效果
        private int num = 0;
        //实例名字
        private readonly string Name;
        public LockTest(string name)
        {
            Name = name;
        }
        //第一种锁执行的方法
        public void AddNum1()
        {
            lock (Locker1)
            {
                num = 0;
                ShowMessage();
            }
        }
        //第二种锁执行的方法
        public void AddNum2()
        {
            lock (Locker2)
            {
                num = 0;
                ShowMessage();
            }
        }
        //锁内的一些操作，并通过事件，把关键的消息显示到主线程中的UI里
        private void ShowMessage()
        {
            string msg = "";
            for (int i = 0; i < 10; i++)
            {
                num += 1;
                msg = string.Format("线程 [{0}]，实例[{1}]中num的值是[{2}]", Thread.CurrentThread.Name, this.Name, num);
                OnMessage(new MyEventArgs(msg));
                Thread.Sleep(100);
            }
            msg = string.Format("======线程 [{0}]执行完毕======", Thread.CurrentThread.Name);
            OnMessage(new MyEventArgs(msg));
        }

        public void button1_Click()
        {
            LockTest test = new LockTest("LockTest 1");
            for (int i = 0; i <= 2; i++)
            {
                Thread a = new Thread(new ThreadStart(test.AddNum1));
                a.Name = i.ToString();
                a.Start();
            }
        }

        public void button2_Click()
        {
            LockTest test = new LockTest("LockTest 1");
            for (int i = 0; i <= 2; i++)
            {
                Thread a = new Thread(new ThreadStart(test.AddNum2));
                a.Name = i.ToString();
                a.Start();
            }
        }

        public void button3_Click()
        {
            for (int i = 0; i <= 2; i++)
            {
                LockTest test = new LockTest("LockTest " + i.ToString());
                Thread a = new Thread(new ThreadStart(test.AddNum1));
                a.Name = i.ToString();
                a.Start();
            }
        }

        public void button4_Click()
        {
            for (int i = 0; i <= 2; i++)
            {
                LockTest test = new LockTest("LockTest " + i.ToString());
                Thread a = new Thread(new ThreadStart(test.AddNum2));
                a.Name = i.ToString();
                a.Start();
            }
        }
    }
}
