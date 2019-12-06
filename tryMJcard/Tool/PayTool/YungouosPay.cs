using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace tryMJcard.Tool.PayTool
{
    /// <summary>
    /// Yungouos的收银台支付
    /// </summary>
    class YungouosPay
    {
        /// <summary>
        /// 支付地址
        /// </summary>
        static string payUrl = @"https://api.pay.yungouos.com/api/pay/wxpay/cashierPay";
        /// <summary>
        /// 订单号（不可重复）
        /// </summary>
        static string out_trade_no = string.Empty;
        /// <summary>
        /// 支付金额（范围：0.01~999999）
        /// </summary>
        static string total_fee = string.Empty;
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        static string mch_id = string.Empty;
        /// <summary>
        /// 商品简单描述
        /// </summary>
        static string body = string.Empty;
        /// <summary>
        /// 附加数据，回调时候原路返回
        /// </summary>
        static string attach = string.Empty;
        /// <summary>
        /// 异步回调地址，用户支付成功后系统将会把支付结果发送到该地址，不填则无回调
        /// </summary>
        static string notify_url = string.Empty;
        /// <summary>
        /// 同步地址，支付完毕后用户浏览器返回到该地址，如果不传递，页面支付完成后页面自动关闭，强烈建议传递。url不可包含？号、不可携带参数
        /// </summary>
        static string return_url = string.Empty;
        /// <summary>
        /// 签名（见签名算法文档）
        /// </summary>
        static string sign = string.Empty;

        /// <summary>
        /// 将string对象反序列化为object对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ScriptDeserialize<T>(string strJson)
        {
            //return JsonConvert.DeserializeObject<T>(strJson);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(strJson);
        }

        public static void Handler()
        {
            total_fee = "0.01";
            Random rd = new Random();
            out_trade_no = DateTime.Now.ToString("yyMMddHHmmss") + rd.Next(10000000, 99999999).ToString();
            body = "测试0.01";
            mch_id = "1544186751";
            notify_url = "http://47.104.237.54:9414/Payment/?reqtype=yungouos";
            sign = "44E57A58245C462C9408458FB74E7A4B";
            SortedDictionary<string, string> pay_dic = new SortedDictionary<string, string>();
            pay_dic.Add("total_fee", total_fee);
            pay_dic.Add("out_trade_no", out_trade_no);
            pay_dic.Add("body", body);
            pay_dic.Add("mch_id", mch_id);
            string get_PaySign = BuildRequest(pay_dic, sign);
            string reqStr = "body=" + body + "&total_fee=" + total_fee + "&out_trade_no=" + out_trade_no + "&mch_id=" + mch_id + "&sign=" + get_PaySign + "&notify_url=" + notify_url;
            string result = WebServiceApp(payUrl, string.Empty, reqStr);
            //{"data":"https://cashier.yungouos.com/#/wxpay/cashier?key=C1731F6416A74BD5B8104F62DC58CD1A","msg":"收银台支付下单成功，请重定向到收银台完成支付","code":0}
            Dictionary<string, object> dic = ScriptDeserialize<Dictionary<string, object>>(result);
            int code = (int)dic["code"];
            if (code == 0)
            {
                string retUrl = dic["data"].ToString();
                //返回前端
            }
        }

        /// <summary>
        /// 向某地址发送 post 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">方法</param>
        /// <param name="param">post参数 例如 a=1&b=2</param>
        /// <returns></returns>
        public static string WebServiceApp(string url, string method, string param)
        {
            //转换输入参数的编码类型，获取byte[]数组 
            byte[] byteArray = Encoding.UTF8.GetBytes(param);
            //初始化新的webRequst
            //1． 创建httpWebRequest对象
            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + "/" + method));
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            //2． 初始化HttpWebRequest对象
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)
            Stream newStream = webRequest.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            //4． 读取服务器的返回信息
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string phpend = php.ReadToEnd();
            return phpend;
        }

        #region 生成签名
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="sParaTemp">值</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string key)
        {
            //获取过滤后的数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = FilterPara(sParaTemp);

            //组合参数数组
            string prestr = CreateLinkString(dicPara);
            //拼接支付密钥
            string stringSignTemp = prestr + "&key=" + key;

            //获得加密结果
            string myMd5Str = GetMD5(stringSignTemp);

            //返回转换为大写的加密串
            return myMd5Str.ToUpper();
            //return myMd5Str;
        }
        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key != "sign" && !string.IsNullOrEmpty(temp.Value))
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 组合参数数组
        /// </summary>
        /// <param name="dicArray"></param>
        /// <returns></returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetMD5(string pwd)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(pwd));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion
    }
}
