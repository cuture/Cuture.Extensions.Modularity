using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 服务注册器
/// </summary>
public interface IServiceRegistrar
{
    #region Public 方法

    /// <summary>
    /// 添加程序集
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    void AddAssembly(IServiceCollection services, Assembly assembly);

    /// <summary>
    /// 添加类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="type"></param>
    void AddType(IServiceCollection services, Type type);

    #endregion Public 方法
}
