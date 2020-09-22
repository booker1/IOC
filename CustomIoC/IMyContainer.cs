using System;
using System.Collections.Generic;
using System.Text;
using CustomIoC.LifeTimeManager;

namespace CustomIoC
{
    public interface IMyContainer
    {
        /// <summary>
        /// 注册单一类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        void RegisterType<TType>();

        /// <summary>
        /// 注册单一类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="name">别名</param>
        void RegisterType<TType>(string name);


        /// <summary>
        /// 注册单一类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="lifeTimeManager">生命周期管理</param>
        void RegisterType<TType>(ILifeTimeManager lifeTimeManager);


        /// <summary>
        /// 注册单一类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="name">别名</param>
        /// <param name="lifeTimeManager">生命周期管理</param>
        void RegisterType<TType>(string name, ILifeTimeManager lifeTimeManager);

        /// <summary>
        /// 映射关系
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        void RegisterType<TType, TImplementation>();

        /// <summary>
        /// 映射关系
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="name">别名</param>
        void RegisterType<TType, TImplementation>(string name);


        /// <summary>
        /// 映射关系
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="lifeTimeManager">生命周期管理</param>
        void RegisterType<TType, TImplementation>(ILifeTimeManager lifeTimeManager);

        /// <summary>
        /// 映射关系
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="name">别名</param>
        /// <param name="lifeTimeManager">生命周期管理</param>
        void RegisterType<TType, TImplementation>(string name, ILifeTimeManager lifeTimeManager);

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        TType Resolve<TType>();

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="name">别名</param>
        /// <returns></returns>
        TType Resolve<TType>(string name);
    }
}
