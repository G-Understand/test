using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool
{
    class LzMjRule
    {

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
                    temp = pais.FindAll(delegate(int d)
                    {
                        return d > 20 && d < 30;
                    });
                    break;
                case PaiXing.TIAO:
                    temp = pais.FindAll(delegate(int d)
                    {
                        return d > 10 && d < 20;
                    });
                    break;
                case PaiXing.TONG:
                    temp = pais.FindAll(delegate(int d)
                    {
                        return d > 0 && d < 10;
                    });
                    break;
                case PaiXing.ZI:
                    temp = pais.FindAll(delegate(int d)
                    {
                        return d > 30 && d < 46;
                    });
                    break;
                default:
                    break;
            }
            return temp;
        }



        /// <summary>
        /// 能否听牌
        /// </summary>
        /// <param name="allpai">所有牌</param>
        /// <param name="laizi">癞子</param>
        /// <returns></returns>
        public static List<TingPaiModel> isCanting(List<int> allpai, int laizi, bool daifeng, out List<fanmodel> listfanmodel, GameRule guize, int hunzi = 0)
        {
            listfanmodel = new List<fanmodel>();
            List<TingPaiModel> CanTinglist = new List<TingPaiModel>();

            List<int> fs = allpai.FindAll(delegate(int a)
            {
                return laizi == a || hunzi == a;
            });
            List<int> nolzlist = allpai.FindAll(delegate(int a)
            {
                return laizi != a && hunzi != a;
            });
            if (fs.Count == 0)
            {
                //跳转到普通牌判断是否能听
                listfanmodel = new List<fanmodel>();
                return new List<TingPaiModel>();
                //   return Rule.isCanTing(allpai);
            }

            var listjiang = new List<int>();
            listjiang.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            listjiang.AddRange(new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            listjiang.AddRange(new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29 });
            if (daifeng)
            {
                listjiang.AddRange(new int[] { 31, 33, 35, 37 });
                listjiang.AddRange(new int[] { 41, 43, 45 });
            }

            for (int i = 0; i < listjiang.Count; i++)
            {
                List<int> paiT = new List<int>(nolzlist);
                var jiang = listjiang[i];
                bool isonlyone = false;
                if (paiT.Count(d => d == jiang) >= 2)
                {
                    paiT.Remove(jiang);
                    paiT.Remove(jiang);
                }
                else if (paiT.Count(d => d == jiang) >= 1)
                {
                    paiT.Remove(jiang);
                    isonlyone = true;
                }

                ////避免重复运算 将光标移到其他牌上
                //i += ds.Count;
                //需要的 癞子个数
                int wancount = 0, tongcount = 0, tiaocount = 0, zicount = 0;
                var sumcount = 0;
                //拿到各种花色的牌
                if (jiang == 23)
                {
                    var d = 0;
                }
                HuModel huModel = new HuModel();

                var wanlist = IsPu(GetPaiXing(PaiXing.WAN, paiT), ref wancount, fs.Count, nolzlist.ToList(), guize, ref  huModel);
                var tonglist = IsPu(GetPaiXing(PaiXing.TONG, paiT), ref tongcount, fs.Count, nolzlist.ToList(), guize, ref huModel);
                var tiaolist = IsPu(GetPaiXing(PaiXing.TIAO, paiT), ref tiaocount, fs.Count, nolzlist.ToList(), guize, ref huModel);
                var zilist = IsPu(GetPaiXing(PaiXing.ZI, paiT), ref zicount, fs.Count, nolzlist.ToList(), guize, ref huModel);

                sumcount = wancount + tongcount + tiaocount + zicount;
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
                    CanTinglist = new List<TingPaiModel>();
                    TingPaiModel tingPaiModel = new TingPaiModel();
                    tingPaiModel.pai = 55;
                    tingPaiModel.paiType = 3;
                    tingPaiModel.fengLianCount += huModel.fengLianCount;
                    CanTinglist.Add(tingPaiModel);
                }
                else
                {//满足胡的条件， 得出需要的牌
                    var listcurrent = new List<TingPaiModel>();
                    if (isonlyone)
                    {
                        TingPaiModel tingPaiModel = new TingPaiModel();
                        tingPaiModel.pai = jiang;
                        tingPaiModel.paiType = 3;

                        listcurrent.Add(tingPaiModel);
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
                    for (int a = 0; a < listcurrent.Count; a++)
                    {
                        listcurrent[a].fengLianCount += huModel.fengLianCount;
                    }
                    if (listcurrent.Count(d => d.pai == jiang) > 0)
                    {
                        var mod = listcurrent.FirstOrDefault(d => d.pai == jiang);
                        mod.paiType = 3;
                    }
                    CanTinglist.AddRange(listcurrent);
                    CanTinglist = CanTinglist.Distinct().ToList();

                }
            }
            if (CanTinglist.Count(d => d.pai == 55) > 0)
            {
                CanTinglist = new List<TingPaiModel>();
                TingPaiModel tingPaiModel = new TingPaiModel();
                tingPaiModel.pai = 55;
                tingPaiModel.paiType = 3;
                CanTinglist.Add(tingPaiModel);
            }
            if (CanTinglist.Count(d => d.pai == 55) <= 0 && fs.Count == 4)
            {
                CanTinglist = new List<TingPaiModel>();
                TingPaiModel tingPaiModel = new TingPaiModel();
                tingPaiModel.pai = 55;
                tingPaiModel.paiType = 3;
                CanTinglist.Add(tingPaiModel);
            }
            CanTinglist = CanTinglist.Where(d => d.pai % 10 != 0).ToList();
            CanTinglist = CanTinglist.OrderByDescending(d => d.paiType).OrderBy(d => d.pai).ToList();

            var rtnTingList = new List<TingPaiModel>();
            for (int i = 0; i < CanTinglist.Count; i++)
            {
                if (rtnTingList.Count(d => d.pai == CanTinglist[i].pai) <= 0)
                {
                    rtnTingList.Add(CanTinglist[i]);
                }
                else
                {
                    TingPaiModel tingPaiModel = rtnTingList.FirstOrDefault(d => d.pai == CanTinglist[i].pai);
                    tingPaiModel.fengLianCount = tingPaiModel.fengLianCount > CanTinglist[i].fengLianCount ? tingPaiModel.fengLianCount : CanTinglist[i].fengLianCount;
                    tingPaiModel.paiType = tingPaiModel.paiType > CanTinglist[i].paiType ? tingPaiModel.paiType : CanTinglist[i].paiType;
                }
            }
            return rtnTingList;
        }


        /// <summary>
        /// 是否是整扑
        /// </summary>
        /// <param name="huaselist"></param>
        /// <returns></returns>
        private static List<TingPaiModel> IsPu(List<int> huaselist, ref int needcount, int laizicount, List<int> listall, GameRule guize, ref HuModel huModel)
        {
            List<TingPaiModel> CanTinglist = new List<TingPaiModel>();
            if (huaselist.Count <= 0)
            {
                return CanTinglist;
            }

            if (needcount > laizicount + 1)
            {
                return CanTinglist;
            }
            //优先刻字 顺子

            var listneedpai = new List<TingPaiModel>();
            List<int> templist = new List<int>(huaselist);

            var listdel = new List<int>();

            //>30 的三个的先移除
            var listsantiaomajiang = GetAllSanTiao(templist.Where(d => d > 30).ToList(), 2);
            if (listsantiaomajiang != null && listsantiaomajiang.Count > 0)
            {
                for (int i = 0; i < listsantiaomajiang.Count; i++)
                {
                    templist.Remove(listsantiaomajiang[i].Value);
                    templist.Remove(listsantiaomajiang[i].Value);
                    templist.Remove(listsantiaomajiang[i].Value);
                }
            }

            //如果不是 风嘴子 才走普通模式 30 以上 计算


            //如果单>30 的 有几个就需要几个*2的癞子
            if (!guize.isFengZuiZi)
            {//如果单>30 的 有几个就需要几个*2的癞子
                var listlastduizi = FindDuiZi(templist.Where(d => d > 30).ToList());//剩下牌找>30 的，有几个对子，就需要几个 癞子来组合成整铺
                for (int i = 0; i < listlastduizi.Count; i++)
                {
                    TingPaiModel tingPaiModel = new TingPaiModel();
                    tingPaiModel.pai = listlastduizi[i];
                    tingPaiModel.paiType = 2;
                    listneedpai.Add(tingPaiModel);
                }
                needcount += listlastduizi.Count;//给对子移除，剩下牌就都是单张，单张找顺子
                for (int i = 0; i < listlastduizi.Count; i++)
                {
                    templist.Remove(listlastduizi[i]);
                    templist.Remove(listlastduizi[i]);
                }//如果单>30 的 有几个就需要几个*2的癞子
                var list30 = templist.Where(d => d > 30).ToList();
                for (int i = 0; i < list30.Count; i++)
                {
                    TingPaiModel tingPaiModel = new TingPaiModel();
                    tingPaiModel.pai = list30[i];
                    tingPaiModel.paiType = 2;
                    listneedpai.Add(tingPaiModel);
                }
                needcount += list30.Count * 2;
                templist = templist.Where(d => d < 30).ToList();
            }

            //先拿到手牌中的顺子，然后一个一个移除之后，计算余牌中的需要癞子
            var listallshunzi = FindShunZi1(templist);
            if (listallshunzi.Count > 1)
            {
                var listzuijia = new List<ZuiJiaTingPai>();

                for (int m = 0; m < listallshunzi.Count; m++)
                {
                    ZuiJiaTingPai zuijiamodel = new ZuiJiaTingPai();
                    var tempneedlistpai = new List<TingPaiModel>();
                    var listlast1 = templist.ToList();

                    int tempneedcount = needcount;
                    var listlast2 = new List<int>();
                    listlast2.AddRange(listlast1);

                    var listnewxuyaopai1 = new List<int>();
                    var listnewxuyaopai2 = new List<int>();

                    BianLiyouxianduizi2(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true, false, guize, ref huModel);

                    zuijiamodel.needcount = tempneedcount;
                    zuijiamodel.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel);


                    listall = templist.OrderByDescending(d => d).ToList();
                    listlast1 = templist.OrderByDescending(d => d).ToList();

                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();//sdfgsdfg
                    BianLiyouxianduizi2Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodeldelsantiaoDesc = new ZuiJiaTingPai();
                    zuijiamodeldelsantiaoDesc.needcount = tempneedcount;
                    zuijiamodeldelsantiaoDesc.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodeldelsantiaoDesc);


                    listall = templist.ToList();
                    listlast1 = templist.ToList();
                    //while (listallshunzi[m].All(d => listlast1.Any(x => x == d)))
                    //{
                    //    listlast1 = FindSantiaoShunzi(listlast1, listallshunzi[m], out listdel);//移除三条、顺子后剩余的牌
                    //}

                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianduizi1(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel1 = new ZuiJiaTingPai();
                    zuijiamodel1.needcount = tempneedcount;
                    zuijiamodel1.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel1);

                    listall = templist.OrderByDescending(d => d).ToList();
                    listlast1 = templist.OrderByDescending(d => d).ToList();
                    //while (listallshunzi[m].All(d => listlast1.Any(x => x == d)))
                    //{
                    //    listlast1 = FindSantiaoShunzi(listlast1, listallshunzi[m], out listdel);//移除三条、顺子后剩余的牌
                    //}

                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianduizi1Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel1Desc = new ZuiJiaTingPai();
                    zuijiamodel1Desc.needcount = tempneedcount;
                    zuijiamodel1Desc.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel1Desc);


                    listall = templist.ToList();
                    listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianduizi0(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel0 = new ZuiJiaTingPai();
                    zuijiamodel0.needcount = tempneedcount;
                    zuijiamodel0.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel0);


                    listall = templist.OrderByDescending(d => d).ToList();
                    listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianduizi0Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel0delsantiaoDesc = new ZuiJiaTingPai();
                    zuijiamodel0delsantiaoDesc.needcount = tempneedcount;
                    zuijiamodel0delsantiaoDesc.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel0delsantiaoDesc);


                    listall = templist.ToList();
                    listlast1 = templist.ToList();
                    //while (listallshunzi[m].All(d => listlast1.Any(x => x == d)))
                    //{
                    //    listlast1 = FindSantiaoShunzi(listlast1, listallshunzi[m], out listdel);//移除三条、顺子后剩余的牌
                    //}

                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianshunzi2(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel2 = new ZuiJiaTingPai();
                    zuijiamodel2.needcount = tempneedcount;
                    zuijiamodel2.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel2);

                    listall = templist.ToList();
                    listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                    //while (listallshunzi[m].All(d => listlast1.Any(x => x == d)))
                    //{
                    //    listlast1 = FindSantiaoShunzi(listlast1, listallshunzi[m], out listdel);//移除三条、顺子后剩余的牌
                    //}

                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianshunzi1(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel3 = new ZuiJiaTingPai();
                    zuijiamodel3.needcount = tempneedcount;
                    zuijiamodel3.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel3);


                    listall = templist.ToList();
                    listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianshunzi0(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel4 = new ZuiJiaTingPai();
                    zuijiamodel4.needcount = tempneedcount;
                    zuijiamodel4.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel4);


                    listall = templist.OrderByDescending(d => d).ToList();
                    listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
                    tempneedcount = needcount;
                    tempneedlistpai = new List<TingPaiModel>();
                    BianLiyouxianshunzi0(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);

                    var zuijiamodel5Desc = new ZuiJiaTingPai();
                    zuijiamodel5Desc.needcount = tempneedcount;
                    zuijiamodel5Desc.needlistpai = tempneedlistpai;
                    listzuijia.Add(zuijiamodel5Desc);

                }
                listzuijia = listzuijia.OrderByDescending(d => d.needlistpai.Count).OrderBy(d => d.needcount).ToList();

                if (listzuijia.Count > 0)
                {
                    var zuijiacount = listzuijia.FirstOrDefault().needcount;
                    for (int i = 0; i < listzuijia.Count; i++)
                    {
                        if (listzuijia[i].needcount == zuijiacount)
                        {
                            listneedpai.AddRange(listzuijia[i].needlistpai);
                        }
                    }
                    needcount = listzuijia.FirstOrDefault().needcount;
                }
            }
            else
            {
                var listzuijia = new List<ZuiJiaTingPai>();
                var listlastall = templist.ToList();//30 以内的找顺子 或刻字

                var listlast1 = listlastall.ToList();
                int needcount2 = needcount;
                var listlast2 = new List<int>();
                listlast2.AddRange(listlast1);

                var listneedpaitotal1 = listneedpai.ToList();
                var listneedpaitotal2 = listneedpai.ToList();

                var listnewxuyaopai1 = new List<int>();
                var listnewxuyaopai2 = new List<int>();
                int tempneedcount = needcount;
                var tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi2(listlastall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, true, false, guize, ref huModel);
                int humodele1 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                ZuiJiaTingPai zuijiamodel = new ZuiJiaTingPai();
                zuijiamodel.needcount = tempneedcount;
                zuijiamodel.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel);


                listlastall = templist.OrderByDescending(d => d).ToList();
                listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi2Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele2 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodeldelsantiaoDesc = new ZuiJiaTingPai();
                zuijiamodeldelsantiaoDesc.needcount = tempneedcount;
                zuijiamodeldelsantiaoDesc.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodeldelsantiaoDesc);


                listlastall = templist.ToList();
                listlast1 = templist.ToList();//30 以内的找顺子 或刻字+++
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi1(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele3 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel1 = new ZuiJiaTingPai();
                zuijiamodel1.needcount = tempneedcount;
                zuijiamodel1.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel1);


                listlastall = templist.OrderByDescending(d => d).ToList();
                listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字---
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi1Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele4 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel1delsantiaoDesc = new ZuiJiaTingPai();
                zuijiamodel1delsantiaoDesc.needcount = tempneedcount;
                zuijiamodel1delsantiaoDesc.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel1delsantiaoDesc);


                listlastall = templist.ToList();
                listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi0(listlastall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele5 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel0 = new ZuiJiaTingPai();
                zuijiamodel0.needcount = tempneedcount;
                zuijiamodel0.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel0);


                listlastall = templist.OrderByDescending(d => d).ToList();
                listlast1 = templist.OrderByDescending(d => d).ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianduizi0Desc(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele6 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel0delsantiaoDesc = new ZuiJiaTingPai();
                zuijiamodel0delsantiaoDesc.needcount = tempneedcount;
                zuijiamodel0delsantiaoDesc.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel0delsantiaoDesc);

                listlastall = templist.ToList();
                listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianshunzi2(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele7 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel2 = new ZuiJiaTingPai();
                zuijiamodel2.needcount = tempneedcount;
                zuijiamodel2.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel2);


                listlastall = templist.ToList();
                listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianshunzi1(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele8 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel3 = new ZuiJiaTingPai();
                zuijiamodel3.needcount = tempneedcount;
                zuijiamodel3.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel3);


                listlastall = templist.ToList();
                listlast1 = templist.ToList();//30 以内的找顺子 或刻字
                tempneedcount = needcount;
                tempneedlistpai = new List<TingPaiModel>();
                BianLiyouxianshunzi0(listall, listlast1, ref tempneedcount, ref tempneedlistpai, laizicount, false, true, guize, ref huModel);
                int humodele9 = huModel.fengLianCount;
                huModel.fengLianCount = 0;//每次都进行单独计算
                var zuijiamodel4 = new ZuiJiaTingPai();
                zuijiamodel4.needcount = tempneedcount;
                zuijiamodel4.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel4);

                huModel.fengLianCount = humodele1;
                if (huModel.fengLianCount < humodele3)//3
                {
                    huModel.fengLianCount = humodele3;
                }
                if (huModel.fengLianCount < humodele2)//2
                {
                    huModel.fengLianCount = humodele2;
                }
                if (huModel.fengLianCount < humodele4)//4
                {
                    huModel.fengLianCount = humodele4;
                }
                if (huModel.fengLianCount < humodele5)//5
                {
                    huModel.fengLianCount = humodele5;
                }
                if (huModel.fengLianCount < humodele6)//6
                {
                    huModel.fengLianCount = humodele6;
                }
                if (huModel.fengLianCount < humodele7)//7
                {
                    huModel.fengLianCount = humodele7;
                }
                if (huModel.fengLianCount < humodele8)//8
                {
                    huModel.fengLianCount = humodele8;
                }
                if (huModel.fengLianCount < humodele9)//9
                {
                    huModel.fengLianCount = humodele9;
                }
                //huModel.fengLianCount = humodele3 > humodele1 ? humodele3 : humodele1;//取最大
                var zuijiamodel6 = new ZuiJiaTingPai();
                zuijiamodel6.needcount = tempneedcount;
                zuijiamodel6.needlistpai = tempneedlistpai;
                listzuijia.Add(zuijiamodel6);

                listzuijia = listzuijia.OrderByDescending(d => d.needlistpai.Count).OrderBy(d => d.needcount).ToList();
                if (listzuijia.Count > 0)
                {
                    var zuijiacount = listzuijia.FirstOrDefault().needcount;
                    for (int i = 0; i < listzuijia.Count; i++)
                    {
                        if (listzuijia[i].needcount == zuijiacount)
                        {
                            listneedpai.AddRange(listzuijia[i].needlistpai);
                        }
                    }
                    needcount = listzuijia.FirstOrDefault().needcount;
                }
            }

            // Logger.Log("牌 " + huaselist.ToJson() + " 优先刻字、顺子 将牌为：" + jiang + "需要的癞子数量为 " + needcount1 + "需要的牌 " + listneedpai1.ToJson());
            listneedpai = listneedpai.Distinct().ToList();
            CanTinglist = listneedpai;

            return CanTinglist;
        }

        public class ZuiJiaTingPai
        {
            public int needcount { get; set; }
            public List<TingPaiModel> needlistpai { get; set; }
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
        public static List<int> BianLiyouxianduizi0(List<int> listallpai, List<int> listpai, ref int needlaizi, ref  List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        if (listpai.Count(d => d == pai) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                    }

                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            int fengCount = 0;
                            //PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            PlayRules.IsFengZuiZi_LZ_Z(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {

                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                    }

                    if (pai < 30)
                    {
                        #region <30 的
                        if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                        {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            listpai.Remove(pai + 2);
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        //与当前牌组合需要一个癞子的牌
                        if (listpai.Contains(pai + 1))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (pai / 10 == (pai + 2) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 2;
                                if (pai < 30 && pai % 10 == 1) //手中有 12 缺3
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            if (pai % 10 >= 2)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 1;
                                if (pai < 30 && pai % 10 == 8)
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai + 1;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            listneedpai.Add(tingPaiModel);

                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);


                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_1.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }

                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        #endregion
                    }

                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
        }


        public static List<int> BianLiyouxianshunzi0(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }

                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }

                    if (pai < 30)
                    {
                        #region 普通牌
                        if (listpai.Contains(pai + 1) && !listpai.Contains(pai + 2) && (pai / 10) == ((pai + 1) / 10))
                        {
                            //如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            #region  如果可与当前值 组成顺子的牌有2个，则需要癞子一个
//                             if (!youxianduizi)
//                             {
//                                 var d = 0;
//                             }
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if ((pai + 2) % 10 != 0)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 2;
                                listneedpai.Add(tingPaiModel);
                            }
                            if (pai % 10 >= 2)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 1;

                                if (pai < 30 && pai % 10 == 8)
                                {
                                    //8 9缺 7
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
//                             if (!youxianduizi)
//                             {
//                                 var d = 0;
//                             }
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai + 1;
                            tingPaiModel.paiType = 2;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个

                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
//                             if (pai == 12)
//                             {
//                                 var x = "dddd";
//                             }
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;

                            listneedpai.Add(tingPaiModel);
                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_2.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            return BianLiyouxianshunzi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        #endregion
                    }

                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 3;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//两张可以是补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 3;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            TingPaiModel tingPaiModelPai = new TingPaiModel();
                            tingPaiModelPai.pai = pai;
                            tingPaiModelPai.fengLianCount = -10;
                            tingPaiModelPai.paiType = 3;
                            listneedpai.Add(tingPaiModelPai);
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {
                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 3;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            needlaizi += 1;
                            huModel.fengLianCount += 1;//两张可以是补成顺子
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 3;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            TingPaiModel tingPaiModelPai = new TingPaiModel();
                            tingPaiModelPai.pai = pai;
                            tingPaiModelPai.paiType = 3;
                            tingPaiModelPai.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelPai);
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi0(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }

                }
            }
            return new List<int>();
        }

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
        public static List<int> BianLiyouxianduizi2(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }

                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            PlayRules.IsFengZuiZi_LZ_Z(ref listpai, ref fengCount);
                            //PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
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
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                    }
                    if (pai < 30)
                    {
                        #region 普通牌
                        //第一个数 先判断加两个癞子是否满足
                        if (laizicount - needlaizi > 0 && pai == listallpai[0])
                        {
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);
                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai - 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 1;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel3 = new TingPaiModel();
                                tingPaiModel3.pai = pai + 2;
                                tingPaiModel3.paiType = 0;
                                listneedpai.Add(tingPaiModel3);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai - 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 1;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel3 = new TingPaiModel();
                                tingPaiModel3.pai = pai + 2;
                                tingPaiModel3.paiType = 0;
                                listneedpai.Add(tingPaiModel3);

                                TingPaiModel tingPaiModel4 = new TingPaiModel();
                                tingPaiModel4.pai = pai - 2;
                                tingPaiModel4.paiType = 0;
                                listneedpai.Add(tingPaiModel4);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai - 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 1;
                                //tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel3 = new TingPaiModel();
                                tingPaiModel3.pai = pai - 2;
                                tingPaiModel3.paiType = 0;
                                listneedpai.Add(tingPaiModel3);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai - 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai - 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }

                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
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
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                if (pai / 10 == (pai + 2) / 10)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 2;
                                    tingPaiModel1.paiType = 0;
                                    listneedpai.Add(tingPaiModel1);
                                }
                                if (pai % 10 >= 2)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    tingPaiModel1.paiType = 0;
                                    listneedpai.Add(tingPaiModel1);
                                }
                                return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            }
                            else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                            {
//                                 if (!youxianduizi)
//                                 {
//                                     var d = 0;
//                                 }
                                //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                                listpai.Remove(pai);
                                listpai.Remove(pai + 2);
                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 0;
                                listneedpai.Add(tingPaiModel1);

                                return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            }
                            else if (listpai.Count(d => d == pai) == 2)
                            {
                                //如果是对子，则需要一个
                                listpai.Remove(pai);
                                listpai.Remove(pai);

                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai;
                                tingPaiModel1.paiType = 3;
                                listneedpai.Add(tingPaiModel1);
                                return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            }
                            else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                            {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                                listpai.Remove(pai);
                                needlaizi += 2;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    //needlaizi -= 2;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai;
                                //tingPaiModel.paiType = 3;
                                listneedpai.Add(tingPaiModel);
                                if (pai % 10 == 1)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);
                                }
                                else if (pai % 10 == 2)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    //tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 1;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);

                                    TingPaiModel tingPaiModel3 = new TingPaiModel();
                                    tingPaiModel3.pai = pai + 2;
                                    tingPaiModel3.paiType = 0;
                                    listneedpai.Add(tingPaiModel3);
                                }
                                else if (pai % 10 > 2 && pai % 10 < 8)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 1;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);

                                    TingPaiModel tingPaiModel3 = new TingPaiModel();
                                    tingPaiModel3.pai = pai + 2;
                                    tingPaiModel3.paiType = 0;
                                    listneedpai.Add(tingPaiModel3);

                                    TingPaiModel tingPaiModel4 = new TingPaiModel();
                                    tingPaiModel4.pai = pai - 2;
                                    tingPaiModel4.paiType = 0;
                                    listneedpai.Add(tingPaiModel4);
                                }
                                else if (pai % 10 == 8)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 1;
                                    listneedpai.Add(tingPaiModel2);

                                    TingPaiModel tingPaiModel3 = new TingPaiModel();
                                    tingPaiModel3.pai = pai - 2;
                                    tingPaiModel3.paiType = 0;
                                    listneedpai.Add(tingPaiModel3);
                                }
                                else if (pai % 10 == 9)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai - 2;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);
                                }

                                return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            }
                        }
                        #endregion
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 3;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//两张可以是补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个或一个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 3;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            TingPaiModel tingPaiModelPai = new TingPaiModel();
                            tingPaiModelPai.pai = pai;
                            tingPaiModelPai.fengLianCount = -10;
                            tingPaiModelPai.paiType = 3;
                            listneedpai.Add(tingPaiModelPai);
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {
                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 3;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            needlaizi += 1;
                            huModel.fengLianCount += 1;//两张可以是补成顺子
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 3;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            TingPaiModel tingPaiModelPai = new TingPaiModel();
                            tingPaiModelPai.pai = pai;
                            tingPaiModelPai.paiType = 3;
                            tingPaiModelPai.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelPai);
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
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

        public class RepeatInfo
        {
            // 值
            public int Value { get; set; }
            // 重复次数
            public int RepeatNum { get; set; }
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

        public static List<int> BianLiyouxianshunzi1(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == pai) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;

                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }

                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (pai < 30)
                    {
                        #region 普通牌
                        if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                        {//
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            listpai.Remove(pai + 2);
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        if (listpai.Contains(pai + 1))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (pai / 10 == (pai + 2) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 2;
                                if (pai < 30 && pai % 10 == 1)
                                {
                                    //1 2缺 3
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            if (pai % 10 >= 2)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 1;
                                if (pai < 30 && pai % 10 == 1)
                                {
                                    //2 3缺 1
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
//                             if (!youxianduizi)
//                             {
//                                 var d = 0;
//                             }
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai + 1;
                            tingPaiModel.paiType = 2;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;

                            listneedpai.Add(tingPaiModel);
                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_2.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        #endregion
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianshunzi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
        }

        public static List<int> BianLiyouxianshunzi2(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }

                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }

                    if (youxianduizi)
                    {
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == pai) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;

                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (pai < 30)
                    {
                        //第一个数 先判断加两个癞子是否满足
                        if (laizicount - needlaizi > 0 && listallpai[0] == pai)
                        {
                            #region 第一个数 先判断加两个癞子是否满足
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            tingPaiModel.paiType = 3;

                            listneedpai.Add(tingPaiModel);
                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_2.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else
                        {
                            if (listpai.Contains(pai + 1) && !listpai.Contains(pai + 2) && (pai / 10) == ((pai + 1) / 10))
                            {
                                #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                                //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
//                                 if (!youxianduizi)
//                                 {
//                                     var d = 0;
//                                 }
                                listpai.Remove(pai);
                                listpai.Remove(pai + 1);
                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                if ((pai + 2) % 10 != 0)
                                {
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = pai + 2;
                                    listneedpai.Add(tingPaiModel);
                                }

                                if (1 < pai % 10 && pai % 10 < 9)
                                {
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = pai - 1;

                                    if (pai < 30 && pai % 10 == 8)
                                    {
                                        //8 9缺 7
                                        tingPaiModel.paiType = 1;
                                    }
                                    listneedpai.Add(tingPaiModel);
                                }
                                return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                            {
                                #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
//                                 if (!youxianduizi)
//                                 {
//                                     var d = 0;
//                                 }
                                //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                                listpai.Remove(pai);
                                listpai.Remove(pai + 2);
                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 1;
                                tingPaiModel.paiType = 2;

                                listneedpai.Add(tingPaiModel);

                                return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (listpai.Count(d => d == pai) == 2)
                            {
                                #region 如果是对子，则需要一个
                                //如果是对子，则需要一个
                                listpai.Remove(pai);
                                listpai.Remove(pai);

                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai;

                                listneedpai.Add(tingPaiModel);
                                return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                            {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                                #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                                listpai.Remove(pai);
                                needlaizi += 2;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    //needlaizi -= 2;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai;
                                //tingPaiModel.paiType = 3;

                                listneedpai.Add(tingPaiModel);
                                if (pai % 10 == 1)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);
                                }
                                else if (pai % 10 == 2)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    listneedpai.Add(tingPaiModel2);
                                }
                                else if (pai % 10 > 2 && pai % 10 < 8)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    listneedpai.Add(tingPaiModel2);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai - 2;
                                    listneedpai.Add(tingPaiModel_2);
                                }
                                else if (pai % 10 == 8)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai - 2;
                                    listneedpai.Add(tingPaiModel_2);
                                }
                                else if (pai % 10 == 9)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai - 2;
                                    tingPaiModel_2.paiType = 1;
                                    listneedpai.Add(tingPaiModel_2);
                                }
                                return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                        }
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianshunzi2(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
        }

        public static List<int> BianLiyouxianduizi1Desc(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            listpai = listpai.OrderBy(d => d).ToList();
                            //RemoveFeng_B
                            //PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            PlayRules.IsFengZuiZi_LZ_B(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        listpai.Remove(pai - 2);
                        return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }

                    if (youxianduizi)
                    {
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == listpai[i]) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (pai < 30)
                    {
                        if (listpai.Contains(pai - 1))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai - 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (pai / 10 == (pai - 2) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 2;
                                if (pai < 30 && pai % 10 == 9) //手中有 32 缺1
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }

                            if (pai / 10 == (pai + 1) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 1;
                                if (pai < 30 && pai % 10 == 2)
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }

                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai - 1) && listpai.Contains(pai - 2) && (pai / 10) == ((pai - 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai - 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai - 1;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);
                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_1.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }

                            return BianLiyouxianduizi1Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
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

        public static List<int> BianLiyouxianduizi2Desc(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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

                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            listpai = listpai.OrderBy(d => d).ToList();
                            PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        listpai.Remove(pai - 2);
                        return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }

                    if (youxianduizi)
                    {
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == listpai[i]) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (pai < 30)
                    {
                        //第一个数 先判断加两个癞子是否满足
                        if (laizicount - needlaizi > 0 && pai == listallpai[0])
                        {
                            #region 第一个数 先判断加两个癞子是否满足
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);

                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_1.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }

                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else
                        {
                            //与当前牌组合需要一个癞子的牌
                            if (listpai.Contains(pai - 1))
                            {
                                #region 与当前牌组合需要一个癞子的牌
                                //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                                listpai.Remove(pai);
                                listpai.Remove(pai - 1);
                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                if (pai / 10 == (pai - 2) / 10)
                                {
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = pai - 2;
                                    if (pai < 30 && pai % 10 == 9) //手中有 89 缺7
                                    {
                                        tingPaiModel.paiType = 1;
                                    }
                                    listneedpai.Add(tingPaiModel);
                                }
                                if (pai / 10 == (pai + 1) / 10)
                                {
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = pai + 1;
                                    if (pai < 30 && pai % 10 == 2)
                                    {
                                        tingPaiModel.paiType = 1;
                                    }
                                    listneedpai.Add(tingPaiModel);
                                }
                                return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (!listpai.Contains(pai - 1) && listpai.Contains(pai - 2) && (pai / 10) == ((pai - 2) / 10))
                            {
                                #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
//                                 if (!youxianduizi)
//                                 {
//                                     var d = 0;
//                                 }
                                //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                                listpai.Remove(pai);
                                listpai.Remove(pai - 2);
                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 1;
                                listneedpai.Add(tingPaiModel);
                                return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (listpai.Count(d => d == pai) == 2)
                            {
                                #region 如果是对子，则需要一个
                                //如果是对子，则需要一个
                                listpai.Remove(pai);
                                listpai.Remove(pai);

                                needlaizi += 1;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    // needlaizi -= 1;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai;
                                listneedpai.Add(tingPaiModel);
                                return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                            else if (!listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                            {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                                #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                                listpai.Remove(pai);
                                needlaizi += 2;
                                if (needlaizi > laizicount + 1)
                                {
                                    listneedpai = new List<TingPaiModel>();
                                    //needlaizi -= 2;
                                    return new List<int>();
                                }
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai;
                                //tingPaiModel.paiType = 3;
                                listneedpai.Add(tingPaiModel);

                                if (pai % 10 == 9)
                                {
                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai - 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai - 2;
                                    tingPaiModel2.paiType = 1;
                                    listneedpai.Add(tingPaiModel2);
                                }
                                else if (pai % 10 == 2)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    listneedpai.Add(tingPaiModel2);
                                }
                                else if (pai % 10 > 2 && pai % 10 < 8)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);

                                    TingPaiModel tingPaiModel2 = new TingPaiModel();
                                    tingPaiModel2.pai = pai + 2;
                                    listneedpai.Add(tingPaiModel2);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai - 2;
                                    listneedpai.Add(tingPaiModel_2);
                                }
                                else if (pai % 10 == 8)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai - 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai - 2;
                                    listneedpai.Add(tingPaiModel_2);

                                    TingPaiModel tingPaiModel1 = new TingPaiModel();
                                    tingPaiModel1.pai = pai + 1;
                                    //tingPaiModel1.paiType = 2;
                                    listneedpai.Add(tingPaiModel1);
                                }
                                else if (pai % 10 == 1)
                                {
                                    TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                    tingPaiModel_1.pai = pai + 1;
                                    tingPaiModel_1.paiType = 2;
                                    listneedpai.Add(tingPaiModel_1);

                                    TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                    tingPaiModel_2.pai = pai + 2;
                                    tingPaiModel_1.paiType = 1;
                                    listneedpai.Add(tingPaiModel_2);
                                }

                                return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                                #endregion
                            }
                        }
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi2Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
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

        public static List<int> BianLiyouxianduizi0Desc(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == pai) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            listpai = listpai.OrderBy(d => d).ToList();
                            PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (listpai.Contains(pai - 1) && listpai.Contains(pai - 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai - 1);
                        listpai.Remove(pai - 2);
                        return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }
                    if (pai < 30)
                    {
                        //与当前牌组合需要一个癞子的牌
                        if (listpai.Contains(pai - 1))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai - 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (pai / 10 == (pai - 2) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 2;
                                if (pai < 30 && pai % 10 == 9) //手中有 89 缺7
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            if (pai / 10 == (pai + 1) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 1;
                                if (pai < 30 && pai % 10 == 2)
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai - 1) && listpai.Contains(pai - 2) && (pai / 10) == ((pai - 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai - 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai - 1;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai - 1) && !listpai.Contains(pai - 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);

                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_1.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }

                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi0Desc(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
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
        public static List<int> BianLiyouxianduizi1(List<int> listallpai, List<int> listpai, ref int needlaizi, ref List<TingPaiModel> listneedpai, int laizicount, bool youxianduizi, bool isdelsantiao, GameRule guize, ref HuModel huModel)
        {
            if (listpai.Count > 0)
            {
                if (needlaizi > laizicount + 1)
                {
                    listneedpai = new List<TingPaiModel>();
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
                        #region 如果是对子，则需要一个
                        if (listpai.Count(d => d == listpai[i]) == 2)
                        {
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;

                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }
                    if (guize.isFengZuiZi)
                    {
                        #region 风嘴子
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();
                        if (list30.Count >= 3)
                        {
                            //                             huModel.fengLianCount += 1;
                            // 
                            //                             var listTake3 = list30.Take(3).ToList();
                            //                             for (int m = 0; m < listTake3.Count; m++)
                            //                             {
                            //                                 listpai.Remove(listTake3[m]);
                            //                             }
                            int fengCount = 0;
                            PlayRules.IsFengZuiZi_LZ_N(ref listpai, ref fengCount);
                            //PlayRules.IsFengZuiZi_LZ(ref listpai, ref fengCount);
                            huModel.fengLianCount += fengCount;
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }

                        // 41 43 45 算一顺
                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();
                        if (list40.Count >= 3)
                        {
                            huModel.fengLianCount += 1;
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                        }
                        #endregion
                    }

                    if (listpai.Contains(pai + 1) && listpai.Contains(pai + 2))
                    {//如果手牌包含 +1 +2 的，则不需要癞子，移除后，继续轮训

                        listpai.Remove(pai);
                        listpai.Remove(pai + 1);
                        listpai.Remove(pai + 2);
                        return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                    }
                    if (pai < 30)
                    {
                        if (listpai.Contains(pai + 1))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 1);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            if (pai / 10 == (pai + 2) / 10)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai + 2;
                                if (pai < 30 && pai % 10 == 1) //手中有 12 缺3
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            if (pai % 10 >= 2)
                            {
                                TingPaiModel tingPaiModel = new TingPaiModel();
                                tingPaiModel.pai = pai - 1;
                                if (pai < 30 && pai % 10 == 8)
                                {
                                    tingPaiModel.paiType = 1;
                                }
                                listneedpai.Add(tingPaiModel);
                            }
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && listpai.Contains(pai + 2) && (pai / 10) == ((pai + 2) / 10))
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            //如果可与当前值 组成顺子的牌有一个，则需要癞子一个
                            listpai.Remove(pai);
                            listpai.Remove(pai + 2);
                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai + 1;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (listpai.Count(d => d == pai) == 2)
                        {
                            #region 如果是对子，则需要一个
                            //如果是对子，则需要一个
                            listpai.Remove(pai);
                            listpai.Remove(pai);

                            needlaizi += 1;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                // needlaizi -= 1;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            listneedpai.Add(tingPaiModel);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (!listpai.Contains(pai + 1) && !listpai.Contains(pai + 2))
                        {//如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            #region 如果可与当前值 组成顺子的牌都没有，则需要癞子两个
                            listpai.Remove(pai);
                            needlaizi += 2;
                            if (needlaizi > laizicount + 1)
                            {
                                listneedpai = new List<TingPaiModel>();
                                //needlaizi -= 2;
                                return new List<int>();
                            }
                            TingPaiModel tingPaiModel = new TingPaiModel();
                            tingPaiModel.pai = pai;
                            //tingPaiModel.paiType = 3;
                            listneedpai.Add(tingPaiModel);

                            if (pai % 10 == 1)
                            {
                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                tingPaiModel2.paiType = 1;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 == 2)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);
                            }
                            else if (pai % 10 > 2 && pai % 10 < 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);

                                TingPaiModel tingPaiModel2 = new TingPaiModel();
                                tingPaiModel2.pai = pai + 2;
                                listneedpai.Add(tingPaiModel2);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);
                            }
                            else if (pai % 10 == 8)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                listneedpai.Add(tingPaiModel_2);

                                TingPaiModel tingPaiModel1 = new TingPaiModel();
                                tingPaiModel1.pai = pai + 1;
                                //tingPaiModel1.paiType = 2;
                                listneedpai.Add(tingPaiModel1);
                            }
                            else if (pai % 10 == 9)
                            {
                                TingPaiModel tingPaiModel_1 = new TingPaiModel();
                                tingPaiModel_1.pai = pai - 1;
                                tingPaiModel_1.paiType = 2;
                                listneedpai.Add(tingPaiModel_1);

                                TingPaiModel tingPaiModel_2 = new TingPaiModel();
                                tingPaiModel_2.pai = pai - 2;
                                tingPaiModel_1.paiType = 1;
                                listneedpai.Add(tingPaiModel_2);
                            }

                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                    if (guize.isFengZuiZi)
                    {
                        var list30 = listpai.Where(d => d > 30 && d < 40).Distinct().ToList();//东南西北
                        if (list30.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll30.Count; m++)
                            {
                                if (!list30.Contains(listAll30[m]))
                                {
                                    listNo30.Add(listAll30[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll30[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }
                            huModel.fengLianCount += 1;//可以补成顺子
                            needlaizi += 1;
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list30.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有一个，则需要癞子2个
                            int card = list30.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list30.Count; m++)
                            {
                                listpai.Remove(list30[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list30[m]);
                                }
                            }

                            var listAll30 = new List<int> { 31, 33, 35, 37 };

                            var listNo30 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll30.Count; m++)
                                {
                                    if (!list30.Contains(listAll30[m]))
                                    {
                                        listNo30.Add(listAll30[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll30[m];
                                        tingPaiModel.fengLianCount = 1;
                                        tingPaiModel.paiType = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }

                        var list40 = listpai.Where(d => d > 40).Distinct().ToList();//中发白
                        if (list40.Count == 2)
                        {
                            #region 如果可与当前值 组成顺子的牌有2个，则需要癞子一个
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            for (int m = 0; m < listAll40.Count; m++)
                            {

                                if (!list40.Contains(listAll40[m]))
                                {
                                    listNo40.Add(listAll40[m]);
                                    TingPaiModel tingPaiModel = new TingPaiModel();
                                    tingPaiModel.pai = listAll40[m];
                                    tingPaiModel.paiType = 1;
                                    listneedpai.Add(tingPaiModel);
                                }
                            }

                            needlaizi += 1;
                            huModel.fengLianCount += 1;//可以补成顺子
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                        else if (list40.Count == 1)
                        {
                            #region 如果可与当前值 组成顺子的牌有1个，则需要癞子2个
                            int card = list40.FirstOrDefault();
                            int size = listpai.Count(d => d == card);
                            for (int m = 0; m < list40.Count; m++)
                            {
                                listpai.Remove(list40[m]);
                                if (size == 2)
                                {
                                    listpai.Remove(list40[m]);
                                }
                            }

                            var listAll40 = new List<int> { 41, 43, 45 };

                            var listNo40 = new List<int>();//需要的风牌
                            if (size != 2)
                            {
                                for (int m = 0; m < listAll40.Count; m++)
                                {
                                    if (!list40.Contains(listAll40[m]))
                                    {
                                        listNo40.Add(listAll40[m]);
                                        TingPaiModel tingPaiModel = new TingPaiModel();
                                        tingPaiModel.pai = listAll40[m];
                                        tingPaiModel.paiType = 1;
                                        tingPaiModel.fengLianCount = 1;
                                        listneedpai.Add(tingPaiModel);
                                    }
                                }
                            }
                            TingPaiModel tingPaiModelNow = new TingPaiModel();
                            tingPaiModelNow.pai = pai;
                            tingPaiModelNow.paiType = 1;
                            tingPaiModelNow.fengLianCount = -10;
                            listneedpai.Add(tingPaiModelNow);
                            if (size == 2)
                            {
                                needlaizi += 1;
                            }
                            else
                            {
                                needlaizi += 2;
                            }
                            //huModel.fengLianCount += 1;//一张缺两张也是可以补成顺子
                            return BianLiyouxianduizi1(listallpai, listpai, ref needlaizi, ref listneedpai, laizicount, youxianduizi, isdelsantiao, guize, ref huModel);
                            #endregion
                        }
                    }
                }
            }
            return new List<int>();
        }
    }
}
