using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));//为新的Datatable添加一个新的列名
            dt.Columns.Add("Name", typeof(string));//为新的Datatable添加一个新的列名
            dt.Columns.Add("sex", typeof(int));//为新的Datatable添加一个新的列名
            dt.Columns.Add("phone", typeof(string));//为新的Datatable添加一个新的列名
            for (int i = 0; i < 100000; i++) //开始循环赋值
            {
                DataRow row = dt.NewRow(); //创建一个行
                row["ID"] = i + 1; //从总的Datatable中读取行数据赋值给新的Datatable
                row["Name"] = "sxd" + (i + 1).ToString();
                row["sex"] = i % 2 == 0 ? 1 : 0;
                row["phone"] = (13500000000 + i + 1).ToString();
                dt.Rows.Add(row);//添加次行
            }
            return dt;
        }

        /// <summary>
        /// 基础数据执行
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static bool MySqlBulkCopy(DataTable Table)
        {
            bool Bool = true;
            string ConnectionString = "server=localhost;database=test;uid=sa;pwd=123456";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into User_1 (ID,Name,sex,phone) values (" + Table.Rows[i][0] + ",'" + Table.Rows[i][1] + "'," + Table.Rows[i][2] + ",'" + Table.Rows[i][3] + "')";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return Bool;
        }

        /// <summary>
        /// 计时
        /// </summary>
        /// <returns></returns>
        public static string GetTimeSpan()
        {
            DataTable dt = GetData();
            DateTime dt1 = DateTime.Now;
            Console.WriteLine(dt1.ToString("yyyy-MM-dd HH:mm:ss"));
            MySqlBulkCopy(dt);
            DateTime dt2 = DateTime.Now;
            Console.WriteLine(dt2.ToString("yyyy-MM-dd HH:mm:ss"));
            TimeSpan span = dt2 - dt1;
            string a = span.TotalSeconds.ToString();
            Console.WriteLine(a);
            return a;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static SqlBulkCopyColumnMapping[] GetMapping()
        {
            SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[4];
            mapping[0] = new SqlBulkCopyColumnMapping("ID", "ID");
            mapping[1] = new SqlBulkCopyColumnMapping("Name", "Name");
            mapping[2] = new SqlBulkCopyColumnMapping("sex", "sex");
            mapping[3] = new SqlBulkCopyColumnMapping("phone", "phone");
            return mapping;
        }

        /// <summary>
        /// DataTable批量添加(有事务)
        /// </summary>
        /// <param name="Table">数据源</param>
        /// <param name="DestinationTableName">目标表即需要插入数据的数据表名称如"User_1"</param>
        public static bool MySqlBulkCopy(DataTable Table, string DestinationTableName)
        {
            bool Bool = true;
            string ConnectionString = "server=localhost;database=test;uid=sa;pwd=123456";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction Tran = con.BeginTransaction())//应用事物
                {
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con, SqlBulkCopyOptions.KeepIdentity, Tran))
                    {
                        Copy.DestinationTableName = DestinationTableName;//指定目标表
                        SqlBulkCopyColumnMapping[] Mapping = GetMapping();//获取映射关系
                        if (Mapping != null)
                        {
                            //如果有数据
                            foreach (SqlBulkCopyColumnMapping Map in Mapping)
                            {
                                Copy.ColumnMappings.Add(Map);
                            }
                        }
                        try
                        {
                            Copy.WriteToServer(Table);//批量添加
                            Tran.Commit();//提交事务
                        }
                        catch
                        {
                            Tran.Rollback();//回滚事务
                            Bool = false;
                        }
                    }
                }
            }
            return Bool;
        }
    }
}
