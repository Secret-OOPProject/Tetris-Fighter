﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Player
    {
        private double maxHP;
        private int damage;

        public double MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Player(double hp, int dam = 0)
        {
            MaxHP = hp;
            Damage = dam;
        }

        public void setDamage(int score)
        {
            damage = score;
        }
    }
}
