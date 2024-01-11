using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 默认的 <inheritdoc cref="IServiceRegistrar"/>
/// </summary>
public class DefaultServiceRegistrar : IServiceRegistrar
{
    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="DefaultServiceRegistrar"/>
    /// </summary>
    public DefaultServiceRegistrar()
    {
    }

    #endregion Public 构造函数

    #region Public 方法

    /// <inheritdoc/>
    public virtual void AddAssembly(IServiceCollection services, Assembly assembly)
    {
        var needsExportTypes = GetNeedsExportTypes(assembly);

        foreach (var needsExportType in needsExportTypes)
        {
            AddType(services, needsExportType);
        }
    }

    /// <inheritdoc/>
    public virtual void AddType(IServiceCollection services, Type type)
    {
        var exportServicesProviders = GetExportServicesProviders(type);
        foreach (var exportServicesProvider in exportServicesProviders)
        {
            InternalAddType(services, type, exportServicesProvider);
        }
    }

    #endregion Public 方法

    #region Protected 方法

    /// <summary>
    /// 获取服务导出提供器
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual IEnumerable<IExportServicesProvider> GetExportServicesProviders(Type type)
    {
        return type.GetExportServicesProviders();
    }

    /// <summary>
    /// 获取类型的导出服务类型
    /// </summary>
    /// <param name="exportServicesProvider"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual IEnumerable<Type> GetExportServiceTypes(IExportServicesProvider exportServicesProvider, Type type) => ExportServicesUtil.GetServiceExportTypes(exportServicesProvider, type);

    /// <summary>
    /// 获取程序集需要导出的类型
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    protected virtual IEnumerable<Type> GetNeedsExportTypes(Assembly assembly) => assembly.GetTypes();

    /// <summary>
    /// 添加类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="type"></param>
    /// <param name="exportServicesProvider"></param>
    protected virtual void InternalAddType(IServiceCollection services, Type type, IExportServicesProvider exportServicesProvider)
    {
        var exportServiceTypes = GetExportServiceTypes(exportServicesProvider, type);

        foreach (var exportServiceType in exportServiceTypes)
        {
            var serviceDescriptor = ServiceDescriptor.Describe(exportServiceType, type, exportServicesProvider.Lifetime);

            switch (exportServicesProvider.AddDIMode)
            {
                case AddDIMode.TryAdd:
                    services.TryAdd(serviceDescriptor);
                    break;

                case AddDIMode.Replace:
                    services.Replace(serviceDescriptor);
                    break;

                case AddDIMode.Default:
                default:
                    services.Add(serviceDescriptor);
                    break;
            }
        }
    }

    #endregion Protected 方法
}
