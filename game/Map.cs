using System;
using System.Collections.Generic;
using System.Text;

namespace game
{
    public struct Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Coord ABS => new Coord(Math.Abs(this.X), Math.Abs(this.Y));
        public static Coord operator -(Coord c1, Coord c2)
        {
            return new Coord { X = c1.X - c2.X, Y = c1.Y - c2.Y };
        }
    }

    public class Mine : GameObj
    {
        public enum typeMine
        {
            gold,
            wood,
            rock,
            crystall
        }
        public typeMine TypeMine;
        public void mining()
        {
            switch (TypeMine)
            {
                case typeMine.gold:
                    this.owner.gold += 1;
                    break;
                case typeMine.wood:
                    this.owner.wood += 1;
                    break;
                case typeMine.rock:
                    this.owner.rock += 1;
                    break;
                case typeMine.crystall:
                    this.owner.crystall += 1;
                    break;
            }
        }
    }

    public class Town : GameObj
    {
        public int level;
        public void upgrade()
        {
            level++;
            if (level == 2) health += 2;
            else if (level == 3) health += 3;
            else throw new Exception("Max upgrade");
        }
    }

    public class _Map // need FIX
    {
        public GameObj[,] Map;
        public Coord[,] SpawnPos;
        public Town[] Towns;
        public Mine[] Mines;
        public _Map(int X = 71, int Y = 51) // expecting 70 X 50 size map // LOAD MAP?!
        {
            Map = new GameObj[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    Map[i, j].Position = new Coord { X = i, Y = j };
        }

        public Coord SpawnUnit(Unit u)
        {
            for (int i = 0; i < 3; i++)
            {
                Coord c = SpawnPos[u.owner.id, i];
                if (Map[c.X, c.Y].type == GameObj.typeObj.empty)
                {
                    Map[c.X, c.Y] = u;
                    return c;
                }
            }
            throw new Exception("no free places");
        }
    }
}
