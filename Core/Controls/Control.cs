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

        public static Cell[] DrawLeftRightWalls(
                Cell[] body, 
                Int32 bodyWidht, 
                Int32 bodyHeight)
        {
            Cell[] temp = body;
            for (int x = 0; x < bodyWidht; x++)
                for (int y = 0; y < bodyHeight; y++)
                {
                    Int32 index = x + bodyWidht * y;
                    if (x.Equals(0) && y > 0)
                        temp[index].Symbol = '|';
                    if (x.Equals(bodyWidht - 1) && y > 0)
                        temp[index].Symbol = '|';

                }
            return temp;
        }

        public static Cell[] DrawUpDownWalls(
                Cell[] body, 
                Int32 bodyWidht, 
                Int32 bodyHeight)
        {
            Cell[] temp = body;
            for (int i = 1; i < bodyWidht - 1; i++)
                temp[i].Symbol = '-';

            for (
                    int i = bodyWidht * bodyHeight - bodyWidht; 
                    i < bodyWidht * bodyHeight - 1; 
                    i++)
                temp[i].Symbol = '-';

            return temp;
        }

        //angles
        public static Cell[] DrawAngels(Cell[] body, Int32 bodyWidht, Int32 bodyHeight)
        {
            Cell[] temp = body;
            for (int x = 0; x < bodyWidht; x++)
                for (int y = 0; y < bodyHeight; y++)
                {
                    Int32 index = x + bodyWidht * y;
                    if (index.Equals(0) 
                        || index.Equals(bodyWidht * bodyHeight - 1) 
                        || index.Equals(bodyWidht * bodyHeight - bodyWidht) 
                        || index.Equals(bodyWidht - 1))
                        temp[index].Symbol = '+';
                }
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
