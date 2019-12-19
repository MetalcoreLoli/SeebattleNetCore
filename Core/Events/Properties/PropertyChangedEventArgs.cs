using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Seebattle.Core.Events.Properties
{
    internal class PropertyChangedEventArgs : EventArgs
    {

        #region Public Properties
        public string PropertyName  { get; set; }
        public PropertyInfo Property { get; set; }

        #endregion

        #region Contstructor
        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
        public PropertyChangedEventArgs(PropertyInfo property)
        {
            Property = property;
            PropertyName = Property.Name;
        }
        #endregion
    }
}
