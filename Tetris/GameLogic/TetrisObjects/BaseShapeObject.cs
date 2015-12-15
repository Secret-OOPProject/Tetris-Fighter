using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameLogic
{
    /// <summary>
    /// Rodicovsky objekt tetrisu
    /// </summary>
    public class BaseShapeObject
    {
        private List<Point> points = new List<Point>();
        private Color[] color = { Colors.Red, Colors.Blue, Colors.Yellow, Colors.Green };
        private Random ran = new Random();

        public bool Moving { get; set; }
        public List<Point> Points { get { return points; } }

        public BaseShapeObject()
        {
            Moving = true;
        }

        public void SetRandomColor()
        {
            Color c = color[ran.Next(0, color.Length)];
            points.ForEach(p => p.Color = c);
        }

        public void SetColor(Color color)
        {
            points.ForEach(p => p.Color = color);
        }

        //reseni kolizi, pokud kolize nastane, objekt se uz nehybe (moving=false)

        public bool MoveToRight(List<Point> listOfOtherObjects)
        {
            Moving = true;
            if (!points.Exists(p => (p.X + 1) == GameManager.Columns))
            {
                if (!listOfOtherObjects.Exists(lo => points.Exists(p => (p.X + 1) == lo.X && p.Y == lo.Y)))
                {
                    points.ForEach(p => p.X++);
                }
            }

            return Moving;
        }

        public bool MoveToLeft(List<Point> listOfOtherObjects)
        {
            Moving = true;
            if (!points.Exists(p => (p.X - 1) < 0))
            {
                if (!listOfOtherObjects.Exists(lo => points.Exists(p => (p.X - 1) == lo.X && p.Y == lo.Y)))
                {
                    points.ForEach(p => { p.X--; });
                }
            }

            return Moving;
        }

        public bool MoveDown(List<Point> listOfOtherObjects)
        {
            if (!points.Exists(p => (p.Y + 1) == GameManager.Rows))
            {
                if (listOfOtherObjects.Exists(lo => points.Exists(p => (p.Y + 1) == lo.Y && p.X == lo.X))) Moving = false;
                else
                {
                    points.ForEach(p => { p.Y++; });
                    Moving = true;
                }
            }
            else Moving = false;


            return Moving;
        }


        /// <summary>
        /// kolize s existujicim bodem
        /// </summary>
        /// <param name="listOfOtherObjects"></param>
        /// <returns>true pokud nastane kolize</returns>
        public bool Collision(List<Point> listOfOtherObjects)
        {
            return (listOfOtherObjects.Exists(lo => points.Exists(p => p.Y == lo.Y && p.X == lo.X)));
        }



        /// <summary>
        /// rotace objektu
        /// </summary>
        /// <param name="listOfOtherObjects"></param>
        /// <returns></returns>
        public bool Rotate(List<Point> listOfOtherObjects)
        {
            List<Point> testPoints = new List<Point>(points.Count());

            int xMax = (points.Max(p => p.X));
            int yMax = (points.Max(p => p.Y));
            int xMin = (points.Min(p => p.X));
            int yMin = (points.Min(p => p.Y));
            int xlenth = xMax - xMin + 1;
            int ylenth = yMax - yMin + 1;
       //     int xMaxMove = xlenth / 2;
       //     int yMaxMove = ylenth / 2;

            if (xlenth > ylenth) xMin++;
            if (ylenth > xlenth) xMin--;

            foreach (Point point in points)
            {
                testPoints.Add(new Point(Math.Abs(point.Y - yMax) + xMin, (point.X - xMin) + yMin, point.Color));
            }

            if (!(listOfOtherObjects.Exists(lo => testPoints.Exists(p => (p.Y == lo.Y && p.X == lo.X) || p.X < 0 || p.Y < 0 || p.X >= GameManager.Columns || p.Y >= GameManager.Rows || p.Y < 0 || p.X < 0))))
            {
                points.Clear();
                points.AddRange(testPoints);
            }
            return true;
        }
    }
}
