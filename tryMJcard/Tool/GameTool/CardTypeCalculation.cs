using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    /// <summary>
    /// @deception CardTypeCalculation
    /// @author Gui
    /// @date 2018/5/25 10:14:17
    /// </summary>
    class CardTypeCalculation
    {
        public class RepeatInfo
        {
            // 值
            public int Value { get; set; }
            // 重复次数
            public int RepeatNum { get; set; }
        }

        /// <summary>
        /// 获取特殊牌型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyData"></param>
        /// <returns></returns>
        public static ShiSanShuiCardEnum GetSpecialCardType_NingHai(List<PDKTypeData> data, List<PDKTypeData> joyData)
        {
            ShiSanShuiCardEnum result = ShiSanShuiCardEnum.None;
            if (data == null || joyData == null)
            {
                return result;
            }
            PushCardData pushCardData = new PushCardData(data);
            if (CardTypeCalculation.IsZhiZhunQingLong(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.至尊清龙;
            }
            else if (CardTypeCalculation.IsYiTiaoLong(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.一条龙;
            }
            else if (CardTypeCalculation.IsSanTongHuaShun(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三同花顺;
            }
            else if (CardTypeCalculation.IsSanFenTianXia(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三分天下;
            }
            else if (CardTypeCalculation.IsSiTaoSanTiao(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.四套三条;
            }
            else if (CardTypeCalculation.IsLiuDuiBan(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.六对半;
            }
            else if (CardTypeCalculation.IsSanShunZi(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三顺子;
            }
            else if (CardTypeCalculation.IsSanTongHua(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三同花;
            }
            return result;
        }

        /// <summary>
        /// 获取特殊牌型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyData"></param>
        /// <returns></returns>
        public static ShiSanShuiCardEnum GetSpecialCardType_SanMen(List<PDKTypeData> data, List<PDKTypeData> joyData)
        {
            ShiSanShuiCardEnum result = ShiSanShuiCardEnum.None;
            if (data == null || joyData == null)
            {
                return result;
            }
            PushCardData pushCardData = new PushCardData(data);
            if (CardTypeCalculation.IsYiTiaoLong(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.一条龙;
            }
            else if (CardTypeCalculation.IsAllBig(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全大;
            }
            else if (CardTypeCalculation.IsAllSmall(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全小;
            }
            else if (CardTypeCalculation.IsAllBlackAndRad(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全黑一点红;
            }
            else if (CardTypeCalculation.IsAllRedAndBlack(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全红一点黑;
            }
            else if (CardTypeCalculation.IsAllBlack(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全黑;
            }
            else if (CardTypeCalculation.IsAllRed(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.全红;
            }
            else if (CardTypeCalculation.IsLiuDuiBan(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.六对半;
            }
            else if (CardTypeCalculation.IsSanShunZi(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三顺子;
            }
            else if (CardTypeCalculation.IsSanTongHua(pushCardData, joyData))
            {
                result = ShiSanShuiCardEnum.三同花;
            }
            return result;
        }


        /// <summary>
        /// 查找单张
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repet"></param>
        /// <returns></returns>
        public static List<RepeatInfo> getRepareDataByNumOneNum(PushCardData data)
        {

            List<int> list = data.data;

            // result集合存放扫描结果
            Dictionary<int, RepeatInfo> result =
                    new Dictionary<int, RepeatInfo>();

            // 遍历整型列表集合，查找其中的重复项
            foreach (int v in list)
            {
                if (result.ContainsKey(v))
                {
                    result[v].RepeatNum += 1;
                }
                else
                {
                    RepeatInfo item =
                        new RepeatInfo() { Value = v, RepeatNum = 1 };
                    result.Add(v, item);
                }
            }
            List<RepeatInfo> m = new List<RepeatInfo>();
            foreach (RepeatInfo info in result.Values)
            {
                if (info.RepeatNum == 1)
                {
                    var cds = new RepeatInfo();
                    cds.Value = info.Value;
                    cds.RepeatNum = info.RepeatNum;
                    m.Add(cds);
                }
            }
            return m;

        }

        /// <summary>
        /// 根据repet 数量查找 =1 找对子 ； =2 找出三个相同的；=3 找出四个相同的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repet"></param>
        /// <returns></returns>
        public static List<RepeatInfo> getRepareDataByNum(PushCardData data, int repet)
        {
            // result集合存放扫描结果
            Dictionary<int, RepeatInfo> result =
                    new Dictionary<int, RepeatInfo>();
            List<int> list = data.data;
            if (list != null && list.Count > 0)
            {


                // 遍历整型列表集合，查找其中的重复项
                foreach (int v in list)
                {
                    if (result.ContainsKey(v))
                    {
                        result[v].RepeatNum += 1;
                    }
                    else
                    {
                        RepeatInfo item =
                            new RepeatInfo() { Value = v, RepeatNum = 1 };
                        result.Add(v, item);
                    }
                }
                List<RepeatInfo> m = new List<RepeatInfo>();
                foreach (RepeatInfo info in result.Values)
                {
                    if (info.RepeatNum > repet)
                    {

                        var cds = new RepeatInfo();
                        cds.Value = info.Value;
                        cds.RepeatNum = info.RepeatNum;
                        m.Add(cds);
                    }
                }
                return m;
            }
            return new List<RepeatInfo>();
        }

        /// <summary>
        /// 数据遍历
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentlist"></param>
        /// <param name="i"></param>
        /// <param name="mtotal"></param>
        /// <returns></returns>
        private static List<int> BianLiData(List<int> data, List<int> currentlist, int i, List<List<int>> mtotal)
        {
            if (data.Count < 5 || currentlist.Count < (5 + i - 3))
            {
                return null;
            }
            if (mtotal == null)
            {
                mtotal = new List<List<int>>();
            }
            bool iscanshunzi = true;
            for (int z = 0; z < 5 + i - 3; z++)
            {
                if (z < 5 + i - 3 - 1)
                {
                    if (currentlist[z] != currentlist[z + 1] + 1 || currentlist[z] > 14)
                    {
                        iscanshunzi = false;
                    }
                }
            }
            if (iscanshunzi && i >= 3)
            {
                var listint = new List<int>();
                for (int j = 0; j <= i + 1; j++)
                {
                    listint.Add(currentlist[j]);
                }

                if (listint != null && listint.Count > 0)
                {
                    mtotal.Add(listint);//第一个五张顺子
                }
            }
            if (currentlist.Count > 1)
            {
                currentlist.RemoveRange(0, 1);
            }
            BianLiData(data, currentlist, i, mtotal);
            return null;
        }

        /// <summary>
        /// 遍历获取二联对
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentlist"></param>
        /// <param name="i"></param>
        /// <param name="mtotal"></param>
        /// <returns></returns>
        private static List<int> BianLiDataErDuiZi(List<int> data, List<int> currentlist, int i, List<List<int>> mtotal)
        {
            if (data.Count < 2 || currentlist.Count < (3 + i - 1))
            {
                return null;
            }
            if (mtotal == null)
            {
                mtotal = new List<List<int>>();
            }
            bool iscanshunzi = true;
            for (int z = 0; z < 3 + i - 1; z++)
            {
                if (z < 3 + i - 1 - 1)
                {
                    if (currentlist[z] != currentlist[z + 1] + 1 || currentlist[z] > 14)
                    {
                        iscanshunzi = false;
                    }
                }
            }
            if (iscanshunzi && i >= 0)
            {
                var listint = new List<int>();
                for (int j = 0; j <= i + 1; j++)
                {
                    listint.Add(currentlist[j]);
                }
                if (listint != null && listint.Count > 0)
                {
                    mtotal.Add(listint);//第一个2张顺子
                }
            }
            if (currentlist.Count > 1)
            {
                currentlist.RemoveRange(0, 1);
            }
            BianLiDataErDuiZi(data, currentlist, i, mtotal);
            return null;
        }

        /// <summary>
        /// 遍历获取三联对
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentlist"></param>
        /// <param name="i"></param>
        /// <param name="mtotal"></param>
        /// <returns></returns>
        private static List<int> BianLiDataSanDuiZi(List<int> data, List<int> currentlist, int i, List<List<int>> mtotal)
        {
            if (data.Count < 3 || currentlist.Count < (3 + i - 1))
            {
                return null;
            }
            if (mtotal == null)
            {
                mtotal = new List<List<int>>();
            }
            bool iscanshunzi = true;
            for (int z = 0; z < 3 + i - 1; z++)
            {
                if (z < 3 + i - 1 - 1)
                {
                    if (currentlist[z] != currentlist[z + 1] + 1 || currentlist[z] > 14)
                    {
                        iscanshunzi = false;
                    }
                }
            }
            if (iscanshunzi && i >= 1)
            {
                var listint = new List<int>();
                for (int j = 0; j <= i + 1; j++)
                {
                    listint.Add(currentlist[j]);
                }

                if (listint != null && listint.Count > 0)
                {
                    mtotal.Add(listint);//第一个3张顺子
                }
            }
            if (currentlist.Count > 1)
            {
                currentlist.RemoveRange(0, 1);
            }
            BianLiDataSanDuiZi(data, currentlist, i, mtotal);
            return null;
        }

        /// <summary>
        /// 找出来 4连飞机 用 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentlist"></param>
        /// <param name="i"></param>
        /// <param name="mtotal"></param>
        /// <returns></returns>
        private static List<int> BianLiDataSiShunZi(List<int> data, List<int> currentlist, int i, List<List<int>> mtotal)
        {
            if (data.Count < 4 || currentlist.Count < (4 + i - 1))
            {
                return null;
            }
            if (mtotal == null)
            {
                mtotal = new List<List<int>>();
            }
            bool iscanshunzi = true;
            for (int z = 0; z < 4 + i - 1; z++)
            {
                if (z < 4 + i - 1 - 1)
                {
                    if (currentlist[z] != currentlist[z + 1] + 1 || currentlist[z] > 14)
                    {
                        iscanshunzi = false;
                    }
                }
            }
            if (iscanshunzi && i >= 1)
            {
                var listint = new List<int>();
                for (int j = 0; j <= i + 1; j++)
                {
                    listint.Add(currentlist[j]);
                }
                if (listint != null && listint.Count > 0)
                {
                    mtotal.Add(listint);//第一个4张顺子
                }
            }
            if (currentlist.Count > 1)
            {
                currentlist.RemoveRange(0, 1);
            }
            BianLiDataSiShunZi(data, currentlist, i, mtotal);
            return null;
        }

        /// <summary>
        /// 找出所有对子
        /// </summary>
        /// <param name="data"></param>
        public static List<int> FindDuiZi(PushCardData data)
        {
            List<int> da = new List<int>();
            var arr = getRepareDataByNum(data, 1);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        /// <summary>
        /// 找出所有的三条
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<int> FindSanTiao(PushCardData data)
        {
            List<int> da = new List<int>();
            var arr = getRepareDataByNum(data, 2);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        /// <summary>
        /// 找出所有的炸弹
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<int> FindDuiZhaDan(PushCardData data)
        {
            List<int> da = new List<int>();
            var arr = getRepareDataByNum(data, 3);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        /// <summary>
        /// 判断王炸
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool hasWangZha(PushCardData data)
        {
            if (data.data[0] == 54 && data.data[1] == 53)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 找出所有单张
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<int> FindDanZhang(PushCardData data)
        {
            List<int> da = new List<int>();
            var arr = getRepareDataByNumOneNum(data);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        /// <summary>
        /// 找出所有顺子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindShunZi(PushCardData data)
        {
            List<List<int>> li = new List<List<int>>();

            var listdispai = data.data.Distinct().ToList().OrderByDescending(d => d).ToList();
            //查找单张
            if (listdispai.Count >= 5)
            {
                for (int i = 0; i < listdispai.Count; i++)
                {
                    if (i == listdispai.Count - 1)
                    {
                        break;
                    }
                    if (listdispai[i] < 15 && i >= 3)//i从3开始才能组合成五张顺子
                    {
                        var currentlist = new List<int>();
                        currentlist.AddRange(listdispai);
                        var listresult = (BianLiData(listdispai, currentlist, i, li));
                    }
                }
            }

            return li;
        }

        /// <summary>
        /// 找出二连对
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindErLianDui(PushCardData data)
        {
            List<List<int>> li = new List<List<int>>();
            var listduizi = FindDuiZi(data).OrderByDescending(d => d).ToList();
            if (listduizi == null || listduizi.Count < 2)
            {
                return new List<List<int>>();
            }
            for (int i = 0; i < listduizi.Count; i++)
            {
                if (i == listduizi.Count - 1)
                {
                    break;
                }
                if (listduizi[i] < 15 && i >= 0)//i从0开始才能组合成2张连队
                {
                    var currentlist = new List<int>();
                    currentlist.AddRange(listduizi);
                    var listresult = (BianLiDataErDuiZi(listduizi, currentlist, i, li));
                }
            }
            return li;
        }

        /// <summary>
        /// 找出所有的连对，所有的三连对子、四连对子、五连对子、六连对子、七连对子、八连对子、九连对子、十连对子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindSanLianDui(PushCardData data)
        {
            List<List<int>> li = new List<List<int>>();
            var listduizi = FindDuiZi(data).OrderByDescending(d => d).ToList();
            if (listduizi == null || listduizi.Count < 2)// 3连对为3
            {
                return new List<List<int>>();
            }
            for (int i = 0; i < listduizi.Count; i++)
            {
                if (i == listduizi.Count - 1)
                {
                    break;
                }
                if (listduizi[i] < 15 && i >= 1)//i从1开始才能组合成3张顺子
                {
                    var currentlist = new List<int>();
                    currentlist.AddRange(listduizi);
                    var listresult = (BianLiDataSanDuiZi(listduizi, currentlist, i, li));
                }
            }
            return li;
        }

        /// <summary>
        /// 找出四连对
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindSiLianTiao(PushCardData data)
        {
            List<List<int>> li = new List<List<int>>();
            var listduizi = FindSanTiao(data).OrderByDescending(d => d).ToList();
            if (listduizi == null || listduizi.Count < 4)
            {
                return new List<List<int>>();
            }
            for (int i = 0; i < listduizi.Count; i++)
            {
                if (i == listduizi.Count - 1)
                {
                    break;
                }
                if (listduizi[i] < 15 && i >= 1)//i从1开始才能组合成3张顺子
                {
                    var currentlist = new List<int>();
                    currentlist.AddRange(listduizi);
                    var listresult = (BianLiDataSiShunZi(listduizi, currentlist, i, li));
                }
            }
            return li;
        }

        /// <summary>
        /// 找出无连对
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindWuLianTiao(PushCardData data)
        {
            List<List<int>> li = new List<List<int>>();
            var listsanntiao = FindSanTiao(data).OrderByDescending(d => d).ToList();
            PushCardData purdata = new PushCardData();
            purdata.data = listsanntiao;

            if (FindShunZi(purdata).Count > 0)
            {
                return FindShunZi(purdata);
            }
            return li;
        }

        /// <summary>
        /// 寻找带混顺子   任意长度
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindShunZi(PushCardData data, List<int> joyList, int size)
        {
            if (data == null || data.data.Count < size || size <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData.OrderBy(d => d.v).ToList());
            carddata.RemoveAll(d => joyList.Contains(d.v));
            List<int> cards = carddata.Select(a => a.v).Distinct().ToList();
            int num = cards.Count - (size - 1 - joyList.Count);
            //只需要遍历到第 长度-混子数量+1 位置的元素即可  比如长度为 7   3456789 两个赖子 顺子长度为5 所以只需要遍历到5个即可即可  7-（5-1-2）
            List<List<int>> cardDataList = new List<List<int>>();
            for (int i = 0; i < num; i++)
            {
                List<int> shunZi = AddItem(cards[i], size);
                int cont = NeedTypeMissingTest(cards, shunZi);
                if (cont <= joyList.Count)
                {
                    shunZi.RemoveAll(d => !cards.Contains(d));
                    for (int a = 0; a < cont; a++)
                    {
                        shunZi.Add(joyList[a]);
                    }
                    if (shunZi.Count < size)
                    {
                        continue;
                    }
                    cardDataList.Add(shunZi);
                }
            }
            if (cards[0] <= 5)
            {
                List<int> shunZi = new List<int>();
                if (size == 5)
                {
                    shunZi = new List<int>() { 2, 3, 4, 5, 14 };
                }
                else if (size == 3)
                {
                    shunZi = new List<int>() { 2, 3, 14 };
                }
                int cont = NeedTypeMissingTest(cards, shunZi);
                if (cont <= joyList.Count)
                {
                    shunZi.RemoveAll(d => !cards.Contains(d));
                    for (int a = 0; a < cont; a++)
                    {
                        shunZi.Add(joyList[a]);
                    }
                    if (shunZi.Count >= size)
                    {
                        cardDataList.Add(shunZi);
                    }
                }
            }
            List<List<PDKTypeData>> typeCardsList = GetTypeCardsList(data.cardData, cardDataList);
            return typeCardsList;
        }

        /// <summary>
        /// 类型转换   将牌值转换为牌
        /// </summary>
        /// <param name="cardsList"></param>
        /// <param name="typeCards"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> GetTypeCardsList(List<PDKTypeData> _cardsList, List<List<int>> typeCards)
        {
            if (_cardsList == null || _cardsList.Count <= 0 || typeCards == null || typeCards.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }

            List<List<PDKTypeData>> cardsListData = new List<List<PDKTypeData>>();
            for (int b = 0; b < typeCards.Count; b++)
            {
                List<PDKTypeData> cardsList = new List<PDKTypeData>(_cardsList);
                List<PDKTypeData> list = new List<PDKTypeData>();
                foreach (int _card in typeCards[b])
                {
                    PDKTypeData card = cardsList.FirstOrDefault(d => d.v == _card);
                    if (card != null)
                    {
                        list.Add(card);
                        cardsList.Remove(card);
                    }
                    else
                    {
                        break;
                    }
                }
                if (list.Count != typeCards[b].Count || typeCards[b].Count <= 0)
                {
                    continue;
                }
                cardsListData.Add(list);
            }
            return cardsListData;
        }

        /// <summary>
        /// 判断是不是清一色  0不是清一色 大小王为e      黑桃>大小王>红>梅>方。(十三道)
        /// </summary>
        /// <returns></returns>
        public static string IsUniform(List<PDKTypeData> data)
        {
            string type = "0";
            if (data == null || data.Count <= 0)
            {
                return "0";
            }
            List<PDKTypeData> cardData = new List<PDKTypeData>(data);
            cardData.RemoveAll(d => d.v == 53 || d.v == 54);
            var isUniform = cardData.Select(d => d.h).ToList();
            if (isUniform.Distinct().ToList().Count > 1)
            {
                type = isUniform[0];
            }
            if (data.Count(d => d.v == 53 || d.v == 54) > 0)
            {
                type = "e";
            }
            return type;
        }

        /// <summary>
        /// 添加所需长度顺子元素，根据所传参数第一个进行向后排列
        /// </summary>
        /// <param name="_pai"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> AddItem(int _pai, int size)
        {
            List<int> _List = new List<int>();
            if (_pai <= 0 || size <= 0 || (_pai + size - 1) >= 15)
            {
                if (_pai < 13 && size == 5)
                {
                    return new List<int>() { 10, 11, 12, 13, 14 };
                }
                return _List;
            }
            for (int i = 0; i < size; i++)
            {
                _List.Add(_pai + i);
            }
            return _List;
        }

        /// <summary>
        /// 当前牌型所需赖子数量------判断顺子
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="typeList"></param>
        /// <returns></returns>
        public static int NeedTypeMissingTest(List<int> cards, List<int> typeList)
        {
            if (cards == null || typeList == null)
            {
                return 100;
            }
            int num = typeList.Count(d => !cards.Contains(d));
            return num;
        }

        /// <summary>
        /// 添加所需长度的相同牌
        /// </summary>
        /// <param name="_pai"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> AddUniform(int _pai, int size)
        {
            List<int> _List = new List<int>();
            if (_pai <= 0 || size <= 0 || (_pai + size - 1) >= 15)
            {
                return _List;
            }
            for (int i = 0; i < size; i++)
            {
                _List.Add(_pai);
            }
            return _List;
        }

        /// <summary>
        /// 找到所需的相同的牌
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindSameSizeCard(PushCardData data, List<int> joyList, int size)
        {
            if (data == null || data.data.Count < size || size <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData);
            carddata.RemoveAll(d => joyList.Contains(d.v));
            List<List<int>> cardDataList = new List<List<int>>();
            List<int> cards = carddata.Select(a => a.v).Distinct().ToList().OrderByDescending(d => d).ToList();
            for (int i = 0; i < cards.Count; i++)
            {
                var cardlist = carddata.Select(d => d.v).ToList();
                cardlist.RemoveAll(a => a != cards[i]);
                int needCont = size - cardlist.Count;
                List<int> list = new List<int>();
                if (needCont >= 0)
                {
                    if (needCont > joyList.Count)
                    {
                        continue;
                    }
                    list.AddRange(cardlist);
                    for (int a = 0; a < size - cardlist.Count; a++)
                    {
                        list.Add(joyList[a]);
                    }
                }
                else
                {
                    for (int a = 0; a < size; a++)
                    {
                        list.Add(cardlist[0]);
                    }
                }
                cardDataList.Add(list);
            }
            List<List<PDKTypeData>> sameSizeCard = new List<List<PDKTypeData>>();
            sameSizeCard = GetTypeCardsList(data.cardData, cardDataList);
            return sameSizeCard;
        }

        /// <summary>
        /// 寻找连对-----赖子  （未完成）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindLianDui(PushCardData data, List<int> joyList, int size)
        {
            if (data == null || data.data.Count < size || size <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData);
            carddata.RemoveAll(d => joyList.Contains(d.v));
            List<List<int>> cardDataList = new List<List<int>>();
            List<int> cards = carddata.Select(a => a.v).Distinct().ToList();
            int maxCout = cards.Count / 2;
            if (maxCout >= size)
            {
                //var shunZiData=FindShunZi(data,joyList.Skip(joyList.Count))
            }
            return new List<List<PDKTypeData>>();
        }

        /// <summary>
        /// 找到五张的连子-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindWuZhangShun(PushCardData data, List<int> _joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> shunZiList = FindShunZi(data, _joyList, 5);
            if (shunZiList.Count > 0)
            {
                shunZiList = shunZiList.OrderByDescending(d => d.Max(a => a.h)).ToList();
            }
            return shunZiList;
        }

        /// <summary>
        /// 找到三张顺--------带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindSanZhangShun(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> shunZiList = FindShunZi(data, joyList.Select(d => d.v).ToList(), 3);
            return shunZiList;
        }

        /// <summary>
        /// 找到清一色牌----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindQingYiSe(PushCardData data, List<PDKTypeData> joyList, int _size)
        {
            if (data == null || data.data.Count < _size || _size <= 0 || _size - joyList.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData);
            carddata.RemoveAll(d => joyList.Contains(d));
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            resultList.Add(carddata.Where(d => d.h == "a").ToList());
            resultList.Add(carddata.Where(d => d.h == "b").ToList());
            resultList.Add(carddata.Where(d => d.h == "c").ToList());
            resultList.Add(carddata.Where(d => d.h == "d").ToList());
            int size = _size - joyList.Count;
            resultList = GetPermutations(resultList, _size, size, joyList);
            return resultList;
        }

        /// <summary>
        /// 找到同花顺牌----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindTongHuaShun(PushCardData data, List<PDKTypeData> joyList, int _size)
        {
            if (data == null || data.cardData.Count < _size)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> _dataList = new List<PDKTypeData>(data.cardData);
            List<List<PDKTypeData>> wuZhangShun = new List<List<PDKTypeData>>();
            if (_size == 5)
            {
                wuZhangShun = FindWuZhangShun(data, joyList.Select(d => d.v).ToList());
            }
            else if (_size == 3)
            {
                wuZhangShun = FindSanZhangShun(data, joyList);
            }
            if (wuZhangShun.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            for (int a = 0; a < wuZhangShun.Count; a++)
            {
                wuZhangShun[a].RemoveAll(d => joyList.Contains(d));
                List<int> wuZhangShunList = wuZhangShun[a].Select(c => c.v).ToList();
                List<PDKTypeData> list = new List<PDKTypeData>(wuZhangShun[a]);//_dataList.Where(d => wuZhangShunList.Contains(d.v)).Distinct().ToList();// new List<PDKTypeData>(wuZhangShun[a]);
                //List<PDKTypeData> result = list.Where(d => list.Select(b => b.h == d.h).ToList().Count >= wuZhangShun[a].Count).ToList();
                List<PDKTypeData> needList = _dataList.Where(d => list.Count(c => c.v == d.v) > 0).ToList();
                if (needList.Count <= 0)
                {
                    continue;
                }
                int maxHuaNumber = needList.Max(d => needList.Count(c => c.h == d.h));
                PDKTypeData card = needList.FirstOrDefault(d => needList.Count(c => c.h == d.h) == maxHuaNumber);
                if (card == null)
                {
                    continue;
                }
                string hua = card.h;
                //needList = needList.Where((x, i) => needList.FindIndex(z => z.v == x.v) == i).ToList();//needList.Distinct().ToList();
                List<PDKTypeData> result = needList.Where(d => wuZhangShunList.Count(c => c == d.v) > 0 && d.h == hua).ToList();
                result = result.Where((x, i) => result.FindIndex(z => z.v == x.v) == i).ToList();
                if (result.Count + joyList.Count < _size)
                {
                    continue;
                }
                List<string> huaList = result.Select(d => d.h).Distinct().ToList();
                if (result.Count - huaList.Count + joyList.Count < _size - 1)
                {
                    continue;
                }
                for (int i = 0; i < huaList.Count; i++)
                {
                    List<PDKTypeData> cards = result.Where(d => d.h == huaList[i]).ToList();
                    if (cards.Count(d => d.v < 20) + joyList.Count < _size)
                    {
                        continue;
                    }
                    int num = cards.Count;
                    for (int g = 0; g < _size - num; g++)
                    {
                        if (cards.Count(d => d.v == joyList[g].v && d.h == joyList[g].h) > 0)
                        {
                            continue;
                        }
                        cards.Add(joyList[g]);
                    }
                    if (cards.Count == _size)
                    {
                        resultList.Add(cards);
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// 找到铁支-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindTieZhi(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 4);
            for (int a = 0; a < sameSizeCard.Count; a++)
            {
                sameSizeCard[a].Add(data.cardData.OrderBy(d => d.v).FirstOrDefault(d => sameSizeCard[a].Count(c => c.v == d.v && c.h == d.h) <= 0));
            }
            return sameSizeCard;
        }

        /// <summary>
        /// 找到所有的葫芦-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindHuLu(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> _dataList = new List<PDKTypeData>(data.cardData);
            List<List<PDKTypeData>> sanZhang = FindSanZhang(data, joyList);
            List<List<PDKTypeData>> duiZi = FindDuiZi(data, joyList);
            if (sanZhang.Count <= 0 || duiZi.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            duiZi = duiZi.OrderBy(d => d.Sum(a => a.v)).ToList();
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            for (int i = 0; i < sanZhang.Count; i++)
            {
                for (int a = 0; a < duiZi.Count; a++)
                {
                    if (duiZi[a].Count(d => sanZhang[i].Count(c => c.v == d.v && c.h == d.h) > 0) > 0)
                    {
                        continue;
                    }
                    List<PDKTypeData> list = new List<PDKTypeData>();
                    list.AddRange(sanZhang[i]);
                    list.AddRange(duiZi[a]);
                    if (list.Count(d => joyList.Contains(d)) > joyList.Count)
                    {
                        continue;
                    }
                    resultList.Add(list);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 找到所有的三张-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindSanZhang(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 3)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 3);
            return sameSizeCard;
        }

        /// <summary>
        /// 找到所有对子-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindDuiZi(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 3)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 2);
            return sameSizeCard;
        }

        /// <summary>
        /// 找到所有二连对子-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindErDui(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 4)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 2);
            if (sameSizeCard.Count < 2)
            {
                return new List<List<PDKTypeData>>();
            }
            sameSizeCard = Permutations(sameSizeCard, 2);
            return sameSizeCard;
        }

        /// <summary>
        /// 找到乌龙  就全部的牌   不做处理  
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindWuLong(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 3)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            List<PDKTypeData> cardList = data.cardData.OrderByDescending(d => d.v).ToList();
            for (int i = 0; i < cardList.Count; i++)
            {
                for (int a = i + 1; a < cardList.Count; a++)
                {
                    for (int b = a + 1; b < cardList.Count; b++)
                    {
                        List<PDKTypeData> result = new List<PDKTypeData>();
                        result.Add(cardList[i]);
                        result.Add(cardList[a]);
                        result.Add(cardList[b]);
                        resultList.Add(result);
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// 找到所有五张相同的-----带赖子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindWuTong(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5 || joyList.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData);
            carddata = carddata.OrderBy(d => d.h).OrderBy(d => d.h).ToList();
            carddata.RemoveAll(d => joyList.Contains(d));
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 5);
            return sameSizeCard;
        }

        /// <summary>
        /// 六同  error
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindLiuTong(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5 || joyList.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<PDKTypeData> carddata = new List<PDKTypeData>(data.cardData);
            carddata = carddata.OrderBy(d => d.h).OrderBy(d => d.h).ToList();
            carddata.RemoveAll(d => joyList.Contains(d));
            List<List<PDKTypeData>> sameSizeCard = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 5);
            return sameSizeCard;
        }

        /// <summary>
        /// 寻找三同花
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindSanTongHua(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5 || joyList.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> result = new List<List<PDKTypeData>>();
            List<PDKTypeData> cardData = new List<PDKTypeData>(data.cardData);
            List<PDKTypeData> cardData_need = cardData.Where(d => d.v < 50).ToList();
            if (cardData_need.Select(d => d.h).Distinct().Count() == 1)
            {
                result.Add(cardData);
            }
            return result;
        }

        /// <summary>
        /// 寻找三顺子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> FindSanShunZi(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5 || joyList.Count <= 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> result = new List<List<PDKTypeData>>();
            List<List<PDKTypeData>> shunList_3 = FindShunZi(data, joyList.Select(d => d.v).ToList(), 3);
            List<List<PDKTypeData>> shunList_5 = FindShunZi(data, joyList.Select(d => d.v).ToList(), 5);
            for (int i = 0; i < shunList_3.Count; i++)
            {
                for (int a = 0; a < shunList_5.Count; a++)
                {
                    for (int b = a + 1; b < shunList_5.Count; b++)
                    {
                        List<PDKTypeData> cardList = new List<PDKTypeData>();
                        cardList.AddRange(shunList_3[i]);
                        cardList.AddRange(shunList_5[a]);
                        cardList.AddRange(shunList_5[b]);
                        if (cardList.All(data.cardData.Contains) && cardList.Count == data.cardData.Count)
                        {
                            result.Add(cardList);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 寻找三顺子
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsSanShunZi(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<List<PDKTypeData>> shunList_3 = FindShunZi(data, joyList.Select(d => d.v).ToList(), 3);
            List<List<PDKTypeData>> shunList_5 = FindShunZi(data, joyList.Select(d => d.v).ToList(), 5);
            for (int i = 0; i < shunList_3.Count; i++)
            {
                for (int a = 0; a < shunList_5.Count; a++)
                {
                    for (int b = a + 1; b < shunList_5.Count; b++)
                    {
                        List<PDKTypeData> cardList = new List<PDKTypeData>();
                        cardList.AddRange(shunList_3[i]);
                        cardList.AddRange(shunList_5[a]);
                        cardList.AddRange(shunList_5[b]);
                        List<int> cards_1 = data.cardData.OrderBy(d => d.v).Select(d => d.v).ToList();
                        List<int> cards_2 = cardList.OrderBy(d => d.v).Select(d => d.v).ToList();
                        if (cards_1.SequenceEqual(cards_2))//resultList.Count(d => data.cardData.Count(t => t == d) == cardList.Count(t => t == d)) >= resultList.Count
                        {
                            result = true;
                            return result;
                        }
                        //                         if (cardList.All(data.cardData.Contains) && cardList.Count == data.cardData.Count)
                        //                         {
                        //                             result = true;
                        //                             return result;
                        //                         }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 牌堆是不是六对半
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsLiuDuiBan(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            needCardList = needCardList.Distinct().ToList();
            int needNum = 0;
            for (int i = 0; i < needCardList.Count; i++)
            {
                if (cardList.Count(d => d.v == needCardList[i]) % 2 != 0)
                {
                    needNum++;
                }
                if (needNum - 1 > joyCardList.Count)
                {
                    return false;
                }
            }
            if (joyCardList.Count >= needNum - 1)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 寻找三同花
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsSanTongHua(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            List<PDKTypeData> cardData = new List<PDKTypeData>(data.cardData);
            List<PDKTypeData> cardData_need = cardData.Where(d => d.v < 50).ToList();
            List<List<PDKTypeData>> shunList_3 = FindQingYiSe(data, joyList, 3);
            List<List<PDKTypeData>> shunList_5 = FindQingYiSe(data, joyList, 5);
            for (int i = 0; i < shunList_3.Count; i++)
            {
                for (int a = 0; a < shunList_5.Count; a++)
                {
                    for (int b = a + 1; b < shunList_5.Count; b++)
                    {
                        List<PDKTypeData> cardList = new List<PDKTypeData>();
                        cardList.AddRange(shunList_3[i]);
                        cardList.AddRange(shunList_5[a]);
                        cardList.AddRange(shunList_5[b]);
                        //List<int> cards_1 = data.cardData.OrderBy(d => d.v).Select(d => d.v).ToList();
                        //List<int> cards_2 = cardList.OrderBy(d => d.v).Select(d => d.v).ToList();
                        List<PDKTypeData> resultList = data.cardData.Distinct().ToList();
                        if (resultList.Count(d => data.cardData.Count(t => t == d) == cardList.Count(t => t == d)) >= resultList.Count)// cards_1.SequenceEqual(cards_2)
                        {
                            return true;
                        }
                    }
                }
            }
            //if (cardData_need.Select(d => d.h).Distinct().Count() == 1)
            //{
            //    return true;
            //}
            return false;
        }

        /// <summary>
        /// 判断五对三条
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsWuDuiSanTiao(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            needCardList = needCardList.Distinct().ToList();
            List<int> needList = new List<int>();
            for (int i = 0; i < needCardList.Count; i++)
            {
                if (cardList.Count(d => d.v == needCardList[i]) % 2 != 0)
                {
                    needList.Add(needCardList[i]);
                }
            }
            bool needSanTiao = needList.Count(d => cardList.Count(a => a.v == d) == 3) > 0;
            if ((needList.Count - 1 + (needSanTiao ? 0 : 1)) <= joyList.Count)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断四套三条
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsSiTaoSanTiao(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<List<PDKTypeData>> needList = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 3);
            if (needList.Count < 4)
            {
                return false;
            }
            for (int i = 0; i < needList.Count; i++)
            {
                for (int a = i + 1; a < needList.Count; a++)
                {
                    for (int b = a + 1; b < needList.Count; b++)
                    {
                        for (int c = b + 1; c < needList.Count; c++)
                        {
                            List<PDKTypeData> cardList = new List<PDKTypeData>();
                            cardList.AddRange(needList[i]);
                            cardList.AddRange(needList[a]);
                            cardList.AddRange(needList[b]);
                            cardList.AddRange(needList[c]);
                            List<PDKTypeData> resultList = data.cardData.Distinct().ToList();
                            if (resultList.Count(d => data.cardData.Count(t => t == d) == cardList.Count(t => t == d)) >= resultList.Count - 1)
                            {
                                result = true;
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断凑一色
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsCouYiSe(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            huaList = huaList.Distinct().ToList();
            if (huaList.Count <= 2)
            {
                List<string> rad = new List<string>() { "b", "d" };
                List<string> black = new List<string>() { "a", "c" };
                if ((huaList.All(t => rad.Any(b => b == t))) || (huaList.All(t => black.Any(b => b == t))))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断全小
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllSmall(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            if (needCardList.Count(d => d <= 8) >= needCardList.Count)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断全大
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllBig(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            if (needCardList.Count(d => d >= 8) >= needCardList.Count)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断三分天下
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsSanFenTianXia(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<List<PDKTypeData>> needList = FindSameSizeCard(data, joyList.Select(d => d.v).ToList(), 4);
            if (needList.Count < 3)
            {
                return false;
            }
            for (int i = 0; i < needList.Count; i++)
            {
                for (int a = i + 1; a < needList.Count; a++)
                {
                    for (int b = a + 1; b < needList.Count; b++)
                    {
                        List<PDKTypeData> cardList = new List<PDKTypeData>();
                        cardList.AddRange(needList[i]);
                        cardList.AddRange(needList[a]);
                        cardList.AddRange(needList[b]);
                        List<PDKTypeData> resultList = data.cardData.Distinct().ToList();
                        if (resultList.Count(d => data.cardData.Count(t => t == d) == cardList.Count(t => t == d)) >= resultList.Count - 1)
                        {
                            result = true;
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断三同花顺
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsSanTongHuaShun(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<List<PDKTypeData>> needList_3 = FindTongHuaShun(data, joyList, 3);
            List<List<PDKTypeData>> needList_5 = FindTongHuaShun(data, joyList, 5);
            if (needList_3.Count < 1 || needList_5.Count < 2)
            {
                return false;
            }
            for (int i = 0; i < needList_5.Count; i++)
            {
                for (int a = i + 1; a < needList_5.Count; a++)
                {
                    for (int c = 0; c < needList_3.Count; c++)
                    {
                        List<PDKTypeData> cardList = new List<PDKTypeData>();
                        cardList.AddRange(needList_5[i]);
                        cardList.AddRange(needList_5[a]);
                        cardList.AddRange(needList_3[c]);
                        List<PDKTypeData> resultList = cardList.Distinct().ToList();
                        string cardInfor = cardList.Select(d => d.v).ToJson();
                        List<int> cards_1 = data.cardData.OrderBy(d => d.v).Select(d => d.v).ToList();
                        List<int> cards_2 = cardList.OrderBy(d => d.v).Select(d => d.v).ToList();
                        if (cards_1.SequenceEqual(cards_2))//resultList.Count(d => data.cardData.Count(t => t == d) == cardList.Count(t => t == d)) >= resultList.Count
                        {
                            result = true;
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断十二皇
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsShiErHuang(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            if (needCardList.Count(d => d > 10) >= needCardList.Count - 1)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断是不是一条龙
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsYiTiaoLong(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            List<int> card_value = new List<int> { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 2 };
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            int needNum = 0;
            for (int i = 0; i < card_value.Count; i++)
            {
                if (!needCardList.Contains(card_value[i]))
                {
                    needNum++;
                    if (needNum > joyCardList.Count)
                    {
                        return false;
                    }
                }
            }
            if (needNum <= joyCardList.Count)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断至尊清龙
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsZhiZhunQingLong(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<int> needCardList = cardList.Where(d => d.v < 50).Select(d => d.v).ToList();
            List<int> card_value = new List<int> { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 2 };
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            if (huaList.Distinct().Count() > 1)
            {
                return false;
            }
            int needNum = 0;
            for (int i = 0; i < card_value.Count; i++)
            {
                if (!needCardList.Contains(card_value[i]))
                {
                    needNum++;
                    if (needNum > joyCardList.Count)
                    {
                        return false;
                    }
                }
            }
            if (needNum <= joyCardList.Count)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断是不是全红一点黑
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllRedAndBlack(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            if (huaList.Count(d => d == "b" || d == "d") == (cardList.Count - 1 - joyCardList.Count))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断是不是全黑一点红
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllBlackAndRad(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            if (huaList.Count(d => d == "a" || d == "c") == (cardList.Count - 1 - joyCardList.Count))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断是不是全红
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllRed(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            if (huaList.Count(d => d == "b" || d == "d") == (cardList.Count - joyCardList.Count))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断是不是全黑一点红
        /// </summary>
        /// <param name="data"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static bool IsAllBlack(PushCardData data, List<PDKTypeData> joyList)
        {
            if (data == null || data.cardData.Count < 5)
            {
                return false;
            }
            bool result = false;
            List<PDKTypeData> cardList = new List<PDKTypeData>(data.cardData);
            List<string> huaList = cardList.Where(d => d.v < 50).Select(d => d.h).ToList();
            List<int> joyCardList = cardList.Where(d => d.v > 50).Select(d => d.v).ToList();
            if (huaList.Count(d => d == "a" || d == "c") == (cardList.Count - joyCardList.Count))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 获取赖子时的所有可能
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> GetPermutations(List<List<PDKTypeData>> _dataList, int max, int min, List<PDKTypeData> joyList)
        {
            if (_dataList == null || max <= 0 || min > max)
            {
                return new List<List<PDKTypeData>>();
            }
            int num = max - min;
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            if (num == 0)
            {
                for (int a = 0; a < _dataList.Count; a++)
                {
                    var list = Permutations(_dataList[a], (max - num));
                    resultList.AddRange(list);
                }
            }
            else
            {
                for (int i = 0; i <= num; i++)
                {
                    for (int a = 0; a < _dataList.Count; a++)
                    {
                        var list = GetListOnData(Permutations(_dataList[a], (max - i)), max, joyList);
                        if (list.Count <= 0)
                        {
                            continue;
                        }
                        resultList.AddRange(list);
                    }
                }
            }
            if (resultList.Count > 0)
            {
                resultList = resultList.OrderByDescending(d => d.Max(a => a.h)).ToList();
            }
            return resultList;
        }

        /// <summary>
        /// 根据数据获取所需LIST
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="max"></param>
        /// <param name="joyList"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> GetListOnData(List<List<PDKTypeData>> _dataList, int max, List<PDKTypeData> joyList)
        {
            if (_dataList == null || max <= 0 || joyList == null || joyList.Count == 0)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            for (int i = 0; i < _dataList.Count; i++)
            {
                int needNum = max - _dataList[i].Count;
                if (needNum > joyList.Count)
                {
                    continue;
                }
                for (int a = 0; a < needNum; a++)
                {
                    if (_dataList[i].Count(d => d.v == joyList[a].v && d.h == joyList[a].h) > 0)
                    {
                        continue;
                    }
                    _dataList[i].Add(joyList[a]);
                }
                if (_dataList[i].Count == max)
                {
                    resultList.Add(_dataList[i]);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 字符拼接排列组合
        /// </summary>
        /// <param name="SampleList"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static List<string> getCombination(List<string> SampleList, int m)
        {
            if (m == 1)
            {
                return SampleList;
            }
            List<string> result = new List<string>();
            if (SampleList.Count == m)
            {
                StringBuilder temp_sb = new StringBuilder();
                foreach (string s in SampleList)
                {
                    temp_sb.Append(s);
                }
                result.Add(temp_sb.ToString());
                Console.WriteLine(temp_sb.ToString());
                return result;
            }
            string temp_firstelement = SampleList[0];
            SampleList.RemoveAt(0);
            List<string> temp_samplist1 = new List<string>();
            temp_samplist1.AddRange(SampleList);
            List<string> temp_list1 = getCombination(temp_samplist1, m - 1);
            foreach (string s in temp_list1)
            {
                result.Add(temp_firstelement + s);
                Console.WriteLine(temp_firstelement + s);
            }
            List<string> temp_samplist2 = new List<string>();
            temp_samplist2.AddRange(SampleList);
            List<string> temp_list2 = getCombination(temp_samplist2, m);
            result.AddRange(temp_list2);
            return result;
        }

        /// <summary>
        /// error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dataList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<T>> Permutations<T>(List<T> _dataList, int size)
        {
            List<List<T>> resultList = new List<List<T>>();
            if (size == 1)
            {
                resultList.Add(_dataList);
                return resultList;
            }
            List<T> dataList = new List<T>(_dataList);
            if (dataList.Count == size)
            {
                resultList.Add(dataList);
                return resultList;
            }
            T temp_firstelement = _dataList[0];
            _dataList.RemoveAt(0);
            List<T> temp_samplist1 = new List<T>();
            temp_samplist1.AddRange(_dataList);
            List<List<T>> temp_list1 = Permutations(temp_samplist1, size - 1);
            foreach (List<T> s in temp_list1)
            {
                s.Add(temp_firstelement);
                resultList.Add(s);
            }
            List<T> temp_samplist2 = new List<T>();
            temp_samplist2.AddRange(dataList);
            List<List<T>> temp_list2 = Permutations(temp_samplist2, size);
            resultList.AddRange(temp_list2);
            return resultList;
        }

        /// <summary>
        /// 元素排序
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> Permutations(List<PDKTypeData> _dataList, int size)
        {
            if (_dataList == null || _dataList.Count < size)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> resultList = new List<List<PDKTypeData>>();
            if (_dataList.Count == size)
            {
                resultList.Add(_dataList);
                return resultList;
            }
            List<PDKTypeData> dataList = new List<PDKTypeData>(_dataList);
            dataList = dataList.OrderByDescending(d => d.v).ToList();
            while (true)
            {
                resultList.Add(GetCombinationSpay(dataList, size));
                if (dataList.Count == size)
                {
                    break;
                }
                dataList.RemoveAt(0);
            }
            return resultList;
        }

        /// <summary>
        /// 筛选拼接牌型
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<PDKTypeData>> Permutations(List<List<PDKTypeData>> _dataList, int size)
        {
            if (_dataList == null || _dataList.Count <= 0 || size > _dataList.Count)
            {
                return new List<List<PDKTypeData>>();
            }
            List<List<PDKTypeData>> reultList = new List<List<PDKTypeData>>();
            for (int i = 1; i < _dataList.Count; i++)
            {
                if (_dataList[i - 1].Count <= 0 || _dataList[i].Count <= 0 || _dataList[i - 1].Count(d => _dataList[i].Contains(d)) > 0)
                {
                    continue;
                }
                List<PDKTypeData> list = new List<PDKTypeData>();
                list.AddRange(_dataList[i - 1]);
                list.AddRange(_dataList[i]);
                reultList.Add(list);
            }
            return reultList;
        }

        /// <summary>
        /// 链表转数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dataList"></param>
        /// <returns></returns>
        public static T[] ListToArry<T>(List<T> _dataList)
        {
            T[] resultList = new T[_dataList.Count];
            for (int i = 0; i < _dataList.Count; i++)
            {
                resultList[i] = _dataList[i];
            }
            return resultList;
        }

        /// <summary>
        /// 阉割版排列  去除部分相识组合   123456     如果有123  便不会再取到1 
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<PDKTypeData> GetCombinationSpay(List<PDKTypeData> _dataList, int size)
        {
            if (_dataList.Count == size)
            {
                return _dataList;
            }
            List<PDKTypeData> resultList = new List<PDKTypeData>();
            List<PDKTypeData> dataList = new List<PDKTypeData>(_dataList);
            dataList = dataList.OrderByDescending(d => d.v).ToList();
            for (int i = 0; i < size; i++)
            {
                resultList.Add(dataList[i]);
            }
            return resultList;
        }

        /// <summary>
        /// 判断是不是二连    带赖子
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsErDui(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null || _joyList == null || _dataList.Count != 5)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            List<int> list = dataList.Select(d => d.v).ToList().Distinct().ToList();
            if (list.Count <= 3)
            {
                for (int i = 0; i < _dataList.Count; i++)
                {
                    if (_dataList[i].v > 20)
                    {
                        list = list.OrderByDescending(d => d).ToList();
                        for (int a = 0; a < list.Count; a++)
                        {
                            if (_dataList.Count(d => d.v == list[a]) != 2)
                            {
                                _dataList[i].v = list[a];
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是葫芦
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsHuLu(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < 5)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            List<int> list = dataList.Select(d => d.v).Distinct().ToList();
            if (list.Count == 2)
            {
                PushCardData data = new PushCardData(_dataList);
                if (FindHuLu(data, _joyList).Count > 0)
                {
                    for (int i = 0; i < _dataList.Count; i++)
                    {
                        if (_dataList[i].v > 20)
                        {
                            if (_joyList.Count > 0)
                            {
                                _dataList[i].v = list.FirstOrDefault(d => dataList.Count(a => a.v == d) >= 2);
                            }
                            else
                            {
                                _dataList[i].v = list.Max();
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是不是五连顺
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsWuLianShun(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList, int _size)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < _size)
            {
                return false;
            }
            List<PDKTypeData> dataList = new List<PDKTypeData>(_dataList.OrderBy(d => d.v).ToList());
            List<int> _list = dataList.Select(d => d.v).Distinct().ToList();
            _list.Sort();
            if (_list.Count != _size)
            {
                return false;
            }
            if (_joyList.Count > 0)
            {
                _list.RemoveAll(d => _joyList.Count(a => a.v == d) > 0);
                List<int> list = AddItem(_list[0], _size);
                if (_joyList.Count >= NeedTypeMissingTest(_list, list))
                {
                    SetCardForItem(_dataList, list);
                    return true;
                }
            }
            else
            {
                List<int> list = AddItem(_list[0], _size);
                if (list.Count <= 0)
                {
                    return false;
                }
                if (0 == NeedTypeMissingTest(_list, list))
                {
                    SetCardForItem(_dataList, list);
                    return true;
                }
            }
            if (_list[0] <= 5 && _size == 5)
            {
                List<int> shunZi = new List<int>() { 2, 3, 4, 5, 14 };
                List<int> shunZi1 = new List<int>() { 2, 3, 4, 5, 1 };
                int cont = NeedTypeMissingTest(_list, shunZi);
                if (cont <= _joyList.Count)
                {
                    SetCardForItem(_dataList, shunZi1);
                    return true;
                }
            }
            else if (_list[0] <= 3 && _size == 3)
            {
                List<int> shunZi = new List<int>() { 2, 3, 14 };
                List<int> shunZi1 = new List<int>() { 2, 3, 1 };
                int cont = NeedTypeMissingTest(_list, shunZi);
                if (cont <= _joyList.Count)
                {
                    SetCardForItem(_dataList, shunZi1);
                    return true;
                }
            }
            return false;
        }

        public static void SetCardForItem(List<PDKTypeData> cardList, List<int> data)
        {
            if (cardList == null || data == null || cardList.Count != data.Count)
            {
                return;
            }
            for (int i = 0; i < data.Count; i++)
            {
                if (cardList.Count(d => d.v == data[i]) <= 0)
                {
                    PDKTypeData card = cardList.FirstOrDefault(d => d.v > 20);
                    if (card != null)
                    {
                        card.v = data[i];
                    }
                }
            }
        }

        /// <summary>
        /// 判断是不是三张
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsSanZhang(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < 3)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => a.v == d.v) > 0);
            List<int> list = dataList.Select(d => d.v).Distinct().ToList();
            if (list.Count < _dataList.Count - 1 && (_joyList.Count > 0 || list.Count(d => dataList.Count(a => a.v == d) > 2) >= 1))/*dataList.Count(d=>list.Contains(d.v))>2)*/
            {
                for (int i = 0; i < _dataList.Count; i++)
                {
                    if (_dataList[i].v > 20)
                    {
                        if (_joyList.Count > 0)
                        {
                            _dataList[i].v = list.FirstOrDefault(d => dataList.Count(a => a.v == d) >= 2);
                        }
                        else
                        {
                            _dataList[i].v = list.Max();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是对子
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsDuiZi(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < 2)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            List<int> list = dataList.Select(d => d.v).Distinct().ToList();
            if (list.Count < _dataList.Count)
            {
                for (int i = 0; i < _dataList.Count; i++)
                {
                    if (_dataList[i].v > 20)
                    {
                        _dataList[i].v = list.Max();
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是同花顺
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsTongHuaShun(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null)
            {
                return false;
            }
            List<PDKTypeData> data = _dataList.DeepCloneList();
            if (_dataList.Count == 5 && IsQingYiSe(ref data, _joyList) && IsWuLianShun(ref _dataList, _joyList, 5))
            {
                return true;
            }
            else if (_dataList.Count == 3 && IsQingYiSe(ref data, _joyList) && IsWuLianShun(ref _dataList, _joyList, 3))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是清一色
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsQingYiSe(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            string hua = dataList.FirstOrDefault().h;
            if (dataList.Count(d => d.h != hua) <= 0)
            {
                for (int i = 0; i < _dataList.Count; i++)
                {
                    if (_dataList[i].v > 20)
                    {
                        _dataList[i].v = 14;
                        _dataList[i].h = hua;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是铁支
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsTieZhi(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < 5)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            List<int> list = dataList.Select(d => d.v).Distinct().ToList();
            if (list.Count > 2)
            {
                return false;
            }
            for (int i = 0; i < list.Count; i++)
            {
                int count = dataList.Count(d => d.v == list[i]);
                if (count + _joyList.Count >= 4)
                {
                    for (int a = 0; a < _dataList.Count; a++)
                    {
                        if (_dataList[a].v > 20)
                        {
                            _dataList[a].v = list[i];
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断有没有五张相同的牌
        /// </summary>
        /// <param name="_dataList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static bool IsWuTong(ref List<PDKTypeData> _dataList, List<PDKTypeData> _joyList, ref int num)
        {
            if (_dataList == null || _joyList == null || _dataList.Count < 5)
            {
                return false;
            }
            List<PDKTypeData> dataList = _dataList.DeepCloneList();
            dataList.RemoveAll(d => _joyList.Count(a => d.v == a.v) > 0);
            List<int> list = dataList.Select(d => d.v).Distinct().ToList();
            if (list.Count > 1)
            {
                return false;
            }
            for (int i = 0; i < _dataList.Count; i++)
            {
                _dataList[i].v = list[0];
            }
            return true;
        }

        /// <summary>
        /// 判断乌龙大小
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareWuLong(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList)
        {
            if (_nextList == null || _oldList == null)
            {
                return 0;
            }
            List<PDKTypeData> oldList = _oldList.OrderByDescending(d => d.v).ToList();
            List<PDKTypeData> nextList = _nextList.OrderByDescending(d => d.v).ToList();
            for (int i = 0; i < oldList.Count; i++)
            {
                if (nextList[i].v != oldList[i].v)
                {
                    return nextList[i].v > oldList[i].v ? -1 : 1;
                }
            }
            return 0;// ComepareHuSe(_oldList, _nextList);
        }

        /// <summary>
        /// 比较对子    joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static int ComepareDuiZi(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null)
            {
                return 0;
            }
            List<PDKTypeData> oldJoyList = _oldList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            List<PDKTypeData> nextJoyList = _nextList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            PushCardData oldCardData = new PushCardData(_oldList);
            PushCardData nextCardData = new PushCardData(_nextList);
            List<List<PDKTypeData>> oldCardsList = FindDuiZi(oldCardData, oldJoyList);
            List<List<PDKTypeData>> nextCardsList = FindDuiZi(nextCardData, nextJoyList);
            if (oldCardsList.Count <= 0 || nextCardsList.Count <= 0)
            {
                return 0;
            }
            int oldcard = oldCardsList.Max(d => d[0].v);
            int nextcard = nextCardsList.Max(d => d[0].v);
            if (oldcard != nextcard)
            {
                return oldcard < nextcard ? -1 : 1;
            }
            List<PDKTypeData> old = _oldList.Where(d => d.v < 15 && d.v != oldcard).ToList();
            List<PDKTypeData> next = _nextList.Where(d => d.v < 15 && d.v != nextcard).ToList();
            for (int i = 0; i < old.Count; i++)
            {
                if (old[i].v != next[i].v)
                {
                    return next[i].v > old[i].v ? -1 : 1;
                }
            }
            //             List<PDKTypeData> oldSanZhang = _oldList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             List<PDKTypeData> nextSanZhang = _nextList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             for (int i = 0; i < oldSanZhang.Count; i++)
            //             {
            //                 if (HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) != HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v))
            //                 {
            //                     return HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) < HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v) ? -1 : 1;
            //                 }
            //             }
            return 0;
        }

        /// <summary>
        /// 比较三张    joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static int ComepareSanZhang(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null)
            {
                return 0;
            }
            List<PDKTypeData> oldJoyList = _oldList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            List<PDKTypeData> nextJoyList = _nextList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            PushCardData oldCardData = new PushCardData(_oldList);
            PushCardData nextCardData = new PushCardData(_nextList);
            List<List<PDKTypeData>> oldCardsList = FindSanZhang(oldCardData, oldJoyList);
            List<List<PDKTypeData>> nextCardsList = FindSanZhang(nextCardData, nextJoyList);
            if (oldCardsList.Count <= 0 || nextCardsList.Count <= 0)
            {
                return 0;
            }
            int oldcard = oldCardsList.Max(d => d[0].v);
            int nextcard = nextCardsList.Max(d => d[0].v);
            if (oldcard != nextcard)
            {
                return oldcard < nextcard ? -1 : 1;
            }
            List<PDKTypeData> old = _oldList.Where(d => d.v < 15 && d.v != oldcard).ToList();
            List<PDKTypeData> next = _nextList.Where(d => d.v < 15 && d.v != nextcard).ToList();
            for (int i = 0; i < old.Count; i++)
            {
                if (old[i].v != next[i].v)
                {
                    return next[i].v > old[i].v ? -1 : 1;
                }
            }
            //             List<PDKTypeData> oldSanZhang = _oldList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             List<PDKTypeData> nextSanZhang = _nextList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             for (int i = 0; i < oldSanZhang.Count; i++)
            //             {
            //                 if (HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) != HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v))
            //                 {
            //                     return HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) < HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v) ? -1 : 1;
            //                 }
            //             }
            return 0;
        }

        /// <summary>
        /// 比较两对    joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareErDui(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null || _oldList.Count < 4 || _nextList.Count < 4)
            {
                return 0;
            }
            List<PDKTypeData> oldJoyList = _oldList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            List<PDKTypeData> nextJoyList = _nextList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            PushCardData oldCardData = new PushCardData(_oldList);
            PushCardData nextCardData = new PushCardData(_nextList);
            List<List<PDKTypeData>> oldCardsList = FindDuiZi(oldCardData, oldJoyList);
            List<List<PDKTypeData>> nextCardsList = FindDuiZi(nextCardData, nextJoyList);
            if (oldCardsList.Count <= 0 || nextCardsList.Count <= 0)
            {
                return 0;
            }
            oldCardsList = oldCardsList.OrderByDescending(d => d[0].v).ToList();
            nextCardsList = nextCardsList.OrderByDescending(d => d[0].v).ToList();
            for (int i = 0; i < oldCardsList.Count; i++)
            {
                List<PDKTypeData> old = oldCardsList[i].OrderBy(d => d.v).ToList();
                List<PDKTypeData> next = nextCardsList[i].OrderBy(d => d.v).ToList();
                if (old[0].v != next[0].v)
                {
                    return old[0].v < next[0].v ? -1 : 1;
                }
            }
            //             List<PDKTypeData> oldSanZhang = _oldList.OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             List<PDKTypeData> nextSanZhang = _nextList.OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             for (int i = 0; i < oldSanZhang.Count; i++)
            //             {
            //                 if (HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) != HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v))
            //                 {
            //                     return HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) < HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v) ? -1 : 1;
            //                 }
            //             }
            return 0;
        }

        /// <summary>
        /// 比较五连顺  joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareWuZhangSun(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList, int size)
        {
            if (_nextList == null)
            {
                return 0;
            }
            _oldList = _oldList.OrderBy(d => d.v).ToList();
            _nextList = _nextList.OrderBy(d => d.v).ToList();
            PDKTypeData oldCard = _oldList[0];
            PDKTypeData nextCard = _nextList[0];
            if (oldCard == null || nextCard == null)
            {
                return 0;
            }
            List<int> oldData = GetItem(_oldList, _oldList.Where(d => d.v > 15).ToList(), size);
            List<int> nextData = GetItem(_nextList, _nextList.Where(d => d.v > 15).ToList(), size);
            for (int i = 0; i < oldData.Count; i++)
            {//比牌值
                if (oldData[i] != nextData[i])
                {
                    return oldData[i] < nextData[i] ? -1 : 1;
                }
                else
                {
                    break;
                }
            }
            //             for (int i = 0; i < oldData.Count; i++)
            //             {//比花色
            //                 PDKTypeData old = _oldList.FirstOrDefault(d => d.v == oldData[i]);
            //                 PDKTypeData next = _nextList.FirstOrDefault(d => d.v == nextData[i]);
            //                 if (old == null) //如何遍历到赖子牌变的时候，需要把赖子填上去
            //                 {
            //                     old = new PDKTypeData(53, "a");
            //                 }
            //                 if (next == null)
            //                 {
            //                     next = new PDKTypeData(53, "a");
            //                 }
            //                 if (HuaSeToNum(old.h, old.v) != HuaSeToNum(next.h, next.v))
            //                 {
            //                     return HuaSeToNum(old.h, old.v) < HuaSeToNum(next.h, next.v) ? -1 : 1;
            //                 }
            //             }
            return 0;
        }

        public static bool ComepareSanZhangSun(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList, int size)
        {
            if (_nextList == null || _oldList == null || _oldList.Count < size || _nextList.Count < size)
            {
                return false;
            }
            _oldList = _oldList.OrderBy(d => d.v).ToList();
            _nextList = _nextList.OrderBy(d => d.v).ToList();
            PDKTypeData oldCard = _oldList[0];
            PDKTypeData nextCard = _nextList[0];
            if (oldCard == null || nextCard == null)
            {
                return false;
            }
            List<int> oldData = GetItem(_oldList, _oldList.Where(d => d.v > 15).ToList(), size);
            List<int> nextData = GetItem(_nextList, _nextList.Where(d => d.v > 15).ToList(), size);
            for (int i = 0; i < oldData.Count; i++)
            {//比牌值
                if (oldData[i] != nextData[i])
                {
                    return oldData[i] < nextData[i];
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < oldData.Count; i++)
            {//比花色
                PDKTypeData old = _oldList.FirstOrDefault(d => d.v == oldData[i]);
                PDKTypeData next = _nextList.FirstOrDefault(d => d.v == nextData[i]);
                if (old == null) //如何遍历到赖子牌变的时候，需要把赖子填上去
                {
                    old = new PDKTypeData(53, "a");
                }
                if (next == null)
                {
                    next = new PDKTypeData(53, "a");
                }
                if (HuaSeToNum(old.h, old.v) != HuaSeToNum(next.h, next.v))
                {
                    return HuaSeToNum(old.h, old.v) < HuaSeToNum(next.h, next.v);
                }
            }
            return true;
        }

        /// <summary>
        /// 比较清一色  joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static int ComepareQingYiSe(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null)
            {
                return 0;
            }
            _oldList = _oldList.OrderByDescending(d => d.v).ToList();
            _nextList = _nextList.OrderByDescending(d => d.v).ToList();
            for (int i = 0; i < _oldList.Count; i++)
            {
                int old = _oldList[i].v > 15 ? 14 : _oldList[i].v;
                int next = _nextList[i].v > 15 ? 14 : _nextList[i].v;
                if (old != next)
                {
                    return next > old ? -1 : 1;
                }
            }
            //             PDKTypeData oldCard = _oldList.FirstOrDefault(w => w.v < 15);
            //             PDKTypeData nextCard = _nextList.FirstOrDefault(d => d.v < 15);
            //             if (HuaSeToNum(oldCard.h, oldCard.v) != HuaSeToNum(nextCard.h, nextCard.v))
            //             {
            //                 return HuaSeToNum(oldCard.h, oldCard.v) < HuaSeToNum(nextCard.h, nextCard.v) ? -1 : 1;
            //             }
            return 0;
        }

        /// <summary>
        /// 比较清一色  joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareHuLu(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null || _oldList.Count < 5 || _nextList.Count < 5)
            {
                return 0;
            }
            List<PDKTypeData> oldJoyList = _oldList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            List<PDKTypeData> nextJoyList = _nextList.Where(d => _joyList.Count(a => a.v == d.v) > 0).ToList();
            PushCardData oldCardData = new PushCardData(_oldList);
            PushCardData nextCardData = new PushCardData(_nextList);
            List<List<PDKTypeData>> oldCardsList = FindSanZhang(oldCardData, oldJoyList);
            List<List<PDKTypeData>> nextCardsList = FindSanZhang(nextCardData, nextJoyList);
            if (oldCardsList.Count <= 0 || nextCardsList.Count <= 0)
            {
                return 0;
            }
            int oldcard = oldCardsList.Max(d => d[0].v);
            int nextcard = nextCardsList.Max(d => d[0].v);
            if (oldcard != nextcard)
            {
                return oldcard < nextcard ? -1 : 1;
            }
            List<PDKTypeData> old = _oldList.Where(d => d.v < 15 && d.v != oldcard).ToList();
            List<PDKTypeData> next = _nextList.Where(d => d.v < 15 && d.v != nextcard).ToList();
            for (int i = 0; i < old.Count; i++)
            {
                if (old[i].v != next[i].v)
                {
                    return next[i].v > old[i].v ? -1 : 1;
                }
            }
            //             List<PDKTypeData> oldSanZhang = _oldList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             List<PDKTypeData> nextSanZhang = _nextList.Where(d => d.v > 15 || d.v == oldcard).OrderBy(d => d.h).OrderByDescending(d => d.v).ToList();
            //             for (int i = 0; i < oldSanZhang.Count; i++)
            //             {
            //                 if (HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) != HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v))
            //                 {
            //                     return HuaSeToNum(oldSanZhang[i].h, oldSanZhang[i].v) < HuaSeToNum(nextSanZhang[i].h, nextSanZhang[i].v) ? -1 : 1;
            //                 }
            //             }
            return 0;
        }

        /// <summary>
        /// 比较铁支    joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static int ComepareTieZhi(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null || _oldList.Count < 5 || _nextList.Count < 5)
            {
                return 0;
            }
            List<int> oldList = _oldList.Select(d => d.v).Distinct().ToList();
            List<int> nextList = _nextList.Select(d => d.v).Distinct().ToList();
            int old = oldList.FirstOrDefault(d => _oldList.Count(a => a.v == d) > 1);
            int next = nextList.FirstOrDefault(d => _nextList.Count(a => a.v == d) > 1);
            if (old != next)
            {
                return old < next ? -1 : 1;
            }
            return 0;
        }

        /// <summary>
        /// 比较同花顺     joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <param name="_joyList"></param>
        /// <returns></returns>
        public static int ComepareTongHuaShun(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList, int size)
        {
            if (_nextList == null)
            {
                return 0;
            }
            _oldList = _oldList.OrderBy(d => d.v).ToList();
            _nextList = _nextList.OrderBy(d => d.v).ToList();
            PDKTypeData oldCard = _oldList[0];
            PDKTypeData nextCard = _nextList[0];
            if (oldCard == null || nextCard == null)
            {
                return 0;
            }
            List<int> oldData = GetItem(_oldList, _oldList.Where(d => d.v > 15).ToList(), size);
            List<int> nextData = GetItem(_nextList, _nextList.Where(d => d.v > 15).ToList(), size);
            for (int i = 0; i < oldData.Count; i++)
            {//比牌值
                if (oldData[i] != nextData[i])
                {
                    return oldData[i] < nextData[i] ? -1 : 1;
                }
                else
                {
                    break;
                }
            }//比花色
            //             PDKTypeData old = _oldList.FirstOrDefault(d => d.v < 15);
            //             PDKTypeData next = _nextList.FirstOrDefault(d => d.v < 15);
            //             if (HuaSeToNum(old.h, old.v) != HuaSeToNum(next.h, next.v))
            //             {
            //                 return HuaSeToNum(old.h, old.v) < HuaSeToNum(next.h, next.v) ? -1 : 1;
            //             }
            return 0;
        }

        /// <summary>
        /// 比较五同     joylist 为游戏中的赖子非玩家手中的赖子
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareWuTong(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList, List<PDKTypeData> _joyList)
        {
            if (_nextList == null || _oldList == null || _oldList.Count < 5 || _nextList.Count < 5)
            {
                return 0;
            }
            List<int> oldList = _oldList.Select(d => d.v).Distinct().ToList();
            List<int> nextList = _nextList.Select(d => d.v).Distinct().ToList();
            int old = oldList.FirstOrDefault(d => _oldList.Count(a => a.v == d) > 1);
            int next = nextList.FirstOrDefault(d => _nextList.Count(a => a.v == d) > 1);
            if (old != next)
            {
                return old < next ? -1 : 1;
            }
            return 0;
        }

        /// <summary>
        /// 紧当牌型牌值均相同的时候进行比较，没有赖子的时候
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static int ComepareHuSe(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList)
        {
            if (_nextList == null || _oldList == null)
            {
                return 0;
            }
            _oldList = _oldList.OrderByDescending(d => d.v).ToList();
            _nextList = _nextList.OrderByDescending(d => d.v).ToList();
            for (int i = 0; i < _oldList.Count; i++)//只有在循环的时候才再次遍历
            {
                if (HuaSeToNum(_nextList[i].h, _nextList[i].v) > HuaSeToNum(_oldList[i].h, _oldList[i].v))
                {
                    return -1;
                }
                else if (HuaSeToNum(_nextList[i].h, _nextList[i].v) < HuaSeToNum(_oldList[i].h, _oldList[i].v))
                {
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 当牌型一样时，并且没有赖子的时候，比较牌值
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_nextList"></param>
        /// <returns></returns>
        public static bool ComepareCardNum(List<PDKTypeData> _oldList, List<PDKTypeData> _nextList)
        {
            if (_nextList == null || _oldList == null)
            {
                return false;
            }
            if (_nextList.Max(d => d.v) != _oldList.Max(d => d.v))
            {
                return _nextList.Max(d => d.v) > _oldList.Max(d => d.v);
            }
            return true;
        }

        /// <summary>
        /// 获取花色值  用于比较花色 a 10  joy 9  b 8  c 7  d 6
        /// </summary>
        /// <param name="hua"></param>
        /// <returns></returns>
        public static int HuaSeToNum(string hua, int v)
        {
            int num = 0;
            switch (hua)
            {
                case "a":
                    num = 10;
                    break;
                case "b":
                    num = 8;
                    break;
                case "c":
                    num = 7;
                    break;
                case "d":
                    num = 6;
                    break;
            }
            if (v == 53 || v == 54)
            {
                num = 9;
            }
            return num;
        }

        /// <summary>
        /// 获取模版顺子
        /// </summary>
        /// <param name="list"></param>
        /// <param name="joyList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> GetItem(List<PDKTypeData> list, List<PDKTypeData> joyList, int size)
        {
            if (list.Count < size || list == null)
            {
                return new List<int>();
            }
            list = list.OrderBy(d => d.v).ToList();
            List<int> cards = list.Select(d => d.v).ToList();
            List<int> joys = joyList.Select(d => d.v).ToList();
            List<int> result = new List<int>();
            if (size == 5 && cards[0] < 5)
            {
                List<int> shunZi1 = new List<int>() { 2, 3, 4, 5, 14 };
                int cont1 = NeedTypeMissingTest(cards, shunZi1);
                if (cont1 <= joyList.Count)
                {
                    shunZi1.RemoveAll(d => !cards.Contains(d));
                    for (int a = 0; a < cont1; a++)
                    {
                        shunZi1.Add(joys[a]);
                    }
                    if (shunZi1.Count >= size)
                    {
                        List<int> list1 = new List<int>() { 1, 2, 3, 4, 5 };
                        result = new List<int>(list1);
                    }
                }
            }
            List<int> shunZi = AddItem(cards[0], size);
            List<int> list2 = new List<int>(shunZi);
            int cont = NeedTypeMissingTest(cards, shunZi);
            if (cont <= joyList.Count)
            {
                shunZi.RemoveAll(d => !cards.Contains(d));
                for (int a = 0; a < cont; a++)
                {
                    shunZi.Add(joys[a]);
                }
                if (shunZi.Count >= size)
                {
                    result = new List<int>(list2);
                }
            }
            result = result.OrderByDescending(d => d).ToList();
            return result;
        }

        /// <summary>
        /// 获取特殊牌的分值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetSpecialCardTypeScore_NingHai(ShiSanShuiCardEnum type, List<PDKTypeData> cards)
        {
            int result = 0;
            switch (type)
            {
                case ShiSanShuiCardEnum.三同花:
                    result = 6;
                    break;
                case ShiSanShuiCardEnum.三顺子:
                    result = 8;
                    break;
                case ShiSanShuiCardEnum.六对半:
                    result = 8;
                    break;
                case ShiSanShuiCardEnum.四套三条:
                    result = 8;
                    break;
                case ShiSanShuiCardEnum.三分天下:
                    result = 16;
                    break;
                case ShiSanShuiCardEnum.三同花顺:
                    result = 18;
                    break;
                case ShiSanShuiCardEnum.一条龙:
                    result = 26;
                    break;
                case ShiSanShuiCardEnum.至尊清龙:
                    result = 108;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取特殊牌的分值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetSpecialCardTypeScore_SanMen(ShiSanShuiCardEnum type)
        {
            int result = 0;
            switch (type)
            {
                case ShiSanShuiCardEnum.三同花:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.三顺子:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.六对半:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.五对三条:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.四套三条:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.凑一色:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.全小:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.全大:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.三分天下:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.三同花顺:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.十二皇族:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.一条龙:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.至尊清龙:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.全红一点黑:
                    result = 10;
                    break;
                case ShiSanShuiCardEnum.全黑一点红:
                    result = 10;
                    break;
            }
            return result;
        }
    }
}
