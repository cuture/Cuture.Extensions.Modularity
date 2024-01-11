using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 以 <see cref="ServiceLifetime.Singleton"/> 为生命周期导出服务特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ExportSingletonServicesAttribute : ExportServicesAttribute
{
    #region Public 构造函数

    /// <inheritdoc cref="ExportSingletonServicesAttribute(AddDIMode, Type[])"/>
    public ExportSingletonServicesAttribute(params Type[] exportTypes) : base(ServiceLifetime.Singleton, exportTypes)
    {
    }

    /// <summary>
    /// <inheritdoc cref="ExportSingletonServicesAttribute"/>
    /// </summary>
    /// <param name="addMode">添加到DI容器的方式</param>
    /// <param name="exportTypes"><inheritdoc cref="ExportServicesAttribute(byte)"/></param>
    public ExportSingletonServicesAttribute(AddDIMode addMode, params Type[] exportTypes) : base(ServiceLifetime.Singleton, addMode, exportTypes)
    {
    }

    /// <summary>
    /// <inheritdoc cref="ExportSingletonServicesAttribute"/>
    /// </summary>
    /// <param name="exportTypeDiscovererType">
    /// 导出类型发现器类型
    /// <para/>
    /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
    /// </param>
    /// <param name="addMode">添加到DI容器的方式</param>
    public ExportSingletonServicesAttribute(Type exportTypeDiscovererType, AddDIMode addMode) : base(ServiceLifetime.Singleton, exportTypeDiscovererType, addMode)
    {
    }

    #endregion Public 构造函数
}
