using System;
using System.Collections.Generic;
using System.Text;

namespace SeeBattle.Menu.Commands
{
    internal class HelpCommand : ICommand
    {
        #region Private Members
        private Int32 _screenHeight = 25;
        private Int32 _screenWidht  = 15;
        Core.Controls.Screen _helpScreen;
     
        #endregion

        #region Constructors
        public HelpCommand()
        {
            _helpScreen = new Core.Controls.Screen(_screenHeight, _screenWidht);
        }
        #endregion

        #region Public Methods
        public void Execute()
        {
            while (true)
            {
                _helpScreen.Draw();
            }
            // throw new Exception("TODO");
        }
        #endregion
    }
}
