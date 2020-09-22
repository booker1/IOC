using System;
using System.Collections.Generic;
using System.Text;
using CustomIoC.LifeTimeManager;

namespace CustomIoC
{
    /// <summary>
    /// 映射信息对象类
    /// </summary>
    public class RegisteredType
    {
        public Type TargetType { get; set; }

        public ILifeTimeManager LifeTimeManager { get; set; }
    }
}
