using SeeBattle.Core;
using SeeBattle.Core.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seebattle.Core.Dialogs
{

    internal sealed class MessageBox 
    {

        #region Private Members
        private static Vector2D _location;
        private static Lable _text;

        private static Cell[] _body;

        private static Int32 _width;
        private static Int32 _height;

        #endregion

        #region Constructors
        static MessageBox()
        {
            _location   
                = new Vector2D(
                        Console.WindowWidth / 2, 
                        Console.WindowHeight / 2);

            _width  = 30;
            _height = 5;

            _body = InitBody(_width, _height);
            _text       = new Lable();
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Метод для вызова диалогового окна с сообщением
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public static void Show(string message)
        {
            _text.Text = message;
            foreach (Cell cell in _body)
               Render.WithOffset(cell, 0, 0); 
            
            _text.Draw();

        }
        #endregion

        #region Private Methods
        private static Cell[] InitBody(Int32 widht, Int32 heigth)
        {
            Cell[] temp = new Cell[widht * heigth];
            //заполения temp клетками
            for (int x = 0; x < widht; x++)
                for (int y = 0; y < heigth; y++)
                {
                    temp[x + widht * y] = new Cell(
                        ' ',
                        (new Vector2D(x, y) + _location));
                }

            
           //замена символов клеток, что находятся по углам на +
           //так же замена символов клеток, что находятся по бокам на |
           temp = Control.DrawUpDownWalls(temp, widht, heigth);
           temp = Control.DrawLeftRightWalls(temp, widht, heigth);
           temp = Control.DrawAngels(temp, widht, heigth); 


            return temp;
        }
        #endregion
    }
}
