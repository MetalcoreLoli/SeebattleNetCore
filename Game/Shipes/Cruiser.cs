using SeeBattle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Shipes
{
    internal class Cruiser : ShipBase
    {
        public Cruiser() : base(3, '#')
        { }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
    }
}
