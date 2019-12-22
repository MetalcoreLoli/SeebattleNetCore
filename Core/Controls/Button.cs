using System;
using SeeBattle.Core;


namespace SeeBattle.Core.Controls
{
    internal class Button : Control
    {
        #region Private Members
        private String _text;
        private Lable _textLable;
        #endregion

        #region Public Properties
        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(this.GetType().GetProperty(nameof(Text)));
            }
        }
        #endregion

        #region Events
        public event EventHandler Click;
        public event EventHandler MouseOver;
        public event EventHandler MouseLeave; 
        #endregion
        
        #region Constructors
        public Button(Int32 width, Int32 height)
        {
            Widht = width;
            Height = height; 
            Init();
        } 
        #endregion
        
        
        #region Public Methods
        public override void Draw()
        {
            foreach(Cell cell in body)
               Render.WithOffset(cell, 0, 0); 
        } 
        #endregion 
        
        #region Private Methods
       
        private void Init()
        {
            //Init body
            body = InitBody(Widht, Height);
            body = Control.DrawUpDownWalls(body, Widht, Height);
            body = Control.DrawLeftRightWalls(body, Widht, Height);
            body = Control.DrawAngels(body, Widht, Height);
            
            //init lable
            _textLable = new Lable();
        } 

        #endregion
    }
}
