using SeeBattle.Core;
using SeeBattle.Game;
using SeeBattle.Game.Actors;
using SeeBattle.Menu;
using System;

namespace Seebattle
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "Новая Игра";
            //Menu menu = new Menu(text.Length + 4, 6, new Vector2D(5, 1));
            //menu.Items.Add(new MenuItem(null, text, new Vector2D(2, 1)));
            //menu.Items.Add(new MenuItem(null, "Выход", new Vector2D(4, 3)));
            //menu.Show();
            Game game = new Game(
              new Player("Test-Chan", new Map("Test-Chan Field")),
              new Player("Test'Er", new Map("Test'Er Field")));
            game.Start();

            Console.Read();

        }
    }
}
