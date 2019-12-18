using SeeBattle.Core;
using SeeBattle.Core.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Menu
{
    internal class Menu : Control
    {
        #region Private Members
        
        private MenuItem _selectedItem;
        #endregion

        #region Public Propertie
        public List<MenuItem> Items { get; set; }
        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        #endregion

        #region Constructors
        public Menu(Int32 width , Int32 height)
        {
            Items = new List<MenuItem>();
            SelectedItem = Items.FirstOrDefault();
            Widht = width;
            Height = height;
            Location = new Vector2D(0, 0);
            Init();
        }
        public Menu(Int32 width, Int32 height, Vector2D location)
        {
            Items = new List<MenuItem>();
            SelectedItem = Items.FirstOrDefault();
            Widht = width;
            Height = height;
            Location = location;
            Init();
        }


        #endregion

        #region Private Members

        private void Init()
        {
            body = InitBody(Widht, Height);
            body = DrawBorders(body, Widht, Height);

           


        }

        private Cell[] DrawBorders(Cell[] body, Int32 width, Int32 height)
        {
            Cell[] temp = body;

            for (int i = 1; i < width - 1; i++)
                temp[i].Symbol = '-';

            for (int i = width * height - width + 1; i < width * height - 1; i++)
                temp[i].Symbol = '-';


            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Int32 index = x + width * y;

                    if (x.Equals(0) && y > 0)
                        temp[index].Symbol = '|';
                    if (x.Equals(width - 1) && y > 0)
                        temp[index].Symbol = '|';

                    if (index.Equals(0) || index.Equals(width - 1) || index.Equals(width * height - 1) || index.Equals(width * height - width))
                        temp[index].Symbol = '+';
                }
            return temp;
        }

        #endregion


        #region Public Members

        public void Show()
        {
            foreach (MenuItem item in Items)
                item.Location += Location;

            Draw();
            while (true)
            {
             
                Draw();
            }
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
            foreach (MenuItem item in Items)
                item.Draw();
        }
        #endregion
    }
}
