using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace tryMJcard
{
    class SqlHelp
    {
        private static SqlHelp _Instance;
        public static SqlHelp Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SqlHelp();
                }
                return _Instance;
            }
        }
        
        private SqlHelp()
        {

        }

        /// <summary>
        /// 初始化SQL连接地址
        /// </summary>
        /// <param name="connectionstring"></param>
        public  void Init(string connectionstring)
        {
            SqlInfo._instence.SqlConnString = connectionstring;
        }

        /// <summary>
        /// 选择出SQL内容，str为SQL语句
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataTable SelectBySql(string str)
        {
            DataTable dt = new DataTable();
            SqlConnection myconn = SqlInfo._instence.GetConnection();
            myconn.Open();
            try
            {
                SqlDataAdapter odr = new SqlDataAdapter(str, myconn);
                DataSet ds = new DataSet();
                odr.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //throw (ex);
            }
            finally
            {
                myconn.Dispose();
                myconn.Close();
            }
            return dt;
        }
    }
    class SqlInfo
    {
        public static SqlInfo sqlinfo;
        public static SqlInfo _instence
        {
            get
            {
                if (sqlinfo == null)
                {
                    sqlinfo = new SqlInfo();
                }
                return sqlinfo;
            }
        }

        private SqlInfo()
        {

        }

        /// <summary>
        /// SQL连接字符串，例如："Server=10.0.0.8;Database=xw;Uid=sa;Pwd=123456;"
        /// </summary>
        public string SqlConnString;

        /// <summary>
        /// 建立SQL连接
        /// </summary>
        /// <returns></returns>
        public  SqlConnection GetConnection()
        {

            var conn = new SqlConnection(SqlConnString);

            return conn;

        }
      
    }
    public enum SqlReturnType
    {
        Int,
        String,
        Bool,
        Float,
        DataTime,
    }
}
