using System;
using System.Collections.Generic;
using System.Text;

namespace CustomIoC.LifeTimeManager
{
    /// <summary>
    /// 瞬时
    /// </summary>
    public class TransientLifeTimeManger : ILifeTimeManager
    {
        public object CreateInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }
    }
}
