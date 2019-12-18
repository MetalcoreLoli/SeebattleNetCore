using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Shipes
{
    internal class Fleet
    {


        private Stack<Submarine> _submarines;
        private Stack<Cruiser> _crusers;
        private Stack<Carrier> _carrier;
        private BattleShip _battleShips;

        public Fleet()
        {
            _submarines = new Stack<Submarine>(4);
            _carrier = new Stack<Carrier>(3);
            _crusers = new Stack<Cruiser>(2);
        }

        #region Public Methods
        public void AddShipeToFleet(ShipBase ship)
        {
            if (ship is Submarine)
                _submarines.Push(ship as Submarine);

            if (ship is BattleShip)
                _battleShips = ship as BattleShip;

            if (ship is Carrier)
                _carrier.Push(ship as Carrier);

            if (ship is Cruiser)
                _crusers.Push(ship as Cruiser);
        }

        #endregion


    }
}
