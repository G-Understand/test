using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    class MaJong
    {
        /// <summary>
        /// 计算胡牌方法（递归）
        /// </summary>
        /// <param name="mahs"></param>
        /// <param name="yaojiu"></param>
        /// <param name="quanjiang"></param>
        /// <returns></returns>
        private static bool HuPaiPanDin(List<int> mahs, ref bool yaojiu, ref bool quanjiang)
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
                if (yaojiu)
                {
                    if (mahs[0] % 10 != 1 && mahs[0] % 10 != 9)//如果已经是 幺九 才判断当前牌是否是 幺九
                    {
                        yaojiu = false;
                    }
                }
                if (quanjiang)
                {
                    if (mahs[0] % 10 != 2 && mahs[0] % 10 != 5 && mahs[0] % 10 != 8)
                    {
                        quanjiang = false;
                    }
                }
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                mahs.Remove(mahs[0]);
                return HuPaiPanDin(mahs, ref yaojiu, ref quanjiang);
            }
            else
            { //组成顺子
                quanjiang = false;
                if (mahs.Contains(mahs[0] + 1) && mahs.Contains(mahs[0] + 2))
                {
                    if (yaojiu)//如果已经是 幺九 才判断当前牌是否是 幺九
                    {
                        if (mahs[0] % 10 != 1 && (mahs[0] + 2) % 10 != 9)
                        {
                            yaojiu = false;
                        }
                    }
                    mahs.Remove(mahs[0] + 2);
                    mahs.Remove(mahs[0] + 1);
                    mahs.Remove(mahs[0]);

                    return HuPaiPanDin(mahs, ref yaojiu, ref quanjiang);
                }
                return false;
            }
        }
    }

}
