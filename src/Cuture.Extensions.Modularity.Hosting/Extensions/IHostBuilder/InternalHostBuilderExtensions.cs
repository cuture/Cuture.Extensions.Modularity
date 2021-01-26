using System;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    ///
    /// </summary>
    internal static class InternalHostBuilderExtensions
    {
        #region Internal 方法

        internal static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder, Action<IServiceCollection> configureDelegate)
        {
            return hostBuilder.ConfigureServices((_, services) => configureDelegate(services));
        }

        internal static IHostBuilder InternalAddModuleSource(this IHostBuilder hostBuilder, IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
        {
            return hostBuilder.ConfigureServices(services => services.LoadModule(moduleSource, optionAction));
        }

        #endregion Internal 方法
    }
}