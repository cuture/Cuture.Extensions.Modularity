using System;

using Cuture.Extensions.Modularity.Internal;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ConfigurationServiceCollectionExtensions
    {
        #region Public 方法

        /// <summary>
        /// 从<see cref="IServiceCollection"/>中获取<see cref="IConfiguration"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IConfiguration? GetConfiguration(this IServiceCollection services)
        {
            if (services.TryGetObjectAccessorValue<IConfigurationContainer>(out var iConfigurationContainer)
                && iConfigurationContainer is not null)
            {
                return iConfigurationContainer.Value;
            }
            var hostBuilderContext = services.GetSingletonServiceInstance<HostBuilderContext>();

            return hostBuilderContext?.Configuration as IConfigurationRoot
                   ?? services.GetSingletonServiceInstance<IConfiguration>()
                   ?? (services.TryGetObjectAccessorValue<IConfiguration>(out var configuration) ? configuration : null);
        }

        /// <inheritdoc cref="GetConfiguration(IServiceCollection)"/>
        public static IConfiguration GetRequiredConfiguration(this IServiceCollection services) => services.GetConfiguration()
                                                                                                   ?? throw new InvalidOperationException($"Not found {nameof(IConfiguration)} in serviceCollection.");

        /// <summary>
        /// 将用于<see cref="GetConfiguration(IServiceCollection)"/>获取的<see cref="IConfiguration"/>添加到<paramref name="services"/>中
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>是否为新添加</returns>
        public static bool SetConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services.TryGetObjectAccessor<IConfigurationContainer>(out var container)
                && container is not null)
            {
                if (container.Value is null)
                {
                    container.Value = new(configuration);
                }
                else
                {
                    container.Value.Value = configuration;
                }
                return false;
            }
            else
            {
                services.AddObjectAccessor<IConfigurationContainer>(new(configuration));
                return true;
            }
        }

        #endregion Public 方法
    }
}