using System;
using System.Collections.Generic;
using System.Text;

namespace CustomIoC.LifeTimeManager
{
    public static class TypeLifeTime
    {
        public static ILifeTimeManager Singleton = new SingletonLifeTimeManager();
        public static ILifeTimeManager Transient = new TransientLifeTimeManger();
        public static ILifeTimeManager PerThread = new PerThreadLifeTimeManager();
    }
}
