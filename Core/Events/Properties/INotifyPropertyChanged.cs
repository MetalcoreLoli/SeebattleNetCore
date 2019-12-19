using System;
using System.Collections.Generic;
using System.Text;

namespace Seebattle.Core.Events.Properties
{
    internal interface INotifyPropertyChanged
    {
        event EventHandler<PropertyChangedEventArgs> PropertyChanged;
    }
}
