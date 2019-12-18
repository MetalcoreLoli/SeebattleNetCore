using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Shipes
{
    internal class Fleet
    {
        public Stack<Submarine> Submarines  { get; private set; }
        public Stack<Cruiser> Crusers       { get; private set; }
        public Stack<Carrier> Carrier       { get; private set; }
        public BattleShip BattleShips       { get; private set; }

        public Fleet()
        {
            Submarines  = new Stack<Submarine>(4);
            Carrier     = new Stack<Carrier>(3);
            Crusers     = new Stack<Cruiser>(2);
        }

        #region Public Methods
        public void AddShipeToFleet(ShipBase ship)
        {
            if (ship is Submarine)
                Submarines.Push(ship as Submarine);

            if (ship is BattleShip)
                BattleShips = ship as BattleShip;

            if (ship is Carrier)
                Carrier.Push(ship as Carrier);

            if (ship is Cruiser)
                Crusers.Push(ship as Cruiser);
        }

        #endregion


    }
}
