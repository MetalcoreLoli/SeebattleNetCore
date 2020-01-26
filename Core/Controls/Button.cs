using System;
using SeeBattle.Core;


namespace SeeBattle.Core.Controls
{
    internal class Button : Control
    {
        #region Private Members
        private String _text;
        private Lable _textLable;

        private bool _isSelected;


        private Vector2D _location;

        #endregion

        #region Public Properties

        public bool IsPressed { get; set; }
        public bool IsSelected 
        { 
            get => _isSelected;

            set
            {
                _isSelected = value;
                if (_isSelected == true)
                {
                    for (int x = 1; x < Widht-1; x++)
                    {
                        for (int y = 1; y < Height - 1; y++)
                        {
                            body[x + Widht * y].BackColor = ConsoleColor.White;
                            body[x + Widht * y].Color     = ConsoleColor.Black;
                        }
                     
                    }
                }
                else
                {

                    for (int i = 0; i < body.Length; i++)
                    {
                        body[i].BackColor = ConsoleColor.Black;
                        body[i].Color = ConsoleColor.White;
                    }
                }
            }
        }

        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(this.GetType().GetProperty(nameof(Text)));
                if (_textLable != null)
                {
                    _textLable.Text = _text;
                    _textLable.Location += _location + 1;
                }
            }
        }

        public new Vector2D Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(this.GetType().GetProperty(nameof(Text)));

                body = InitBody(Widht, Height);
                body = Control.DrawUpDownWalls(body, Widht, Height);
                body = Control.DrawLeftRightWalls(body, Widht, Height);
                body = Control.DrawAngels(body, Widht, Height);
            }
        }

        #endregion

        #region Events
        public event EventHandler Click;
        public event EventHandler MouseOver;
        public event EventHandler MouseLeave; 
        #endregion
        
        #region Constructors
        public Button(Int32 width, Int32 height)
        {
            Widht = width;
            Height = height; 
            Init();
        }

        public Button(Int32 width, Int32 height, Vector2D location)
        {
            Widht = width;
            Height = height;
            Location = location;
            Init();
        }
        #endregion


        #region Public Methods
        public override void Draw()
        {
             foreach (Cell cell in body)
                  Render.WithOffset(cell, 0, 0);
            _textLable.Draw();
        }

        public void OnClick()
            => Click?.Invoke(this, new EventArgs());
        #endregion

        #region Private Methods




        private void Init()
        {
            //Init body
            body = InitBody(Widht, Height);
            body = Control.DrawUpDownWalls(body, Widht, Height);
            body = Control.DrawLeftRightWalls(body, Widht, Height);
            body = Control.DrawAngels(body, Widht, Height);
            
            //init lable
            _textLable = new Lable();
        }

        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] cells =  base.InitBody(width, height);

            for (int i = 0; i < cells.Length; i++)
                cells[i].Position += _location;
            
            return cells;
        }

        #endregion
    }
}
