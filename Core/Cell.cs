using System;

namespace SeeBattle.Core
{
    internal struct Cell : IRenderable
    {
        public Vector2D Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }


        public char Symbol { get; set; }


        public Cell(char symbol, Vector2D position)
        {
            Symbol = symbol;
            Position = position;
            Color = ConsoleColor.White;
            BackColor = ConsoleColor.Black;
        }

        public Cell(char symbol, Vector2D position, ConsoleColor color, ConsoleColor backColor)
        {
            Symbol = symbol;
            Position = position;
            Color = color;
            BackColor = backColor;
        }
    }
}