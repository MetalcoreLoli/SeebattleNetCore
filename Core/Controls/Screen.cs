using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core.Controls
{
    internal class Screen : Control
    {
        #region Private Members

        private new Cell[] body;

        #endregion

        #region Public Properties
        internal List<Control> Controls { get; set; }
        #endregion


        #region Events

        #endregion

        #region Constructors
        public Screen() : this(10, 10)
        {
        
        }


        public Screen(Int32 width, Int32 height) 
        {
            Widht   = width;
            Height  = height;
            Initialization();
            Controls = new List<Control>();

        }

        public Screen(Int32 width, Int32 height, params Control[] controls) : this(width, height)
        {
            Controls = new List<Control>(controls);
        }
        #endregion

        #region Private Methods

        private void Initialization()
        {
            body = InitBody(Widht, Height);
            DrawWallsAndAngels();
        }

        private void DrawWallsAndAngels()
        {
            body = Control.DrawUpDownWalls(body, Widht, Height);
            body = Control.DrawLeftRightWalls(body, Widht, Height);
            body = Control.DrawAngels(body, Widht, Height);
        }

        private void OnUpdate()
        {

        }

        #endregion

        #region Public Methods

        public void Clean()
        {
            for (int i = 0; i < Widht * Height; i++)
                body[i].Symbol = ' ';
            DrawWallsAndAngels();
        }

        public void Update()
        {
            Draw();
            OnUpdate();
        }

        public override void Draw()
        {
            Console.Clear();
            Clean();
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);

            foreach (Control control in Controls)
                control.Draw();   
        }
        #endregion
    }
}
