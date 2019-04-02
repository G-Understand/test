using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    public class DouNiuPdType
    {

        public static Dictionary<string, int> huaseCom = new Dictionary<string, int>();

        public static void Init()
        {
            huaseCom = new Dictionary<string, int>() {

                    {"a",10},
                    {"b",9},
                    {"c",8},
                    {"d",7},
                 {"e",6}

             };
        }

        /// <summary>
        /// 比较花色
        /// </summary>
        /// <param name="zjcard"></param>
        /// <param name="usercard"></param>
        /// <returns></returns>
        public static bool ComepareHuaSe(List<PDKTypeData> zjcard, List<PDKTypeData> usercard)
        {
            List<int> val = new List<int>();
            foreach (var item in zjcard)
            {
                val.Add(huaseCom[item.h]);
            }

            List<int> us = new List<int>();
            foreach (var item in usercard)
            {
                us.Add(huaseCom[item.h]);
            }

            return val.Max() > us.Max() ? true : false;

        }

        /// <summary>
        /// 比较大小...
        /// </summary>
        /// <returns></returns>
        public static bool ComareCard(DouNiuType zj, List<PDKTypeData> zjcard, DouNiuType user, List<PDKTypeData> usercard)
        {
            if (zjcard.Count < 5 || usercard.Count < 5)
            {
                return false;
            }
            zjcard = zjcard.ToList();
            usercard = usercard.ToList();
            //var linshizjcard = zjcard.ToList();
            //linshizjcard = linshizjcard.Where(d => d.v != 14).OrderByDescending(d => d.v).OrderByDescending(d=>d.h).ToList();
            //var zjfirstpai = linshizjcard.FirstOrDefault();
            //linshizjcard.RemoveAll(d => d.v == zjfirstpai.v && d.h == zjfirstpai.h);
            //linshizjcard.Insert(0, zjfirstpai);

            //zjcard = linshizjcard.ToList();

            //var linshiusercard = usercard.ToList();
            //linshiusercard = linshiusercard.Where(d => d.v != 14).OrderByDescending(d => d.v).OrderByDescending(d=>d.h).ToList();
            //var userfirstpai = linshiusercard.FirstOrDefault();
            //linshiusercard.RemoveAll(d => d.v == userfirstpai.v && d.h == userfirstpai.h);
            //linshiusercard.Insert(0, userfirstpai);

            //usercard = linshiusercard.ToList();
            //如果都是欢乐牛==>,直接比较大小

            if (zj == DouNiuType.炸弹牛)
            {
                //如果是炸弹牛
                var sdata = zjcard.GroupBy(r => r.v).Where(g => g.Count() >= 4).ToList();
                var sdata1 = usercard.GroupBy(r => r.v).Where(g => g.Count() >= 4).ToList();
                if (sdata[0].ToList()[0].v == 14)
                {
                    return false;
                }
                if (sdata[0].ToList()[0].v > sdata1[0].ToList()[0].v)
                {
                    return true;
                }
                return false;
            }
            else if (zj == DouNiuType.葫芦牛)
            {
                var sdata = zjcard.GroupBy(r => r.v).Where(g => g.Count() == 3).ToList();
                var sdata1 = usercard.GroupBy(r => r.v).Where(g => g.Count() == 3).ToList();
                if (sdata[0].ToList()[0].v == 14)
                {
                    return false;
                }
                if (sdata[0].ToList()[0].v > sdata1[0].ToList()[0].v)
                {
                    return true;
                }
                return false;

            }
            else if (zj == DouNiuType.同花牛)
            {

                zjcard.RemoveAll(m => m.v == 14);
                usercard.RemoveAll(m => m.v == 14);
                int zmaxv = zjcard.Max(m => m.v); //查找出庄家最大的值
                int usermax = usercard.Max(m => m.v);
                if (zmaxv > usermax)
                {
                    return true;
                }
                else if (zmaxv < usermax)
                {
                    return false;
                }
                else  //相等比花色
                {

                    return ComepareHuaSe(zjcard, usercard);
                }

            }
            else if (zj == DouNiuType.银牛 || zj == DouNiuType.金牛)
            {

                int zmaxv = zjcard.Max(m => m.v); //查找出庄家最大的值
                int usermax = usercard.Max(m => m.v);
                if (zmaxv > usermax)
                {
                    return true;
                }
                else if (zmaxv < usermax)
                {
                    return false;
                }
                else  //相等比花色
                {
                    var zjhuase = zjcard.FindAll(m => m.v == zmaxv);
                    var userhuase = usercard.FindAll(m => m.v == usermax);  //获取花色了....
                    return ComepareHuaSe(zjhuase, userhuase);
                }


            }
            else if (zj == DouNiuType.顺子牛)
            {

                zjcard.RemoveAll(m => m.v == 14);
                usercard.RemoveAll(m => m.v == 14);
                if (zjcard[0].v > usercard[0].v)
                {
                    return true;
                }
                else if (zjcard[0].v < usercard[0].v)
                {
                    return false;
                }
                else  //相等了==>
                {
                    if (huaseCom[zjcard[0].h] > huaseCom[usercard[0].h])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //--------------------------------
            else if (zj == DouNiuType.五小牛)
            {

                int zmaxv = zjcard.Max(m => m.v); //查找出庄家最大的值
                int usermax = usercard.Max(m => m.v);
                if (zmaxv > usermax)
                {
                    return true;
                }
                else if (zmaxv < usermax)
                {
                    return false;
                }
                else  //相等比花色
                {
                    //var listpaizj = new List<PDKTypeData>();
                    //listpaizj.Add(zjfirstpai);

                    //var listpaiuser = new List<PDKTypeData>();
                    //listpaiuser.Add(userfirstpai);


                    var zjhuase = zjcard.FindAll(m => m.v == zmaxv);
                    var userhuase = usercard.FindAll(m => m.v == usermax);  //获取花色了....
                    return ComepareHuaSe(zjhuase, userhuase);
                }
            }
            //------------------------------------------------------
            if (zj.getInt() < 11) //牌是一样的,然后判断哪家的大
            {
                zjcard.RemoveAll(m => m.v == 14);
                usercard.RemoveAll(m => m.v == 14);
                int zmaxv = zjcard.Max(m => m.v); //查找出庄家最大的值
                int usermax = usercard.Max(m => m.v);
                if (zmaxv > usermax)
                {
                    return true;
                }
                else if (zmaxv < usermax)
                {
                    return false;
                }
                else  //相等比花色
                {
                    var zjhuase = zjcard.FindAll(m => m.v == zmaxv);
                    var userhuase = usercard.FindAll(m => m.v == usermax);  //获取花色了....
                    return ComepareHuaSe(zjhuase, userhuase);
                }
            }
            //普通比较完成  没牛==>牛9

            Logger.Log(typeof(DouNiuPdType), "没有适当的牌型!");
            return false;
        }

        /// <summary>
        /// 判断是否顺子牛
        /// </summary>
        /// <returns></returns>
        public static bool is顺子牛(List<PDKTypeData> pktype)
        {
            bool isShunZiNiu = false;
            if (pktype.Count < 5)
            {
                return false;
            }

            if (FindRuanShunZi(pktype).Count > 0)
            {
                isShunZiNiu = true;
            }
            return isShunZiNiu;
        }
        /// <summary>
        /// 判断是否五小牛
        /// </summary>
        /// <returns></returns>
        public static bool is五小牛(List<PDKTypeData> pktype)
        {
            var isWuXiaoNiu = false;
            if (pktype.Count < 5)
            {
                return false;
            }

            var listNoHunZiPai = pktype.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            if (listNoHunZiPai.Count(d => d.v >= 5) > 0)
            {
                return false;
            }

            if (listNoHunZiPai.Sum(d => d.v) <= 10)
            {
                isWuXiaoNiu = true;
            }
            return isWuXiaoNiu;
        }

        /// <summary>
        /// 判断是不是五花牛
        /// </summary>
        /// <param name="pktype"></param>
        /// <returns></returns>
        public static bool is五花牛(List<PDKTypeData> pktype)
        {
            bool isWuHuaNiu = false;

            var listNoHunZiPai = pktype.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();
            if (pktype.Count < 5)
            {
                return false;
            }
            if (listNoHunZiPai.Count(d => d.v <= 10) <= 0)
            {
                //有 <=10 的牌 都不算五花牛了
                isWuHuaNiu = true;
            }

            return isWuHuaNiu;
        }

        /// <summary>
        /// 判断是不是同花牛
        /// </summary>
        /// <param name="pktype"></param>
        /// <returns></returns>
        public static bool is同花牛(List<PDKTypeData> pktype)
        {
            bool isTongHuaNiu = false;
            if (pktype.Count <= 0)
            {
                return false;
            }
            var listNoHunZiPai = pktype.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();
            if (listNoHunZiPai.Select(d => d.h).Distinct().Count() == 1)
            {
                isTongHuaNiu = true;
            }

            return isTongHuaNiu;
        }

        /// <summary>
        /// 判断是否是葫芦牛
        /// </summary>
        /// <returns></returns>
        public static bool is葫芦牛(List<PDKTypeData> pktype)
        {
            bool isHuLuNiu = false;
            if (pktype.Count < 5)
            {
                return false;
            }
            if (FindRuanSanDaiEr(pktype).Count > 0)
            {
                isHuLuNiu = true;
            }
            return isHuLuNiu;
        }

        /// <summary>
        /// 判断是否是炸弹牛
        /// </summary>
        /// <param name="pktype"></param>
        /// <returns></returns>
        public static bool is炸弹牛(List<PDKTypeData> pktype)
        {
            bool isZhaDanNiu = false;
            if (pktype.Count < 5)
            {
                return false;
            }

            if (FindRuanZhaDan(pktype).Count > 0)
            {
                isZhaDanNiu = true;
            }

            return isZhaDanNiu;
        }

        /// <summary>
        /// 判断斗牛的牌型
        /// </summary>
        /// <param name="pktype"></param>
        /// <returns></returns>
        public static DouNiuType getNiuNiu(List<PDKTypeData> pktype)
        {
            try
            {
                var listHunZiPai = pktype.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
                var listNoHunZiPai = pktype.Where(d => hunzi.Count(x => x == d.v) <= 0).ToList();

                //if (guize.huanleniu)
                {
                    if (is顺子牛(pktype) && is同花牛(pktype))
                    {
                        return DouNiuType.欢乐牛;
                    }
                }
                //if (guize.shunziniu)
                {
                    if (is顺子牛(pktype))
                    {
                        if (pktype.Where(d => d.v < 20).Count(d => d.v > 5 && d.v != 14) <= 0)
                        {
                            return DouNiuType.一条龙;
                        }
                    }
                }
                //if (guize.zhadanniu)
                {
                    if (is炸弹牛(pktype))
                    {
                        return DouNiuType.炸弹牛;
                    }
                }
                //if (guize.wuxiaoniu)
                {
                    if (is五小牛(pktype))
                    {
                        //  Console.WriteLine("五小牛====比欢乐牛小============================");
                        return DouNiuType.五小牛;
                    }
                }


                //if (guize.huluniu)
                {
                    if (is葫芦牛(pktype))
                    {
                        return DouNiuType.葫芦牛;
                    }
                }
                //if (guize.wuhuaniu)
                {
                    if (is五花牛(pktype))
                    {
                        if (pktype.Count(d => d.v == 10) <= 0)
                        {
                            return DouNiuType.金牛;
                        }
                        //return DouNiuType.五花牛;
                    }
                }
                //if (guize.tonghuaniu)
                {
                    if (is同花牛(pktype))
                    {
                        return DouNiuType.同花牛;
                    }
                }
                //if (guize.wuhuaniu)
                {
                    if (is五花牛(pktype))
                    {
                        if (pktype.Count(d => d.v == 10) <= 0)
                        {
                            return DouNiuType.金牛;
                        }
                        else
                        {
                            return DouNiuType.银牛;
                        }
                        //return DouNiuType.五花牛;
                    }
                }
                //if (guize.shunziniu)
                {
                    if (is顺子牛(pktype))
                    {
                        if (pktype.Where(d => d.v < 20).Count(d => d.v > 5 && d.v != 14) <= 0)
                        {
                            return DouNiuType.一条龙;
                        }
                        else
                        {
                            return DouNiuType.顺子牛;
                        }
                    }
                }

                //                 if (FindTianNiu(pktype).Count > 0)
                //                 {
                //                     return DouNiuType.天牛;
                //                 }
                // 
                //                 if (FindDiNiu(pktype).Count > 0)
                //                 {
                //                     return DouNiuType.地牛;
                //                 }

                if (listHunZiPai.Count <= 0)
                {
                    #region 没有混子 基本牌算 牛
                    List<int> li = new List<int>();
                    foreach (var item in pktype)
                    {
                        if (item.v == 14)
                        {
                            li.Add(1);
                        }
                        else if (item.v == 15)
                        {
                            li.Add(2);
                        }
                        else if (item.v > 10)
                        {
                            li.Add(10);
                        }
                        else
                        {
                            li.Add(item.v);
                        }
                    }

                    int niuniu = isNiuNum(li);
                    if (niuniu == -1)
                    {
                        return DouNiuType.没牛;
                    }

                    var yi = (DouNiuType)Enum.Parse(typeof(DouNiuType), niuniu.ToString());
                    return yi;
                    #endregion
                }
                else if (listHunZiPai.Count == 1)
                {
                    var outListPai = new List<PDKTypeData>();
                    return GetHunNiuType(listHunZiPai, listNoHunZiPai, out outListPai);
                }
                else if (listHunZiPai.Count == 2)
                {
                    return DouNiuType.牛牛;
                    #region 2个混子 （2个混子加1个牌凑成牛，剩余两个牌之和%10 余数是多少 就是牛几了）
                    var listYuShu = new List<int>();

                    for (int i = 0; i < listNoHunZiPai.Count; i++)
                    {
                        for (int j = i + 1; j < listNoHunZiPai.Count; j++)
                        {
                            var paiDian1 = GetPaiDian(listNoHunZiPai[i]);
                            var paiDian2 = GetPaiDian(listNoHunZiPai[j]);

                            var yuShu = (paiDian1 + paiDian2) % 10;
                            if (listYuShu.Count(d => d == yuShu) <= 0)
                            {
                                listYuShu.Add(yuShu);
                            }
                        }
                    }
                    listYuShu = listYuShu.OrderBy(d => d).ToList();
                    if (listYuShu.FirstOrDefault() == 0)
                    {
                        return DouNiuType.牛牛;
                    }
                    else
                    {
                        return GetNiuNum(listYuShu.LastOrDefault());
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
            }
            return DouNiuType.没牛;
        }

        public static DouNiuType GetHunNiuType(List<PDKTypeData> listHunZiPai, List<PDKTypeData> listNoHunZiPai, out List<PDKTypeData> outListPai)
        {
            outListPai = new List<PDKTypeData>();
            if (listHunZiPai.Count == 1)
            {
                var listAllPai = listNoHunZiPai.ToList();
                listAllPai.AddRange(listHunZiPai);

                //优先找 天牛 地牛 

                var listTianNiu = FindTianNiu(listAllPai);

                var listDiNiu = FindDiNiu(listAllPai);

                //                 if (listTianNiu.Count > 0)
                //                 {
                //                     var listOutPdk = new List<PDKTypeData>();
                // 
                //                     NiuNiu4Pk(listNoHunZiPai, out listOutPdk);
                // 
                //                     listOutPdk.AddRange(listHunZiPai);
                //                     outListPai = listOutPdk;
                //                     return DouNiuType.天牛;
                //                 }
                //                 else if (listDiNiu.Count > 0)
                //                 {
                //                     var listOutPdk = new List<PDKTypeData>();
                // 
                //                     NiuNiu4Pk(listNoHunZiPai, out listOutPdk);
                // 
                //                     listOutPdk.AddRange(listHunZiPai);
                //                     outListPai = listOutPdk;
                //                     return DouNiuType.地牛;
                //                 }

                #region 一个混子 （一个混子加两个牌凑成牛，剩余两个牌之和%10 余数是多少 就是牛几了）
                var listYuShu = new List<YuShuModel>();
                for (int i = 0; i < listNoHunZiPai.Count; i++)
                {
                    for (int j = i + 1; j < listNoHunZiPai.Count; j++)
                    {
                        var paiDian1 = GetPaiDian(listNoHunZiPai[i]);
                        var paiDian2 = GetPaiDian(listNoHunZiPai[j]);
                        var yuShu = (paiDian1 + paiDian2) % 10;
                        if (listYuShu.Count(d => d.type == yuShu) <= 0)
                        {
                            var listOtherPai = listNoHunZiPai.Where(d => !(d.v == listNoHunZiPai[i].v && d.h == listNoHunZiPai[i].h) && !(d.v == listNoHunZiPai[j].v && d.h == listNoHunZiPai[j].h)).ToList();//找到其余两张牌要跟王 拼牛，
                            listOtherPai.Add(listHunZiPai[0]);
                            listOtherPai.Add(listNoHunZiPai[i]);
                            listOtherPai.Add(listNoHunZiPai[j]);

                            YuShuModel yuShuModel = new YuShuModel();
                            yuShuModel.type = yuShu;
                            yuShuModel.listPai = listOtherPai.ToList();
                            listYuShu.Add(yuShuModel);
                        }
                    }
                }
                listYuShu = listYuShu.OrderBy(d => d.type).ToList();
                if (listYuShu.FirstOrDefault().type == 0)
                {
                    outListPai = listYuShu.FirstOrDefault().listPai;
                    return DouNiuType.牛牛;
                }
                else
                {
                    var niuType = DouNiuType.没牛;
                    niuType = GetNiuNum(listYuShu.LastOrDefault().type);
                    outListPai = listYuShu.LastOrDefault().listPai;
                    return niuType;
                }
                #endregion
            }
            else if (listHunZiPai.Count == 2)
            {
                // 识别 是否是金牛
                if (listNoHunZiPai.Count == 3 && listHunZiPai.Count == 2)
                {
                    //                     if ((GetPaiDian(listNoHunZiPai[0]) + GetPaiDian(listNoHunZiPai[1]) + GetPaiDian(listNoHunZiPai[2])) % 10 == 0)
                    //                     {
                    //                         PaiData paiData = new PaiData();
                    //                         listNoHunZiPai.AddRange(listHunZiPai.ToList());
                    //                         paiData.listPai = listNoHunZiPai;
                    //                         paiData.HunCount = 2;
                    //                         //paiData.type = DouNiuType.金牛;
                    //                         paiData.type = DouNiuType.牛牛;
                    //                         paiData.PaiValue = 54;
                    //                         outListPai = listNoHunZiPai;
                    // 
                    //                         return DouNiuType.金牛;
                    //                     }
                    //                     else
                    //                     {
                    //                         // 其中一个王和 其他牌 拼成牛  ，大王 和 剩余一张牌 组合 变为  天牛
                    //                         PaiData paiData = new PaiData();
                    //                         var rtnListPai = listNoHunZiPai.Take(2).ToList();
                    //                         rtnListPai.Add(listHunZiPai[0]);
                    //                         rtnListPai.AddRange(listNoHunZiPai.Skip(2).ToList());
                    //                         rtnListPai.Add(listHunZiPai[1]);
                    // 
                    //                         paiData.listPai = rtnListPai;
                    //                         paiData.HunCount = 2;
                    //                         paiData.type = DouNiuType.天牛;
                    //                         paiData.PaiValue = 54;
                    //                         outListPai = rtnListPai;
                    // 
                    //                         return DouNiuType.天牛;
                    //                     }
                }
            }

            return DouNiuType.没牛;
        }

        public class YuShuModel
        {

            public int type { get; set; }
            public List<PDKTypeData> listPai = new List<PDKTypeData>();
        }

        public static DouNiuType GetNiuNum(int yuShu)
        {
            DouNiuType type = DouNiuType.没牛;
            switch (yuShu)
            {
                case 0:
                    type = DouNiuType.牛牛;
                    break;

                case 1:
                    type = DouNiuType.牛1;
                    break;
                case 2:
                    type = DouNiuType.牛2;
                    break;
                case 3:
                    type = DouNiuType.牛3;
                    break;
                case 4:
                    type = DouNiuType.牛4;
                    break;
                case 5:
                    type = DouNiuType.牛5;
                    break;
                case 6:
                    type = DouNiuType.牛6;
                    break;
                case 7:
                    type = DouNiuType.牛7;
                    break;
                case 8:
                    type = DouNiuType.牛8;
                    break;
                case 9:
                    type = DouNiuType.牛9;
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 判断是不是五花牛
        /// </summary>
        /// <param name="pktype"></param>
        /// <returns></returns>
        public static bool isWuHuaNiu(List<PDKTypeData> pktype)
        {
            var list = pktype.FindAll(m => m.v > 10);
            if (list.Count == 5)
            {
                return true;
            }
            return false;
        }

        //判断是牛几
        public static int isNiuNum(List<int> ps)
        {
            if (ps == null)
            {
                return -1;
            }
            if (ps.Count < 5)
            {
                return -1;
            }
            List<int> n = new List<int>() { 0, 0, 0, 0, 0 };
            for (int i = 0; i < ps.Count; i++)
            {
                if (ps[i] > 10)
                {
                    n[i] = 10;
                }
                else
                {
                    n[i] = ps[i];
                }
            }
            Dictionary<string, bool> map = NiuNiu(n);
            if (map["isNiuNiu"])
            {
                return 10;
            }
            if (map["isNiuNum"])
            {
                int num = 0;
                foreach (var item in n)
                {
                    num += item;

                }
                return num % 10;
            }
            else
            {
                return -1;
            }
        }
        public static Dictionary<string, bool> NiuNiu(List<int> i)
        {
            bool isNiuNum = false;
            bool isNiuNiu = false;
            for (int m = 0; m <= 2; m++)
            {
                for (int n = m + 1; n <= 3; n++)
                {
                    for (int z = n + 1; z <= 4; z++)
                    {
                        if ((i[m] + i[n] + i[z]) % 10 == 0)
                        {
                            isNiuNum = true;
                            int num = 0;
                            for (int x = 0; x <= 4; x++)
                            {
                                if (x != m && x != n && x != z)
                                {
                                    num += i[x];
                                }
                            }
                            if (num % 10 == 0)
                            {
                                isNiuNiu = true;
                            }
                        }
                    }
                }
            }
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            result.Add("isNiuNum", isNiuNum);
            result.Add("isNiuNiu", isNiuNiu);
            return result;
        }


        public static bool isHun顺子牛(List<PDKTypeData> pktype)
        {
            bool rtn = false;



            return rtn;
        }
        [Serializable]
        public class PaiData
        {
            public DouNiuType type = DouNiuType.没牛;
            public List<PDKTypeData> listPai = new List<PDKTypeData>();
            /// <summary>
            /// 牛几中的最大值
            /// </summary>
            public int PaiValue { get; set; }
            /// <summary>
            /// 利用混子的个数
            /// </summary>
            public int HunCount { get; set; }
        }

        #region 带混 找顺子

        /// <summary>
        /// 判断是否是顺子
        /// </summary>
        /// <param name="listchupai">出牌中排除了 混的牌的点</param>
        /// <param name="hunList"></param>
        /// <returns></returns>
        public static List<int> GetRuanShunzi(List<int> listchupai, List<PDKTypeData> hunList)
        {
            listchupai = listchupai.OrderBy(d => d).ToList();
            var listshunzi = new List<int>();
            if (listchupai.Count < 3)
            {
                return listshunzi;
            }
            var clons = new List<int>(listchupai);
            int hunNum = hunList.Count;
            for (int i = 0; i < clons.Count - 1; i++)
            {

                if (clons[i] == clons[i + 1])
                {
                    listshunzi = new List<int>();
                    return listshunzi;
                }
                else
                {
                    //if (clons[i] == hunList[0].v)
                    //{
                    //    continue;
                    //}
                    listshunzi.Add(clons[i]);
                    if (clons[i] + 1 != clons[i + 1])
                    {
                        hunNum--;
                        if (hunNum < 0)
                        {
                            //混子超支了 
                            return new List<int>();
                        }
                        listshunzi.Add(listshunzi.LastOrDefault() + 1);

                        //如果下一张还不包含，则再添加下一张
                        if (listshunzi.LastOrDefault() + 1 != clons[i + 1])
                        {
                            hunNum--;
                            listshunzi.Add(listshunzi.LastOrDefault() + 1);
                        }
                    }
                    if (i == clons.Count - 2)
                    {
                        //轮训完了，如果混子还充足，识别看能否满足 
                        if (hunNum == 0)
                        {
                            if (listshunzi.LastOrDefault() + 1 == clons[i + 1])
                            {
                                listshunzi.Add(clons[i + 1]);
                            }
                            else
                            {
                                listshunzi = new List<int>();
                            }
                            //走完了 返回列表
                            return listshunzi;
                        }
                        else if (hunNum == 1)
                        {
                            if (clons[i + 1] == 14)
                            {
                                listshunzi.Insert(0, clons[i + 1] - 4); //最后一个数为 A 则加混子充当 10
                                listshunzi.Add(14);
                            }
                            else
                            {
                                //否则，混子往后
                                if (listshunzi.LastOrDefault() + 1 == clons[i + 1])
                                {
                                    listshunzi.Add(clons[i + 1]);
                                }
                                else
                                {
                                    listshunzi.Add(listshunzi.LastOrDefault() + 1);
                                }
                                if (listshunzi.Count == 4)//如果只有三个数 拼了一个混子才 4个 并且混子还充足，则 再加最后一个数字
                                {
                                    listshunzi.Add(listshunzi.LastOrDefault() + 1);
                                }
                            }

                            //走完了 返回列表
                            return listshunzi;
                        }
                        else if (hunNum == 2)
                        {

                            if (listshunzi.LastOrDefault() + 1 == clons[i + 1])
                            {
                                listshunzi.Add(clons[i + 1]);
                            }
                            else
                            {
                                listshunzi.Add(listshunzi.LastOrDefault() + 1);
                            }

                            if (clons[i + 1] == 14)
                            {
                                if (listshunzi.Count == 4)
                                {
                                    listshunzi.Insert(0, clons[i + 1] - 4);
                                }
                                else if (listshunzi.Count == 3)
                                {
                                    listshunzi.Insert(0, clons[i + 1] - 3);
                                    listshunzi.Insert(0, clons[i + 1] - 4);
                                }
                            }
                            else
                            {
                                if (listshunzi.Count == 4)
                                {
                                    if (listshunzi.LastOrDefault() + 1 == clons[i + 1])
                                    {
                                        listshunzi.Add(clons[i + 1]);
                                    }
                                    else
                                    {
                                        listshunzi.Add(listshunzi.LastOrDefault() + 1);
                                    }
                                }
                                else if (listshunzi.Count == 3)
                                {
                                    if (listshunzi.LastOrDefault() + 1 == clons[i + 1])
                                    {
                                        listshunzi.Add(clons[i + 1]);
                                    }
                                    else
                                    {
                                        listshunzi.Add(listshunzi.LastOrDefault() + 1);
                                    }

                                    if (listshunzi.LastOrDefault() == 14)
                                    {
                                        listshunzi.Insert(0, 10);
                                    }
                                    else
                                    {
                                        listshunzi.Add(listshunzi.LastOrDefault() + 1);
                                    }
                                }
                            }
                            //走完了 返回列表
                            return listshunzi;
                        }
                    }
                }
            }
            listshunzi = new List<int>();
            return listshunzi;
        }

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

            if (iscanshunzi && i == 3)
            {
                var listint = new List<int>();
                for (int j = 0; j <= i + 1; j++)
                {
                    listint.Add(currentlist[j]);
                }

                if (listint != null && listint.Count > 0)
                {
                    listint = listint.OrderBy(d => d).ToList();
                    mtotal.Add(listint);//第一个五张顺子
                }
                //  return BianLiData(data, i - 1);
            }

            if (currentlist.Count > 1)
            {
                currentlist.RemoveRange(0, 1);
            }
            BianLiData(data, currentlist, i, mtotal);

            return null;
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
            if (listdispai.Count(d => d == 14) > 0)
            {
                listdispai.Add(1);
            }
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
        public static int GetHunZiCount(List<PDKTypeData> listdata)
        {
            return listdata.ToList().Count(d => d.v >= 53);
        }

        public static List<int> hunzi = new List<int> { 53, 54 };
        public static List<PaiData> FindRuanShunZi(List<PDKTypeData> data)
        {
            List<PaiData> listPaiData = new List<PaiData>();

            if (GetHunZiCount(data) <= 0)
            {
                var listdata = data.ToList();
                var listhunzi = listdata.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();

                var listnohunzipai = listdata.ToList();

                listnohunzipai.RemoveAll(d => hunzi.Count(x => x == d.v) > 0);
                var listnohunzi = listnohunzipai.Where(d => d.v < 15).Select(d => d.v).Distinct().OrderBy(d => d).ToList();

                PushCardData purdata = new PushCardData();
                purdata.data = listnohunzi.Distinct().ToList();
                var listShunZi = FindShunZi(purdata);
                for (int i = 0; i < listShunZi.Count; i++)
                {
                    PaiData paiData = new PaiData();
                    paiData.listPai = new List<PDKTypeData>();

                    for (int j = 0; j < listShunZi[i].Count; j++)
                    {
                        var paiDian = listShunZi[i][j];
                        if (paiDian == 1)
                        {
                            paiData.listPai.Add(listnohunzipai.FirstOrDefault(d => d.v == 14));
                        }
                        else
                        {
                            paiData.listPai.Add(listnohunzipai.FirstOrDefault(d => d.v == paiDian));
                        }
                    }
                    paiData.PaiValue = listShunZi[i].FirstOrDefault();
                    paiData.HunCount = 0;

                    paiData.listPai = paiData.listPai.OrderBy(d => d.v).ToList();
                    if (paiData.listPai.Count == 5)
                    {
                        paiData.type = DouNiuType.顺子牛;
                    }

                    listPaiData.Add(paiData);
                }
            }
            else if (GetHunZiCount(data) == 1)//如果手牌混子个数>=1
            {
                //补一个混子拼顺子
                var listdata = data.ToList();
                var listhunzi = listdata.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();

                var listnohunzipai = listdata.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();
                //if (listnohunzipai.Count(d => d.v == 14) > 0)
                //{
                //    var pdkPai1 = new PDKTypeData();
                //    pdkPai1.h = listnohunzipai.FirstOrDefault(d => d.v == 14).h;
                //    pdkPai1.v = 1;
                //    listnohunzipai.Add(pdkPai1);
                //}

                listnohunzipai.RemoveAll(d => hunzi.Count(x => x == d.v) > 0);
                var listnohunzi = listnohunzipai.Where(d => d.v < 15).Select(d => d.v).Distinct().OrderBy(d => d).ToList();

                for (int i = 0; i < listnohunzi.Count; i++)
                {
                    var listshunzi = new List<int>();
                    var listlinshi = listnohunzi.Skip(i).Take(4).ToList();
                    List<int> needList = new List<int>();
                    for (int c = 0; c < listlinshi.Count; c++)
                    {
                        if (listlinshi[c] == 14)
                        {
                            needList.Add(1);
                        }
                        else
                        {
                            needList.Add(listlinshi[c]);
                        }
                    }
                    listlinshi = needList.OrderBy(d => d).ToList();
                    if (listlinshi.LastOrDefault() - listlinshi.FirstOrDefault() >= 5)
                    {
                        continue;
                    }
                    listshunzi = GetRuanShunzi(listlinshi, listhunzi);
                    if (listshunzi.Count >= 5)
                    {
                        PaiData paiData = new PaiData();
                        paiData.listPai = new List<PDKTypeData>();
                        for (int j = 0; j < listshunzi.Count; j++)
                        {
                            var paiDian = listshunzi[j] == 1 ? 14 : listshunzi[j];
                            if (listnohunzi.Count(d => d == paiDian) <= 0)
                            {
                                paiData.HunCount += 1;
                                paiData.listPai.Add(listhunzi[0]);
                            }
                            else
                            {
                                // var paiDian = listshunzi[j] == 1 ? 14 : listshunzi[j];//listshunzi[j]; //
                                paiData.listPai.Add(listnohunzipai.FirstOrDefault(d => d.v == paiDian));
                            }
                        }
                        paiData.type = DouNiuType.顺子牛;

                        paiData.PaiValue = listshunzi.FirstOrDefault();


                        if (listPaiData.Count(d => d.PaiValue == paiData.PaiValue && d.HunCount == paiData.HunCount) <= 0)
                        {
                            listPaiData.Add(paiData);
                        }
                    }
                }
            }
            if (GetHunZiCount(data) == 2)
            {
                //补两个混子拼顺子
                var listdata = data.ToList();
                var listhunzi = listdata.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();

                var listnohunzipai = listdata.ToList();

                //if (listnohunzipai.Count(d => d.v == 14) > 0)
                //{
                //    var pdkPai1 = new PDKTypeData();
                //    pdkPai1.h = listnohunzipai.FirstOrDefault(d => d.v == 14).h;
                //    pdkPai1.v = 1;
                //    listnohunzipai.Add(pdkPai1);
                //}

                listnohunzipai.RemoveAll(d => hunzi.Count(x => x == d.v) > 0);
                var listnohunzi = listnohunzipai.Where(d => d.v < 15).Select(d => d.v).Distinct().OrderBy(d => d).ToList();
                for (int i = 0; i < listnohunzi.Count; i++)
                {
                    var listshunzi = new List<int>();
                    var listlinshi = listnohunzi.Skip(i).Take(3).ToList();
                    List<int> needList = new List<int>();
                    for (int c = 0; c < listlinshi.Count; c++)
                    {
                        if (listlinshi[c] == 14)
                        {
                            needList.Add(1);
                        }
                        else
                        {
                            needList.Add(listlinshi[c]);
                        }
                    }
                    listlinshi = needList.OrderBy(d => d).ToList();
                    if (listlinshi.LastOrDefault() - listlinshi.FirstOrDefault() >= 5)
                    {
                        continue;
                    }
                    listshunzi = GetRuanShunzi(listlinshi, listhunzi);
                    if (listshunzi.Count >= 5)
                    {
                        PaiData paiData = new PaiData();
                        paiData.listPai = new List<PDKTypeData>();
                        for (int j = 0; j < listshunzi.Count; j++)
                        {
                            var paiDian = listshunzi[j] == 1 ? 14 : listshunzi[j];
                            if (listnohunzi.Count(d => d == paiDian) <= 0)
                            {
                                if (paiData.HunCount == 0)
                                {
                                    paiData.listPai.Add(listhunzi[0]);
                                }
                                else if (paiData.HunCount == 1)
                                {
                                    paiData.listPai.Add(listhunzi[1]);
                                }
                                paiData.HunCount += 1;
                            }
                            else
                            {
                                // paiData.listPai.Add(listnohunzipai.FirstOrDefault(d => d.v == (listshunzi[j] == 1 ? 14 : listshunzi[j])));
                                paiData.listPai.Add(listnohunzipai.FirstOrDefault(d => d.v == paiDian));
                            }
                        }
                        if (paiData.listPai.Count == 5)
                        {
                            paiData.type = DouNiuType.顺子牛;
                        }

                        paiData.PaiValue = listshunzi.FirstOrDefault();
                        if (listPaiData.Count(d => d.PaiValue == paiData.PaiValue && d.HunCount == paiData.HunCount) <= 0)
                        {
                            listPaiData.Add(paiData);
                        }
                    }
                }
            }
            listPaiData = listPaiData.OrderBy(d => d.PaiValue).ToList();
            return listPaiData;
        }
        #endregion

        #region 带混 找 葫芦牛 （三带2）

        public static List<PaiData> FindRuanSanDaiEr(List<PDKTypeData> listPaiData)
        {
            var listRuanSanDaiEr = new List<PaiData>();
            var listRuanSanTiao = FindRuanSanTiao(listPaiData);
            PushCardData purData = new PushCardData();
            purData.data = listPaiData.Where(d => hunzi.Count(x => x == d.v) <= 0).Select(d => d.v).ToList();//找到所有的 不是混子的牌

            var listzhadan = FindDuiZhaDan(purData);
            listRuanSanTiao = listRuanSanTiao.Where(d => listzhadan.Count(x => x.v == d.PaiValue) <= 0).ToList();
            var listHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
            var listNoHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) <= 0).ToList();
            var listRuanDuiZi = FindRuanDui(listPaiData).Where(d => listzhadan.Count(x => x.v == d.PaiValue) <= 0).ToList();

            for (int i = 0; i < listRuanSanTiao.Count; i++)
            {
                var ruanSanTiao = listRuanSanTiao[i];

                for (int j = 0; j < listRuanDuiZi.Count; j++)
                {
                    if (ruanSanTiao.PaiValue == listRuanDuiZi[j].PaiValue)
                    {
                        continue;
                    }
                    PaiData linShiRuanSanTiao = new PaiData();
                    linShiRuanSanTiao.listPai = ruanSanTiao.listPai.ToList();
                    linShiRuanSanTiao.HunCount = ruanSanTiao.HunCount;
                    linShiRuanSanTiao.type = ruanSanTiao.type;
                    linShiRuanSanTiao.PaiValue = ruanSanTiao.PaiValue;

                    if (linShiRuanSanTiao.HunCount + listRuanDuiZi[j].HunCount <= listHunZiPai.Count)
                    {
                        linShiRuanSanTiao.type = DouNiuType.葫芦牛;
                        linShiRuanSanTiao.HunCount = linShiRuanSanTiao.HunCount + listRuanDuiZi[j].HunCount;
                        linShiRuanSanTiao.listPai.AddRange(listRuanDuiZi[j].listPai);
                        listRuanSanDaiEr.Add(linShiRuanSanTiao);
                    }
                }
            }
            listRuanSanDaiEr = listRuanSanDaiEr.OrderBy(d => d.PaiValue).OrderBy(d => d.HunCount).ToList();
            return listRuanSanDaiEr;
        }

        public static List<PaiData> FindRuanSanTiao(List<PDKTypeData> listPaiData)
        {
            var listRuanSanTiao = new List<PaiData>();
            PushCardData purData = new PushCardData();
            purData.data = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).Select(d => d.v).ToList();//找到所有的 不是混子的牌

            var listHunZiPai = listPaiData.Where(d => (hunzi.Count(x => x == d.v) > 0)).ToList();
            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            var listSanTiao = FindSanTiao(purData);
            for (int i = 0; i < listSanTiao.Count; i++)
            {
                int chucount = 0;
                PaiData paiData = new PaiData();

                for (int j = 0; j < listNoHunZiPai.Count; j++)
                {
                    if (listNoHunZiPai[j].v == listSanTiao[i])
                    {
                        chucount++;
                        paiData.listPai.Add(listNoHunZiPai[j]);
                        if (chucount >= 3)
                        {
                            break;//找到3张就停止，防止有多张这个点 都会添加到出牌区
                        }
                    }
                }
                paiData.HunCount = 0;
                paiData.PaiValue = listSanTiao[i];
                listRuanSanTiao.Add(paiData);
            }

            if (listHunZiPai.Count >= 1)
            {
                var listDuiZi = FindDuiZi(purData).Where(d => d < 50 && listRuanSanTiao.Count(x => x.PaiValue == d) <= 0).ToList();
                for (int i = 0; i < listDuiZi.Count; i++)
                {
                    int chucount = 0;
                    PaiData paiData = new PaiData();

                    for (int j = 0; j < listNoHunZiPai.Count; j++)
                    {
                        if (listNoHunZiPai[j].v == listDuiZi[i])
                        {
                            chucount++;
                            paiData.listPai.Add(listNoHunZiPai[j]);
                            if (chucount >= 2)
                            {
                                break;//找到2张就停止，防止有多张这个点 都会添加到出牌区
                            }
                        }
                    }
                    paiData.listPai.Add(listHunZiPai.FirstOrDefault());
                    paiData.PaiValue = listDuiZi[i];
                    paiData.HunCount = 1;
                    listRuanSanTiao.Add(paiData);
                }
            }
            if (listHunZiPai.Count == 2)
            {
                var listDanZhang = FindDanZhang(purData).Where(d => d < 50 && listRuanSanTiao.Count(x => x.PaiValue == d) <= 0).ToList();
                for (int i = 0; i < listDanZhang.Count; i++)
                {
                    int chucount = 0;
                    PaiData paiData = new PaiData();

                    for (int j = 0; j < listNoHunZiPai.Count; j++)
                    {
                        if (listNoHunZiPai[j].v == listDanZhang[i])
                        {
                            chucount++;
                            paiData.listPai.Add(listNoHunZiPai[j]);
                            if (chucount >= 1)
                            {
                                break;//找到2张就停止，防止有多张这个点 都会添加到出牌区
                            }
                        }
                    }
                    paiData.listPai.Add(listHunZiPai.FirstOrDefault());
                    paiData.listPai.Add(listHunZiPai[1]);
                    paiData.PaiValue = listDanZhang[i];
                    paiData.HunCount = 2;
                    listRuanSanTiao.Add(paiData);
                }
            }
            listRuanSanTiao = listRuanSanTiao.OrderBy(d => d.PaiValue).OrderBy(d => d.HunCount).ToList();
            return listRuanSanTiao;
        }

        public static List<int> FindSanTiao(PushCardData data)
        {
            //找到所有三条
            List<int> da = new List<int>();

            var arr = getRepareDataByv(data, 2);

            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }

        /// <summary>
        /// 根据repet 数量查找 =1 找对子 ； =2 找出三个相同的；=3 找出四个相同的
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repet"></param>
        /// <returns></returns>
        public static List<RepeatInfo> getRepareDataByv(PushCardData data, int repet)
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
                        result[v].Repeatv += 1;
                    }
                    else
                    {
                        RepeatInfo item =
                            new RepeatInfo() { Value = v, Repeatv = 1 };
                        result.Add(v, item);
                    }
                }
                List<RepeatInfo> m = new List<RepeatInfo>();
                foreach (RepeatInfo info in result.Values)
                {
                    if (info.Repeatv > repet)
                    {
                        var cds = new RepeatInfo();
                        cds.Value = info.Value;
                        cds.Repeatv = info.Repeatv;
                        m.Add(cds);
                    }
                }
                return m;
            }
            return new List<RepeatInfo>();
        }

        public class RepeatInfo
        {
            // 值
            public int Value { get; set; }
            // 重复次数
            public int Repeatv { get; set; }
        }


        /// <summary>
        /// 找出所有对子
        /// </summary>
        /// <param name="data"></param>
        public static List<int> FindDuiZi(PushCardData data)
        {
            //找到所有对子
            List<int> da = new List<int>();
            var arr = getRepareDataByv(data, 1);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            var listNoHunZi = da.Where(d => hunzi.Count(x => x == d) <= 0).ToList();

            var listHunZi = da.Where(d => hunzi.Count(x => x == d) > 0).ToList();
            if (listHunZi.Count > 0)
            {
                listNoHunZi.AddRange(listHunZi);
            }
            return listNoHunZi;
        }

        /// <summary>
        /// 找到排序后的对子 打2 找到的是  3 4 2
        /// </summary>
        /// <param name="listPaiData"></param>
        /// <returns></returns>
        public static List<PaiData> FindRuanDui(List<PDKTypeData> listPaiData)
        {
            var listRuanDuiZi = new List<PaiData>();
            PushCardData purData = new PushCardData();
            purData.data = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).Select(d => d.v).ToList();//找到所有的 不是混子的牌

            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();
            var listHunZiPai = listPaiData.Where(d => (hunzi.Count(x => x == d.v) > 0)).ToList();

            var listSanTiao = FindSanTiao(purData);
            var listZhaDan = FindDuiZhaDan(purData);

            var listDuiZi = FindDuiZi(purData);
            for (int i = 0; i < listDuiZi.Count; i++)
            {
                int chucount = 0;
                PaiData paiData = new PaiData();

                for (int j = 0; j < listPaiData.Count; j++)
                {
                    if (listPaiData[j].v == listDuiZi[i])
                    {
                        chucount++;
                        paiData.listPai.Add(listPaiData[j]);
                        if (chucount >= 2)
                        {
                            break;//找到2张就停止，防止有多张这个点 都会添加到出牌区
                        }
                    }
                }
                paiData.HunCount = 0;
                paiData.PaiValue = listDuiZi[i];
                listRuanDuiZi.Add(paiData);
            }

            if (listHunZiPai.Count > 0)
            {
                var listDanZhang = FindDanZhang(purData).Where(d => d < 50).ToList();

                for (int i = 0; i < listDanZhang.Count; i++)
                {
                    int chucount = 0;
                    PaiData paiData = new PaiData();

                    for (int j = 0; j < listNoHunZiPai.Count; j++)
                    {
                        if (listNoHunZiPai[j].v == listDanZhang[i])
                        {
                            chucount++;
                            paiData.listPai.Add(listNoHunZiPai[j]);
                            if (chucount >= 1)
                            {
                                break;//找到2张就停止，防止有多张这个点 都会添加到出牌区
                            }
                        }
                    }
                    paiData.listPai.Add(listHunZiPai.FirstOrDefault());
                    paiData.PaiValue = listDanZhang[i];
                    paiData.HunCount = 1;
                    listRuanDuiZi.Add(paiData);
                }
            }
            if (listHunZiPai.Count == 2)
            {
                PaiData paiData = new PaiData();

                paiData.listPai.AddRange(listHunZiPai);
                paiData.PaiValue = 50;
                paiData.HunCount = 2;
                listRuanDuiZi.Add(paiData);
            }
            listRuanDuiZi = listRuanDuiZi.OrderBy(d => d.PaiValue).OrderBy(d => d.HunCount).ToList();
            return listRuanDuiZi;
        }
        #endregion

        #region 带混 找炸弹牛

        public static List<PaiData> FindRuanZhaDan(List<PDKTypeData> listPaiData)
        {
            List<PaiData> listRuanZhaDan = new List<PaiData>();

            var listHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            var purdata = new PushCardData();
            purdata.data = listNoHunZiPai.Select(d => d.v).ToList();

            #region  不带混
            var listZhaDan4 = FindDuiZhaDan4(purdata);
            for (int i = 0; i < listZhaDan4.Count; i++)
            {
                int chucount = 0;
                PaiData paiData = new PaiData();
                paiData.type = DouNiuType.炸弹牛;
                for (int j = 0; j < listNoHunZiPai.Count; j++)
                {
                    if (listNoHunZiPai[j].v == listZhaDan4[i])
                    {
                        chucount++;
                        paiData.listPai.Add(listNoHunZiPai[j]);
                        if (chucount >= 4)
                        {
                            break;//找到4张就停止，防止有多张这个点 都会添加到出牌区
                        }
                    }
                }
                paiData.HunCount = 0;
                paiData.PaiValue = listZhaDan4[i];
                listRuanZhaDan.Add(paiData);
            }

            #endregion
            if (listHunZiPai.Count >= 1)
            {
                #region 1个混
                var listSanTiao = FindSanTiao(purdata);
                for (int i = 0; i < listSanTiao.Count; i++)
                {
                    int chucount = 0;
                    PaiData paiData = new PaiData();
                    paiData.type = DouNiuType.炸弹牛;
                    for (int j = 0; j < listNoHunZiPai.Count; j++)
                    {
                        if (listNoHunZiPai[j].v == listSanTiao[i])
                        {
                            chucount++;
                            paiData.listPai.Add(listNoHunZiPai[j]);
                            if (chucount >= 3)
                            {
                                break;//找到4张就停止，防止有多张这个点 都会添加到出牌区
                            }
                        }
                    }

                    paiData.listPai.Add(listHunZiPai.FirstOrDefault());

                    paiData.HunCount = 1;
                    paiData.PaiValue = listSanTiao[i];
                    listRuanZhaDan.Add(paiData);
                }

                #endregion
            }
            if (listHunZiPai.Count >= 2)
            {
                #region 2个混
                var listDuiZi = FindDuiZi(purdata).Where(d => d < 50).ToList();
                for (int i = 0; i < listDuiZi.Count; i++)
                {
                    int chucount = 0;
                    PaiData paiData = new PaiData();
                    paiData.type = DouNiuType.炸弹牛;
                    for (int j = 0; j < listNoHunZiPai.Count; j++)
                    {
                        if (listNoHunZiPai[j].v == listDuiZi[i])
                        {
                            chucount++;
                            paiData.listPai.Add(listNoHunZiPai[j]);
                            if (chucount >= 2)
                            {
                                break;//找到4张就停止，防止有多张这个点 都会添加到出牌区
                            }
                        }
                    }
                    paiData.listPai.Add(listHunZiPai[0]);
                    paiData.listPai.Add(listHunZiPai[1]);

                    paiData.HunCount = 2;
                    paiData.PaiValue = listDuiZi[i];
                    listRuanZhaDan.Add(paiData);
                }
                #endregion
            }

            listRuanZhaDan = listRuanZhaDan.OrderBy(d => d.type).OrderBy(d => d.PaiValue).OrderBy(d => d.HunCount).ToList();
            return listRuanZhaDan;
        }
        #endregion

        public static int GetPaiDian(PDKTypeData pk)
        {

            int paiDian = pk.v;
            if (pk.v >= 10 && pk.v < 14)
            {
                paiDian = 10;
            }
            else if (pk.v == 14)
            {
                paiDian = 1;
            }
            return paiDian;
        }
        #region 找 金牛

        public static List<PaiData> FindJinNiu(List<PDKTypeData> listPaiData)
        {
            List<PaiData> listJinNiu = new List<PaiData>();

            var listHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            if (listNoHunZiPai.Count == 3 && listHunZiPai.Count == 2)
            {
                if ((GetPaiDian(listNoHunZiPai[0]) + GetPaiDian(listNoHunZiPai[1]) + GetPaiDian(listNoHunZiPai[2])) % 10 == 0)
                {
                    PaiData paiData = new PaiData();
                    paiData.listPai = listPaiData.OrderBy(d => d.v).ToList();
                    paiData.HunCount = 2;
                    //paiData.type = DouNiuType.金牛;
                    paiData.type = DouNiuType.牛牛;
                    paiData.PaiValue = 54;
                    listJinNiu.Add(paiData);
                }
            }

            return listJinNiu;
        }

        #endregion

        #region 找 天牛

        public static List<PaiData> FindTianNiu(List<PDKTypeData> listPaiData)
        {
            List<PaiData> listTianNiu = new List<PaiData>();

            var listHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            if (listNoHunZiPai.Count == 4 && listHunZiPai.Count(d => d.v == 54) == 1)
            {
                var Pk4NiuType = getNiuNiu4Pk(listNoHunZiPai);
                if (Pk4NiuType.getInt() > 0)
                {
                    PaiData paiData = new PaiData();
                    paiData.listPai = listPaiData.OrderBy(d => d.v).ToList();
                    paiData.HunCount = 1;
                    paiData.type = DouNiuType.牛牛;
                    //paiData.type = DouNiuType.天牛;
                    paiData.PaiValue = 54;
                    listTianNiu.Add(paiData);
                }
            }
            else if (listNoHunZiPai.Count == 3 && listHunZiPai.Count == 2)
            {
                if (listNoHunZiPai.Count == 3 && listHunZiPai.Count == 2)
                {
                    if ((GetPaiDian(listNoHunZiPai[0]) + GetPaiDian(listNoHunZiPai[1]) + GetPaiDian(listNoHunZiPai[2])) % 10 == 0)
                    {
                        PaiData paiData = new PaiData();
                        listNoHunZiPai.AddRange(listHunZiPai.ToList());
                        paiData.listPai = listNoHunZiPai;
                        paiData.HunCount = 2;
                        paiData.type = DouNiuType.牛牛;//paiData.type = DouNiuType.金牛;
                        paiData.PaiValue = 54;

                        listTianNiu.Add(paiData);
                    }
                    else
                    {
                        // 其中一个王和 其他牌 拼成牛  ，大王 和 剩余一张牌 组合 变为  天牛
                        PaiData paiData = new PaiData();
                        var rtnListPai = listNoHunZiPai.Take(2).ToList();
                        rtnListPai.Add(listHunZiPai[0]);
                        rtnListPai.AddRange(listNoHunZiPai.Skip(2).ToList());
                        rtnListPai.Add(listHunZiPai[1]);

                        paiData.listPai = rtnListPai;
                        paiData.HunCount = 2;
                        paiData.type = DouNiuType.牛牛;//paiData.type = DouNiuType.天牛;
                        paiData.PaiValue = 54;

                        listTianNiu.Add(paiData);
                    }
                }
            }

            return listTianNiu;
        }

        #endregion

        #region 找 地牛

        public static List<PaiData> FindDiNiu(List<PDKTypeData> listPaiData)
        {
            List<PaiData> listTianNiu = new List<PaiData>();

            var listHunZiPai = listPaiData.Where(d => hunzi.Count(x => x == d.v) > 0).ToList();
            var listNoHunZiPai = listPaiData.Where(d => !(hunzi.Count(x => x == d.v) > 0)).ToList();

            if (listNoHunZiPai.Count == 4 && listHunZiPai.Count(d => d.v == 53) == 1)
            {
                var Pk4NiuType = getNiuNiu4Pk(listNoHunZiPai);
                if (Pk4NiuType.getInt() > 0)
                {
                    PaiData paiData = new PaiData();
                    paiData.listPai = listPaiData.OrderBy(d => d.v).ToList();
                    paiData.HunCount = 1;
                    //paiData.type = DouNiuType.地牛;
                    paiData.type = DouNiuType.牛牛;
                    paiData.PaiValue = 53;
                    listTianNiu.Add(paiData);
                }
            }
            return listTianNiu;
        }

        #endregion

        #region  四张牌中找到牛 天牛、地牛 用

        public static Dictionary<string, bool> NiuNiu4Pk(List<PDKTypeData> listPai, out List<PDKTypeData> listOutPai)
        {
            listOutPai = listPai.ToList();
            bool isNiuNum = false;
            bool isNiuNiu = false;
            for (int m = 0; m <= 1; m++)
            {
                for (int n = m + 1; n <= 2; n++)
                {
                    for (int z = n + 1; z <= 3; z++)
                    {
                        if ((GetPaiDian(listPai[m]) + GetPaiDian(listPai[n]) + GetPaiDian(listPai[z])) % 10 == 0)
                        {
                            isNiuNum = true;
                            int num = 0;
                            var xidx = 0;
                            for (int x = 0; x <= 3; x++)
                            {
                                if (x != m && x != n && x != z)
                                {
                                    num += GetPaiDian(listPai[x]);
                                    xidx = x;
                                }
                            }
                            if (num % 10 == 0)
                            {
                                isNiuNiu = true;
                            }
                            listOutPai = new List<PDKTypeData>();
                            PDKTypeData pk1 = new PDKTypeData();
                            pk1.v = listPai[m].v;
                            pk1.h = listPai[m].h;
                            listOutPai.Add(pk1);

                            PDKTypeData pk2 = new PDKTypeData();
                            pk2.v = listPai[n].v;
                            pk2.h = listPai[n].h;
                            listOutPai.Add(pk2);

                            PDKTypeData pk3 = new PDKTypeData();
                            pk3.v = listPai[z].v;
                            pk3.h = listPai[z].h;
                            listOutPai.Add(pk3);

                            PDKTypeData pk4 = new PDKTypeData();
                            pk4.v = listPai[xidx].v;
                            pk4.h = listPai[xidx].h;
                            listOutPai.Add(pk4);

                            break;
                        }
                    }
                }
            }

            Dictionary<string, bool> result = new Dictionary<string, bool>();
            result.Add("isNiuNum", isNiuNum);
            result.Add("isNiuNiu", isNiuNiu);

            return result;
        }

        public static int isNiuNum4Pk(List<PDKTypeData> listPdk)
        {
            if (listPdk == null || listPdk.Count <= 0)
            {
                return -1;
            }
            List<PDKTypeData> listOutPdk = new List<PDKTypeData>();
            Dictionary<string, bool> map = NiuNiu4Pk(listPdk, out listOutPdk);
            if (map["isNiuNiu"])
            {
                return 10;
            }
            if (map["isNiuNum"])
            {
                int num = 0;
                foreach (var item in listPdk)
                {
                    num += GetPaiDian(item);
                }
                return num % 10;
            }
            else
            {
                return -1;
            }
        }
        public static DouNiuType getNiuNiu4Pk(List<PDKTypeData> listPdk)
        {
            int niuniu = isNiuNum4Pk(listPdk);
            if (niuniu == -1)
            {
                return DouNiuType.没牛;
            }

            var yi = (DouNiuType)Enum.Parse(typeof(DouNiuType), niuniu.ToString());
            return yi;
        }
        #endregion

        /// <summary>
        /// 找出所有单张
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<int> FindDanZhang(PushCardData data)
        {
            List<int> da = new List<int>();
            var arr = getRepareDataByvOnev(data, 3);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }

            var listNoHunZi = da.Where(d => hunzi.Count(x => x == d) <= 0).ToList();

            var listHunZi = data.data.Where(d => hunzi.Count(x => x == d) > 0).ToList();
            if (listHunZi.Count > 0)
            {
                listNoHunZi.AddRange(listHunZi);
            }
            return listNoHunZi;
        }
        public class zhadan
        {
            public int v { get; set; }

            public int zhang { get; set; }
        }

        public static List<zhadan> FindDuiZhaDan(PushCardData data)
        {
            List<zhadan> listzhadan = new List<zhadan>();
            //找到所有对子
            List<int> da = new List<int>();
            var arr = getRepareDataByv(data, 3);
            foreach (var m in arr)
            {
                zhadan zha = new zhadan();
                zha.v = m.Value;
                zha.zhang = m.Repeatv;
                da.Add(m.Value);
                listzhadan.Add(zha);
            }
            return listzhadan;
        }

        /// <summary>
        /// 找到4个的炸弹
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<int> FindDuiZhaDan4(PushCardData data)
        {
            //找到所有对子
            List<int> da = new List<int>();
            var arr = getRepareDataByv(data, 3);
            foreach (var m in arr)
            {
                da.Add(m.Value);
            }
            return da;
        }
        /// <summary>
        /// 查找单张
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repet"></param>
        /// <returns></returns>
        public static List<RepeatInfo> getRepareDataByvOnev(PushCardData data, int repet)
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
                    result[v].Repeatv += 1;
                }
                else
                {
                    RepeatInfo item =
                        new RepeatInfo() { Value = v, Repeatv = 1 };
                    result.Add(v, item);
                }
            }
            List<RepeatInfo> m = new List<RepeatInfo>();
            foreach (RepeatInfo info in result.Values)
            {
                if (info.Repeatv == 1)
                {
                    var cds = new RepeatInfo();
                    cds.Value = info.Value;
                    cds.Repeatv = info.Repeatv;
                    m.Add(cds);
                }
            }
            return m;
        }
    }


    public enum DouNiuType : int
    {
        没牛 = 0,
        牛1 = 1,
        牛2 = 2,
        牛3 = 3,
        牛4 = 4,
        牛5 = 5,
        牛6 = 6,
        牛7 = 7,
        牛8 = 8,
        牛9 = 9,
        牛牛 = 10,
        //--------------
        //五小牛 = 11,

        //五花牛 = 12,
        //顺子牛 = 13,
        //同花牛 = 14,
        //葫芦牛 = 15,
        //炸弹牛 = 16,
        //欢乐牛 = 17,
        //        五花牛 = 11,
        顺子牛 = 11,
        银牛 = 12,
        同花牛 = 13,
        金牛 = 14,
        葫芦牛 = 15,
        五小牛 = 16,//比欢乐牛小
        炸弹牛 = 17,
        一条龙 = 18,
        欢乐牛 = 29,


    }
}
