using System;
using System.Collections.Generic;
using System.Text;

namespace Seebattle.Core
{
    internal class Singleton<T> where T : new()
    {
        static T _instance;

        public T Instance()
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }
    }
}
