using System;
using System.Collections.Generic;

namespace game
{    
    interface IAction
    {
        static Player player;
        static _Map map;
        static public void SelectUnit(Unit unit) // click (Left)
        {
            if (unit.owner != player) throw new Exception("This is enemy");
            player.selectUnit = unit;
        }
        static Coord[] MoveUnit(Coord A)  // click (Left)
        {
            if (player.selectUnit == null) throw new Exception("Not select unit");
            // SANIN ALG // 
            int costMoving = 0;
            if (player.selectUnit.actionPoints - costMoving < 0) throw new Exception("Not moving");
            return null; 
        }
        static public GameObj[] Attack(GameObj obj) // click (Right)
        {
            if (player.selectUnit == null) throw new Exception("Not select unit");
            player.selectUnit.atack(obj);
            // CHECK DIE! and maybe delete object in map//
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
        static public void Market() // Params: type trading
        { 
            
        }
        static public void CaptureMine(Mine mine) // click (Right)
        {
            Coord c = (player.selectUnit.Position - mine.Position).ABS;
            if (c.X > 1 || c.Y > 1) throw new Exception("out of range");
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
            Market,
            NextTurn
        }


        Game(_Map map, Player[] p) // expecting 2 players
        {
            this.map = map;
            players = p;
            players[0].town = map.Towns[0];
            players[0].id = 0;
            players[1].town = map.Towns[1];
            players[1].id = 1;
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
                case Buttons.Market:
                    IAction.Market();
                    break;
                case Buttons.NextTurn:
                    nextTurn();
                    break;
                case Buttons.Left:
                case Buttons.Right:
                    switch (map.Map[pos.X, pos.Y].type)
                    {
                        case GameObj.typeObj.empty when button == Buttons.Left:
                            return IAction.MoveUnit(pos); 
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
