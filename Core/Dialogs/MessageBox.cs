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
            _location   = new Vector2D(Console.WindowWidth / 2, Console.WindowHeight / 2);

            _width  = 15;
            _height = 10;

            _body = new Cell[_width * _height];
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

            
            for (int x = 0; x < widht; x++)
                for (int y = 0; y < heigth; y++)
                { 
                    
                }

           //замена символов клеток, что находятся по углам на +
           //так же замена символов клеток, что находятся по бокам на |
           for (int x = 0; x < widht; x++)
                for (int y = 0; y < heigth; y++)
                {
                    Int32 index = x + widht * y;

                    if (x.Equals(0) && y > 0)
                        temp[index].Symbol = '|';
                    if (x.Equals(widht - 1) && y > 0)
                        temp[index].Symbol = '|';

                    if (index.Equals(0) || index.Equals(widht * heigth - 2) 
                        || index.Equals(widht * heigth - widht) || index.Equals(widht - 1))
                        temp[index].Symbol = '+';
                }


            return temp;
        }
        #endregion
    }
}
