using SeeBattle.Core;
using SeeBattle.Core.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.LogConsoles
{
    internal class GameConsole : Control
    {

        #region Private Members
        private Cell[] _body;

        private List<String> _linesString;
        private List<Lable> _lines;

        private Int32 _currentLineNumber = 0;
        #endregion

        #region Public Properties

        public Int32 Widht { get; set; }
        public Int32 Height { get; set; }

        public List<String> Lines 
        { 
            get => _linesString;
            private set
            {
                _linesString = value;
            }
        }
        #endregion

        #region Constructorns

        public GameConsole()
        {
            Widht = 20;
            Height = 5;
            Location = new Vector2D(0, 0);
            _body = InitBody(Widht, Height);
            IsVisible = true;
            _linesString = new List<string>();
        }

        public GameConsole(Vector2D location)
        {
            Widht = 20;
            Height = 5;
            Location = location;
            Init(Widht, Height);
        }

        public GameConsole(Int32 width, Int32 height)
        {
            Widht = width;
            Height = height;
            Location = new Vector2D(0, 0);
            Init(Widht, Height);

        }

        public GameConsole(Int32 width, Int32 height, Vector2D location)
        {
            Widht = width;
            Height = height;
            Location = location;
            Init(Widht, Height);
        }
        #endregion

        #region Private Methods


        private void Init(Int32 width, Int32 height)
        {
           
            _body = InitBody(Widht, Height);
            _linesString = new List<string>();
            _lines = new List<Lable>();
            IsVisible = true;
        }
        private Cell[] InitWalls(Cell[] body, Int32 width, Int32 height)
        {
            Cell[] temp = body;
            
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Int32 cell_index = x + width * y;
                    if (temp[cell_index].Position.X.Equals(0) && temp[cell_index].Position.Y > 0)
                        temp[cell_index].Symbol = '|';
                    if (temp[cell_index].Position.X.Equals(width - 1) && temp[cell_index].Position.Y > 0)
                        temp[cell_index].Symbol = '|';
                }

            for (int i = 1; i < width - 1; i++)
                temp[i].Symbol = '-';

            for (int i = width * height - width + 1; i < width * height - 1; i++)
                temp[i].Symbol = '-';

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Int32 cell_index = x + width * y;
                    if (cell_index.Equals(0)
                        || cell_index.Equals(width - 1)
                        || cell_index.Equals(width * height - width)
                        || cell_index.Equals(width * height - 1))
                        temp[cell_index].Symbol = '+';
                }

            return temp;
        }
        private Cell[] InitBody(Int32 width, Int32 height)
        {
            Cell[] temp = new Cell[width * height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    temp[x + width * y] = new Cell(' ', new Vector2D(x, y));
            temp = InitWalls(temp, width, height);
            return temp;
        }

        #endregion

        #region Public Methods

        public string Input()
        {
            string intput = Console.ReadLine();
            body = InitBody(Widht, Height);
            Console.SetCursorPosition(this.Location.X + 1, this.Location.Y + 1);
            return intput;
        }
        public override void Draw()
        {
            Console.SetCursorPosition(this.Location.X + 1, this.Location.Y + 1);
            foreach (Cell cell in _body)
                Render.WithOffset(cell, this.Location.X, this.Location.Y);
            foreach (Lable lable in _lines)
                lable.Draw();
        }

        public void AddString(String text)
        {
            Lines.Add(text);
            if (_lines.Count >= Height - 2)
                Clean();
            _lines.Add(new Lable
            {
                Text = text,
                Location 
                    = new Vector2D(
                            Location.X + 1, 
                            Location.Y + (_currentLineNumber++) + 1)
            });
        }

        public void Clean()
        {
            _currentLineNumber = 0;
            _lines = new List<Lable>();
        }

        public void Clear()
        {
            for (int i = 1; i < Widht * Height - 2; i++)
                body[i].Symbol = ' ';
        }
        #endregion
    }
}
