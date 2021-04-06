using System;
using System.Collections.Generic;
using System.Reflection;

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
            return hostBuilder.AutoBindModuleOptions(optionAction => { });
        }

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Action{OptionsBindOptions}?)"/>
        public static IHostBuilder AutoBindModuleOptions(this IHostBuilder hostBuilder, Action<OptionsBindOptions>? optionAction = null)
        {
            var bindOptions = new OptionsBindOptions();
            optionAction?.Invoke(bindOptions);

            return hostBuilder.InternalAddBootstrapInterceptor(new OptionsBindModulesBootstrapInterceptor(new DefaultOptionsBinder(bindOptions)));
        }

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Func{Assembly, IEnumerable{Type}}?, Func{Type, string?}?)"/>
        public static IHostBuilder AutoBindModuleOptions(this IHostBuilder hostBuilder, Func<Assembly, IEnumerable<Type>>? findOptionsTypesFunc = null, Func<Type, string?>? sectionKeyGetFunc = null)
        {
            return hostBuilder.InternalAddBootstrapInterceptor(new OptionsBindModulesBootstrapInterceptor(new CustomKeyGetOptionsBinder(findOptionsTypesFunc, sectionKeyGetFunc)));
        }

        /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, IOptionsBinder)"/>
        public static IHostBuilder AutoBindModuleOptions(this IHostBuilder hostBuilder, IOptionsBinder optionsBinder)
        {
            return hostBuilder.InternalAddBootstrapInterceptor(new OptionsBindModulesBootstrapInterceptor(optionsBinder));
        }

        #endregion Public 方法

        #region Private 方法

        private static IHostBuilder InternalAddBootstrapInterceptor(this IHostBuilder hostBuilder, IModulesBootstrapInterceptor interceptor)
        {
            return hostBuilder.InternalOptionModuleLoadBuilder(options =>
            {
                options.ModulesBootstrapInterceptors.Add(interceptor);
            });
        }

        #endregion Private 方法
    }
}