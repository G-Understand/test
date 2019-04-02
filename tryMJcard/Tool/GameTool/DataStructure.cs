using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    class DataStructure
    {
    }

    /// <summary>
    ///    {"a",10}, 黑 b 红  c 梅 f 方     
    /// </summary>
    public class PDKTypeData
    {
        public int v { get; set; }

        public string h { get; set; }

        public PDKTypeData()
        {

        }

        public PDKTypeData(int _value, string _huase)
        {
            v = _value;
            h = _huase;
        }
        public PDKTypeData Clone()
        {
            return new PDKTypeData(v, h);
        }
    }

    public class PushCardData
    {
        /// <summary>
        /// 牌组类型
        /// </summary>
        public List<int> data { get; set; }

        /// <summary>
        /// 牌数据
        /// </summary>
        public List<PDKTypeData> cardData = new List<PDKTypeData>();

        /// <summary>
        /// 牌型
        /// </summary>
        public CardPools.PokerGroupType type = CardPools.PokerGroupType.单张;

        /// <summary>
        /// 牌个数
        /// </summary>
        public CardPools.PokerGroupIndex index = CardPools.PokerGroupIndex.单张;

        public PushCardData()
        {

        }
        public PushCardData(List<PDKTypeData> _cardData)
        {
            cardData = new List<PDKTypeData>(_cardData);
            data = new List<int>(_cardData.Select(d => d.v).ToList());
        }
    }

    public class CardPools
    {
        /// <summary>
        /// 牌型
        /// </summary>
        public enum PokerGroupType
        {
            单张 = 1,
            对子 = 2,
            双王 = 3,
            三张相同 = 4,
            三带一 = 5,
            二连对 = 35,
            三带二 = 33,
            炸弹 = 6,
            五张顺子 = 7,
            六张顺子 = 8,
            三连对 = 9,
            四带二 = 10,
            二连飞机 = 11,
            七张顺子 = 12,
            四连对 = 13,
            八张顺子 = 14,
            四带二对 = 36,
            飞机带翅膀 = 15,
            飞机带两对 = 37,
            飞机带三对 = 38,
            九张顺子 = 16,
            三连飞机 = 17,
            五连对 = 18,
            十张顺子 = 19,
            十一张顺子 = 20,
            十二张顺子 = 21,
            四连飞机 = 22,
            三连飞机带翅膀 = 23,
            六连对 = 24,
            七连对 = 25,
            五连飞机 = 26,
            八连对 = 27,
            四连飞机带翅膀 = 28,
            九连对 = 29,
            六连飞机 = 30,
            十连对 = 31,
            五连飞机带翅膀 = 32

        }

        /// <summary>
        /// 牌型的个数
        /// </summary>
        public enum PokerGroupIndex : int
        {
            单张 = 1,
            对子 = 2,
            双王 = 2,
            三张相同 = 3,
            三带一 = 4,
            炸弹 = 4,
            五张顺子 = 5,
            六张顺子 = 6,
            三连对 = 6,
            四带二 = 6,
            二连飞机 = 6,
            七张顺子 = 7,
            四连对 = 8,
            八张顺子 = 8,
            飞机带翅膀 = 8,
            四带二对 = 8,
            九张顺子 = 9,
            三连飞机 = 9,
            五连对 = 10,
            十张顺子 = 10,
            十一张顺子 = 11,
            十二张顺子 = 12,
            四连飞机 = 12,
            三连飞机带翅膀 = 12,
            六连对 = 12,
            七连对 = 14,
            五连飞机 = 15,
            八连对 = 16,
            四连飞机带翅膀 = 16,
            九连对 = 18,
            六连飞机 = 18,
            十连对 = 20,
            五连飞机带翅膀 = 20


        }
    }

    /// <summary>
    /// 牌型枚举
    /// </summary>
    public enum ShiSanShuiCardEnum : int
    {
        None = 0,

        乌龙 = 1,
        对子 = 2,
        两对 = 3,
        三条 = 4,
        顺子 = 5,
        同花 = 6,
        葫芦 = 7,
        铁支 = 8,
        同花顺 = 9,
        五同 = 10,
        六同 = 11,

        三同花 = 51,
        三顺子 = 52,
        六对半 = 53,
        五对三条 = 54,
        四套三条 = 55,
        凑一色 = 56,
        全小 = 57,
        全大 = 58,
        三分天下 = 59,
        三同花顺 = 60,
        十二皇族 = 61,
        一条龙 = 63,
        至尊清龙 = 64,
        全黑一点红 = 65,
        全红一点黑 = 66,
        全黑 = 67,
        全红 = 68,
    }
}
