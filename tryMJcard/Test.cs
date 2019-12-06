using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using tryMJcard.Tool.TimerTool;
using WebSocketSharp;

namespace tryMJcard
{
    class Test
    {

        public static void SetSQLSERVER()
        {
            tryMJcard.SQL.SQLTest.GetTimeSpan();
        }

        /// <summary>
        /// 定时器测试
        /// </summary>
        public static void TestTimer()
        {
            TimerTest.GetInstance().setDueTime(0, 0, 0, 0, 0);
            TimerTest.GetInstance().setPeriod(0, 0, 0, 0, 1);
            TimerTest.GetInstance().Start();

        }

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

        /// <summary>
        /// 捕鱼测试
        /// </summary>
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
        /// 根据IP获取城市
        /// </summary>
        public static void GetCtryForIP()
        {
            string aaaa = GetAdrByIp("171.15.61.106");
            string sdsdsd = GetIpDetails();
            string dddddd = GetCityName("47.112.130.157");
            string cccccc = GetstringIpAddress("47.99.53.227");
            //GetAddressByIp();
            Console.WriteLine(15315161);
        }

        /// <summary>         
        /// 得到真实IP以及所在地详细信息（Porschev）         
        /// </summary>         
        /// <returns></returns>         
        static string GetIpDetails()
        {
            //设置获取IP地址和国家源码的网址           
            string url = "http://www.ip138.com/ips8.asp";
            string regStr = "(?<=<td\\s*align=\\\"center\\\">)[^<]*?(?=<br/><br/></td>)";

            //IP正则
            string ipRegStr = "((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)";

            //IP地址                 
            string ip = "171.15.61.106";

            //国家            
            string country = string.Empty;

            //省市             
            string adr = string.Empty;

            //得到网页源码             
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html); html = ma.Value;
            Regex ipReg = new Regex(ipRegStr, RegexOptions.None);
            ma = ipReg.Match(html);

            //得到IP  
            ip = ma.Value;
            int index = html.LastIndexOf("：") + 1;

            //得到国家
            country = html.Substring(index);
            adr = GetAdrByIp(ip);
            return "IP：" + ip + "  国家：" + country + "  省市：" + adr;
        }

        /// <summary>         
        /// 通过IP得到IP所在地省市（Porschev）         
        /// </summary>         
        /// <param name="ip"></param>         
        /// <returns></returns>         
        static string GetAdrByIp(string ip)
        {
            string url = "http://www.cz88.net/ip/?ip=" + ip;
            string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";

            //得到网页源码
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html);
            html = ma.Value;
            string[] arr = html.Split(' ');
            return arr[0];
        } 

        /// <summary>         
        /// 获取HTML源码信息(Porschev)         
        /// </summary>         
        /// <param name="url">获取地址</param>         
        /// <returns>HTML源码</returns>         
        static string GetHtml(string url)
        {
            string str = "";
            try
            {
                Uri uri = new Uri(url);
                System.Net.WebRequest wr = System.Net.WebRequest.Create(uri);
                System.IO.Stream s = wr.GetResponse().GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e)
            {
            }
            return str;
        }

        /// <summary>
        /// 红包随机数
        /// </summary>
        public static void ChangeNumberListTest()
        {
            while (true)
            {
                var numberList = GetNumberList(100, 5, 1);
                var data = ChangeNumberList(numberList);
                Console.WriteLine(data.ToJson());
                if (data.Sum() != 100)
                {
                    Console.WriteLine(151561);
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 二分树查询
        /// </summary>
        /// <param name="args"></param>
        static void BinaryQuery(string[] args)
        {

            int[] array = { 10, 20, 50, 6, 45, 10, 33, 25, 40, 5 };

            Array.Sort(array);

            Console.Write("数组排序之后: ");
            foreach (var i in array)
            {
                Console.Write(i + ",");
            }

            Console.WriteLine();
            int a = SearchFun(array, 45);
            Console.WriteLine("找到的值:" + a);




            Console.Read();
        }

        static int SearchFun(int[] array, int value)
        {
            int mid, low, high;

            low = 0;
            high = array.Length - 1;

            while (low < high)
            {
                mid = (low + high) / 2;                 //数组从中间找
                if (array[mid] == value)
                    return array[mid];

                if (array[mid] > value)                 //数组中的值 大于 要找的值, 继续在数组下部分查询   
                    high = mid - 1;
                else
                    low = mid + 1;                      //数组中的值 大于 要找的值, 继续在数组上部分查询   
            }

            return -1;

        }

        public static void BinaryTree(string[] args)
        {
            int[] array = { 43, 69, 11, 72, 28, 21, 56, 80, 48, 94, 32, 8 };

            Console.WriteLine(BinaryTreeSearch(array));

            Console.ReadKey();
        }

        public static int BinaryTreeSearch(int[] array)
        {
            var bstNode = new BSTNode(array[0], 0);
            for (int i = 1; i < array.Length; i++)
            {
                bstNode.Insert(array[i], i);
            }
            return bstNode.Search(80);

        }

        /// <summary>
        /// 二叉树类型
        /// </summary>
        public class BSTNode
        {

            public int Key { get; set; }

            public int Index { get; set; }

            public BSTNode Left { get; set; }

            public BSTNode Right { get; set; }

            public BSTNode(int key, int index)
            {
                Key = key;
                Index = index;
            }

            public void Insert(int key, int index)
            {
                var tree = new BSTNode(key, index);
                if (tree.Key <= Key)
                {
                    if (Left == null)
                    {
                        Left = tree;
                    }
                    else
                    {
                        Left.Insert(key, index);
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        Right = tree;
                    }
                    else
                    {
                        Right.Insert(key, index);
                    }
                }
            }

            public int Search(int key)
            {
                //找左子节点
                var left = Left?.Search(key);
                if (left.HasValue && left.Value != -1) return left.Value;
                //找当前节点
                if (Key == key) return Index;
                //找右子节点
                var right = Right?.Search(key);
                if (right.HasValue && right.Value != -1) return right.Value;
                //找不到时返回-1
                return -1;
            }

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
            Console.WriteLine(1111);
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
        /// 虚函数测试
        /// </summary>
        public static void OverrideTest()
        {
            RedPck redPack = new RedPck();
            redPack.Test();
            Console.WriteLine(222);
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

        /// <summary>
        /// RemoveAll测试
        /// </summary>
        public static void RemoveAllTest()
        {
            List<domo> domos = new List<domo>();
            domos.RemoveAll(d => d.a == 1);
            Console.WriteLine(111);
        }

        /// <summary>
        /// 红包金额，红包数，可发红包最小值，生成与红包数相等且所有值相加等于红包金额的 decimal 集合
        /// </summary>
        /// <param name="allAmount">红包金额</param>
        /// <param name="count">红包数</param>
        /// <param name="min">可发红包最小金额</param>
        /// <returns></returns>
        public static List<decimal> GetNumberList(int allAmount, int count, int min)
        {
            //金额
            int amount = allAmount * 100;
            //包数
            int packageCount = count;
            List<decimal> numberList = new List<decimal>();
            for (int i = 1; i <= count; i++)
            {
                var money = 0;
                if (i == count)
                {
                    money = amount;
                    if (money < 0)
                    {
                        throw new Exception("生成了一个小于0的数字");
                    }
                }
                else
                {
                    int max = amount / packageCount * 2; //(amount - (min * (packageCount - i))) / (packageCount - i) * 2;
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    money = r.Next(min, max);
                    money = money <= min ? min : money;//判断最小值
                    amount -= money;
                    if (money < 0)
                    {
                        throw new Exception("生成了一个小于0的数字");
                    }
                }
                //numberList.Add(money / 100.0m);
                numberList.Add(decimal.Parse(string.Format("{0:N}", money / 100.0m)));
                packageCount--;
            }
            if (numberList.Contains(0))
            {
                Console.WriteLine(1231564);
            }
            return numberList;
        }

        /// <summary>
        /// 接龙修改雷点
        /// </summary>
        /// <param name="numberList"></param>
        /// <returns></returns>
        static List<decimal> ChangeNumberList(List<decimal> numberList)
        {
            if (numberList == null) return new List<decimal>();
            decimal min = numberList.Min();
            int count = numberList.Count(d => d == min);
            if (count <= 1) return numberList;//不需要改变
            decimal max = numberList.Max();
            decimal changeValue = 0.01m;
            decimal sum = changeValue * (count - 1);
            numberList = numberList.OrderBy(d => d).ToList();
            for (int i = 0; i < numberList.Count; i++)//改雷
            {
                decimal value = numberList[i];
                if (value == min && count > 1)//非雷点或只剩最后一个雷点
                {
                    count--;
                    numberList[i] += changeValue;
                }
                if (value == max)
                {
                    numberList[i] -= sum;
                }
            }
            if (numberList.Contains(0))
            {
                Console.WriteLine(1231564);
            }
            return numberList;
        }

        /// <summary>
        /// 根据IP查询城市
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetCityByIp(string ip)
        {
            try
            {
                string city = string.Empty;
                string PostUrl = "http://ip.taobao.com/service/getIpInfo.php?ip=" + ip + "";
                System.Net.WebRequest web = System.Net.WebRequest.Create(PostUrl);
                web.Timeout = 2000;
                System.Net.WebResponse webres = web.GetResponse();
                System.IO.Stream stream = webres.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    Dictionary<string, object> dic = Json.ParseJson<Dictionary<string, object>>(json);
                    string data = dic["data"].ToJson();
                    Dictionary<string, object> dataDic = Json.ParseJson<Dictionary<string, object>>(data);
                    city = dataDic["city"].ToString();
                }
                stream.Close();
                stream.Dispose();
                return city;
            }
            catch (Exception)
            {
                return "无法获取";
            }
        }

        /// <summary>
        /// 根据IP获取省市
        /// </summary>
        public static void GetAddressByIp()
        {
            string ip = "115.193.217.249";
            string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;

            string post = "https://oapi.dingtalk.com/robot/send?access_token=cbb90cd43b46ba015cacbb96b5808947d990ea2c54e9251eb8305c9e84ed23e6";
            string res = GetDataByPost(post);//该条请求返回的数据为：res=1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
            string[] arr = getAreaInfoList(res);
        }

        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static string GetDataByPost(string url)
        {
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            string s = "测试信息";
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = requestBytes.Length;
            System.IO.Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string backstr = sr.ReadToEnd();
            sr.Close();
            res.Close();
            return backstr;
        }

        public static void texxx()
        {

            string CorpId = "你的CorpId ";
            string CorpSecret = "你的CorpSecret ";
            string AccessToken = "";
            string AccessUrl = string.Format("https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}", CorpId, CorpSecret);
//             https://oapi.dingtalk.com/robot/send?access_token=cbb90cd43b46ba015cacbb96b5808947d990ea2c54e9251eb8305c9e84ed23e6' \-H 'Content-Type: application/json' \
//    -d '{"msgtype": "text", 
//         "text": {
//              "content": "我就是我, 是不一样的烟火"
//         }
//       }
        }

        /// <summary>
        /// 处理所要的数据
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        static string[] getAreaInfoList(string ipData)
        {
            //1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
            string[] areaArr = new string[10];
            string[] newAreaArr = new string[2];
            try
            {
                //取所要的数据，这里只取省市
                areaArr = ipData.Split('\t');
                newAreaArr[0] = areaArr[4];//省
                newAreaArr[1] = areaArr[5];//市
            }
            catch (Exception e)
            {
                // TODO: handle exception
            }
            return newAreaArr;
        }

        /// <summary>
        /// 根据IP 获取物理地址
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        static string GetCityName(string strIP)
        {
            string Location = "";
            //string strURL = "http://pv.sohu.com/cityjson/" + strIP;
            string strURL = "http://ip.taobao.com/service/getIpInfo.php?ip=" + strIP;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();                     //Xml文档
            string dsdsd = GetHtml(strURL);                                        //加载strURL指定XML数据

            System.Xml.XmlNodeList nodeLstCity = doc.GetElementsByTagName("City"); //获取标签
            Location = "获取单个物理位置:" + nodeLstCity[0].InnerText + "";
            Console.WriteLine(Location);
            //通过SelectSingleNode匹配匹配第一个节点
            System.Xml.XmlNode root = doc.SelectSingleNode("Response");
            if (root != null)
            {
                string CountryName = (root.SelectSingleNode("CountryName")).InnerText;
                string RegionName = (root.SelectSingleNode("RegionName")).InnerText;
                string City = (root.SelectSingleNode("City")).InnerText;
                Location = "国家名称:" + CountryName + "\n区域名称:" + RegionName + "\n城市名称:" + City;
                return Location;
            }
            return Location;
        }

        /// <summary>
        /// 根据IP 获取物理地址
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        static string GetstringIpAddress(string strIP) //strIP为IP
        {
            //string sURL = "http://ip.taobao.com/service/getIpInfo.php?ip=" + strIP + "";
            string sURL = "http://pv.sohu.com/cityjson/74.125.31.104";
            string stringIpAddress = "";                     //地理位置
            using (System.Xml.XmlReader read = System.Xml.XmlReader.Create(sURL))  //获取youdao返回的xml格式文件内容
            {
                while (read.Read())                          //从流中读取下一个字节
                {
                    switch (read.NodeType)
                    {
                        case System.Xml.XmlNodeType.Text:               //取xml格式文件当中的文本内容
                            if (string.Format("{0}", read.Value).ToString().Trim() != strIP)
                            {
                                stringIpAddress = string.Format("{0}", read.Value).ToString().Trim();
                            }
                            break;
                    }
                }
            }
            return stringIpAddress;
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

    public class domo
    {
        public int d = 0;
        public int a = 0;
    }

    public class Base
    {
        public void Test()
        {
            Tuck();
        }

        public virtual void Tuck()
        {

        }
    }

    public class RedPck:Base
    {
        public override void Tuck()
        {
            base.Tuck();
            Console.WriteLine(111);
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
