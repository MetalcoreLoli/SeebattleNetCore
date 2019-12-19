using SeeBattle.Core;
using SeeBattle.Core.Controls;
using SeeBattle.Menu.Commands;
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

        private new Lable _lTitle;
        private new String _title;
        #endregion

        #region Public Propertie

        public new String Title
        {
            get => _title;
            set
            {
                _title = value;
                _lTitle = new Lable(_title);
                _lTitle.Location = new Vector2D (Location.X + (Widht  - _title.Length) / 4 + _title.Length - 2, Location.Y - 1);
                OnPropertyChanged(this.GetType().GetProperty(nameof(Title)));
            }
        }

        public List<MenuItem> Items { get; set; }

        public Cursor Cursor { get; private set; }

        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        public bool IsClose { get; set; }
        #endregion

        #region Constructors
        public Menu(Int32 width, Int32 height) : this(width, height, new Vector2D(0, 0))
        { }

        public Menu(Int32 width, Int32 height, Vector2D location)
        {
            Items = new List<MenuItem>();
            SelectedItem = Items.FirstOrDefault();
            Widht = width;
            Height = height;
            Location = location;
            Cursor = new Cursor(Location + 1);
            Init();
        }


        #endregion

        #region Private Members

        private void Init()
        {
            body = InitBody(Widht, Height);
            body = Control.DrawUpDownWalls(body, Widht, Height);
            body = Control.DrawLeftRightWalls(body, Widht, Height);
            body = Control.DrawAngels(body, Widht, Height);
            Title = "Menu";
            PropertyChanged += (sender, evArgs) => 
            {
                switch (evArgs.PropertyName)
                {
                    case nameof(Title):
                        _lTitle = new Lable(evArgs.Property.GetValue(sender).ToString());
                        break;
                    default:
                        break;
                }
            };
        }
        #endregion


        #region Public Members

        public void Show()
        {
             UpdateMenuItems();
            do
            {
                if (IsClose) break;
                Draw();
                Cursor.Draw();
                
                var keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if ((Cursor.Location.Y - 1) < Height)
                            Cursor.MoveTo(new Vector2D(0, -1));
                        break;
                    case ConsoleKey.DownArrow:
                        if ((Cursor.Location.Y + 1) < Widht)
                            Cursor.MoveTo(new Vector2D(0, 1));
                        break;
                    case ConsoleKey.Enter:
                        var item = Items.FirstOrDefault(item => item.IsSelected);
                        if (item!= null)
                            ActivateMenuItem(item);
                        break;
                }

              
                
                foreach (MenuItem menuItem in Items)
                {
                    if (menuItem.Location.Y.Equals(Cursor.Location.Y))
                    {
                        SelectedItem = menuItem;
                        if (!menuItem.IsSelected)
                            menuItem.IsSelected = true;
                    }
                    else
                        menuItem.IsSelected = false;
                }
            }
            while (true);
        }

        public override void Draw()
        {
            Console.Clear();
            _lTitle.Draw();
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
            foreach (MenuItem item in Items)
            { 
                if (item.Location.X < Widht && item.Location.Y < Height)
                    item.Draw();
            }
        }
        #endregion

        #region Private Methods

        private void UpdateMenuItems()
        {
            foreach (MenuItem item in Items)
                item.Location += Location;
        }

        private void ActivateMenuItem(MenuItem menuItem)
        {
            menuItem.Action.Execute();
            if (menuItem.Action is CloseCommand)
                IsClose = true;
        }
        #endregion
    }
}
