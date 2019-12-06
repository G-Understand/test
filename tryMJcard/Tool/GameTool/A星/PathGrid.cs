using System.Collections.Generic;
using System.Linq;
using System;


namespace tryMJcard.Tool.GameTool.A星
{
    public class PathGrid
    {
        private SortedDictionary<int, List<Point2>> openTree = new SortedDictionary<int, List<Point2>>();

        private HashSet<Point2> openSet = new HashSet<Point2>();
        private HashSet<Point2> closeSet = new HashSet<Point2>();
        private Dictionary<Point2, PathNode> allNodes = new Dictionary<Point2, PathNode>();

        private Point2 endPos;
        private Point2 gridSize;

        private List<Point2> currentPath;

        //这一部分在实际寻路中并不需要，只是为了方便外部程序实现寻路可视化
        public HashSet<Point2> GetCloseList()
        {
            return closeSet;
        }

        //这一部分在实际寻路中并不需要，只是为了方便外部程序实现寻路可视化
        public HashSet<Point2> GetOpenList()
        {
            return openSet;
        }

        //这一部分在实际寻路中并不需要，只是为了方便外部程序实现寻路可视化
        public List<Point2> GetCurrentPath()
        {
            return currentPath;
        }

        //新建一个PathGrid，包含了网格大小和障碍物信息
        public PathGrid(int x, int y, List<Point2> walls)
        {
            gridSize = new Point2(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Point2 newPos = new Point2(i, j);
                    allNodes.Add(newPos, new PathNode(walls.Contains(newPos), newPos));
                }
            }
        }

        //寻路主要逻辑，通过调用该方法来获取路径信息，由一串Point2代表
        public List<Point2> FindPath(Point2 beginPos, Point2 endPos)
        {
            List<Point2> result = new List<Point2>();

            this.endPos = endPos;
            Point2 currentPos = beginPos;
            openSet.Add(currentPos);

            while (!currentPos.Equals(this.endPos))
            {
                UpdatePath(currentPos);
                if (openSet.Count == 0) return null;

                currentPos = openTree.First().Value.First();
            }

            Point2 path = currentPos;

            while (!path.Equals(beginPos))
            {
                result.Add(path);
                path = allNodes[path].parent.position;
                currentPath = result;
            }

            result.Add(beginPos);
            return result;
        }

        //寻路
        private void UpdatePath(Point2 currentPos)
        {
            closeSet.Add(currentPos);
            RemoveOpen(currentPos, allNodes[currentPos]);
            List<Point2> neighborNodes = FindNeighbor(currentPos);
            foreach (Point2 nodePos in neighborNodes)
            {

                PathNode newNode = new PathNode(false, nodePos);
                newNode.parent = allNodes[currentPos];

                int g;
                int h;

                g = currentPos.x == nodePos.x || currentPos.y == nodePos.y ? 10 : 14;

                int xMoves = Math.Abs(nodePos.x - endPos.x);
                int yMoves = Math.Abs(nodePos.y - endPos.y);

                int min = Math.Min(xMoves, yMoves);
                int max = Math.Max(xMoves, yMoves);
                h = min * 14 + (max - min) * 10;


                newNode.gCost = g + newNode.parent.gCost;
                newNode.hCost = h;

                PathNode originNode = allNodes[nodePos];

                if (openSet.Contains(nodePos))
                {
                    if (newNode.fCost < originNode.fCost)
                    {
                        UpdateNode(newNode, originNode);
                    }
                }
                else
                {
                    allNodes[nodePos] = newNode;
                    AddOpen(nodePos, newNode);
                }
            }
        }

        //将旧节点更新为新节点
        private void UpdateNode(PathNode newNode, PathNode oldNode)
        {
            Point2 nodePos = newNode.position;
            int oldCost = oldNode.fCost;
            allNodes[nodePos] = newNode;
            List<Point2> sameCost;

            if (openTree.TryGetValue(oldCost, out sameCost))
            {
                sameCost.Remove(nodePos);
                if (sameCost.Count == 0) openTree.Remove(oldCost);
            }

            if (openTree.TryGetValue(newNode.fCost, out sameCost))
            {
                sameCost.Add(nodePos);
            }
            else
            {
                sameCost = new List<Point2> { nodePos };
                openTree.Add(newNode.fCost, sameCost);
            }
        }

        //将目标节点移出开启列表
        private void RemoveOpen(Point2 pos, PathNode node)
        {
            openSet.Remove(pos);
            List<Point2> sameCost;
            if (openTree.TryGetValue(node.fCost, out sameCost))
            {
                sameCost.Remove(pos);
                if (sameCost.Count == 0) openTree.Remove(node.fCost);
            }
        }

        //将目标节点加入开启列表
        private void AddOpen(Point2 pos, PathNode node)
        {
            openSet.Add(pos);
            List<Point2> sameCost;
            if (openTree.TryGetValue(node.fCost, out sameCost))
            {
                sameCost.Add(pos);
            }
            else
            {
                sameCost = new List<Point2> { pos };
                openTree.Add(node.fCost, sameCost);
            }
        }

        //找到某节点的所有相邻节点
        private List<Point2> FindNeighbor(Point2 nodePos)
        {
            List<Point2> result = new List<Point2>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;
                    if (Math.Abs(x) == Math.Abs(y)) continue;
                    Point2 currentPos = new Point2(nodePos.x + x, nodePos.y + y);

                    if (currentPos.x >= gridSize.x || currentPos.y >= gridSize.y || currentPos.x < 0 || currentPos.y < 0) continue; //out of bondary
                    if (closeSet.Contains(currentPos)) continue; // already in the close list
                    if (allNodes[currentPos].isWall) continue;  // the node is a wall

                    result.Add(currentPos);
                }
            }

            return result;
        }
    } 
}