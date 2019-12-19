using SeeBattle.Game;
using SeeBattle.Game.Actors;

namespace SeeBattle.Menu.Commands
{

    internal class StartGameCommand : ICommand
    {

        private Game.Game _game;

        public StartGameCommand() 
            : this (new Game.Game(
                    new Player("Test-Chan", new Map("Test-Chan Field")),
                    new Player("Test'Er", new Map("Test'Er Field"))))
        {
        }

        public StartGameCommand(IActor fisrtPlayer, IActor secondPlayer) 
            : this (new Game.Game(fisrtPlayer, secondPlayer))
        {
        }

        public StartGameCommand(Game.Game game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Start();
        }
    }
}
