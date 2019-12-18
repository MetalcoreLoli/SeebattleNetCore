using SeeBattle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core.Controls
{
    internal class Lable : Control
    {
        private Cell[] body;

        string _text;

        public string Text 
        { 
            get => _text;
            set
            {
                _text = value;
                Clean();
                body = CreateLableFromString(_text);
            }
        }

        public Lable()
        {
            this.Text = "";
            body = CreateLableFromString(Text);
        }

        public Lable(string Text)
        {
            this.Text = Text;
            body = CreateLableFromString(Text);
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, Location.X, Location.Y);
        }

        private Cell[] CreateLableFromString(string title)
        {
            Cell[] temp = new Cell[title.Length];
            for (int i = 0; i < title.Length; i++)
                temp[i] = new Cell(title[i], new Vector2D(i, 0));
            return temp;
        }

        public void Clean()
        {
            body = new Cell[Text.Length];
            for (int i = 0; i < Text.Length; i++)
                body[i] = new Cell(' ', new Vector2D(i, 0));
        }
    }
}
