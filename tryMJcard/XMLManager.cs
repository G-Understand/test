using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tryMJcard
{
    class XMLManager
    {
        public static XMLManager _Instance;
        public static XMLManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new XMLManager();
                }
                return _Instance;
            }
        }
        public string App_Path = string.Empty;

        public string XMLPath = string.Empty;

        public string LuaPath = string.Empty;

        public string ip = "";

        public int port = 0;

        public int MaxConnec = 0;

        /// <summary>
        /// 是否调试模式!
        /// </summary>
        public static bool Debug = true;
        private XMLManager()
        {
            
        }


        /// <summary>
        /// 缓存数据
        /// </summary>
        private static Dictionary<string, XmlDocument> xmlData = new Dictionary<string, XmlDocument>();

        /// <summary>
        /// 获取XML数据,返回一个XML文档
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XmlDocument getXmlData(string path)
        {
            
            if (File.Exists(path) == false)
            {
                return null;
            }
            if (xmlData.ContainsKey(path))
            {
                return xmlData[path];
            }
            string data = File.ReadAllText(path);
            var xml = new XmlDocument();
            xml.LoadXml(data);
            xmlData.Add(path, xml);
            return xml;
        }
        private static void InitListener(string XMLPath)
        {
            Console.WriteLine("The Sys.Xml Ready To Init Please Wait!");
            var data = Instance.getXmlData(XMLPath + "Sys.xml");
            var childs = data.GetElementsByTagName("sys");
            foreach (var m in childs)
            {
                XmlElement x = m as XmlElement;
                var val = x.GetAttribute("value");

                if (val.Equals("Ipv6"))
                {
                    //RoomConfig.ipv6 = x.GetAttribute("id");
                }
                else if (val.Equals("ip"))
                {
                    Instance.ip = x.GetAttribute("ip");
                    Instance.port = x.GetAttribute("port").getInt();
                }
                else if (val.Equals("sql"))
                {
                    SqlHelp.Instance.Init(x.GetAttribute("sql"));
                }
                else if (val.Equals("maxCount"))
                {
                    Instance.MaxConnec = x.GetAttribute("maxCount").getInt();
                }
                else if (val.Equals("lua"))
                {
                    Instance.LuaPath = Instance.App_Path + x.GetAttribute("lua");
                }
                else if (val.Equals("gametimer"))
                {
                    //RoomConfig.gametimer = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("gametimer1"))
                {
                    //RoomConfig.gametimerwait = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("teamlevel"))
                {
                    //RoomConfig.teamlevel = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("userlevel"))
                {
                   // RoomConfig.userlevel = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("zhandiandefen"))
                {
                    //RoomConfig.zhandiandefen = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("totaldefen"))
                {
                    //RoomConfig.totaldefen = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("gameCount"))
                {
                    //RoomConfig.gameCount = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("pipeicount"))
                {
                    //RoomConfig.pipeicount = x.GetAttribute("id").getInt();
                }
                else if (val.Equals("GMCount"))
                {
                    //RoomConfig.GMCount = x.GetAttribute("id").getInt();
                }
            }
            Console.WriteLine("The Sys.Xml Init Success!");
        }
    }
}
