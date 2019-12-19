using SeeBattle.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeeBattle.Core
{
    internal class Cursor 
    {
        public Vector2D Location { get; set; }

        public Cursor(Vector2D location) 
            : this(location.X, location.Y)
        {
        }
        public Cursor(Int32 x, Int32 y)
        {
            Location = new Vector2D(x, y);
        }

        public void MoveTo(Vector2D postion)
        {
            Location += postion;
            Draw();
        }
        public void Draw() => Console.SetCursorPosition(Location.X, Location.Y);
    }
}
