using Cuture.Extensions.Modularity;
using Cuture.Extensions.Modularity.Hosting;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    ///
    /// </summary>
    public static class AutoBindOptionsHostBuilderExtensions
    {
        #region Public 方法

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder)"/>
        public static IHostBuilder AutoBindModuleOptions(this IHostBuilder hostBuilder)
        {
            hostBuilder.InternalOptionModuleLoadBuilder(options =>
            {
                options.ModulesBootstrapInterceptors.Add(new OptionsAutoBindModulesBootstrapInterceptor());
            });
            return hostBuilder;
        }

        #endregion Public 方法
    }
}