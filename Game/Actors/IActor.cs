using SeeBattle.Game.Shipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Actors
{
    internal interface IActor
    {
        /// <summary>
        /// Крата игрока
        /// </summary>
        Map Map { get; set; }

        /// <summary>
        /// Крата противника
        /// </summary>
        Map EnemyMap { get; set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Количество потеряных кораблей
        /// </summary>
        Int32 Lost { get; set; }

        /// <summary>
        /// Количество уничтоженых кораблей
        /// </summary>
        Int32 Destroyed { get; set; }

        /// <summary>
        /// True - если игрок выйграл
        /// </summary>
        bool IsWin { get; set;  }

        Fleet Fleet { get; set; }
    }
}
