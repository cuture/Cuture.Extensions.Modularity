using System;

using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class AutoBindOptionsServiceCollectionExtensions
    {
        #region Public 方法

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Action{OptionsAutoBindOptions}?)"/>
        public static IServiceCollection AutoBindModuleOptions(this IServiceCollection services, Action<OptionsAutoBindOptions>? optionAction = null)
        {
            var moduleLoaderBuilder = services.GetRequiredSingletonServiceInstance<IModuleLoaderBuilder>();

            moduleLoaderBuilder.AutoBindModuleOptions(optionAction);

            return services;
        }

        #endregion Public 方法
    }
}