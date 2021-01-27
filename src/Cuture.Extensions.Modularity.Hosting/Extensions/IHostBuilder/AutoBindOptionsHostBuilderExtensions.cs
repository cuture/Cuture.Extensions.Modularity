using System;

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

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Action{OptionsAutoBindOptions}?)"/>
        public static IHostBuilder AutoBindModuleOptions(this IHostBuilder hostBuilder, Action<OptionsAutoBindOptions>? optionAction = null)
        {
            var autoBindOptions = new OptionsAutoBindOptions();
            optionAction?.Invoke(autoBindOptions);

            hostBuilder.InternalOptionModuleLoadBuilder(options =>
            {
                options.ModulesBootstrapInterceptors.Add(new OptionsAutoBindModulesBootstrapInterceptor(autoBindOptions));
            });
            return hostBuilder;
        }

        #endregion Public 方法
    }
}