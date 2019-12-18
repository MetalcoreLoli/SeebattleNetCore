using SeeBattle.Core;
using SeeBattle.Core.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeeBattle.Menu
{
    internal class MenuItem : Control
    {
        #region Private Members
        private Lable _text;
        private String _itemText;
        private Vector2D _location;

        #endregion

        #region Public Properties
        public String Text
        {
            get => _itemText;
            set
            {
                _itemText = value;
                _text = new Lable(_itemText);
            }
        }

        public new Vector2D Location
        {
            get => _location;
            set 
            {
                _location = value;
                _text.Location = _location;
            }
        }

        public ICommand Action { get; internal set; }

        public List<MenuItem> Items { get; set; }

        public bool IsHightLight { get; set; } = false;

        #endregion



        #region Constructors

        public MenuItem(ICommand Action, Int32 width, Int32 height)
           : base(width, height)
        {
            this.Action = Action;
            Init(new Vector2D(0, 0));
        }

        public MenuItem(ICommand Action, String text)
           : base(text.Length, 1)
        {
            this.Action = Action;
            Text = text;
            Init(new Vector2D(0, 0));

        }

        public MenuItem(ICommand Action, Int32 width, Int32 height, Vector2D location)
            : base (width, height)
        {
            this.Action = Action;
            Location = location;
        }

        public MenuItem(ICommand Action, String text, Vector2D location)
           : base(text.Length, 1)
        {
            this.Action = Action;
            Text = text;
            Location = location;

        }

        #endregion

        #region Private Methods

        private void Init(Vector2D location)
        {
            Location = location;
            body = InitBody(Widht, Height);
        }
        #endregion

        #region Public Methods
        public override void Draw()
        {
            if (IsHightLight)
                Text = "[ " + Text + " ]";
            //foreach (Cell cell in body)
            //    Render.WithOffset(cell, 0, 0);
            _text.Draw();
        }
        #endregion

    }
}
