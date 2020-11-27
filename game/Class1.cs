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
        public int type;
        public Coord Position;
        public int Player;
    }

    public class Unit: GameObj
    {
        public Player player;
        public int attack;
        public int defense;
        public int damage;
        public int health;
        public int actionPoints;
    }

    public class Cave: GameObj
    {

    }

    public class Town: GameObj
    {

    }

    public class Market: GameObj
    {

    }
    public class _Map
    {
        public GameObj[,] Map { get; set; }
        _Map(int X = 71, int Y = 51)
        {
            Map = new GameObj[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    Map[i, j].Position = new Coord { X = i, Y = j};
        }
    }

    public class Player
    {
        public Unit selectUnit;
    }

    interface IAction
    {
        static Player player;
        static _Map map;

        static public void SelectUnit(Unit unit) // click (Left)
        {
            player.selectUnit = unit;
        }
        static public void MoveUnit()  // click (Left)
        {

        }
        static public void AttackUnit() // click (Right)
        {

        }

        static public void SpawnUnit(int id) // interface buttons
        { 

        } 
        static public void UpgradeTown(int id) // interface buttons
        {

        }
        static public void Market() 
        { 

        }
        static public void CaptureCave() 
        { 

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


        Game(_Map map)
        {
            this.map = map;
            IAction.map = map;
            players = new Player[2];
        }

        public void Action(Buttons button, int param, Coord pos)
        {
            switch (button)
            {
                case Buttons.SpawnUnit:
                    IAction.SpawnUnit(param);
                    break;
                case Buttons.UpgradeTown:
                    IAction.UpgradeTown(param);
                    break;
                case Buttons.NextTurn:
                    nextTurn();
                    break;
                case Buttons.Left:
                case Buttons.Right:
                    switch (map.Map[pos.X, pos.Y].type)
                    {

                    }
                    break;
            }

        }

        public void nextTurn() {
            IAction.player = IAction.player == players[0] 
                ? players[1] 
                : players[0];
        }
    }
}
