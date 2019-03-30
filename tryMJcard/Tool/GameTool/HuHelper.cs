using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class HuHelper
    {
        public static List<int> IsSuccess(List<int> handCards ,int changeCard)
        {
            List<int> _handCards = handCards.DeepCloneObject();//将手牌进行深度克隆
            List<int> resultList = new List<int>();
            if (handCards.Count(d=>d==changeCard)>0)//判断是否含有癞子
            {

            }
            else
            {

            }
            return resultList;
        }

        public static List<int> IsSuccess_CanChange()//带癞子的
        {
            List<int> resultList = new List<int>();
            return resultList;
        }

        /// <summary>
        /// 不带癞子的胡牌
        /// </summary>
        /// <param name="_handCards"></param>
        /// <returns></returns>
        public static List<int> IsSuccess_NoChange(List<int> _handCards)//不带癞子的
        {
            List<int> resultList = new List<int>();
            List<int> jiangList = _handCards.GroupBy(d => d).Where(d => d.Count() == 2).Select(d => d.FirstOrDefault()).ToList();//筛选出来能做将牌
            for (int i = 0; i < jiangList.Count; i++)
            {
                List<int> cards = RemoveCard_CertainNumber(_handCards, jiangList[i], 2);//获取一个移除过将牌的手牌
                bool isSuccess = noChangeLogic(cards);
                if (isSuccess)
                {
                    resultList = _handCards;
                    break;
                }
            }
            return resultList;
        }

        /// <summary>
        /// 移除一定数量的某牌  获取一个新的手牌
        /// </summary>
        /// <param name="_handCards"></param>
        /// <param name="card"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> RemoveCard_CertainNumber(List<int> _handCards,int card,int size)
        {
            if (size<=0||card<=0||_handCards==null||_handCards.Count<=0)
            {
                return new List<int>();
            }
            List<int> handCards = new List<int>(_handCards);
            for (int i = 0; i < size; i++)
            {
                handCards.Remove(card);
            }
            return handCards;
        }

        /// <summary>
        /// 移除顺子的牌  获取一个新的手牌
        /// </summary>
        /// <param name="_handCards"></param>
        /// <param name="card"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> RemoveCard_Continuously(List<int> _handCards,int card ,int size)
        {
            if (size<=0||card<=0||_handCards==null||_handCards.Count<=0)
            {
                return new List<int>();
            }
            List<int> handCards = new List<int>(_handCards);
            for (int i = 0; i < size; i++)
            {
                handCards.Remove(card+i);
            }
            return handCards;
        }

        /// <summary>
        /// 不带癞子基础胡牌逻辑
        /// </summary>
        /// <param name="mahs"></param>
        /// <returns></returns>
        private static bool noChangeLogic(List<int> mahs)
        {
            if (mahs.Count == 0)
            {
                return true;
            }
            List<int> fs = mahs.FindAll(delegate(int a)
            {
                return mahs[0] == a;
            });
            if (fs.Count == 3) //组成克子
            {
                mahs = RemoveCard_CertainNumber(mahs,mahs[0],3);//移除刻子
                return noChangeLogic(mahs);
            }
            else//组成顺子
            { 
                if (mahs.Contains(mahs[0] + 1) && mahs.Contains(mahs[0] + 2))//判断顺子是否在
                {
                    mahs = RemoveCard_Continuously(mahs, mahs[0], 3);//移除刻子
                    return noChangeLogic(mahs);
                }
                return false;
            }
        }

        /// <summary>
        /// 移除一定数量的某个牌  会对原数组照成影响
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="_pai"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> RemoveElement(List<int> myList, int _pai, int size)
        {
            List<int> _List = new List<int>();
            if (myList == null || myList.Count == 0 || _pai <= 0 || size <= 0)
            {
                return _List;
            }
            for (int i = 0; i < 3; i++)
            {
                if (myList.Remove(_pai))
                {
                    _List.Add(_pai);
                }
            }
            return _List;
        }

        /// <summary>
        /// 添加顺子元素
        /// </summary>
        /// <param name="_pai"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> AddItem(int _pai, int size)
        {
            List<int> _List = new List<int>();
            if (_pai <= 0 || size <= 0)
            {
                return _List;
            }
            if (_pai % 10 == 8 || _pai % 10 == 9)
            {
                _pai = _pai - _pai % 10 + 7;
                for (int i = 0; i < size; i++)
                {
                    _List.Add(_pai + i);
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    _List.Add(_pai + i);
                }
            }

            return _List;
        }

        /// <summary>
        /// 顺子
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<int> FindStraight(List<int> myList, int size)
        {
            List<int> _List = new List<int>();
            if (myList == null || myList.Count < size || size <= 0)
            {
                return _List;
            }
            List<int> _myList = new List<int>(myList);
            foreach (int item in _myList)
            {
                if (myList.Count >= size)
                {
                    List<int> temp = AddItem(item, size);
                    if (Include(myList, temp))
                    {
                        RemoveElement(myList, temp);
                        _List.AddRange(temp);
                    }
                }
                else
                {
                    break;
                }
            }
            return _List;
        }

        /// <summary>
        /// 从myList中移除_myList
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="_myList"></param>
        /// <returns></returns>
        public static List<int> RemoveElement(List<int> myList, List<int> _myList)
        {
            List<int> _List = new List<int>();
            if (myList == null || myList.Count == 0 || _myList == null || _myList.Count == 0)
            {
                return _List;
            }

            foreach (int item in _myList)
            {
                if (myList.Remove(item))
                {
                    _List.Add(item);
                }
            }
            return _List;
        }

        /// <summary>
        /// 是否为同一类型(用于判断顺子)
        /// </summary>
        /// <param name="myList"></param>
        /// <returns></returns>
        public static bool WithType(List<int> myList)
        {
            if (myList == null || myList.Count == 0)
            {
                return false;
            }

            foreach (int item in myList)
            {
                if (item % 10 == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="_myList"></param>
        /// <returns></returns>
        public static bool Include(List<int> myList, List<int> _myList)
        {
            if (myList == null || myList.Count == 0 || _myList == null || _myList.Count == 0)
            {
                return false;
            }

            List<int> _List = new List<int>(myList);
            foreach (int item in _myList)
            {
                if (!_List.Contains(item))
                {
                    return false;
                }
                else
                {
                    _List.Remove(item);
                }
            }
            return true;
        }

        /// 是否包含
        /// </summary>
        /// <param name="myList"></param>
        /// <param name="_myList"></param>
        /// <returns></returns>
        public static int Include(List<int> myList, List<int> _myList, int hunZiNumber)
        {
            if (myList == null || myList.Count == 0 || _myList == null || _myList.Count == 0)
            {
                return -1;
            }
            int needNumber = 0;
            List<int> _List = new List<int>(myList);
            foreach (int item in _myList)
            {
                if (item % 10 == 0)
                {
                    return -1;
                }
                if (!_List.Contains(item))
                {
                    if (hunZiNumber > 0 && needNumber < 1) //如果缺两张可以不补顺子
                    {
                        hunZiNumber--;
                        needNumber++;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    _List.Remove(item);
                }
            }
            return needNumber <= 0 ? 0 : needNumber;
        }

        /// <summary>
        /// 最大顺子数量
        /// </summary>
        /// <param name="_myList"></param>
        /// <param name="hunzi"></param>
        /// <returns></returns>
        public static int MaxShunCount(List<int> _myList, int hunzi = 0)
        {
            List<int> myList = new List<int>(_myList);
            List<int> fs = myList.FindAll(delegate(int a)
            {
                return hunzi == a;
            });
            List<int> nolzlist = myList.FindAll(delegate(int a)
            {
                return hunzi != a;
            });
            List<int> ziList = nolzlist.Where(d => d > 30).ToList();
            return 1;
        }

        /// <summary>
        /// 手牌排序   根据牌的数量然后大小  三门专用
        /// </summary>
        /// <param name="list"></param>
        public static List<int> ListSort(List<int> _myList)
        {
            if (_myList == null)
            {
                return new List<int>();
            }
            List<int> cardList_O = _myList.FindAll(delegate(int a)//获取普通
            {
                return a < 40;
            });
            List<int> cardList_S = _myList.FindAll(delegate(int a)//获取特殊
            {
                return a > 40;
            });
            List<int> result = new List<int>();
            List<int> cardList_O_D = cardList_O.Distinct().ToList();
            List<int> cardList_S_D = cardList_S.Distinct().ToList();
            result.AddRange(cardList_S_D.OrderByDescending(d => d).OrderByDescending(d => cardList_S.Count(a => a == d)).ToList());
            result.AddRange(cardList_O_D);
            return result;
        }

        /// <summary>
        /// 混子补齐顺子
        /// </summary>
        /// <param name="_mylist"></param>
        /// <param name="hunZiNumber"></param>
        /// <returns></returns>
        public static List<int> GetLinkList(List<int> _mylist, ref int hunZiNumber)
        {
            if (_mylist == null || hunZiNumber == 0)
            {
                return _mylist;
            }
            List<int> resultList = new List<int>();
            List<int> itemList = _mylist.OrderBy(d => d).ToList();
            for (int i = 0; i < itemList.Count; i++)
            {
                List<int> temp = AddItem(itemList[i], 3);
                int result = Include(_mylist, temp, hunZiNumber);
                if (result <= hunZiNumber && result >= 0 && itemList[i] < 30)
                {
                    hunZiNumber -= result;
                    RemoveElement(_mylist, temp);
                    resultList.AddRange(temp);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 获取最大刻
        /// </summary>
        /// <param name="_handCard"></param>
        /// <param name="hunZi"></param>
        public static List<int> GetMaxKe(List<int> _handCard, int hunZi)
        {
            List<int> constantList = new List<int>();
            List<int> hunZiList = _handCard.FindAll(delegate(int a)//获取赖子
            {
                return hunZi == a;
            });
            if (hunZiList.Count <= 0)
            {
                return _handCard;
            }
            List<int> nolzlist = _handCard.FindAll(delegate(int a)//获取非赖子
            {
                return hunZi != a;
            });
            List<int> needList = new List<int>(nolzlist);
            List<int> itemList = nolzlist.Distinct().ToList();
            constantList.AddRange(FindStraight(needList, 3));
            needList = needList.OrderByDescending(d => d).ToList();
            int hunZiNumber = hunZiList.Count;
            constantList.AddRange(GetLinkList(needList, ref hunZiNumber));
            List<int> keList = ListSort(needList);
            List<int> result = new List<int>(constantList);
            result.AddRange(needList);
            for (int i = 0; i < keList.Count; i++)
            {
                int card = keList[i];
                int cardNumber = needList.Count(d => d == card);
                if (cardNumber >= 3)
                {
                    RemoveElement(needList, card, cardNumber);
                    continue;
                }
                if (cardNumber + hunZiNumber >= 3)
                {
                    int needNumber = (3 - cardNumber);//刻
                    hunZiNumber -= needNumber;
                    for (int a = 0; a < needNumber; a++)
                    {
                        result.Add(card);
                    }
                    RemoveElement(needList, card, cardNumber);
                }
                else
                {
                    if (cardNumber == 1)
                    {
                        int needNumber = (2 - cardNumber);//将
                        hunZiNumber -= needNumber;
                        result.Add(card);
                    }
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取最大刻
        /// </summary>
        /// <param name="_handCard"></param>
        /// <param name="hunZi"></param>
        public static List<int> GetMaxShun(List<int> _handCard, int hunZi)
        {
            List<int> constantList = new List<int>();
            List<int> hunZiList = _handCard.FindAll(delegate(int a)//获取赖子
            {
                return hunZi == a;
            });
            if (hunZiList.Count <= 0)
            {
                return _handCard;
            }
            List<int> nolzlist = _handCard.FindAll(delegate(int a)//获取非赖子
            {
                return hunZi != a;
            });
            List<int> needList = new List<int>(nolzlist);
            List<int> itemList = nolzlist.Distinct().ToList();
            constantList.AddRange(FindStraight(needList, 3));
            needList = needList.OrderByDescending(d => d).ToList();
            int hunZiNumber = hunZiList.Count;
            constantList.AddRange(GetLinkList(needList, ref hunZiNumber));
            List<int> shunList = ListSort(needList);
            List<int> result = new List<int>(constantList);
            result.AddRange(needList);
            for (int i = 0; i < shunList.Count; i++)
            {
                int card = shunList[i];
                int cardNumber = needList.Count(d => d == card);
                if (cardNumber >= 3)
                {
                    RemoveElement(needList, card, cardNumber);
                    continue;
                }
                if (cardNumber + hunZiNumber >= 3)
                {
                    int needNumber = (3 - cardNumber);//刻
                    hunZiNumber -= needNumber;
                    for (int a = 0; a < needNumber; a++)
                    {
                        result.Add(card);
                    }
                    RemoveElement(needList, card, cardNumber);
                }
                else
                {
                    if (cardNumber == 1)
                    {
                        int needNumber = (2 - cardNumber);//将
                        hunZiNumber -= needNumber;
                        result.Add(card);
                    }
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取顺子
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static List<List<int>> GetShunList(int card)
        {
            int ten = card / 10;
            int card_mode = card - 2;
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                card_mode++;
                List<int> mode = new List<int>() { card_mode - 1, card_mode, card_mode + 1 };
                if (mode.Count(d => d % 10 == 0) > 0)
                {
                    continue;
                }
                result.Add(mode);

            }
            return result;
        }

        /// <summary>
        /// 十三幺
        /// </summary>
        /// <param name="mah"></param>
        /// <returns></returns>
        private static bool ShiSanYao(List<int> mah)
        {
            if (mah.Count != 14)
            {
                return false;
            }
            bool isShiSanYao = true;
            List<int> shisanyao = new List<int>();
            int[] shiSanYao = { 1, 9, 11, 19, 21, 29, 31, 33, 35, 37, 41, 43, 45 };
            for (int j = 0; j < mah.Count; ++j)
            {
                if (shiSanYao.Contains(mah[j]))
                {
                    if (!shisanyao.Contains(mah[j]))
                    {
                        shisanyao.Add(mah[j]);
                    }
                }
                else
                {
                    isShiSanYao = false;
                    break;
                }
            }
            if (shisanyao.Count != shiSanYao.Length)
            {
                return false;
            }
            return isShiSanYao;
        }

        /// <summary>
        /// 十三幺补齐
        /// </summary>
        /// <param name="mah"></param>
        /// <param name="hunzi"></param>
        /// <returns></returns>
        public static List<int> ShiSanYao(List<int> mah, int hunzi)
        {
            List<int> result = new List<int>();
            if (mah.Count != 13)
            {
                return result;
            }
            List<int> fs = mah.FindAll(delegate(int a)
            {
                return hunzi == a;
            });
            List<int> nolzlist = mah.FindAll(delegate(int a)
            {
                return hunzi != a;
            });
            List<int> shisanyao = new List<int>() { 1, 9, 11, 19, 21, 29, 31, 33, 35, 37, 41, 43, 45 };
            List<int> needCards = new List<int>();//需要的牌
            bool exist_Jiang = false;
            #region 将牌
            if (shisanyao.Count(d => nolzlist.Count(a => a == d) == 2) == 1)
            {
                exist_Jiang = true;
            }
            #endregion
            #region 得出我所需要的牌
            for (int i = 0; i < shisanyao.Count; i++)
            {
                int card = shisanyao[i];
                if (!nolzlist.Contains(card))
                {
                    needCards.Add(card);
                }
            }
            #endregion
            if (needCards.Count + (exist_Jiang ? 0 : 1) <= fs.Count + 1) //比如说  少一个45   需要数量为1个来进行补齐，如果
            {
                if (exist_Jiang)
                {
                    result.AddRange(needCards);
                }
                else
                {
                    result.AddRange(shisanyao);
                }
            }
            return result;
        }

        /// <summary>
        /// 是否为十三幺
        /// </summary>
        /// <param name="mah"></param>
        /// <param name="hunzi"></param>
        /// <returns></returns>
        public static bool ShiSanYao_bool(List<int> mah, int hunzi)
        {
            if (mah.Count != 14)
            {
                return false;
            }
            List<int> fs = mah.FindAll(delegate(int a)
            {
                return hunzi == a;
            });
            List<int> nolzlist = mah.FindAll(delegate(int a)
            {
                return hunzi != a;
            });
            List<int> shisanyao = new List<int>() { 1, 9, 11, 19, 21, 29, 31, 33, 35, 37, 41, 43, 45 };
            List<int> needCards = new List<int>();//需要的牌
            bool exist_Jiang = false;
            #region 将牌
            if (shisanyao.Count(d => nolzlist.Count(a => a == d) == 2) == 1)
            {
                exist_Jiang = true;
            }
            #endregion
            #region 得出我所需要的牌
            for (int i = 0; i < shisanyao.Count; i++)
            {
                int card = shisanyao[i];
                if (!nolzlist.Contains(card))
                {
                    needCards.Add(card);
                }
            }
            #endregion
            if (needCards.Count + (exist_Jiang ? 0 : 1) == fs.Count) //比如说  少一个45   需要数量为1个来进行补齐，如果
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 一条龙
        /// </summary>
        /// <param name="mah"></param>
        /// <returns></returns>
        public static bool YiTiaoLong(List<int> mah)
        {
            bool WanLong = true;
            bool TiaoLong = true;
            bool GangLong = true;
            int[] index = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 3; ++i)
            {
                int card;
                foreach (int a in index)
                {
                    card = a + i * 10;
                    if (!mah.Contains(card))
                    {
                        switch (i)
                        {
                            case 0:
                                WanLong = false;
                                break;
                            case 1:
                                TiaoLong = false;
                                break;
                            case 2:
                                GangLong = false;
                                break;
                            default:
                                Console.WriteLine("错误");
                                break;
                        }
                        break;
                    }

                }
            }
            if (WanLong || TiaoLong || GangLong)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 碰碰胡
        /// </summary>
        /// <param name="mah"></param>
        /// <returns></returns>
        public static bool pengpenghu(List<int> mah)
        {
            bool ok = false;
            List<int> mahclon = new List<int>(mah);
            List<int> cardnumber = new List<int>();
            int[] pph = new int[5];
            foreach (int card in mahclon)
            {
                if (!cardnumber.Contains(card))
                {
                    cardnumber.Add(card);
                }
            }
            if (cardnumber.Count == 5)
            {
                ok = true;
                int i = 0;
                for (int g = 0; g < mah.Count; g++)
                {
                    if (mahclon.Contains(cardnumber[i]))
                    {
                        pph[i] += 1;
                        mahclon.Remove(cardnumber[i]);
                        if (!mahclon.Contains(cardnumber[i]))
                        {
                            i++;
                        }
                    }
                }
            }
            if (ok)
            {
                int jiang = 0;
                int lian = 0;
                foreach (int num in pph)
                {
                    if (num == 2)
                    {
                        jiang += 1;
                    }
                    else if (num == 3)
                    {
                        lian += 1;
                    }
                }
                if (jiang == 1 && lian == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 七小对
        /// </summary>
        /// <param name="mah"></param>
        /// <returns></returns>
        public static bool qixiaodui(List<int> mah)
        {//七对+1番(student s) => s.Sex == false)

            if (true)
            {
                bool haohuaqixiaodui = false;
                foreach (int a in mah)
                {
                    int c = 0;
                    foreach (int b in mah)
                    {
                        if (a == b)
                        {
                            c++;
                        }
                    }
                    if (c == 4)
                    {
                        haohuaqixiaodui = true;
                        break;
                    }
                }
                if (haohuaqixiaodui)
                {
                    Console.WriteLine("豪华七小对");
                }
                else
                {
                    Console.WriteLine("七小对");
                }
                return true;
            }
        }

        /// <summary>
        /// 排列组合
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
    }
}
