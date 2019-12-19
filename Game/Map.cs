using Seebattle.Core.Events.Properties;
using SeeBattle.Core;
using SeeBattle.Core.Controls;
using SeeBattle.Game.Shipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game
{
    /// <summary>
    /// Игровая карта
    /// </summary>
    internal class Map : Control
    {
        #region Public Properties

        /// <summary>
        /// False    - не прорисовывать символы на карте
        /// True     - прорисовывать символы на карте
        /// </summary>
        public bool IsCellsVisible { get; set; }


        /// <summary>
        /// Корабли, которые находятся на карте
        /// </summary>
        public List<Shipes.ShipBase> Shipes { get; set; }

        /// <summary>
        /// Расположение карты на консоле
        /// </summary>
        public new Vector2D Location {
            get => _location;
            set 
            {
                _location = value;
                OnPropertyChanged(this.GetType().GetProperty(nameof(Location)));
            }
        }

        #endregion

        #region Private Members
        /// <summary>
        /// Основное тело карты
        /// </summary>
        private Cell[] _map;

        /// <summary>
        /// Тело карты, которое будет видно противнику
        /// </summary>
        private Cell[] _mapBuffer;
        
        /// <summary>
        /// Тело столбца с цифрами 
        /// </summary>
        private Cell[] _leftLine;

        /// <summary>
        /// Тело столбца с буквами 
        /// </summary>
        private Cell[] _topLine;

        /// <summary>
        /// Заголовок
        /// </summary>
        private Lable _title;

        /// <summary>
        /// Расположение карты на Консоле
        /// </summary>
        private Vector2D _location;

        private string topLineText  = "ABCDEFJHIG";
        private string leftLineText = "1234567890";

        #endregion

        #region Events
     
        #endregion


        public Map(string title, Int32 width = 10, Int32 height = 10)
        {
            Widht           = width;
            Height          = height;
            Title           = title;
            Initializtion(Widht, Height);

            PropertyChanged += (sender, evArgs) => 
            {
                switch (evArgs.PropertyName) 
                {
                    case nameof(Location):
                        _map = InitMap(Widht, Height);
                        _mapBuffer = InitMap(Widht, Height);
                        _leftLine = InitLineWithText(leftLineText, new Vector2D(0, 0), false);
                        _topLine = InitLineWithText(topLineText, new Vector2D(0, 0), true);
                        break;
                }
            };
        }

        #region Private Methods
        /// <summary>
        /// Инициализация тела карты
        /// </summary>
        /// <param name="width">ширина тела</param>
        /// <param name="heigth">высота тела</param>
        /// <returns>тело карты</returns>
        private Cell[] InitMap(Int32 width, Int32 heigth)
        {
            Cell[] temp = new Cell[width * heigth];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < heigth; y++)
                    temp[x + width * y] = new Cell(
                        symbol: '~',
                        position: new Vector2D(x + Location.X, y + Location.Y),
                        color: ConsoleColor.Blue,
                        backColor: ConsoleColor.DarkBlue);
            return temp;
        }
        /// <summary>
        /// Инициализация тела линии
        /// </summary>
        /// <param name="startPostion">откуда отрисовывать</param>
        /// <param name="Lenght">длина линии</param>
        /// <param name="vectical">вертикально - true, горизонтально - flase</param>
        /// <returns>тело линии</returns>
        private Cell[] InitLine(Vector2D startPostion, Int32 Lenght, bool vectical = false)
        {
            Cell[] temp = new Cell[Lenght];
            for (int i = 0; i < Lenght; i++)
            {
                if (vectical)
                    temp[i] = new Cell('.', position: new Vector2D(startPostion.X + Location.X + i, startPostion.Y + Location.Y));
                else
                    temp[i] = new Cell('.', position: new Vector2D(startPostion.X + Location.X, startPostion.Y + i + Location.Y));
            }
            return temp;
        }
        /// <summary>
        /// Инициализация тела линии текстом
        /// </summary>
        /// <param name="text">текст для инициализации</param>
        /// <param name="startPostion">откуда отрисовывать</param>
        /// <param name="vectical">отрисовывать вертикально - true, или горизонтально - flase</param>
        /// <returns>тело линии</returns>
        private Cell[] InitLineWithText(string text, Vector2D startPostion, bool vectical = false)
        {
            Int32 lenght = text.Length;
            Cell[] temp = InitLine(startPostion, lenght, vectical);
            for (int i = 0; i < lenght; i++)
                temp[i].Symbol = text[i];
            return temp;
        }

        /// <summary>
        /// Конвертация символа в координату
        /// </summary>
        /// <param name="letter">символ, который необходимо конвертировать</param>
        /// <returns>Координата</returns>
        private Int32 ConvertLetterToInt(char letter)
        {
            Int32 number = 0;
            string str = topLineText.ToLower();
            try
            {
                while (str[number] != letter)
                    number++;
                return number + 1;
            }
            catch (Exception ex)
            {
            }
            return number;
        }
        /// <summary>
        /// Инициализация основных элементов карты
        /// </summary>
        /// <param name="width">ширина карты</param>
        /// <param name="height">высота карты</param>
        private void Initializtion(Int32 width, Int32 height)
        {

            _leftLine   = InitLineWithText(leftLineText,    new Vector2D(0, 0), false);
            _topLine    = InitLineWithText(topLineText,     new Vector2D(0, 0), true);
            _map =       InitMap(width, height);
            _mapBuffer = InitMap(width, height);

            _title = new Lable(Title);
            Location = new Vector2D(0, 0);

            IsCellsVisible = true;
            IsVisible = true;
            
            Shipes = new List<ShipBase>();
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Добавление корабля на карты, строго рекомендуется использовать это реализицию
        /// вместо стандартной
        /// </summary>
        /// <param name="ship">Корабль, который необходимо добавить на карту</param>
        public void AddShipToMap(Shipes.ShipBase ship)
        {
            ship.AtMap(this);
            Shipes.Add(ship);
        }

        /*   1 2 3 4 5 6 7 8 9 0 
             A B C D E F J H I G
           1 
           2
           3
           4
           5
           6
           7
           8
           9
           0

            */
        /// <summary>
        /// Конвертация строки в позицию на консоле
        /// </summary>
        /// <param name="str">строка, которую необходимо конвертировать</param>
        /// <returns>результат конвертации</returns>
        public Vector2D ConvertToPostion(string str)
        {
            Int32 x = 0;
            Int32 y = 0;
            foreach (char sym in str)
            {
                if (char.IsDigit(sym))
                    x = Convert.ToInt32(sym.ToString());
                else
                    y = ConvertLetterToInt(sym);
            }
            if (x == 0) x = 10;
            if (y == 0) y = 10;
            return new Vector2D(x, y);
        }

        /// <summary>
        /// Метод для отрисивки карты и элементов на ней
        /// </summary>
        public override void Draw()
        {
            _title.Location = this.Location + new Vector2D(1, 0);
            _title.Draw();

            foreach (Cell cell in _topLine)
                Render.WithOffset(cell, 1, 1);

            foreach (Cell cell in _leftLine)
                Render.WithOffset(cell, 0, 2);

            if (IsCellsVisible)
            {
                foreach (Cell cell in _map)
                    Render.WithOffset(cell, 1, 2);

                foreach (ShipBase ship in Shipes)
                {
                    ship.Draw();
                }
            }
            else
                foreach (Cell cell in _mapBuffer)
                    Render.WithOffset(cell, 1, 2);
        }
       
        /// <summary>
        /// Выстрел по позиции
        /// </summary>
        /// <param name="position">позиция, которая простреливается</param>
        /// <returns>возвращает сообщение о том куда пришелся выстрел</returns>
        public string ShotAt(Vector2D position)
        {

            if (position.X == 0)
                position.X = 10;

            if (position.Y == 0)
                position.Y = 10;
            //1 2 3
            //4 5 6
            //7 8 9

            if (position.X != 0 && position.Y != 0)
            {
                position.X--;
                position.Y--;
            }

            if (_map[position.X * Widht + position.Y].Symbol.Equals('~'))
            {
                _map[position.X * Widht + position.Y].Symbol = '*';
                _map[position.X * Widht + position.Y].Color = ConsoleColor.White;
                _mapBuffer[position.X * Widht + position.Y].Symbol = '*';
                _mapBuffer[position.X * Widht + position.Y].Color = ConsoleColor.White;
            }
            else 
            {
                _map[position.X * Widht + position.Y].Symbol = '*';
                _map[position.X * Widht + position.Y].Color = ConsoleColor.Red;
                _mapBuffer[position.X * Widht + position.Y].Symbol = '*';
                _mapBuffer[position.X * Widht + position.Y].Color = ConsoleColor.Red;
            }

            Vector2D position_swaped = new Vector2D(position.Y, position.X);
            Vector2D pos = position_swaped + Location;
            pos.X += 1;
            pos.Y += 2;
            foreach (ShipBase ship in Shipes)
            {
                if (!ship.IsDestroied)
                {
                    foreach (Cell cell in ship.Body)
                    {
                        if (cell.Position == pos)
                        {
                            _mapBuffer[position.X * Widht + position.Y].Symbol = '*';
                            _mapBuffer[position.X * Widht + position.Y].Color = ConsoleColor.Red;
                            ship.HitCell(pos);
                        }
                    }
                }

            }
            return $"Выстрел произведен в позицию - x: {position.X} y: {position.Y}";
        }
        /// <summary>
        /// Выстрел по позиции
        /// </summary>
        /// <param name="position">позиция, которая простреливается</param>
        /// <returns>возвращает сообщение о том куда пришелся выстрел</returns>
        public string ShotAt(string position)
        {
            Vector2D pos = ConvertToPostion(position);
            return ShotAt(pos);
        }
        #endregion
    }
}
