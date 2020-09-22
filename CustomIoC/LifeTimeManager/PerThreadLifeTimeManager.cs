using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CustomIoC.LifeTimeManager
{
    /// <summary>
    /// 线程单例，作用域
    /// </summary>
    public class PerThreadLifeTimeManager : ILifeTimeManager
    {
        //通过线程本地存储进行线程单例管理
        private readonly ThreadLocal<Dictionary<string, object>> _perThreadDictonary =
            new ThreadLocal<Dictionary<string, object>>();
        public object CreateInstance(Type type, params object[] args)
        {
            //判断线程存储是否存在字段对象
            if (_perThreadDictonary.Value == null)
                _perThreadDictonary.Value = new Dictionary<string, object>();

            var key = type.FullName;
            if (_perThreadDictonary.Value.ContainsKey(key)) return _perThreadDictonary.Value[key];

            var instance = Activator.CreateInstance(type, args);
            _perThreadDictonary.Value.Add(key, instance);
            return instance;
        }
    }
}
