using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeBattle.Core.Controls
{
    internal class Screen : Control
    {
        #region Public Properties
        public List<Control> Controls { get; set; }
        #endregion

        #region Constructors
        public Screen()
        {
            Controls = new List<Control>();
        }

        public Screen(Int32 width, Int32 height, params Control[] controls)
        {
            Controls = new List<Control>(controls);
        }
        #endregion

        #region Public Methods
        public override void Draw()
        {
            foreach (Control control in Controls)
            {
                control.Draw();   
            }
        }
        #endregion
    }
}
