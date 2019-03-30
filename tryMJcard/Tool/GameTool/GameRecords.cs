using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    public class GetGameRecords:Singleton<GetGameRecords>
    {
        public string GetGameRecordOnSQL(int _roomId,int _round)
        {
            var sqlgameRecords = string.Format("   select * from tb_gameRecords where roomId={0} and round={1}  ", _roomId, _round);
            var dat = SqlHelp.Instance.SelectBySql(sqlgameRecords);
            string gameRecords = string.Empty;
            for (int b = 0; b < dat.Rows.Count; b++)
            {
                gameRecords = dat.Rows[b]["gameRecords"].ToString();
            }
            if (gameRecords ==null)
            {
                gameRecords = string.Empty;
            }
            return gameRecords;
        }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    enum ActionType
    {
        /// <summary>
        /// 手牌操作
        /// </summary>
        SetHandCards = 1,

        /// <summary>
        /// 碰区操作
        /// </summary>
        SetPengCards = 3,

        /// <summary>
        /// 打出牌操作
        /// </summary>
        SetDaChuCards = 2,

        /// <summary>
        /// 杠牌区操作
        /// </summary>
        SetGangCards = 4,

        /// <summary>
        /// 自摸胡操作
        /// </summary>
        SetZiMoHu = 5,

        /// <summary>
        /// 点炮胡操作
        /// </summary>
        SetDianPaoHu = 6,
    }

    /// <summary>
    /// 大局记录
    /// </summary>
    public class GameRecords
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int roomId { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public int gameType { get; set; }

        /// <summary>
        /// 游戏规则
        /// </summary>
        public string guize { get; set; }

        /// <summary>
        /// 记录游戏中各小局记录
        /// </summary>
        public List<GameRecord> gameRecordList = new List<GameRecord>();

        public GameRecords()
        {

        }

        public GameRecords(string _guize, int _gameType, int _roomId)
        {
            roomId = _roomId;
            guize = _guize;
            gameType = _gameType;
        }

        /// <summary>
        /// 添加步骤  round 当前局数 type操作类型:1、手中的牌 2、打出的牌 3、碰杠的牌
        /// </summary>
        /// <param name="_round"></param>
        /// <param name="_type"></param>
        /// <param name="_idx"></param>
        /// <param name="_paiList"></param>
        public void AddRecord(int _round, int _type, int _idx, List<int> _paiList)
        {
            if (gameRecordList.Count(d => d.round == _round) > 0)
            {
                gameRecordList.Find(d => d.round == _round).AddRecord(_type, _idx, _paiList);
            }
        }
    }

    /// <summary>
    /// 小局记录
    /// </summary>
    public class GameRecord
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int roomId { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public int gameType { get; set; }

        /// <summary>
        /// 游戏规则
        /// </summary>
        public string guize { get; set; }

        /// <summary>
        /// 局数
        /// </summary>
        public int round { get; set; }

        /// <summary>
        /// 庄家的idx
        /// </summary>
        public int makersIdx { get; set; }

        /// <summary>
        /// 混子数组
        /// </summary>
        public List<int> hunziList = new List<int>();

        /// <summary>
        /// 游戏的用户记录
        /// </summary>
        public List<UserRecordData> userRecordList = new List<UserRecordData>();

        /// <summary>
        /// 游戏过程记录
        /// </summary>
        public List<Record> recordList = new List<Record>();

        public GameRecord()
        {

        }

        /// <summary>
        /// 添加当前小局中的操作步骤  type操作类型:1、手中的牌 2、打出的牌 3、碰杠的牌
        /// </summary>
        /// <param name="type"></param>
        /// <param name="idx"></param>
        /// <param name="paiList"></param>
        public void AddRecord(int type, int idx, List<int> paiList)
        {
            recordList.Add(new Record(type, idx, paiList));
        }
    }

    /// <summary>
    /// 记录
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 操作类型:1、手中的牌 2、打出的牌 3、碰的牌 4、杠的牌 5、自摸胡 6、点炮胡 
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 那个玩家的操作
        /// </summary>
        public int userIdx { get; set; }

        /// <summary>
        /// 操作对应的牌
        /// </summary>
        public List<int> paiList { get; set; }

        public Record ()
        {

        }
        public Record(int _type, int _userIdx, List<int> _paiList)
        {
            type = _type;
            userIdx = _userIdx;
            paiList = _paiList;
        }
    }

    /// <summary>
    /// 用户记录
    /// </summary>
    public class UserRecordData
    {
        /// <summary>
        /// 玩家头像
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 玩家下跑
        /// </summary>
        public int mashu { get; set; }

        /// <summary>
        /// 玩家分数
        /// </summary>
        public int socer { get; set; }

        /// <summary>
        /// 玩家位置
        /// </summary>
        public int idx { get; set; }

        public UserRecordData(string _image, int _idx, int _mashu, int _socer)
        {
            image = _image;
            idx = _idx;
            mashu = _mashu;
            socer = _socer;
        }
        public UserRecordData ()
        {

        }
    }
}
