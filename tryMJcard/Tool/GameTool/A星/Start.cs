using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool.A星
{
    class Start
    {
        int[,] map;

        int length = 15;

        int width = 15;

        public void StartMap()
        {
            map = new int[15, 15] {
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,0,1,0,0,1,1,1,1,0,1,1,1,1,1},
                { 1,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
                { 1,0,0,0,0,1,0,1,1,1,1,0,0,0,1},
                { 1,0,0,0,0,1,0,1,1,0,0,0,0,0,1},
                { 1,0,0,0,0,0,0,0,1,0,0,1,1,0,1},
                { 1,1,1,1,1,1,1,0,1,0,1,1,0,0,1},
                { 1,0,0,0,0,0,0,0,0,0,1,0,1,0,1},
                { 1,0,1,1,1,0,1,1,1,1,1,0,0,0,1},
                { 1,0,0,0,1,1,1,0,0,0,0,0,0,0,1},
                { 1,0,1,0,0,0,1,0,0,0,0,0,0,0,1},
                { 1,0,1,1,1,0,0,0,1,0,0,0,0,0,1},
                { 1,0,1,0,0,0,0,0,0,0,0,0,0,0,1},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };
            Drawing(map);
            List<Point2> walls = new List<Point2>();
            for (int i = 0; i < length; i++)
            {
                for (int a = 0; a < width; a++)
                {
                    if (map[i,a] == 1)
                    {
                        walls.Add(new Point2(i, a));
                    }
                }
            }

            PathGrid pathGrid = new PathGrid(length, width, walls);
            var result = pathGrid.FindPath(new Point2(1, 1), new Point2(13, 13));
            var resultMap = map;
            for (int i = 0; i < result.Count; i++)
            {
                resultMap[result[i].x,result[i].y] = 2;
            }
            Drawing(resultMap);
            var currentPath = pathGrid.GetCurrentPath();
            Console.ReadLine();
            Console.Clear();
            var closeList = pathGrid.GetCloseList().ToList();
            var closeMap = map;
            for (int i = 0; i < closeList.Count; i++)
            {
                closeMap[closeList[i].x, closeList[i].y] = 3;
                Drawing(closeMap);
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine("end!");
            //             var openList = pathGrid.GetOpenList();
            //             var openMap = map;
            //             for (int i = 0; i < closeList.Count; i++)
            //             {
            //                 openMap[openList[i].y, openList[i].x] = 4;
            //             }
            //             Drawing(openMap);
        }

        public void Drawing(int[,] needMap)
        {
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                for (int a = 0; a < width; a++)
                {
                    if (needMap[i, a] == 1)
                    {
                        text += "□";
                    }
                    else if (needMap[i, a] == 2) text += "※";
                    else if (needMap[i, a] == 3) text += "△";
                    else if (needMap[i, a] == 4) text += "☆";
                    else text += "  ";
                }
                text += "\r\n";
            }
            Console.WriteLine(text);
        }
    }
}
