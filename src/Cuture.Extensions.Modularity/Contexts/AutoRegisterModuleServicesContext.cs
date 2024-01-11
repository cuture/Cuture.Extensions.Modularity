using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 模块服务自动注册上下文
/// </summary>
public class AutoRegisterModuleServicesContext
{
    #region Public 属性

    /// <summary>
    /// 已处理的程序集
    /// </summary>
    public HashSet<Assembly> ProcessedAssemblies { get; } = [];

    /// <summary>
    ///
    /// </summary>
    public IServiceCollection Services { get; set; }

    #endregion Public 属性

    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="AutoRegisterModuleServicesContext"/>
    /// </summary>
    /// <param name="services"></param>
    public AutoRegisterModuleServicesContext(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    #endregion Public 构造函数
}
