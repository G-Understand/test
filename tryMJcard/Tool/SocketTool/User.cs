using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using tryMJcard.SocketToolHandler;

namespace tryMJcard
{
    class User:SendRespose
    {
        public static User getAppModel(string app, Socket socket)
        {
//             if (userList.ContainsKey(app))
//             {
//                 Loggers.Log(typeof(User), "用户已在缓存列表");
//                 userList[app].socket = socket;
//                 return userList[app];
//             }
            User user = new User();
            user.socket = socket;
//             //user.uid = app;
//             //userList.Add(app, am);
//             LogManager .Instance.(typeof(User), "新用户登录");
            return user;
        }
    }
}
