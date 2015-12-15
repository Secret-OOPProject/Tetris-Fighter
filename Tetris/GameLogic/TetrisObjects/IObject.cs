using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.TetrisObjects
{
    public interface IObject
    {
        /// <summary>
        /// vytvori body objektu
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        void Create(int startX, int startY);

    }
}
