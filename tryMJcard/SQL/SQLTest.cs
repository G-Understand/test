using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.SQL
{
    /// <summary>
    /// 数据库测试
    /// </summary>
    class SQLTest
    {
        #region Member
        /// <summary>
        /// 用户头像储存
        /// </summary>
        public static List<string> imageList = new List<string>();
        #endregion

        /// <summary>
        /// ADO.Net 测试
        /// </summary>
        public static void EntityTest()
        {
            Logger.Log("ADO.Net test is beginning......");
            using (var sql = new tryMJcard.SQL.dafuwongEntities())
            {
                var list = (from infor in sql.tb_User
                            where infor.jinbi > 30000
                            select new
                            {
                                name = infor.username,
                                wxid = infor.wxId,
                                uid = infor.uid,
                                image = infor.headimgurl,
                                money = infor.jinbi
                            })
                    .ToList();
                var userInfor = (from c in sql.tb_User
                                 where c.uid == "111"
                                 select c).FirstOrDefault();
                //                 if (obj != null)
                //                 {
                //                     ////////修改操作
                //                     obj.summary += "……";
                //                     ctx.SaveChanges();
                //                     ///////删除操作
                //                     ctx.tb_user_jinbi.Remove(obj);
                //                     ctx.SaveChanges();
                //                     //增
                //                     tb_user_jinbi AddObj = new tb_user_jinbi() { uid = "123", addnum = 0, summary = "1321312321312321", type = 1 };
                // 
                //                     ctx.tb_user_jinbi.Add(AddObj);
                //                     ctx.SaveChanges();
                //                 }
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i].name);
                }
                int a = 0;
            }
            Logger.Log("ADO.Net test is end......");
        }

        /// <summary>
        /// sql 基础链接测试 
        /// </summary>
        public static void SqlTest()
        {
            Sign_in.Instance.ToAction(123456);
        }

        /// <summary>
        /// 数据库测试
        /// </summary>
        private static void SqlServerTest()
        {
            var sqlGetImage = string.Format("select headimgurl from tb_User ");//select orderdetailid,openid,zongfen from tb_game_detail where roomid=756076
            var datImage = SqlHelp.Instance.SelectBySql(sqlGetImage);
            for (int i = 0; i < datImage.Rows.Count; i++)
            {
                string image = datImage.Rows[i]["headimgurl"].ToString();
                if (!imageList.Contains(image))
                {
                    imageList.Add(image);
                    TextFile.Instance.Wirte(image);
                }
            }
            var sqldayjiesuan = string.Format("   select * from tb_gameRecords where roomId=750368 and round=1  ");//tb_User   tb_user_jiesuan   select * from tb_game_SignIn

            //Console.WriteLine(SqlHelp.Instance.SelectBySql(sqldayjiesuan).Rows.Count);
            var dat = SqlHelp.Instance.SelectBySql(sqldayjiesuan);
            var sqlGetFK = string.Format("select fk from tb_User where uid='{0}'", "guiziyang");//select orderdetailid,openid,zongfen from tb_game_detail where roomid=756076
            var dat11 = SqlHelp.Instance.SelectBySql(sqlGetFK);
            var sqlGetFK6 = string.Format(" select *,username=(select top 1 username from tb_User where uid=x.openid) from ( select distinct orderdetailid,openid,zongfen from tb_game_detail where roomid= {0}) x order by orderdetailid", 756076);//select orderdetailid,openid,zongfen from tb_game_detail where roomid=756076
            var dat116 = SqlHelp.Instance.SelectBySql(sqlGetFK6);
            var sqlGetFK2 = string.Format("select orderdetailid,openid,zongfen from tb_game_detail where roomid={0} order by orderdetailid", 756076);
            var dat112 = SqlHelp.Instance.SelectBySql(sqlGetFK2);

            string gameRecords = string.Empty;
            int fk = 0;
            for (int b = 0; b < dat11.Rows.Count; b++)
            {
                fk = dat11.Rows[b]["fk"].ToString().getInt();
            }

            for (int b = 0; b < dat.Rows.Count; b++)
            {
                gameRecords = dat.Rows[b]["gameRecords"].ToString();
            }
            GameRecord gameRecord = Json.ParseJson<GameRecord>(gameRecords);

        }
    }
}
