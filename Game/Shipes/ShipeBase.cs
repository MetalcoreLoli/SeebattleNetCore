using SeeBattle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Shipes
{
    internal abstract class ShipBase
    {
        #region Protected Members

        protected Cell[] body;
        
        #endregion

        #region Private Members
        private bool _isVertical;
        private Int32 _countOfHits;

        private Vector2D _location;
        #endregion

        #region Public Properties

        public bool IsDestroied { get; set; } = false;
        
        public Char Symbol { get; set; }

        public Int32 Size { get; set; }
        
        public Vector2D Location 
        { 
            get => _location;
            set
            {
                _location = value;
                body = InitBodyWithSymbol(Symbol, Size);
            }
        }

        public Cell[] Body { get => body; }

        public Int32 CountOfHits 
        {
            get => _countOfHits;
            set
            {
                _countOfHits = value;
                IsDestroied = _countOfHits >= Size ? true : false;  
            }
        }


        public bool IsVertical 
        { 
            get => _isVertical;
            set
            {
                _isVertical = value;
                body = InitBodyWithSymbol(Symbol, Size);
            }
        } 
        #endregion

        #region Constructor

        public ShipBase(Int32 Size, char symbol)
        {
            Symbol = symbol;
            this.Size = Size;
            IsVertical = true;
            Location = new Vector2D(0, 0);
            //body = InitBodyWithSymbol(Symbol, Size);
        }

        #endregion

        #region Abstract Methods
        public abstract void Draw();

        #endregion
        
        #region Protected Methods
        protected Cell[] InitBodyWithSymbol(char symbol, Int32 size)
        {
            var temp = new Cell[size];
            for (int i = 0; i < size; i++)
            {
                if (IsVertical)
                    temp[i] = new Cell(symbol, new Vector2D(this.Location.X, this.Location.Y + i), ConsoleColor.White, ConsoleColor.DarkBlue);
                else
                    temp[i] = new Cell(symbol, new Vector2D(this.Location.X + i, this.Location.Y), ConsoleColor.White, ConsoleColor.DarkBlue);
            }
            return temp;
        }
        #endregion

        #region Public Methods
        public void AtMap(Map map)
        {
            Vector2D mLocation = new Vector2D(map.Location.X + 1, map.Location.Y + 2);
            for (int i = 0; i < Size; i++)
            {
                if (IsVertical)
                    body[i].Position = new Vector2D(this.Location.X + mLocation.X , mLocation.Y + Location.Y + i);
                else
                    body[i].Position = new Vector2D(this.Location.X + mLocation.X + i, mLocation.Y + Location.Y);
            }
        }

        internal bool IsShipInFov(ShipBase ship)
        {
            if (Location == ship.Location) return true;
            foreach (Cell cell in body)
            {
                if (cell.Position.X + 1 == ship.Location.X + 1 || cell.Position.Y + 1 == ship.Location.Y)
                    return true;
                if (cell.Position.X - 1 == ship.Location.X && cell.Position.Y - 1 == ship.Location.Y)
                    return true;
            }
            return false;
        }

        internal void HitCell(Vector2D positionToShipLocation)
        {
            for (int i = 0; i < Size; i++)
            {
                if (body[i].Position == positionToShipLocation)
                    body[i].Color = ConsoleColor.Red;
            }
            CountOfHits++;
            if (CountOfHits >= Size)
                IsDestroied = true;
        }
        #endregion
    }
}
