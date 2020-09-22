using System;
using System.Collections.Generic;
using System.Text;

namespace CustomIoC.LifeTimeManager
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class SingletonLifeTimeManager : ILifeTimeManager
    {
        //单例字典
        private readonly Dictionary<string, object> _singletonDictionary = new Dictionary<string, object>();
        public object CreateInstance(Type type, params object[] args)
        {
            var key = type.FullName;
            if (_singletonDictionary.ContainsKey(key)) return _singletonDictionary[key];

            var instance = Activator.CreateInstance(type, args);
            _singletonDictionary.Add(key, instance);
            return instance;
        }
    }
}
