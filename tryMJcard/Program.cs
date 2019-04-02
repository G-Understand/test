using System;
using System.Timers;

namespace tryMJcard
{
   
    class Program
    {
        #region Member

        /// <summary>
        /// 碰杠托管管理者
        /// </summary>
        TimerManage timeronPengGangHu = new TimerManage();
        #endregion

        static void Start()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Console.Title = DateTime.Now.ToString("yyyy-MM-dd HH:mmLss") + "===>" + " 该程序为工具和一些基类 ";
            SqlHelp.Instance.Init("Server=.;Database=xw;Uid=sa;Pwd=123456;");//and setdate>dateadd(day,-3,getdate())
        }

        static void Main(string[] args)
        {
            Start();
            Test.MaJong();
            LogManagerSimple.WriteLog_Gui("我真棒------------------------------------------------------------------------------------", "" + "gui");
            Test.WriteLineTest();
            Program program = new Program();
            if (program.timeronPengGangHu != null && program.timeronPengGangHu.Enabled)
            {
                program.timeronPengGangHu.Stop();
            }
            object obj = 1;
            program.timeronPengGangHu=new TimerManage ();
            program.timeronPengGangHu.parameter = obj;
            program.timeronPengGangHu.Interval = 10000;
            program.timeronPengGangHu.Elapsed += new ElapsedEventHandler(program.TimerHanlderOnPengGangHu);
            program.timeronPengGangHu.AutoReset = false;
            program.timeronPengGangHu.Start();
            Console.ReadLine();
        }

        #region 定时器
        System.Timers.Timer timer = new System.Timers.Timer();

        /// <summary>
        /// 111
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeFailMsg(object sender, System.Timers.ElapsedEventArgs e)
        {
            //这里处理你定时重发的事件
        }
        private void TimerThisM()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(InvokeFailMsg);
            timer.Enabled = true;//是否触发Elapsed事件
            timer.AutoReset = false; //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true）(默认为true)
            //timer.Interval = 5000;// 设置时间间隔为5秒
        }
        #endregion

        private void TimerHanlderOnPengGangHu(object sender, ElapsedEventArgs e)
        {

            TuoGuanPengGangHu(((TimerManage)sender).parameter);
        }

        /// <summary>
        /// 获取倍数（番数）
        /// </summary>
        /// <param name="_max"></param>
        /// <param name="_beishu"></param>
        /// <returns></returns>
        public static int GetFan(int _max, int _beishu)
        {
            int num = 1;
            return num = (int)(_beishu < Math.Pow(2, _max) ? _beishu : Math.Pow(2, _max));
        }

        /// <summary>
        /// 托管碰杠
        /// </summary>
        /// <param name="sa"></param>
        public static void TuoGuanPengGangHu(object sa)
        {

        }
    }
}
