﻿using System;
using System.Collections.Generic;
using System.Linq;
using SeeBattle.Core;
using SeeBattle.Core.Controls;
using SeeBattle.Game.Actors;
using SeeBattle.Game.LogConsoles;
using SeeBattle.Game.Shipes;

namespace SeeBattle.Game
{
    internal class Game
    {
        #region Private Members
        /// <summary>
        /// Игровая консоль отвечающая за пользовательский ввод
        /// </summary>
        private GameConsole _gameConsole;

        /// <summary>
        /// Игровая консоль отвечающая за логирование
        /// </summary>
        private GameConsole _logConsole;

        /// <summary>
        /// размеры игровой консоли
        /// </summary>
        private const Int32 _gameConsoleWidth   = 25;
        private const Int32 _gameConsoleHeight  = 5;

        /// <summary>
        /// размеры консоли логгирования
        /// </summary>
        private const Int32 _logConsoleWidth    = 55;
        private const Int32 _logConsoleHeight   = 10;

        /// <summary>
        /// Первый игрок
        /// </summary>
        private IActor _firstPlayer;

        /// <summary>
        /// Второй игрок
        /// </summary>
        private IActor _secondPlayer;

        /// <summary>
        /// метка с инфомацией о том чей ход на данный момент
        /// </summary>
        private Lable _currentPlayerLable;

        /// <summary>
        /// метка с инфомацией о счете игрока
        /// </summary>
        private Lable _currentPlayerScoreLable;

        /// <summary>
        /// метка с информацией о количестве кораблей
        /// </summary>
        private Lable _currentPlayerShipCountLable;
        #endregion

        #region Public Propertie
        /// <summary>
        /// Контролы на консоле
        /// </summary>
        public List<Control>   Controls  { get; set; }
        #endregion

        #region Constructors
        public Game(IActor FirstPlayer, IActor SecondPlayer)
        {

            InitPlayers(FirstPlayer, SecondPlayer);
            InitControls();
          
            Controls.Add(_firstPlayer.Map);
            Controls.Add(_firstPlayer.EnemyMap);
            Controls.Add(_secondPlayer.Map);
            Controls.Add(_secondPlayer.EnemyMap);
           // Controls.Add(_logConsole);
            Controls.Add(_gameConsole);
            // GameScene       = new Screen(Console.WindowTop, Console.WindowLeft, _gameConsole, _logConsole);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Метод запускает игру
        /// </summary>
        public void Start()
        {
            //игроки создаю свой флот и размещают его на карте
            //PlayerCreateFleet(_firstPlayer);
            //PlayerCreateFleet(_secondPlayer);


            _firstPlayer.Map.AddShipToMap(CreateShip(_firstPlayer.Map, "s a1 v"));
            _firstPlayer.Map.AddShipToMap(CreateShip(_firstPlayer.Map, "s a3 v"));
            _firstPlayer.Map.AddShipToMap(CreateShip(_firstPlayer.Map, "c b5"));

            _secondPlayer.Map.AddShipToMap(CreateShip(_secondPlayer.Map, "s a1 v"));
            _secondPlayer.Map.AddShipToMap(CreateShip(_secondPlayer.Map, "s a3"));
            _secondPlayer.Map.AddShipToMap(CreateShip(_secondPlayer.Map, "c b5"));

            //очистка консоли
            Console.Clear();
            do
            {
                //ход первого игрока
                PlayerTurn(_firstPlayer);
                if (_firstPlayer.IsWin)
                {
                    DrawWinnerStatistic(_firstPlayer);
                    break;
                }

                //ход второго игрока
                PlayerTurn(_secondPlayer);
                if (_secondPlayer.IsWin) 
                {
                    DrawWinnerStatistic(_secondPlayer); 
                    break;
                }
            }
            while (true);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Инизиализация контролов
        /// </summary>
        private void InitControls()
        {
            Controls                                = new List<Control>();

            _currentPlayerLable                     = new Lable();
            _currentPlayerScoreLable                = new Lable();
            _currentPlayerShipCountLable            = new Lable();

            _currentPlayerLable.Location            = new Core.Vector2D(0, _firstPlayer.Map.Height + 2);
            _currentPlayerScoreLable.Location       = new Core.Vector2D(0, _firstPlayer.Map.Height + 3);
            _currentPlayerShipCountLable.Location   = new Core.Vector2D(0, _firstPlayer.Map.Height + 4);

            Controls.Add(_currentPlayerLable);
            Controls.Add(_currentPlayerScoreLable);
            Controls.Add(_currentPlayerShipCountLable);
        }

        /// <summary>
        /// Метод инициализирует играков и игре
        /// </summary>
        /// <param name="FirstPlayer">Первый игрок</param>
        /// <param name="SecondPlayer">Второй игрок</param>
        private void InitPlayers(IActor FirstPlayer, IActor SecondPlayer)
        {
            _firstPlayer    = FirstPlayer;
            _secondPlayer   = SecondPlayer;

            _secondPlayer.Map.Location += new Core.Vector2D(20, 0);

            _firstPlayer.EnemyMap   = _secondPlayer.Map;
            _secondPlayer.EnemyMap  = _firstPlayer.Map;

            _firstPlayer.Map.IsVisible      = true;
            _firstPlayer.EnemyMap.IsVisible = true;
            _firstPlayer.EnemyMap.IsCellsVisible = false;

            //_secondPlayer.Map.IsVisible = false;
            //_secondPlayer.EnemyMap.IsVisible = false;

            _gameConsole    = new GameConsole(_gameConsoleWidth + 10, _gameConsoleHeight,   new Core.Vector2D(0, 15));
            _logConsole     = new GameConsole(_logConsoleWidth, _logConsoleHeight,          new Core.Vector2D(_gameConsoleWidth + 15, 0));

        }

        /// <summary>
        /// Метод реализующий ход игрока
        /// </summary>
        /// <param name="Player">Игрок</param>
        private void PlayerTurn(IActor Player)
        {
            if (!Player.IsWin)
            {
               
                _currentPlayerLable.Text = $"{Player.Name} ваш ход";
                ShowPlayerStatistic(Player);

                //отрисовка контролов
                DrawControls();

                Player.EnemyMap.IsCellsVisible = false;
                Player.Map.IsCellsVisible = true;

                Controls.First(c => c == Player.EnemyMap).Draw();
                Controls.First(c => c == Player.Map).Draw();

                string pos = _gameConsole.Input();

                _logConsole.AddString(Player.EnemyMap.ShotAt(pos));
                Player.Lost = Player.Map.Shipes.Count(s => s.IsDestroied);
                Player.Destroyed = Player.EnemyMap.Shipes.Count(s => s.IsDestroied);
                Player.IsWin = (Player.EnemyMap.Shipes.Count - Player.EnemyMap.Shipes.Count(s => s.IsDestroied) == 0) ? true : false;
            }
        }

        private void DrawWinnerStatistic(IActor player)
        {
                _currentPlayerLable.Text = $"{player.Name}  - победитель";
                ShowPlayerStatistic(player);
                player.EnemyMap.IsCellsVisible = true;
                DrawControls();
        }

        /// <summary>
        /// Вывoд статистики игрока
        /// </summary>
        /// <param name="actor">Игрок</param>
        private void ShowPlayerStatistic(IActor actor)
        {
            _currentPlayerScoreLable.Text = $"{actor.Name} ваш счет(Подбито / Потеряно): {actor.Destroyed} / {actor.Lost}";
            _currentPlayerShipCountLable.Text = $"Количество кораблей выживших кораблей: {(actor.Map.Shipes.Count - actor.Lost)}";
        }

        /// <summary>
        /// Отрисовка контролов в консоле
        /// </summary>
        private void DrawControls()
        {
            Console.Clear();
            foreach (Control control in Controls)
                if (control.IsVisible) control.Draw();
        }

        /// <summary>
        /// В методе игрок создает флот и размещает его на крате
        /// </summary>
        /// <param name="Player">Игрок</param>
        private void PlayerCreateFleet(IActor Player)
        {
            //очистка игровой консоли
            _gameConsole.Clean();
            
            //создание меток с текстом-подсказкой
            Lable firstLine     = new Lable($"{Player.Name} разместите флот");
            Lable secondLine    = new Lable($"линкор(b) крейсер(cr) эсминец(c) подводная лодка(s)");
            Lable thirdLine     = new Lable($"Пример: c a1 v");
            
            //размешение меток в консоле
            firstLine.Location  = new Core.Vector2D(0, _firstPlayer.Map.Height + 2);
            secondLine.Location = new Core.Vector2D(0, _firstPlayer.Map.Height + 3);
            thirdLine.Location  = new Core.Vector2D(0, _firstPlayer.Map.Height + 4);
            
            //добавление их на консоль
            Controls.Add(firstLine);
            Controls.Add(secondLine);
            Controls.Add(thirdLine);
            
            //отрисовка конторлов
            DrawControls();

            try
            {
                ShipBase ship = CreateShip(Player.Map, _gameConsole.Input());
                Player.Fleet.AddShipeToFleet(ship);
                Player.Map.AddShipToMap(ship);
            }
            catch (Exception ex)
            {
                _gameConsole.AddString(ex.Message);
            }
            
            //удаление меток с консоли
            Controls.Remove(firstLine);
            Controls.Remove(secondLine);
            Controls.Remove(thirdLine);
        }

        /// <summary>
        ///  Метод создает корабль изходя из пользовательского ввода
        ///  Пример:
        ///  b a1 v;
        ///  Метод создаст вертикально напревленный линкор на карте на координатах a1
        /// </summary>
        /// <param name="map">Карта, где будет размещене корабль</param>
        /// <param name="playersInput">строка из которой создается корабль</param>
        /// <returns>Корабль</returns>
        private ShipBase CreateShip(Map map, string playersInput)
        {
            string shipType = playersInput.Split(' ')[0];
            string position_from_str = playersInput.Split(' ')[1];
            bool isVectical = (playersInput.Split(' ').Count() == 3) ? true : false;

            ShipBase ship   = CreateShipFromString(shipType);
            ship.IsVertical = isVectical;

            Vector2D swap           = map.ConvertToPostion(position_from_str) - 1;
            Vector2D position_vec   = new Vector2D(swap.Y, swap.X);
            ship.Location           = position_vec;

            return ship;
        }

        /// <summary>
        /// Метод возвращает корабль в зависимости от перданного типа корaбля
        /// </summary>
        /// <param name="shipType"></param>
        /// <returns>Корабль полученый из строки</returns>
        private ShipBase CreateShipFromString(string shipType)
        {
            ShipBase shipBase = null;
            switch (shipType)
            {
                case "b":
                    shipBase = new BattleShip(); // линкор
                    break;
                case "c":
                    shipBase = new Carrier();   // эсминец
                    break;
                case "cr":
                    shipBase = new Cruiser();   // крейсер
                    break;
                case "s":
                    shipBase = new Submarine(); // подлодка
                    break;
                default:
                    throw new Exception("Нет так кого корабля!");
            }
            return shipBase;
        }
        #endregion
    }
}