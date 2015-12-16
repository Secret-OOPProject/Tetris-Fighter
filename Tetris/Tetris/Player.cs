using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Player
    {
        private int hp;
        private int damage;

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Player(int hp, int dam)
        {
            HP = hp;
            Damage = dam;
        }
    }
}
