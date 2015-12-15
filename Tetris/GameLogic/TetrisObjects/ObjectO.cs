﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameLogic.TetrisObjects
{

    /// <summary>
    /// ctverecek
    /// </summary>
    public class ObjectO : BaseShapeObject, IObject
    {
        public void Create(int startX, int startY)
        {
            startX--;
            base.Points.Add(new Point(startX, startY));
            base.Points.Add(new Point(startX + 1, startY));
            base.Points.Add(new Point(startX, startY + 1));
            base.Points.Add(new Point(startX + 1, startY + 1));
            base.SetColor(Colors.Yellow);
        }
    }
}
