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
            var hostBuilderContext = services.GetSingletonServiceInstance<HostBuilderContext>();

            return hostBuilderContext?.Configuration as IConfigurationRoot
                   ?? services.GetSingletonServiceInstance<IConfiguration>()
                   ?? (services.TryGetObjectAccessorValue<IConfiguration>(out var configuration) ? configuration : null);
        }

        #endregion Public 方法
    }
}