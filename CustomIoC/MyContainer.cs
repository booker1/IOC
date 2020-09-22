using CustomIoC.LifeTimeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomIoC
{
    public class MyContainer : IMyContainer
    {
        //键用来映射关系，使用字典作为登记本
        private static readonly Dictionary<string, RegisteredType> ContainerDictionary = new Dictionary<string, RegisteredType>();
        public void RegisterType<TType>()
            => RegisterType<TType, TType>("");
        public void RegisterType<TType, TImplementation>()
            => RegisterType<TType, TImplementation>("");

        public void RegisterType<TType>(string name) =>
            RegisterType<TType, TType>("");

        public void RegisterType<TType, TImplementation>(string name)=>
            RegisterType<TType, TImplementation>(name, TypeLifeTime.Transient);

        public void RegisterType<TType>(ILifeTimeManager lifeTimeManager)=>
             RegisterType<TType, TType>(lifeTimeManager);

        public void RegisterType<TType, TImplementation>(ILifeTimeManager lifeTimeManager)=>
            RegisterType<TType, TImplementation>("", lifeTimeManager);

        public void RegisterType<TType>(string name, ILifeTimeManager lifeTimeManager) =>
            RegisterType<TType, TType>(name, lifeTimeManager);

        public void RegisterType<TType, TImplementation>(string name, ILifeTimeManager lifeTimeManager)
        {
            string key = typeof(TType).FullName;
            if (!string.IsNullOrEmpty(name)) key = name;
            if (ContainerDictionary.ContainsKey(key)) //如果存在覆盖
                ContainerDictionary[key] = new RegisteredType
                {
                    TargetType = typeof(TImplementation),
                    LifeTimeManager = lifeTimeManager
                }; 
            else
                ContainerDictionary.Add(key, new RegisteredType
                {
                    TargetType = typeof(TImplementation),
                    LifeTimeManager = lifeTimeManager
                });//将传进来的泛型Type进行关系映射
        }

        public TType Resolve<TType>() =>
            Resolve<TType>("");

        public TType Resolve<TType>(string name)
        {
            //解析泛型Type获取key
            var key = typeof(TType).FullName;
            if (!string.IsNullOrEmpty(name)) key = name;
            // 根据key从容器中获取实现类
            var type = ContainerDictionary[key];

            //创建对象
            return (TType)CreateInstance(type);
        }

        //多个构造函数选择构造函数来创建实例
        //1.优先使用被特性标记的构造函数
        //2.没有特性标记选择参数最多的构造函数，参数个数一致，按默认顺序选择第一个

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object CreateInstance(RegisteredType type)
        {
            //获取所有的构造函数
            var ctors = type.TargetType.GetConstructors();

            //获取被标记的构造函数
            var ctor = ctors.FirstOrDefault(t => t.IsDefined(typeof(MyinjectionAttribute), true));
            if (ctor != null) CreateInstance(type, ctor);

            //没有特性获取参数最多的一个
            ctor = ctors.OrderByDescending(t => t.GetParameters().Length).First();

            return CreateInstance(type, ctor);
        }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ctor"></param>
        /// <returns></returns>
        private object CreateInstance(RegisteredType type, ConstructorInfo ctor)
        {
            //获取构造函数参数
            var paraArray = ctor.GetParameters();
            if (paraArray.Length == 0)
            {
              return type.CreateInstance();
            }

            var parameters = new List<object>();
            foreach (var para in paraArray)
            {
                //通过反射获取参数类型名
                var paraKey = para.ParameterType.FullName;
                //根据paraKey从字典获取已注册的实现类
                var paraType = ContainerDictionary[paraKey];

                // 递归注入构造参数
                var paraObj = CreateInstance(paraType);
                //将对象存储在list数组
                parameters.Add(paraObj);
            }

            //调用生命周期管理器创建时间方法
            //type.LifeTimeManager.CreateInstance(type, parameters.ToArray());

            //这里使用扩展类调用生命周期管理器创建时间方法
            return type.CreateInstance(parameters.ToArray());
        }
    }
}
