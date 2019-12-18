using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core
{
    internal struct Vector2D
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
            => new Vector2D(a.X + b.X, a.Y + b.Y);

        public static Vector2D operator -(Vector2D a, Vector2D b)
         => new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2D operator +(Vector2D a, Int32 b)
          => new Vector2D(a.X + b, a.Y + b);

        public static Vector2D operator -(Vector2D a, Int32 b)
          => new Vector2D(a.X - b, a.Y - b);

        public static Vector2D operator +(Int32 a, Vector2D b)
             => new Vector2D(a+ b.X, a+ b.Y);

        public static Vector2D operator -(Int32 a, Vector2D b)
          => new Vector2D(a - b.X, a - b.Y);

        public static bool operator ==(Vector2D a, Vector2D b)
            => (a.X == b.X) && (a.Y == b.Y);

        public static bool operator !=(Vector2D a, Vector2D b)
            => (a.X != b.X) || (a.Y != b.Y);

    }
}
