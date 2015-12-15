using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameLogic.TetrisObjects
{
    /// <summary>
    /// Výpis GAME OVER
    /// </summary>
    public class ObjectGO : BaseShapeObject, IObject
    {
        public void Create(int startX, int startY)
        {
            startX--;
            //G
            AddPointFrom00Coord(1, 0);
            AddPointFrom00Coord(2, 0);
            AddPointFrom00Coord(0, 1);
            AddPointFrom00Coord(0, 2);
            AddPointFrom00Coord(0, 3);
            AddPointFrom00Coord(1, 4);
            AddPointFrom00Coord(2, 4);
            AddPointFrom00Coord(3, 3);
            AddPointFrom00Coord(2, 2);
            AddPointFrom00Coord(3, 2);


            //A
            int shiftY = 5;
            AddPointFrom00Coord(1, 0 + shiftY);
            AddPointFrom00Coord(2, 0 + shiftY);
            AddPointFrom00Coord(0, 1 + shiftY);
            AddPointFrom00Coord(3, 1 + shiftY);
            AddPointFrom00Coord(0, 2 + shiftY);
            AddPointFrom00Coord(3, 2 + shiftY);
            AddPointFrom00Coord(0, 3 + shiftY);
            AddPointFrom00Coord(3, 3 + shiftY);
            AddPointFrom00Coord(1, 2 + shiftY);
            AddPointFrom00Coord(2, 2 + shiftY);
            shiftY += 5;
            //M
            AddPointFrom00Coord(0, 0 + shiftY);
            AddPointFrom00Coord(3, 0 + shiftY);
            AddPointFrom00Coord(0, 1 + shiftY);
            AddPointFrom00Coord(3, 1 + shiftY);
            AddPointFrom00Coord(0, 2 + shiftY);
            AddPointFrom00Coord(3, 2 + shiftY);
            AddPointFrom00Coord(0, 3 + shiftY);
            AddPointFrom00Coord(3, 3 + shiftY);
            AddPointFrom00Coord(1, 1 + shiftY);
            AddPointFrom00Coord(2, 1 + shiftY);
            shiftY += 5;

            //E
            AddPointFrom00Coord(0, 0 + shiftY);
            AddPointFrom00Coord(1, 0 + shiftY);
            AddPointFrom00Coord(2, 0 + shiftY);
            AddPointFrom00Coord(3, 0 + shiftY);

            AddPointFrom00Coord(0, 2 + shiftY);
            AddPointFrom00Coord(1, 2 + shiftY);
            AddPointFrom00Coord(2, 2 + shiftY);
            AddPointFrom00Coord(3, 2 + shiftY);

            AddPointFrom00Coord(0, 4 + shiftY);
            AddPointFrom00Coord(1, 4 + shiftY);
            AddPointFrom00Coord(2, 4 + shiftY);
            AddPointFrom00Coord(3, 4 + shiftY);

            AddPointFrom00Coord(0, 1 + shiftY);
            AddPointFrom00Coord(0, 3 + shiftY);

            shiftY = 0;
            int shiftX = 6;
            //O
            AddPointFrom00Coord(1 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 4 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 4 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 2 + shiftY);
            shiftY += 6;

            //V
            AddPointFrom00Coord(0 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 0 + shiftY);
            shiftY += 4;
            //E
            AddPointFrom00Coord(0 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 0 + shiftY);

            AddPointFrom00Coord(0 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 2 + shiftY);

            AddPointFrom00Coord(0 + shiftX, 4 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 4 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 4 + shiftY);
            AddPointFrom00Coord(3 + shiftX, 4 + shiftY);

            AddPointFrom00Coord(0 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 3 + shiftY);
            shiftY += 6;
            //R
            AddPointFrom00Coord(0 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 2 + shiftY);
            AddPointFrom00Coord(0 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 0 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 1 + shiftY);
            AddPointFrom00Coord(2 + shiftX, 3 + shiftY);
            AddPointFrom00Coord(1 + shiftX, 2 + shiftY);


            //           base.Points.Add(new Point(startX, startY));
            /*
            base.Points.Add(new Point(startX + 1, startY));
            base.Points.Add(new Point(startX + 2, startY));
            base.Points.Add(new Point(startX, startY + 1));
            base.Points.Add(new Point(startX, startY + 2));
            base.Points.Add(new Point(startX + 1, startY + 3));
            base.Points.Add(new Point(startX + 2, startY + 3));
             * */
            base.SetColor(Colors.Black);
        }

        private void AddPointFrom00Coord(int x, int y)
        {
            base.Points.Add(new Point(x, y));
        }
    }
}
