using SeeBattle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core.Controls
{
    internal abstract class Control
    {
        #region Private Members

        #endregion

        #region Protected Members

        protected Cell[] body;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Расположение на консоле
        /// </summary>
        public Vector2D Location { get; set; }

        public bool IsVisible { get; set; } = true;


        /// <summary>
        /// Ширина
        /// </summary>
        public Int32 Widht { get; protected set; }

        /// <summary>
        /// Высота
        /// </summary>
        public Int32 Height { get; protected set; }

        #endregion

        #region Constructor
        public Control()
        {
            Widht = Height = 4;
            body = InitBody(Widht, Height);
        }

        public Control(Int32 width, Int32 height)
        {
            Widht = width;
            Height = height;
            body = InitBody(Widht, Height);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Отрисовка
        /// </summary>
        public abstract void Draw();

        public static Cell[] DrawLeftRightWalls(Cell[] body, Int32 bodyWidht, Int32 bodyHeight)
        {
            Cell[] temp = body;

            return temp;
        }

        public static Cell[] DrawUpDownWalls(Cell[] body, Int32 bodyWidht, Int32 bodyHeight)
        {
            Cell[] temp = body;

            return temp;
        }
        //angles
        public static Cell[] DrawAngels(Cell[] body, Int32 bodyWidht, Int32 bodyHeight)
        {
            Cell[] temp = body;

            return temp;
        }
        #endregion


        #region Protected Methods
        protected virtual Cell[] InitBody(Int32 width, Int32 height)
        {
            Cell[] temp = new Cell[width * height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    temp[x + width * y] = new Cell(
                        ' ',
                        (new Vector2D(x, y) + Location));
            return temp;
        }
        #endregion


    }
}
