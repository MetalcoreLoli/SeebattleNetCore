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
        private Lable _text;
        private String _itemText;

        public String Text 
        {
            get => _itemText;
            set 
            {
                _itemText = value;
                _text = new Lable(_itemText);
            }
        }

        public ICommand Action { get; private set; }

        public MenuItem(ICommand Action, Int32 width, Int32 height)
            : base (width, height)
        {
            this.Action = Action;
        }


        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
            _text.Draw();
        }
    }
}
