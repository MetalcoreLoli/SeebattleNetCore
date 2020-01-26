using Seebattle.Core.Dialogs.Enums;
using SeeBattle.Core;
using SeeBattle.Core.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seebattle.Core.Dialogs
{

    internal class MessageBox 
    {

        #region Private Members
        private static Vector2D _location;
        private static Lable _text;

        private static Cell[] _body;

        private static Int32 _width;
        private static Int32 _height;

        private static Button _ok;
        private static Button _cancel;

        private static Int32 _buttonHeight  = 3;
        private static Int32 _buttonWidth   = 10;


        private static Cursor _cursor;

        private static List<Button> _buttons;

        private static bool _isAlive;
        #endregion

        #region Public Properties

        public static DialogResult DialogResult;

        #endregion

        #region Constructors
        static MessageBox()
        {

            _text       = new Lable();
            _location   
                = new Vector2D(
                        Console.WindowWidth / 2 - _text.Widht, 
                        Console.WindowHeight / 2 - _text.Height);



            _width  = 30;
            _height = 10;
            InitControls();
            _body = InitBody(_width, _height);
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Метод для вызова диалогового окна с сообщением
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public static DialogResult Show(string message)
        {
            _text.Text = message;
            Draw(MessageBoxButtons.None);
            Console.Clear();
            return DialogResult;
        }


        public static DialogResult Show(string message, MessageBoxButtons buttons)
        {
            _text.Text = message;
            _isAlive = true;
            do
            {
                Draw(buttons);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        foreach (Button button in _buttons)
                        {
                            if(button.IsSelected)
                            {
                                button.OnClick();
                                if (!button.IsPressed) button.IsPressed = true;
                                else button.IsPressed = false;
                            }
                        }
                        _isAlive = false;
                        break;
                    
                    case ConsoleKey.Tab:
                        _cursor.MoveTo(new Vector2D(_buttonWidth + 1, 0));
                        break;
                    
                    case ConsoleKey.LeftArrow:
                        _cursor.MoveTo(new Vector2D(-_buttonWidth - 1, 0));
                        break;
                    
                    case ConsoleKey.RightArrow:
                        _cursor.MoveTo(new Vector2D(_buttonWidth + 1, 0));
                        break;

                    default: break;
                }
                foreach (Button button in _buttons)
                    IsButtonSelected(button);
            } while (_isAlive);
            Console.Clear();
            return DialogResult;
        }

        #endregion

        #region Private Methods

        private static void InitControls()
        {
            _buttons = new List<Button>();
            _cursor = new Cursor(_location.X + _buttonWidth / 2 + 1, _location.Y + _height - _buttonHeight - 1);
        }

        private static void Draw(MessageBoxButtons buttons)
        {
            _text.Location = _location + 1;
            foreach (Cell cell in _body)
                Render.WithOffset(cell, 0, 0);

            _text.Draw();
            DrawButtons(buttons);
        }

        private static bool IsButtonSelected(Button button)
        {
            if (button.Location.X + 1 == _cursor.Location.X)
            {
                if (button.IsSelected == false) button.IsSelected = true;
                return true;
            }
            else
            {
                button.IsSelected = false;
                return false;
            }
        }

        private static Cell[] InitBody(Int32 widht, Int32 heigth)
        {
            Cell[] temp = new Cell[widht * heigth];
            //заполение temp клетками
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

        private static void DrawButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    _ok = new Button(_buttonWidth, _buttonHeight, new Vector2D(_location.X + _buttonWidth / 2 + 1, _location.Y + _height - _buttonHeight - 1));
                    _ok.Text = "OK";

                    _ok.Click += _ok_Click;

                    if (!_buttons.Contains(_ok))
                        _buttons.Add(_ok);
                    break;
                
                case MessageBoxButtons.OkCancel:
                    _ok     = new Button(_buttonWidth, _buttonHeight, new Vector2D(_location.X + _buttonWidth / 2, _location.Y + _height - _buttonHeight - 1));
                    _cancel = new Button(_buttonWidth, _buttonHeight, new Vector2D(_location.X + _buttonWidth / 2  + _buttonWidth + 1, _location.Y + _height - _buttonHeight - 1));
                    
                    _ok.Click       += _ok_Click;
                    _cancel.Click   += _cancel_Click;

                    _ok.Text        = "OK";
                    _cancel.Text    = "Cancel";

                    if (!_buttons.Contains(_ok))
                        _buttons.Add(_ok);

                    if (!_buttons.Contains(_cancel))
                        _buttons.Add(_cancel);
                    
                    break;
                
                default:
                    break;
            }
            if (_buttons != null)
                foreach (var button in _buttons)
                    button.Draw();
        }

        private static void _cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private static void _ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
