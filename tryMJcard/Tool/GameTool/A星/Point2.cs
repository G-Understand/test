using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.GameTool.A星
{
    //A class used to store the position information
    public class Point2
    {
        public Point2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }

        public int y { get; set; }

        public override bool Equals(object obj)
        {
            return this.x == (obj as Point2).x && this.y == (obj as Point2).y;
        }

        public override int GetHashCode()
        {
            return x ^ (y * 256);
        }

        public override string ToString()
        {
            return x + "," + y;
        }

        public static bool operator ==(Point2 a, Point2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point2 a, Point2 b)
        {
            return !a.Equals(b);
        }
    }
}
