using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 以 <see cref="ServiceLifetime.Transient"/> 为生命周期导出服务特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ExportTransientServicesAttribute : ExportServicesAttribute
{
    #region Public 构造函数

    /// <inheritdoc cref="ExportTransientServicesAttribute(AddDIMode, Type[])"/>
    public ExportTransientServicesAttribute(params Type[] exportTypes) : base(ServiceLifetime.Transient, exportTypes)
    {
    }

    /// <summary>
    /// <inheritdoc cref="ExportTransientServicesAttribute"/>
    /// </summary>
    /// <param name="addMode">添加到DI容器的方式</param>
    /// <param name="exportTypes"><inheritdoc cref="ExportServicesAttribute(byte)"/></param>
    public ExportTransientServicesAttribute(AddDIMode addMode, params Type[] exportTypes) : base(ServiceLifetime.Transient, addMode, exportTypes)
    {
    }

    /// <summary>
    /// <inheritdoc cref="ExportTransientServicesAttribute"/>
    /// </summary>
    /// <param name="exportTypeDiscovererType">
    /// 导出类型发现器类型
    /// <para/>
    /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
    /// </param>
    /// <param name="addMode">添加到DI容器的方式</param>
    public ExportTransientServicesAttribute(Type exportTypeDiscovererType, AddDIMode addMode) : base(ServiceLifetime.Transient, exportTypeDiscovererType, addMode)
    {
    }

    #endregion Public 构造函数
}
