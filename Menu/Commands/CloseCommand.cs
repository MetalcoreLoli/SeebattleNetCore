using System;
using System.Collections.Generic;
using System.Text;

namespace SeeBattle.Menu.Commands
{
    internal class CloseCommand : ICommand
    {
        public void Execute()
        {
            Console.Clear();
        }
    }
}
