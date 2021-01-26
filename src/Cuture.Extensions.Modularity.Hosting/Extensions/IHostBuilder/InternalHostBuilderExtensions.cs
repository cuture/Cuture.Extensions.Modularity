﻿using System;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    ///
    /// </summary>
    internal static class InternalHostBuilderExtensions
    {
        #region Public 字段

        public const string HostBuilderPropertiesKey = "Cuture.Extensions.Modularity.Hosting.ModuleSources";

        #endregion Public 字段

        #region Internal 方法

        internal static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder, Action<IServiceCollection> configureDelegate)
        {
            return hostBuilder.ConfigureServices((_, services) => configureDelegate(services));
        }

        internal static IHostBuilder InternalAddModuleSource(this IHostBuilder hostBuilder, IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
        {
            if (moduleSource is null)
            {
                throw new ArgumentNullException(nameof(moduleSource));
            }

            if (hostBuilder.Properties.TryGetValue(HostBuilderPropertiesKey, out var storedModuleSources))
            {
                if (storedModuleSources is HostBuilderModuleSourceCollection moduleSources)
                {
                    moduleSources.Add(moduleSource);
                }
                else
                {
                    throw new ModularityException($"The key '{HostBuilderPropertiesKey}' stored in {nameof(IHostBuilder)}.{nameof(IHostBuilder.Properties)} is invalid. Don't change it.");
                }
            }
            else
            {
                var moduleSources = new HostBuilderModuleSourceCollection();
                hostBuilder.Properties.Add(HostBuilderPropertiesKey, moduleSources);

                moduleSources.Add(moduleSource);
                hostBuilder.ConfigureServices(services =>
                {
                    foreach (var item in moduleSources)
                    {
                        services.LoadModule(item, optionAction);
                    }

                    services.ModuleLoadComplete();
                });
            }

            return hostBuilder;
        }

        #endregion Internal 方法
    }
}