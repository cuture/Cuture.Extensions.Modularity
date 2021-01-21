using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 自动注册模块所在程序集中导出的服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutoRegisterServicesInAssemblyAttribute : Attribute
    {
        #region Public 属性

        /// <summary>
        /// 服务注册器类型
        /// <para/>
        /// 需要实现 <see cref="IServiceRegistrar"/> 接口
        /// </summary>
        public virtual Type? ServiceRegistrarType { get; protected set; }

        #endregion Public 属性

        #region Public 构造函数

        /// <inheritdoc cref="AutoRegisterServicesInAssemblyAttribute"/>
        public AutoRegisterServicesInAssemblyAttribute()
        {
        }

        /// <summary>
        /// <inheritdoc cref="AutoRegisterServicesInAssemblyAttribute"/>
        /// </summary>
        public AutoRegisterServicesInAssemblyAttribute(Type type)
        {
            ServiceRegistrarType = type ?? throw new ArgumentNullException(nameof(type));

            type.ThrowIfNotInherit<IServiceRegistrar>();
        }

        #endregion Public 构造函数
    }
}