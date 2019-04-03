using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    public class PlayRules
    {
        public static bool IsCanHU(List<int> mah, int ID, out int fengCount, out List<fanmodel> listfanmodel)//, FengYun_GuiZe guize
        {
            List<int> pais = new List<int>(mah);
            listfanmodel = new List<fanmodel>();
            fengCount = 0;
            //先添加手牌，再排序
            pais.Add(ID);
            pais.Sort();

            //基本牌型胡牌
            bool renresult = false;
            //依据牌的顺序从左到右依次分出将牌
            for (int i = 0; i < pais.Count; i++)
            {
                List<int> paiT = new List<int>(pais);
                List<int> ds = pais.FindAll(delegate(int d)
                {
                    return pais[i] == d;
                });
                int count = 0;
                //判断是否能做将牌
                if (ds.Count >= 2)
                {
                    //移除两张将牌
                    paiT.Remove(pais[i]);
                    paiT.Remove(pais[i]);

                    if (HuPaiPanDin(paiT, ref count))
                    {
                        renresult = true;
                        fengCount = fengCount < count ? count : fengCount;
                    }
                }
            }
            return renresult;
        }
        /// <summary>
        /// 基础胡牌
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="yaojiu"></param>
        /// <param name="quanjiang"></param>
        /// <returns></returns>
        public static bool HuPaiPanDin(List<int> mahs, ref int fengCount)
        {
            if (mahs.Count == 0)
            {
                return true;
            }

            List<int> fs = mahs.FindAll(delegate(int a)
            {
                return mahs[0] == a;
            });

            //组成克子
            if (fs.Count == 3)
            {
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                return HuPaiPanDin(mahs, ref fengCount);
            }
            else
            { //组成顺子
                #region 如果是风嘴子
                if (mahs[0] > 30 && mahs[0] < 40)
                {
                    //31 33 35 37 任意三个算一顺 
                    bool result = IsFengZuiZi(ref mahs, ref fengCount);
                    if (result)
                    {
                        return HuPaiPanDin(mahs, ref fengCount);
                    }
                    //                     var list30 = mahs.Where(d => d > 30 && d < 40).Distinct().ToList();
                    //                     if (list30.Count >= 3)
                    //                     {
                    //                         fengCount++;
                    //                         var listTake30 = list30.Take(3).ToList();
                    //                         for (int i = 0; i < listTake30.Count; i++)
                    //                         {
                    //                             mahs.Remove(listTake30[i]); 
                    //                         }
                    //                         return HuPaiPanDin(mahs, ref fengCount);
                    //                     }
                }
                else if (mahs[0] > 40)
                {
                    // 41 43 45 算一顺
                    var list40 = mahs.Where(d => d > 40).Distinct().ToList();
                    if (list40.Count >= 3)
                    {
                        fengCount++;
                        for (int i = 0; i < list40.Count; i++)
                        {
                            mahs.Remove(list40[i]);
                        }
                        return HuPaiPanDin(mahs, ref fengCount);
                    }
                }
                #endregion
                if (mahs.Contains(mahs[0] + 1) && mahs.Contains(mahs[0] + 2))
                {
                    mahs.Remove(mahs[0] + 2);
                    mahs.Remove(mahs[0] + 1);
                    mahs.Remove(mahs[0]);

                    return HuPaiPanDin(mahs, ref fengCount);
                }
                return false;
            }
        }

        /// <summary>
        /// 封嘴子判断
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="fengCount"></param>
        /// <returns></returns>
        public static bool IsFengZuiZi(ref List<int> mahs, ref int fengCount)
        {
            List<int> cards1 = new List<int>(mahs);
            int fengCount1 = fengCount;
            bool result1 = RemoveFeng(cards1, true, ref fengCount1);
            if (result1)
            {
                fengCount = fengCount1;
                mahs = new List<int>(cards1);
                return result1;
            }
            List<int> cards2 = new List<int>(mahs);
            int fengCount2 = fengCount;
            bool result2 = RemoveFeng(cards2, false, ref fengCount2);
            if (result2)
            {
                fengCount = fengCount2;
                mahs = new List<int>(cards2);
                return result2;
            }
            return false;
        }

        /// <summary>
        /// 封嘴子判断
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="fengCount"></param>
        /// <returns></returns>
        public static void IsFengZuiZi_LZ(ref List<int> mahs, ref int fengCount)
        {
            List<int> cards1 = new List<int>(mahs);
            int fengCount1 = fengCount;
            bool result1 = RemoveFeng(cards1, true, ref fengCount1);
            if (result1)
            {
                fengCount = fengCount1;
                mahs = new List<int>(cards1);
                return;
            }
            List<int> cards2 = new List<int>(mahs);
            int fengCount2 = fengCount;
            bool result2 = RemoveFeng(cards2, false, ref fengCount2);
            if (result2)
            {
                fengCount = fengCount2;
                mahs = new List<int>(cards2);
                return;
            }
            fengCount = fengCount1;
            mahs = new List<int>(cards2);
        }

        /// <summary>
        /// 封嘴子判断 正
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="fengCount"></param>
        public static void IsFengZuiZi_LZ_Z(ref List<int> mahs, ref int fengCount)
        {
            List<int> cards1 = new List<int>(mahs);
            int fengCount1 = fengCount;
            bool result1 = RemoveFeng(cards1, true, ref fengCount1);
            fengCount = fengCount1;
            mahs = new List<int>(cards1);
            return;
        }

        /// <summary>
        /// 封嘴子判断 逆
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="fengCount"></param>
        public static void IsFengZuiZi_LZ_N(ref List<int> mahs, ref int fengCount)
        {
            List<int> cards2 = new List<int>(mahs);
            int fengCount2 = fengCount;
            bool result2 = RemoveFeng(cards2, false, ref fengCount2);
            fengCount = fengCount2;
            mahs = new List<int>(cards2);
            return;
        }

        /// <summary>
        /// 封嘴子判断 半倒叙
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="fengCount"></param>
        public static void IsFengZuiZi_LZ_B(ref List<int> mahs, ref int fengCount)
        {
            List<int> cards2 = new List<int>(mahs);
            int fengCount2 = fengCount;
            bool result2 = RemoveFeng_B(cards2, false, ref fengCount2);
            fengCount = fengCount2;
            mahs = new List<int>(cards2);
            return;
        }

        /// <summary>
        /// 移除风 半倒叙
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="type"></param>
        /// <param name="fengCount"></param>
        /// <returns></returns>
        public static bool RemoveFeng_B(List<int> mahs, bool type, ref int fengCount)
        {
            bool result = false;
            if (mahs.Count <= 0 || (mahs[0] < 30 || mahs[0] > 40))
            {
                return true;
            }
            var list30 = OrderByCount_N(mahs.Where(d => d > 30 && d < 40).ToList(), type);
            if (list30.Count >= 3)
            {
                fengCount++;
                var listTake30 = list30.Take(3).ToList();
                for (int i = 0; i < listTake30.Count; i++)
                {
                    mahs.Remove(listTake30[i]);
                }
                return RemoveFeng_B(mahs, type, ref fengCount);
            }
            return result;
        }

        /// <summary>
        /// 移除风
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="type"></param>
        /// <param name="fengCount"></param>
        /// <returns></returns>
        public static bool RemoveFeng(List<int> mahs, bool type, ref int fengCount)
        {
            bool result = false;
            if (mahs.Count <= 0 || (mahs[0] < 30 || mahs[0] > 40))
            {
                return true;
            }
            if (type && mahs.Count(d => d == mahs[0]) == 3)
            {
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                return RemoveFeng(mahs, type, ref fengCount);
            }
            var list30 = OrderByCount(mahs.Where(d => d > 30 && d < 40).ToList(), type);
            if (list30.Count >= 3)
            {
                fengCount++;
                var listTake30 = list30.Take(3).ToList();
                for (int i = 0; i < listTake30.Count; i++)
                {
                    mahs.Remove(listTake30[i]);
                }
                return RemoveFeng(mahs, type, ref fengCount);
            }
            return result;
        }

        /// <summary>
        /// 根据数量排序  type : true 正序     
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<int> OrderByCount(List<int> mahs, bool type)
        {
            List<int> result = new List<int>();
            List<int> data = mahs.Distinct().ToList();
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = data.Count - 1; j > i; j--)
                {
                    if (mahs.Count(d => d == data[j]) > mahs.Count(d => d == data[j - 1]))
                    {
                        data[j] = data[j] + data[j - 1];
                        data[j - 1] = data[j] - data[j - 1];
                        data[j] = data[j] - data[j - 1];
                    }
                }
            }
            if (type)
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    result.Add(data[i]);
                }
                return result;
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// 根据数量排序  type : true 半正序     
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<int> OrderByCount_N(List<int> mahs, bool type)
        {
            List<int> result = new List<int>();
            List<int> data = mahs.Distinct().ToList();
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = data.Count - 1; j > i; j--)
                {
                    if (mahs.Count(d => d == data[j]) > mahs.Count(d => d == data[j - 1]))
                    {
                        data[j] = data[j] + data[j - 1];
                        data[j - 1] = data[j] - data[j - 1];
                        data[j] = data[j] - data[j - 1];
                    }
                }
            }
            if (data.Count > 2)
            {
                int first = data[0];
                data.Remove(first);
                result.Add(first);
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    result.Add(data[i]);
                }
                return result;
            }
            else
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    result.Add(data[i]);
                }
                return result;
            }
        }

        public static bool IsCanHU(List<int> mah)
        {
            List<int> pais = new List<int>(mah);
            pais.Sort();
            //基本牌型胡牌
            bool renresult = false;
            //依据牌的顺序从左到右依次分出将牌
            for (int i = 0; i < pais.Count; i++)
            {
                List<int> paiT = new List<int>(pais);
                List<int> ds = pais.FindAll(delegate(int d)
                {
                    return pais[i] == d;
                });
                int count = 0;
                //判断是否能做将牌
                if (ds.Count >= 2)
                {
                    //移除两张将牌
                    paiT.Remove(pais[i]);
                    paiT.Remove(pais[i]);

                    if (HuPaiPanDin(paiT, ref count))
                    {
                        renresult = true;
                    }
                }
            }
            return renresult;
        }
    }
}
