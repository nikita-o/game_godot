using System;
using System.Collections.Generic;

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
    }

    public class GameObj
    {
        public enum typeObj
        {
            empty,
            block,
            unit,
            mine,
            town
        }
        public typeObj type;
        public Coord Position;
        public Player owner;
        public int health;
    }

    // Units //
    abstract public class Unit: GameObj
    {
        public enum typeUnit
        {
            Scout,
            Warior,
            Shooter,
            Top
        }
        public int attack;
        public int defense;
        public int damage;
        public int actionPoints;

        public int MAXactionPoints;
        abstract public void atack(GameObj unit);
    }

    public class Scout: Unit
    {
        public Scout()
        {
            this.attack = 1;
            this.defense = 1;
            this.damage = 1;
            this.health = 1;
            this.actionPoints = 1;
        }
        override public void atack(GameObj unit)
        {

        }
    }

    public class Warior : Unit
    {
        public int level;
        public int rangeAttack;
        public int shootingDamage;
        public Warior(int level)
        {
            this.level = level;
            switch (level)
            {
                case 1:
                    this.attack         = 1;
                    this.defense        = 1;
                    this.damage         = 1;
                    this.health         = 1;
                    this.actionPoints   = 1;
                    break;
                case 2:
                    this.attack         = 2;
                    this.defense        = 2;
                    this.damage         = 2;
                    this.health         = 2;
                    this.actionPoints   = 2;
                    break;
                default:
                    this.attack         = 3;
                    this.defense        = 3;
                    this.damage         = 3;
                    this.health         = 3;
                    this.actionPoints   = 3;
                    break;
            }
        }
        override public void atack(GameObj unit)
        {

        }
    }

    public class Shooter : Unit
    {
        public int level;
        public Shooter(int level)
        {
            this.level = level;
            switch (level)
            {
                case 1:
                    this.attack = 1;
                    this.defense = 1;
                    this.damage = 1;
                    this.health = 1;
                    this.actionPoints = 1;
                    break;
                case 2:
                    this.attack = 2;
                    this.defense = 2;
                    this.damage = 2;
                    this.health = 2;
                    this.actionPoints = 2;
                    break;
                default:
                    this.attack = 3;
                    this.defense = 3;
                    this.damage = 3;
                    this.health = 3;
                    this.actionPoints = 3;
                    break;
            }
        }
        override public void atack(GameObj unit)
        {

        }
    }

    public class Top : Unit
    {
        public Top()
        {
            this.attack = 1;
            this.defense = 1;
            this.damage = 1;
            this.health = 1;
            this.actionPoints = 1;
        }
        override public void atack(GameObj unit)
        {

        }
    }

    // ... //

    public class Mine: GameObj
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

    public class Town: GameObj
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

    public class _Map
    {
        public GameObj[,] Map;
        public Coord[,] SpawnPos; 
        public Town[] Towns;
        public Mine[] Mines;
        public _Map(int X = 71, int Y = 51) // expecting 70 X 50 size map
        {
            Map = new GameObj[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    Map[i, j].Position = new Coord { X = i, Y = j};
        }
        public void InitGame(Player[] players)
        {
            for (int i = 0; i < 2; i++)
            {
                players[i].town = Towns[i];
                players[i].gold = 0;
                players[i].rock = 0;
                players[i].wood = 0;
                players[i].crystall = 0;
                players[i].id = i;
            }            
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
        }
    }

    interface IAction
    {
        static Player player;
        static _Map map;
        static public void SelectUnit(Unit unit) // click (Left)
        {
            if (unit.owner != player) throw new Exception("This is enemy");
            player.selectUnit = unit;
        }
        static Coord[] MoveUnit()  // click (Left)
        {
            if (player.selectUnit == null) throw new Exception("Not select unit");
            // SANYA // 
            int costMoving = 0;
            if (player.selectUnit.actionPoints - costMoving < 0) throw new Exception("Not moving");
            return null; 
        }
        static public GameObj[] Attack(GameObj obj) // click (Right)
        {
            if (player.selectUnit == null) throw new Exception("Not select unit");
            player.selectUnit.atack(obj);
            return new GameObj[] { player.selectUnit, obj };
        }

        static public Coord SpawnUnit(Unit.typeUnit id, int level = 1) // interface buttons
        {
            Unit u = null;
            switch (id)
            {
                case Unit.typeUnit.Scout:
                    u = new Scout();
                    break;
                case Unit.typeUnit.Warior:
                    u = new Warior(level);
                    break;
                case Unit.typeUnit.Shooter:
                    u = new Shooter(level);
                    break;
                case Unit.typeUnit.Top:
                    u = new Top();
                    break;
            }
            u.owner = player;
            return map.SpawnUnit(u);
        } 
        static public void UpgradeTown() // interface buttons
        {
            player.town.upgrade();
        }
        static public void Market() // 
        { 

        }
        static public void CaptureMine(Mine mine) // click (Right)
        {
            int X = Math.Abs(player.selectUnit.Position.X - mine.Position.X);
            int Y = Math.Abs(player.selectUnit.Position.Y - mine.Position.Y);
            if (X > 1 || Y > 1) throw new Exception("out of range");
            mine.owner = player;            
        }
    }

    public class Game: IAction
    {
        public _Map map;
        public Player[] players;
        public enum Buttons
        {
            Left,
            Right,
            SpawnUnit,
            UpgradeTown,
            NextTurn
        }


        Game(_Map map, Player[] p) // expecting 2 players
        {
            this.map = map;
            players = p;
            map.InitGame(players);
            IAction.player = players[0];
        }

        public object Action(Buttons button, int param, Coord pos)
        {   
            switch (button)
            {
                case Buttons.SpawnUnit:
                    return IAction.SpawnUnit((Unit.typeUnit)param);
                case Buttons.UpgradeTown:
                    IAction.UpgradeTown();
                    break;
                case Buttons.NextTurn:
                    nextTurn();
                    break;
                case Buttons.Left:
                case Buttons.Right:
                    switch (map.Map[pos.X, pos.Y].type)
                    {
                        case GameObj.typeObj.empty when button == Buttons.Left:
                            return IAction.MoveUnit(); 
                        case GameObj.typeObj.unit when button == Buttons.Left:
                            IAction.SelectUnit((Unit)map.Map[pos.X, pos.Y]);
                            break;
                        case GameObj.typeObj.unit when button == Buttons.Right:
                        case GameObj.typeObj.town when button == Buttons.Right:
                            return IAction.Attack(map.Map[pos.X, pos.Y]); 
                        case GameObj.typeObj.mine when button == Buttons.Right:
                            IAction.CaptureMine((Mine)map.Map[pos.X, pos.Y]);
                            break;
                    }
                    break;
            }
            return null;
        }

        public void nextTurn() {
            IAction.player = IAction.player == players[0] 
                ? players[1] 
                : players[0];
        }
    }
}
