using System;

using Cuture.Extensions.Modularity.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cuture.Extensions.Modularity;

/// <summary>
///
/// </summary>
public static class HostBuilderServiceCollectionExtensions
{
    #region Public 方法

    /// <summary>
    /// 从<see cref="IServiceCollection"/>中获取<see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IHostBuilder? GetHostBuilder(this IServiceCollection services) => services.GetObjectAccessorValue<IHostBuilderContainer>()?.Value;

    /// <inheritdoc cref="GetHostBuilder(IServiceCollection)"/>
    public static IHostBuilder GetRequiredHostBuilder(this IServiceCollection services) => services.GetHostBuilder()
                                                                                           ?? throw new InvalidOperationException($"Not found {nameof(IHostBuilder)} in serviceCollection.");

    /// <summary>
    /// 将用于<see cref="GetHostBuilder(IServiceCollection)"/>获取的<see cref="IHostBuilder"/>添加到<paramref name="services"/>中
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hostBuilder"></param>
    /// <returns>是否为新添加</returns>
    public static bool SetHostBuilder(this IServiceCollection services, IHostBuilder hostBuilder)
    {
        if (services.TryGetObjectAccessor<IHostBuilderContainer>(out var container)
            && container is not null)
        {
            if (container.Value is null)
            {
                container.Value = new(hostBuilder);
            }
            else
            {
                container.Value.Value = hostBuilder;
            }
            return false;
        }
        else
        {
            services.AddObjectAccessor<IHostBuilderContainer>(new(hostBuilder));
            return true;
        }
    }

    #endregion Public 方法
}