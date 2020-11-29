using System;
using System.Collections.Generic;
using System.Text;

namespace game
{
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
        public int attack;
        public int defense;
        public int damage;
    }

    // Units //
    abstract public class Unit : GameObj
    {
        public enum typeUnit
        {
            Scout,
            Warior,
            Shooter,
            Top
        }
        public int actionPoints;
        public int MAXactionPoints;
        abstract public void atack(GameObj unit);
    }

    public class Scout : Unit
    {
        public Scout()
        {
            this.attack = 1;
            this.defense = 1;
            this.damage = 1;
            this.health = 1;
            this.MAXactionPoints = 1;
            this.actionPoints = this.MAXactionPoints;
        }
        override public void atack(GameObj unit)
        {
            Coord range = (this.Position - unit.Position).ABS;
            if (range.X > 1 || range.Y > 1) throw new Exception("long range");

            //unit.health -= (this.damage * this.attack) / unit.defense;
            unit.health -= this.damage; // simple
        }
    }

    public class Warior : Unit
    {
        public int level;
        public Warior(int level)
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
            Coord range = (this.Position - unit.Position).ABS;
            if (range.X > 1 || range.Y > 1) throw new Exception("long range");

            //unit.health -= (this.damage * this.attack) / unit.defense;
            unit.health -= this.damage; // simple
        }
    }

    public class Shooter : Unit
    {
        public int level;
        public int rangeAttack;
        public int shootingDamage;
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
            Coord range = (this.Position - unit.Position).ABS;
            if (range.X > 1 || range.Y > 1)
            {
                if (range.X > rangeAttack || range.Y > rangeAttack)
                    throw new Exception("Long range");
                else
                    unit.health -= this.damage; // simple
                    //unit.health -= (this.shootingDamage * this.attack) / unit.defense;
            }
            else
                unit.health -= this.damage; // simple
                //unit.health -= (this.damage * this.attack) / unit.defense;
        }
    }

    public class Top : Unit
    {
        public int rangeAttack;
        public int shootingDamage;
        public Top()
        {
            this.attack       = 1;
            this.defense      = 1;
            this.damage       = 1;
            this.health       = 1;
            this.actionPoints = 1;
        }
        override public void atack(GameObj unit)
        {
            Coord range = (this.Position - unit.Position).ABS;
            if (range.X > 1 || range.Y > 1)
            {
                if (range.X > rangeAttack || range.Y > rangeAttack)
                    throw new Exception("Long range");
                else
                    unit.health -= this.damage; // simple
                    //unit.health -= (this.shootingDamage * this.attack) / unit.defense;
            }
            else
                unit.health -= this.damage; // simple
                //unit.health -= (this.damage * this.attack) / unit.defense;
        }
    }

    // ... //
}
