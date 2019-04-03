using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace tryMJcard.Tool.SocketTool
{
  public class SerializerUtils
    {
        #region 序列化
        /// <summary>
        ///将object转换为string对象，这种比较简单没有什么可谈的； 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ScriptSerialize<T>(T t)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(t);
        }
        /// <summary>
        /// 将object转换为xml对象：
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>

        #endregion

        #region 反序列化        
        /// <summary>
        /// 将string对象反序列化为object对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ScriptDeserialize<T>(string strJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(strJson);
        }

        /// <summary>
        /// 将string对象反序列化为list对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string strJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<T> objList = serializer.Deserialize<List<T>>(strJson);
            return objList;
        }



        #endregion

        public static string JsonToEntityJS<T>(List<T> entity)
        {
            JavaScriptSerializer serizlizer = new JavaScriptSerializer();
            string jsonstr = serizlizer.Serialize(entity);
            return jsonstr;
        }
    }
}
