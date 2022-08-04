using System;

using Cuture.Extensions.Modularity.Internal;

using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
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

        #endregion Public 方法
    }
}