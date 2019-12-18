using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core
{
    internal static class Render
    {
        public static void WithOffset(IRenderable obj, int offX, int offY)
        {
            Console.CursorVisible = false;
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            Console.SetCursorPosition(obj.Position.X + offX, obj.Position.Y + offY);
            Console.ForegroundColor = obj.Color;
            Console.BackgroundColor = obj.BackColor;
            Console.Write(obj.Symbol);
            Console.ResetColor();
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = true;
        }
    }
}
