using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core
{
    internal interface IRenderable
    {
        Vector2D Position { get; set; }
        Char Symbol { get; set; }
        
        ConsoleColor Color { get; set; }
        ConsoleColor BackColor { get; set; }
    }
}
