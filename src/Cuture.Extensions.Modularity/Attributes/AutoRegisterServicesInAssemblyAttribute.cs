namespace Cuture.Extensions.Modularity;

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
    [Obsolete("1.1.6之后的版本默认会进行注册，不需要再手动指定此特性，除非需要自定义 ServiceRegistrar")]
    public AutoRegisterServicesInAssemblyAttribute()
    {
    }

    /// <summary>
    /// <inheritdoc cref="AutoRegisterServicesInAssemblyAttribute"/>
    /// </summary>
    public AutoRegisterServicesInAssemblyAttribute(Type serviceRegistrarType)
    {
        ServiceRegistrarType = serviceRegistrarType ?? throw new ArgumentNullException(nameof(serviceRegistrarType));

        serviceRegistrarType.ThrowIfNotInherit<IServiceRegistrar>();
    }

    #endregion Public 构造函数
}
