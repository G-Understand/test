using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class Sign_in
    {
        private static Sign_in _Instance;
        public static Sign_in Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Sign_in();
                }
                return _Instance;
            }
        }
        private Sign_in()
        {

        }

        public void ToAction(int num)
        {
            try
            {
                if (Find_SignInRecord(num))
                {
                    string reward = SignInReward();
                    Add_SignInRecord(num, reward);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("用户签到");
            }
        }

        /// <summary>
        /// 获取签到时今天应该获取的奖励
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string SignInReward()
        {
            string week = "";
            var sqldayjiesuan = string.Format("  select * from tb_game_SignIn ");
            string day = DateTime.Now.DayOfWeek.ToString();
            var dat = SqlHelp.Instance.SelectBySql(sqldayjiesuan);
            for (int b = 0; b < dat.Rows.Count; b++)
            {
                week = dat.Rows[b][day].ToString();
            }
            return week;
        }

        /// <summary>
        /// 查询用户签到记录
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Find_SignInRecord(int num)
        {
            bool isSignIn = true;
            string asdof = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var sqldayjiesuan = string.Format("  select * from tb_game_SignInRecord where wxid={0} and signInTime='{1}'",num ,DateTime .Now .Date.ToString("yyyy-MM-dd"));
            var dat = SqlHelp.Instance.SelectBySql(sqldayjiesuan);
            if (dat.Rows .Count >0)
            {
                Console.WriteLine("已经存在");
                isSignIn = false;
            }
            return isSignIn;
        }

        /// <summary>
        /// 添加用户签到记录
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public void Add_SignInRecord(int num , string Reward)
        {
            var sqldayjiesuan = string.Format("insert into tb_game_SignInRecord(wxid,signInTime,reward) values({0},'{1}','{2}') ", num, DateTime.Now.Date, Reward);
            var dat = SqlHelp.Instance.SelectBySql(sqldayjiesuan);
        }
    }
}
