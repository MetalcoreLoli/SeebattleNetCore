using SeeBattle.Core;
using SeeBattle.Menu;
using System;
using SeeBattle.Menu.Commands;
using Seebattle.Core.Dialogs;

namespace Seebattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            string text = "Новая Игра";
            Menu menu = new Menu(text.Length + 6, 5, new Vector2D(2, 1));
            menu.Items.Add(new MenuItem(new StartGameCommand(), text,       new Vector2D(2, 1)));
            menu.Items.Add(new MenuItem(new HelpCommand(),      "Справка",  new Vector2D(2, 2)));
            menu.Items.Add(new MenuItem(new CloseCommand(),     "Выход",    new Vector2D(2, 3)));
            menu.Show();
            //var dialogResult = MessageBox.Show("Hello, World!", Core.Dialogs.Enums.MessageBoxButtons.OkCancel);
            Console.ReadKey();
        }
    }
}
