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

        #endregion Public 方法
    }
}