using System;
using System.Collections.Generic;
using System.Text;

namespace CustomIoC.LifeTimeManager
{
    public interface ILifeTimeManager
    {
        object CreateInstance(Type type, params object[] args);
    }
}
