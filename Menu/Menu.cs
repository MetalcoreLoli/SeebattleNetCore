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

        private MenuItem _selectedItem;

        public List<MenuItem> Items { get; set; }

        public MenuItem SelectedItem 
        { 
            get => _selectedItem; 
            set => _selectedItem = value; 
        }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public override void Draw()
        {
            foreach (MenuItem item in Items)
                item.Draw();
        }
    }
}
