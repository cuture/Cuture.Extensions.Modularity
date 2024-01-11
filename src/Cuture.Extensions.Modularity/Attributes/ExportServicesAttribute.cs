using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 导出服务特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ExportServicesAttribute : Attribute, IExportServicesProvider
{
    #region Protected 属性

    /// <summary>
    /// 当前特性标记的类型
    /// </summary>
    protected Type? TargetType { get; set; }

    #endregion Protected 属性

    #region Public 属性

    /// <summary>
    /// <inheritdoc cref="Modularity.AddDIMode"/>
    /// <para/>
    /// 默认为 <see cref="AddDIMode.Add"/>
    /// </summary>
    public AddDIMode AddDIMode { get; set; } = AddDIMode.Add;

    /// <summary>
    /// 导出类型发现器类型
    /// <para/>
    /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
    /// </summary>
    public Type? ExportTypeDiscovererType { get; }

    /// <summary>
    /// 导出的服务
    /// </summary>
    public Type[] ExportTypes { get; protected set; }

    /// <summary>
    /// 导出服务的生命周期
    /// </summary>
    public ServiceLifetime Lifetime { get; }

    #endregion Public 属性

    #region Public 构造函数

    /// <inheritdoc cref="ExportServicesAttribute(ServiceLifetime, AddDIMode, Type[])"/>
    public ExportServicesAttribute(ServiceLifetime lifetime, params Type[] exportTypes)
    {
        Lifetime = lifetime;
        ExportTypes = exportTypes;
    }

    /// <summary>
    /// <inheritdoc cref="ExportServicesAttribute"/>
    /// </summary>
    /// <param name="lifetime">导出服务的生命周期</param>
    /// <param name="addMode">添加到DI容器的方式</param>
    /// <param name="exportTypes"><inheritdoc cref="ExportServicesAttribute(byte)"/></param>
    public ExportServicesAttribute(ServiceLifetime lifetime, AddDIMode addMode, params Type[] exportTypes) : this(lifetime, exportTypes)
    {
        AddDIMode = addMode;
    }

    /// <summary>
    /// <inheritdoc cref="ExportServicesAttribute"/>
    /// </summary>
    /// <param name="lifetime">导出服务的生命周期</param>
    /// <param name="exportTypeDiscovererType">
    /// 导出类型发现器类型
    /// <para/>
    /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
    /// </param>
    /// <param name="addMode">添加到DI容器的方式</param>
    public ExportServicesAttribute(ServiceLifetime lifetime, Type exportTypeDiscovererType, AddDIMode addMode)
    {
        Lifetime = lifetime;
        ExportTypeDiscovererType = exportTypeDiscovererType ?? throw new ArgumentNullException(nameof(exportTypeDiscovererType));

        exportTypeDiscovererType.ThrowIfNotInherit<IExportTypeDiscoverer>(nameof(exportTypeDiscovererType));

        ExportTypes = Array.Empty<Type>();

        AddDIMode = addMode;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="exportTypes">
    /// 导出的服务类型<para/>
    /// 未明确指定时，将会尝试自动获取接口类型（排除IDisposable、IAsyncDisposable）<para/>
    /// 如果自动获取到的接口类型数量不等于 1 个，则会抛出异常<para/>
    /// 自动获取方法参见 <see cref="CutureExtensionsModularityTypeReflectionExtensions.GetMostLikelyDirectInterfaces"/><para/>
    /// ------------------<para/>
    /// Note!!! 使用自动导出时，务必测试是否符合预期。<para/>
    /// </param>
    /// <exception cref="NotImplementedException"></exception>
    protected ExportServicesAttribute(byte exportTypes)
    {
        //此构造函数仅用于引用文档
        throw new NotImplementedException();
    }

    #endregion Public 构造函数

    #region Public 方法

    /// <inheritdoc/>
    public virtual IEnumerable<Type> GetExportServiceTypes(Type targetType)
    {
        if (targetType.IsAbstract && targetType.IsSealed)
        {
            throw new ArgumentException($"Can not export service from static class {targetType}");
        }

        TargetType = targetType;

        if (ExportTypeDiscovererType is null)
        {
            if (!ExportTypes.IsNullOrEmpty())
            {
                return ExportTypes;
            }
            var types = targetType.GetMostLikelyDirectInterfacesExcludeDefaults()?.ToArray();
            if (types is null
                || !types.Any())
            {
                if (targetType.GetInterfaces().IsNullOrEmpty())
                {
                    types = new[] { targetType };
                }
                else
                {
                    throw new ModularityException($"Can not get export service for type {targetType} automatically . Must use {nameof(ExportServicesAttribute)} define it clearly .");
                }
            }
            else if (types.Length > 1)
            {
                throw new ModularityException($"More than one interface was got by automatically get the export service interface for type {targetType} . Must use {nameof(ExportServicesAttribute)} define it clearly .");
            }
            return types.Take(1);
        }
        else
        {
            var exportTypeDiscoverer = ExportTypeDiscovererType.ConstructInstance<IExportTypeDiscoverer>();

            if (ExportTypes.IsNullOrEmpty())
            {
                return exportTypeDiscoverer.DiscoveryExportTypes(targetType);
            }
            else
            {
                return ExportTypes.Concat(exportTypeDiscoverer.DiscoveryExportTypes(targetType));
            }
        }
    }

    #endregion Public 方法
}
