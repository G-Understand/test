using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    public class GameTest
    {
        /// <summary>
        /// 获取可能的value
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<List<int>> GetCardValue(int num)
        {
            List<List<int>> result = new List<List<int>>();
            if (num % 10 == 1)
            {
                result.Add(new List<int>() { num, num + 1, num + 2 });
            }
            else if (num % 10 == 2)
            {
                result.Add(new List<int>() { num - 1, num, num + 1 });
                result.Add(new List<int>() { num, num + 1, num + 2 });
            }
            else if (num % 10 == 8)
            {
                result.Add(new List<int>() { num - 2, num - 1, num });
                result.Add(new List<int>() { num - 1, num, num + 1 });
            }
            else if (num % 10 == 9)
            {
                result.Add(new List<int>() { num - 2, num - 1, num });
            }
            else
            {
                result.Add(new List<int>() { num - 2, num - 1, num });
                result.Add(new List<int>() { num - 1, num, num + 1 });
                result.Add(new List<int>() { num, num + 1, num + 2 });
            }
            return result;
        }

        /// <summary>
        /// 获取满足条件的牌数量
        /// </summary>
        /// <returns></returns>
        public static List<List<int>> GetNeedCards(List<int> cards, int hunNum)
        {
            List<List<int>> result = new List<List<int>>();
            int count = cards.Count + hunNum;
            for (int i = 0; i < cards.Count; i++)
            {
                List<int> needValue = new List<int>();
                List<List<int>> value = GetCardValue(cards[i]);

            }
            return result;
        }

        public static bool ReturnValue(List<int> cards, int hunNum)
        {
            //匹配的话，始终用最小癞子消耗来匹配
            if (cards.Count <= 0) return true;
            if (hunNum <= 0) return false;
            int card = cards[0];
            if (card > 30)
            {
                //风牌需要3个以上
                int num = cards.Count(d => d == card);
                int needNum = (3 - num);
                hunNum -= (needNum <= 0 ? 0 : needNum);
                for (int i = 0; i < num; i++)
                {
                    cards.Remove(card);
                }
                return ReturnValue(cards, hunNum);
            }
            else
            {
                int num = cards.Count(d => d == card);
                if (num >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        cards.Remove(card);
                    }
                    return ReturnValue(cards, hunNum);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (cards.Contains(card + i))
                        {
                            cards.Remove(card + i);
                        }
                        else
                        {
                            hunNum -= 1;
                        }
                    }
                    return ReturnValue(cards, hunNum);
                }
            }
            //result.Add(new List<int>() { num, num, num });
        }

        public static bool IsCanHu(List<int> cards, int hunzi)
        {
            System.Diagnostics.Stopwatch areaWatch = new System.Diagnostics.Stopwatch();
            areaWatch.Start(); //  开始监视代码
            List<int> noCards = cards.Where(d => d != hunzi).ToList();//找出不是癞子的
            int hunCount = cards.Count - noCards.Count;//算出混子数量
            List<int> needList = noCards.Distinct().ToList();
            for (int i = 0; i < needList.Count; i++)
            {
                int card = needList[i];
                int num = hunCount;
                if (noCards.Count(d=>d == card) < 2)
                {
                    num = hunCount - 1;
                }
                if (ReturnValue(noCards, num))
                {
                    areaWatch.Stop(); //  停止监视
                    TimeSpan timeSpan = areaWatch.Elapsed; //  获取总时间
                    double value = timeSpan.TotalMilliseconds;  //  毫秒数
                    Console.WriteLine("++++++++++" + value);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 听牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="hunzi"></param>
        /// <returns></returns>
        public static List<int> Ting(List<int> cards, int hunzi)
        {
            List<int> result = new List<int>();
            if (cards == null || cards.Count <= 0) return result;

            #region 任意牌测试
            List<int> noCards = cards.Where(d => d != hunzi).ToList();
            int hunCount = cards.Count - noCards.Count;
            if (ReturnValue(noCards, hunCount - 1))
            {
                return new List<int>() { 55 };
            }
            #endregion

            List<int> allCards = new List<int>();
            #region 添加牌库
            allCards.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            allCards.AddRange(new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            allCards.AddRange(new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29 });
            allCards.AddRange(new int[] { 31, 33, 35, 37 });
            allCards.AddRange(new int[] { 41, 43, 45 });
            #endregion
            for (int i = 0; i < allCards.Count; i++)
            {
                List<int> models = new List<int>(cards);
                models.Add(allCards[i]);
                models.Sort();
                if (IsCanHu(models, hunzi))
                {
                    result.Add(allCards[i]);
                }

            }
            return result;
        }



        public static void Test()
        {
            List<int> cards = new List<int> { 1, 2, 3, 4, 5, 5, 22, 23, 43, 43, 43 };
            int hunzi = 43;
            List<int> cards1 = new List<int> { 2, 3, 4, 5, 5, 22, 23, 43, 43, 43 };
            int hunzi1 = 43;


            System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start(); //  开始监视代码
            var result1 = isCanting(new List<int>(cards1), hunzi1);
            stopwatch1.Stop(); //  停止监视
            TimeSpan timeSpan1 = stopwatch1.Elapsed; //  获取总时间
            double value1 = timeSpan1.TotalMilliseconds;  //  毫秒数
            Console.WriteLine("--------" + value1);


            System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();
            stopwatch2.Start(); //  开始监视代码
            var result2 = isCanting(new List<int> (cards1), hunzi1);
            stopwatch2.Stop(); //  停止监视
            TimeSpan timeSpan2 = stopwatch2.Elapsed; //  获取总时间
            double value2 = timeSpan2.TotalMilliseconds;  //  毫秒数
            Console.WriteLine("--------" + value2);


            System.Diagnostics.Stopwatch globalWatch = new System.Diagnostics.Stopwatch();
            globalWatch.Start(); //  开始监视代码
            var result = Ting(new List<int> (cards), hunzi);
            //var result = IsCanHu(cards, hunzi);
            globalWatch.Stop(); //  停止监视
            TimeSpan timeSpan = globalWatch.Elapsed; //  获取总时间
            double value = timeSpan.TotalMilliseconds;  //  毫秒数
            Console.WriteLine("&&&&&&" + value);

            System.Diagnostics.Stopwatch globalWatch11 = new System.Diagnostics.Stopwatch();
            globalWatch11.Start(); //  开始监视代码
            var result11 = CanTing(new List<int> (cards), hunzi);
            //var result = IsCanHu(cards, hunzi);
            globalWatch11.Stop(); //  停止监视
            TimeSpan timeSpan11 = globalWatch11.Elapsed; //  获取总时间
            double value11 = timeSpan11.TotalMilliseconds;  //  毫秒数
            Console.WriteLine("^^^^^" + value11);

            int a = 0;


        }








        public static bool IsPu(List<int> cards, int laizi)
        {
            if (cards.Count == 0)
            {
                return true;
            }
            var firstCard = cards[0];
            // 若第一张是顺子中的第一张  
            var shunCount = 1;
            if (cards.Contains(firstCard + 1))
            {
                shunCount++;
            }
            if (cards.Contains(firstCard + 2))
            {
                shunCount++;
            }
            if (firstCard % 10 < 8 && (shunCount == 3 || shunCount + laizi >= 3))
            {
                var puCards = new List<int>(cards);
                var puLaizi = laizi;
                for (var i = 0; i < 3; i++)
                {
                    if (puCards.Contains(firstCard + i))
                    {
                        puCards.Remove(firstCard + i);
                    }
                    else
                    {
                        puLaizi--;
                    }
                }
                if (IsPu(puCards, puLaizi))
                {
                    // cc.log('顺', firstCard, firstCard+1, firstCard+2);  
                    return true;
                }
            }
            // 若第一张是刻子中的第一张  
            var keziCount = 1;
            if (cards.Count >= 2 && cards[1] == firstCard)
            {
                keziCount++;
            }
            if (cards.Count >= 3 && cards[2] == firstCard)
            {
                keziCount++;
            }
            if (keziCount == 3 || keziCount + laizi >= 3)
            {
                var puCards = new List<int>(cards);
                var puLaizi = laizi;
                for (var i = 0; i < 3; i++)
                {
                    if (puCards.Contains(firstCard))
                    {
                        puCards.Remove(firstCard);
                    }
                    else
                    {
                        puLaizi--;
                    }
                }
                if (IsPu(puCards, puLaizi))
                {
                    return true;
                }
            }
            return false;
        }


        public static bool canHuLaizi(List<int> cards, int hunzi)
        {
            List<int> noCards = cards.Where(d => d != hunzi).OrderBy(d => d).ToList();
            int laizi = cards.Count(d => d == hunzi);
            // 依次删除一对牌做将，其余牌全部成扑则可胡  
            for (var i = 0; i < noCards.Count; i++)
            {
                if (i > 0 && noCards[i] == noCards[i - 1])
                {
                    // 和上一次是同样的牌，避免重复计算  
                    continue;
                }
                if ((i + 1 < noCards.Count && noCards[i] == noCards[i + 1]) || laizi > 0)
                {
                    // 找到对子、或是用一张癞子拼出的对子  
                    var puCards = new List<int>(noCards);
                    var puLaizi = laizi;
                    puCards.Remove(i);
                    if (puCards.Count > i && puCards[i] == noCards[i])
                    {
                        puCards.Remove(i);
                    }
                    else
                    {
                        puLaizi--;
                    }
                    // 删去对子判断剩下的牌是否成扑  
                    if (IsPu(puCards, puLaizi))
                    {
                        return true;
                    }
                }
            }
            if (laizi >= 2 && IsPu(noCards, laizi - 2))
            {
                // 两个癞子做将牌  
                return true;
            }
            return false;
        }


        /// <summary>
        /// 听牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="hunzi"></param>
        /// <returns></returns>
        public static List<int> CanTing(List<int> cards, int hunzi)
        {
            List<int> result = new List<int>();
            if (cards == null || cards.Count <= 0) return result;

            #region 任意牌测试
            List<int> noCards = cards.Where(d => d != hunzi).ToList();
            int hunCount = cards.Count - noCards.Count;
            if (ReturnValue(noCards, hunCount - 1))
            {
                return new List<int>() { 55 };
            }
            #endregion

            List<int> allCards = new List<int>();
            #region 添加牌库
            allCards.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            allCards.AddRange(new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            allCards.AddRange(new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29 });
            allCards.AddRange(new int[] { 31, 33, 35, 37 });
            allCards.AddRange(new int[] { 41, 43, 45 });
            #endregion
            for (int i = 0; i < allCards.Count; i++)
            {
                List<int> models = new List<int>(cards);
                models.Add(allCards[i]);
                models.Sort();
                if (canHuLaizi(models, hunzi))
                {
                    result.Add(allCards[i]);
                }

            }
            return result;
        }














































        #region 癞子工具
        /// <summary>
        /// 第一张需要两个 癞子的
        /// </summary>
        /// <param name="listallpai"></param>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="youxianduizi"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianduizi2(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }

                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                        listallpai.Remove(listsantiaomajiang[i].Value);
                        listallpai.Remove(listsantiaomajiang[i].Value);
                        listallpai.Remove(listsantiaomajiang[i].Value);
                    }
                }

                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];
                    if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        listpai.Remove(pai + 2);
                        return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                    if (youxianduizi)
                    {
                        if (listpai.Count(d => d == listpai[i]) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                return new List<int>();
                            }
                            listneedpai.Add(pai);
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                    }

                    //第一个数 先判断加两个癞子是否满足
                    if (laizicount - needlaizi > 0 && pai == listallpai[0])
                    {
                        listpai.Remove(pai);
                        needlaizi += 2;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        listneedpai.Add(pai);

                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }

                        return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                    else
                    {
                        //与当前牌组合需要一个癞子的牌
                        if (listpai.Contains(pai + 1))
                        {
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (!listpai.Contains(pai + 2) && pai / 10 == pai + 2)
                            {
                                listneedpai.Add(pai + 2);
                            }
                            if (pai % 10 >= 2)
                            {
                                listneedpai.Add(pai - 1);
                            }
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                        else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                        {
                            if (!youxianduizi)
                            {
                                var d = 0;
                            }
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            listneedpai.Add(pai + 1);

                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            listneedpai.Add(pai);
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                        else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            listneedpai.Add(pai);
                            listneedpai.Add(pai);

                            if (pai % 10 == 1)
                            {
                                listneedpai.Add(pai + 1);
                                listneedpai.Add(pai + 2);
                            }
                            else if (pai % 10 == 2)
                            {
                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai + 1);

                                listneedpai.Add(pai + 1);
                                listneedpai.Add(pai + 2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai + 1);
                                listneedpai.Add(pai + 1);
                                listneedpai.Add(pai + 2);
                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai - 2);
                            }
                            else if (pai % 10 == 8)
                            {
                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai + 1);

                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai - 2);
                            }
                            else if (pai % 10 == 9)
                            {
                                listneedpai.Add(pai - 1);
                                listneedpai.Add(pai - 2);
                            }

                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 第一张需要一个癞子的 
        /// </summary>
        /// <param name="listallpai"></param>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="youxianduizi"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianduizi1(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }

                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                        listallpai.Remove(listsantiaomajiang[i].Value);
                        listallpai.Remove(listsantiaomajiang[i].Value);
                        listallpai.Remove(listsantiaomajiang[i].Value);
                    }
                }

                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];
                    if (youxianduizi)
                    {
                        if (listpai.Count(d => d == listpai[i]) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                return new List<int>();
                            }
                            listneedpai.Add(pai);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                    }
                    if (pai == listallpai[0])
                    {
                        //与当前牌组合需要一个癞子的牌
                        if (listpai.Contains(pai + 1) && (pai / 10) == ((pai + 1) / 10))
                        {
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            if (!youxianduizi)
                            {
                                var d = 0;
                            }
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<int>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if ((pai + 2) % 10 != 0)
                            {
                                listneedpai.Add(pai + 2);
                            }

                            if (1 < pai % 10 && pai % 10 < 9)
                            {
                                listneedpai.Add(pai - 1);
                            }
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                    }
                    else
                    {
                        if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                        {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            listpai.Remove(pai + 2);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                        }
                    }
                    if (listpai.Contains(pai + 1))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        if (!listpai.Contains(pai + 2) && pai / 10 == pai + 2)
                        {
                            listneedpai.Add(pai + 2);
                        }
                        if (pai % 10 >= 2)
                        {
                            listneedpai.Add(pai - 1);
                        }
                        return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai + 2);
                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        listneedpai.Add(pai + 1);

                        return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                    else if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                    {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                        listpai.Remove(pai);
                        needlaizi += 2;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        listneedpai.Add(pai);

                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }

                        return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao);
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 第一张不需要癞子的
        /// </summary>
        /// <param name="listallpai"></param>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="youxianduizi"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianduizi0(List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool isdelsantiao)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }
                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                    }
                }
                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];

                    if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }

                    if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        listpai.Remove(pai + 2);
                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    //与当前牌组合需要一个癞子的牌
                    if (listpai.Contains(pai + 1))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        if (!listpai.Contains(pai + 2) && pai / 10 == pai + 2)
                        {
                            listneedpai.Add(pai + 2);
                        }
                        if (pai % 10 >= 2)
                        {
                            listneedpai.Add(pai - 1);
                        }
                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai + 2);
                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        listneedpai.Add(pai + 1);

                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                    {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                        listpai.Remove(pai);
                        needlaizi += 2;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        listneedpai.Add(pai);

                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }

                        return BianLiyouxianduizi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 第一张不需要癞子的  倒叙
        /// </summary>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="isdelsantiao"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianduizi0Desc(List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool isdelsantiao)
        {
            //listallpai = listallpai.OrderByDescending(d => d).ToList();
            //listpai = listpai.OrderByDescending(d => d).ToList();

            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }
                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                    }
                }
                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];

                    if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            return new List<int>();
                        }
                        listneedpai.Add(pai);
                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }

                    if (listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        listpai.Remove(pai - 2);
                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    //与当前牌组合需要一个癞子的牌
                    if (listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        needlaizi += 1;

                        listneedpai.Add(pai - 2);
                        listneedpai.Add(pai + 1);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai - 2);
                        needlaizi += 1;
                        listneedpai.Add(pai - 1);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }
                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;

                        listneedpai.Add(pai);

                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                    {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                        listpai.Remove(pai);
                        needlaizi += 2;

                        listneedpai.Add(pai);

                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }
                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }

                        return BianLiyouxianduizi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 游戏遍历顺子
        /// </summary>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="isdelsantiao"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianshunzi0(List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool isdelsantiao)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }

                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                    }
                }

                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];

                    if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        listpai.Remove(pai + 2);
                        return BianLiyouxianshunzi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }

                    if (listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个

                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        needlaizi += 1;

                        if ((pai + 2) % 10 != 0)
                        {
                            listneedpai.Add(pai + 2);
                        }
                        if (pai % 10 >= 2)
                        {
                            listneedpai.Add(pai - 1);
                        }

                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {

                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai + 2);
                        needlaizi += 1;

                        listneedpai.Add(pai + 1);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        listneedpai.Add(pai);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                    {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个

                        if (pai == 12)
                        {
                            var x = "dddd";
                        }
                        listpai.Remove(pai);
                        needlaizi += 2;

                        listneedpai.Add(pai);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }

                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }
                        return BianLiyouxianshunzi0(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 优先遍历顺子倒叙
        /// </summary>
        /// <param name="listpai"></param>
        /// <param name="needlaizi"></param>
        /// <param name="listneedpai"></param>
        /// <param name="laizicount"></param>
        /// <param name="isdelsantiao"></param>
        /// <returns></returns>
        public static List<int> BianLiyouxianshunzi0Desc(List<int> listpai, ref int needlaizi, ref List<int> listneedpai, int laizicount, bool isdelsantiao)
        {
            //listallpai = listallpai.OrderByDescending(d => d).ToList();
            //listpai = listpai.OrderByDescending(d => d).ToList();
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<int>();
                    return new List<int>();
                }

                if (isdelsantiao)
                {
                    var listsantiaomajiang = GetAllSanTiao(listpai, 2);
                    for (int i = 0; i < listsantiaomajiang.Count; i++)
                    {
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);
                        listpai.Remove(listsantiaomajiang[i].Value);

                    }
                }

                for (int i = 0; i < listpai.Count; i++)
                {
                    var pai = listpai[i];

                    if (listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        listpai.Remove(pai - 2);
                        return BianLiyouxianshunzi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }

                    if (listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                    {
                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        needlaizi += 1;

                        listneedpai.Add(pai - 2);
                        listneedpai.Add(pai + 1);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {

                        //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                        listpai.Remove(pai);
                        listpai.Remove(pai - 2);
                        needlaizi += 1;

                        listneedpai.Add(pai - 1);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (listpai.Count(d => d == pai) == 2)
                    {
                        //如果是对子，则需要一个
                        listpai.Remove(pai);
                        listpai.Remove(pai);

                        needlaizi += 1;
                        listneedpai.Add(pai);
                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            // needlaizi -= 1;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                    else if (!listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                    {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个

                        listpai.Remove(pai);
                        needlaizi += 2;

                        listneedpai.Add(pai);

                        if (pai % 10 == 1)
                        {
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 == 2)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai + 2);
                        }
                        else if (pai % 10 > 2 && pai % 10 < 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);
                            listneedpai.Add(pai + 2);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 8)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai + 1);

                            listneedpai.Add(pai - 2);
                        }
                        else if (pai % 10 == 9)
                        {
                            listneedpai.Add(pai - 1);
                            listneedpai.Add(pai - 2);
                        }

                        if (needlaizi > laizicount + 1)
                        {
                            listneedpai = new List<int>();
                            //needlaizi -= 2;
                            return new List<int>();
                        }

                        return BianLiyouxianshunzi0Desc(listpai, ref needlaizi, ref listneedpai, laizicount, isdelsantiao);
                    }
                }
            }
            return new List<int>();
        }

        /// <summary>
        /// 找出所有对子
        /// </summary>
        /// <param name="data"></param>
        public static List<int> FindDuiZi(List<int> data)
        {
            //找到所有对子
            List<int> da = new List<int>();
            var arr = getRepareDataByNum(data, 1);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        public static List<RepeatInfo> GetAllSanTiao(List<int> data, int repet)
        {

            List<int> list = data.ToList();

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

        /// <summary>
        /// 根据repet 数量查找 =1 找对子 ； =2 找出三个相同的；=3 找出四个相同的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repet"></param>
        /// <returns></returns>
        public static List<RepeatInfo> getRepareDataByNum(List<int> data, int repet)
        {

            List<int> list = data;

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

        /// <summary>
        /// 找出所有顺子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindShunZi(List<int> data)
        {
            List<List<int>> li = new List<List<int>>();

            var listdispai = data.ToList().OrderByDescending(d => d).ToList();
            //查找单张
            var currentlist = listdispai.ToList();
            var listresult = (BianLiData(listdispai, currentlist, 1, li));

            return li;
        }


        private static List<int> BianLiData(List<int> data, List<int> currentlist, int i, List<List<int>> mtotal)
        {

            if (data.Count < 3 || currentlist.Count < 3)
            {
                return null;
            }
            if (mtotal == null)
            {
                mtotal = new List<List<int>>();
            }
            bool iscanshunzi = false;
            if (currentlist.Contains(currentlist[0]) && currentlist.Contains(currentlist[0] - 1) && currentlist.Contains(currentlist[0] - 2))
            {
                iscanshunzi = true;
            }


            if (iscanshunzi && i >= 1)
            {
                var currentnum = currentlist[0];
                var listint = new List<int>();

                listint.Add(currentlist[0]);

                listint.Add(currentnum - 1);
                listint.Add(currentnum - 2);


                currentlist.Remove(currentnum);
                currentlist.Remove(currentnum - 1);
                currentlist.Remove(currentnum - 2);
                if (listint != null && listint.Count > 0)
                {

                    mtotal.Add(listint);//第一个三张顺子

                }
                //  return BianLiData(data, i - 1);
            }
            else
            {
                if (currentlist.Count > 1)
                {
                    currentlist.RemoveRange(0, 1);
                }
            }


            BianLiData(data, currentlist, i, mtotal);

            return null;
        }


        public static List<int> FindSantiaoShunzi(List<int> listpai, List<int> willdelshunzi, out List<int> dellist)
        {
            listpai = listpai.ToList();
            dellist = new List<int>();

            if (willdelshunzi != null && willdelshunzi.Count > 0)
            {
                for (int i = 0; i < willdelshunzi.Count; i++)
                {
                    listpai.Remove(willdelshunzi[i]);//手牌中移除的顺子 ，计算出余下的牌

                    dellist.Add(willdelshunzi[i]);
                }
            }


            var listyu = listpai;
            return listyu;

        }


        /// <summary>
        /// 找出所有顺子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<List<int>> FindShunZi1(List<int> data)
        {
            List<List<int>> li = new List<List<int>>();

            var listdispai = data.Distinct().ToList().OrderBy(d => d).ToList();
            //查找单张
            if (listdispai.Count >= 3)
            {
                for (int i = 0; i < listdispai.Count; i++)
                {
                    var currentlist = new List<int>();
                    currentlist.AddRange(listdispai);
                    var listresult = (BianLiData1(listdispai, currentlist, listdispai[i], li));

                }
            }
            return li;
        }


        private static List<int> BianLiData1(List<int> data, List<int> currentlist, int currentpai, List<List<int>> mtotal)
        {
            var listint = new List<int>();
            if (currentlist.Contains(currentpai + 1) && currentlist.Contains(currentpai + 2))
            {
                listint.Add(currentpai);
                listint.Add(currentpai + 1);
                listint.Add(currentpai + 2);
            }
            if (listint.Count > 0)
            {
                mtotal.Add(listint);
            }
            return listint;
        }

        /// <summary>
        /// 是否是整扑
        /// </summary>
        /// <param name="huaselist"></param>
        /// <returns></returns>
        private static List<int> IsPu(List<int> huaselist, ref int needcount, int laizicount, List<int> listall)
        {
            List<int> CanTinglist = new List<int>();
            if (huaselist.Count <= 0)
            {
                return CanTinglist;
            }

            if (needcount > laizicount + 1)
            {
                return CanTinglist;
            }
            //优先刻字 顺子

            var listneedpai = new List<int>();
            List<int> templist = new List<int>(huaselist);

            var listdel = new List<int>();

            //>30 的三个的先移除

            var list30 = templist.Where(d => d > 30).ToList();

            var listsantiaomajiang = GetAllSanTiaos(list30, 3);
            if (listsantiaomajiang != null && listsantiaomajiang.Count > 0)
            {
                for (int i = 0; i < listsantiaomajiang.Count; i++)
                {
                    templist.Remove(listsantiaomajiang[i]);
                    templist.Remove(listsantiaomajiang[i]);
                    templist.Remove(listsantiaomajiang[i]);
                }
            }

            //如果单>30 的 有几个就需要几个*2的癞子
            list30 = templist.Where(d => d > 30).ToList();
            var listlastduizi = GetAllSanTiaos(list30, 2);//剩下牌找>30 的，有几个对子，就需要几个 癞子来组合成整铺
            for (int i = 0; i < listlastduizi.Count; i++)
            {
                listneedpai.Add(listlastduizi[i]);
            }
            needcount += listlastduizi.Count;
            //给对子移除，剩下牌就都是单张，单张找顺子

            for (int i = 0; i < listlastduizi.Count; i++)
            {
                templist.Remove(listlastduizi[i]);
                templist.Remove(listlastduizi[i]);
            }
            //如果单>30 的 有几个就需要几个*2的癞子
            list30 = templist.Where(d => d > 30).ToList();
            for (int i = 0; i < list30.Count; i++)
            {
                listneedpai.Add(list30[i]);
            }
            needcount += list30.Count * 2;

            templist = templist.Where(d => d < 30).ToList();

            var listzuijia = new List<ZuiJiaTingPai>();

            var listlast1 = templist.ToList();//30 以内的找顺子 或刻字


            int tempneedcount = needcount;
            var tempneedlistpai = new List<int>();

            tempneedcount = needcount;
            tempneedlistpai = new List<int>();
            BianLiyouxianduizi0(listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true);


            var zuijiamodel0delsantiao = new ZuiJiaTingPai();
            zuijiamodel0delsantiao.needcount = tempneedcount;
            zuijiamodel0delsantiao.needlistpai = tempneedlistpai;
            listzuijia.Add(zuijiamodel0delsantiao);


            listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
            tempneedcount = needcount;
            tempneedlistpai = new List<int>();
            BianLiyouxianduizi0Desc(listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true);


            var zuijiamodel0delsantiaoDesc = new ZuiJiaTingPai();
            zuijiamodel0delsantiaoDesc.needcount = tempneedcount;
            zuijiamodel0delsantiaoDesc.needlistpai = tempneedlistpai;
            listzuijia.Add(zuijiamodel0delsantiaoDesc);


            listlast1 = templist.ToList();//30 以内的找顺子 或刻字
            tempneedcount = needcount;
            tempneedlistpai = new List<int>();
            BianLiyouxianshunzi0(listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true);


            var zuijiamodel5 = new ZuiJiaTingPai();
            zuijiamodel5.needcount = tempneedcount;
            zuijiamodel5.needlistpai = tempneedlistpai;
            listzuijia.Add(zuijiamodel5);


            listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
            tempneedcount = needcount;
            tempneedlistpai = new List<int>();
            BianLiyouxianshunzi0Desc(listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true);


            var zuijiamodel5Desc = new ZuiJiaTingPai();
            zuijiamodel5Desc.needcount = tempneedcount;
            zuijiamodel5Desc.needlistpai = tempneedlistpai;
            listzuijia.Add(zuijiamodel5Desc);



            listzuijia = listzuijia.OrderByDescending(d => d.needlistpai.Count).OrderBy(d => d.needcount).ToList();
            if (listzuijia.Count > 0)
            {
                var zuijiacount = listzuijia.FirstOrDefault().needcount;

                var listZuiJiaCount = listzuijia.Where(d => d.needcount == zuijiacount).ToList();

                for (int i = 0; i < listZuiJiaCount.Count; i++)
                {
                    listneedpai.AddRange(listzuijia[i].needlistpai);
                }
                needcount = zuijiacount;
            }
            //}

            // Logger.Log("牌 " + huaselist.ToJson() + " 优先刻字、顺子 将牌为：" + jiang + "需要的癞子数量为 " + needcount1 + "需要的牌 " + listneedpai1.ToJson());
            listneedpai = listneedpai.Distinct().ToList();
            CanTinglist = listneedpai;

            return CanTinglist;
        }
        #endregion


        /// <summary>
        /// 能否听牌
        /// </summary>
        /// <param name="allpai">所有牌</param>
        /// <param name="laizi">癞子</param>
        /// <returns></returns>
        public static List<int> isCanting(List<int> allpai, int laizi, int hunzi = 0)
        {

            List<int> CanTinglist = new List<int>();

            List<int> fs = allpai.FindAll(delegate (int a)
            {
                return laizi == a || hunzi == a;
            });
            //当混子数量大于等于普通牌数量的时候为绝对任意牌
            if (fs.Count == allpai.Count)
            {
                CanTinglist.Add(55);
            }

            List<int> nolzlist = allpai.FindAll(delegate (int a)
            {
                return laizi != a && hunzi != a;
            });
            //只按将取来提升效率
            var listjiang = allpai.Distinct().ToList();
            for (int i = 0; i < listjiang.Count; i++)
            {
                List<int> paiT = new List<int>(nolzlist);
                var jiang = listjiang[i];

                //                 if (jiang == 13)
                //                 {
                //                     var d = 0;
                //                 }
                bool isonlyone = false;
                var jiangCount = paiT.Count(d => d == jiang);
                if (jiangCount >= 2)
                {
                    paiT.Remove(jiang);
                    paiT.Remove(jiang);
                }
                else if (jiangCount >= 1)
                {
                    paiT.Remove(jiang);
                    isonlyone = true;
                }

                ////避免重复运算 将光标移到其他牌上
                //i += ds.Count;
                //需要的 癞子个数

                var sumcount = 0;
                //拿到各种花色的牌
                //                 if (jiang == 22)//测bug用
                //                 {
                //                     var d = 0;
                //                 }
                //增加总和数量来减少遍历次数
                var wanlist = IsPu(GetPaiXing(PaiXing.WAN, paiT), ref sumcount, fs.Count, nolzlist.ToList());
                var tonglist = IsPu(GetPaiXing(PaiXing.TONG, paiT), ref sumcount, fs.Count, nolzlist.ToList());
                var tiaolist = IsPu(GetPaiXing(PaiXing.TIAO, paiT), ref sumcount, fs.Count, nolzlist.ToList());
                var zilist = IsPu(GetPaiXing(PaiXing.ZI, paiT), ref sumcount, fs.Count, nolzlist.ToList());

                if (isonlyone)
                {//如果只有一个将，则，再需要一个癞子
                    sumcount = sumcount + 1;
                }
                //判断花色牌是否是整扑
                if (sumcount > fs.Count + 1)
                {
                    // Logger.Log("将为 " + pai + " 不满足");
                    continue;
                }
                else if (sumcount < fs.Count)
                {
                    CanTinglist = new List<int>();
                    CanTinglist.Add(55);
                }
                else
                {
                    //满足胡的条件， 得出需要的牌
                    var listcurrent = new List<int>();
                    if (isonlyone)
                    {
                        listcurrent.Add(jiang);
                    }

                    if (wanlist.Count > 0)
                    {
                        listcurrent.AddRange(wanlist);
                    }
                    if (tonglist.Count > 0)
                    {
                        listcurrent.AddRange(tonglist);
                    }
                    if (tiaolist.Count > 0)
                    {
                        listcurrent.AddRange(tiaolist);
                    }
                    if (zilist.Count > 0)
                    {
                        listcurrent.AddRange(zilist);
                    }
                    listcurrent = listcurrent.Distinct().ToList();
                    CanTinglist.AddRange(listcurrent);
                    CanTinglist = CanTinglist.Distinct().ToList();
                }
            }
            CanTinglist = CanTinglist.Distinct().ToList();
            if (CanTinglist.Contains(55))
            {
                CanTinglist = new List<int>();
                CanTinglist.Add(55);
            }
            CanTinglist.RemoveAll(d => d % 10 == 0);
            CanTinglist.Sort();
            return CanTinglist;
        }

        /// <summary>
        /// 牌类型
        /// </summary>
        public enum PaiXing
        {
            WAN,
            TIAO,
            TONG,
            ZI
        }

        /// <summary>
        /// 获取牌型
        /// </summary>
        /// <param name="pai"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<int> GetPaiXing(PaiXing pai, List<int> pais)
        {
            List<int> temp = new List<int>();
            switch (pai)
            {
                case PaiXing.WAN:
                    temp = pais.FindAll(delegate (int d)
                    {
                        return d > 20 && d < 30;
                    });
                    break;
                case PaiXing.TIAO:
                    temp = pais.FindAll(delegate (int d)
                    {
                        return d > 10 && d < 20;
                    });
                    break;
                case PaiXing.TONG:
                    temp = pais.FindAll(delegate (int d)
                    {
                        return d > 0 && d < 10;
                    });
                    break;
                case PaiXing.ZI:
                    temp = pais.FindAll(delegate (int d)
                    {
                        return d > 30 && d < 46;
                    });
                    break;
                default:
                    break;
            }
            return temp;
        }

        public static List<int> GetAllSanTiaos(List<int> data, int dianCount)
        {
            var listRtn = new List<int>();
            for (int i = 0; i < data.Count; i++)
            {
                List<int> fs = data.FindAll(delegate (int a)
                {
                    return data[i] == a;
                });
                if (fs.Count >= dianCount)
                {
                    listRtn.Add(data[i]);
                }
            }

            listRtn = listRtn.Distinct().ToList();
            return listRtn;
        }

        public class ZuiJiaTingPai
        {
            public int needcount { get; set; }
            public List<int> needlistpai { get; set; }
        }

        public class RepeatInfo
        {
            // 值
            public int Value { get; set; }
            // 重复次数
            public int RepeatNum { get; set; }
        }
    }
}
