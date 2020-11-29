using System;
using System.Collections.Generic;
using System.Text;

namespace game
{
    public class Player
    {
        public string name;
        public int id;
        public int gold, rock, wood, crystall;
        public Unit selectUnit;
        public Town town;
        public Player(string name)
        {
            this.name = name;
            this.gold = 100;
            this.wood = 0;
            this.rock = 0;
            this.crystall = 0;
        }
    }
}
