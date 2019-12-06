using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{ 
    /////////////////////////////////////////////////////////
    //此处为约束，和部分公有属性定义
    /////////////////////////////////////////////////////////
    //RoomBase<T> : IDisposable  where T : RoomBase<T>, new()
    public abstract class RoomBase
    {
       
        #region Member
        /// <summary>
        /// 房间号
        /// </summary>
        public int roomId { get; set; }
        /// <summary>
        /// 俱乐部号
        /// </summary>
        public int? clubId { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        public long roomnumer { set; get; }
        /// <summary>
        /// 数据库房间操作中表示的roomId
        /// </summary>
        public int rowId { get; set; }
        /// <summary>
        /// 房间属于哪个平台下
        /// </summary>
        public string roomPID { get; set; }
        /// <summary>
        /// 游戏是否开始
        /// </summary>
        public bool isGameBegin { set; get; }
        /// <summary>
        /// 是否为好友房
        /// </summary>
        public bool isFriendRoom { set; get; }
        /// <summary>
        /// 局数设置
        /// </summary>
        public int round { set; get; }
        /// <summary>
        /// 房间总局数
        /// </summary>
        public int allRound { set; get; }
        /// <summary>
        /// 房费
        /// </summary>
        public int rate { set; get; }
        /// <summary>
        /// 开始本局游戏
        /// </summary>
        public bool gameStart { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime _createTime = DateTime.Now;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime _startTime = DateTime.Now;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime _endTime = DateTime.Now;
        #endregion

        public void Push<T>(T a) where T : new()
        {
            Push(123);
            //int sss = GetUserByGameUserId(123);
        }

        public abstract T GetUserByGameUserId<T>(int gameUserId) where T : new();

        public void Dispose()     //处理
        {
        }
    }

    class UserActor
    {

    }
}
