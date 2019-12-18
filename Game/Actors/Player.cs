using SeeBattle.Game.Shipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Game.Actors
{
    internal class Player : IActor
    {
        #region Public Properties
        /// <summary>
        /// Крата игрока
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        /// Крата противника
        /// </summary>
        public Map EnemyMap { get; set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество потеряных кораблей
        /// </summary>
        public int Lost { get; set; }

        /// <summary>
        ///  Количество уничтоженых кораблей
        /// </summary>
        public int Destroyed { get; set; }

        /// <summary>
        /// True - если игрок выйграл
        /// </summary>
        public bool IsWin { get; set; }
        public Fleet Fleet { get; set; }
        #endregion

        #region Constructors
        public Player(string Name, Map Map)
        {
            this.Name   = Name;
            this.Map    = Map;
            IsWin = false;
            Lost = Destroyed = 0;
            Fleet = new Fleet();
        }
        #endregion
    }
}
