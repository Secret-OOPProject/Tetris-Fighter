using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;

namespace GameLogic
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int x, int y, Color color)
        {
            X = x;
            Y = y;
            this.Color = color;
        }
    }
}
