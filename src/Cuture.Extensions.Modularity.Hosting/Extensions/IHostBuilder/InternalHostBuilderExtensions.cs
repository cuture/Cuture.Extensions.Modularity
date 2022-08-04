using System;

using Cuture.Extensions.Modularity.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cuture.Extensions.Modularity.Hosting
{
    /// <summary>
    ///
    /// </summary>
    internal static class InternalHostBuilderExtensions
    {
        #region Public 字段

        public const string HostBuilderPropertiesKey = "Cuture.Extensions.Modularity.Hosting.ModuleLoadContext";

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

            if (hostBuilder.Properties.TryGetValue(HostBuilderPropertiesKey, out var storedLoadContext))
            {
                if (storedLoadContext is HostBuilderModuleLoadContext loadContext)
                {
                    loadContext.Add(moduleSource, optionAction);
                }
                else
                {
                    throw new ModularityException($"The key '{HostBuilderPropertiesKey}' stored in {nameof(IHostBuilder)}.{nameof(IHostBuilder.Properties)} is invalid. Don't change it.");
                }
            }
            else
            {
                var loadContext = new HostBuilderModuleLoadContext();
                hostBuilder.Properties.Add(HostBuilderPropertiesKey, loadContext);
                loadContext.Add(moduleSource, optionAction);

                hostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddObjectAccessor<IHostBuilderContainer>(new(hostBuilder));
                    services.AddObjectAccessor<IConfigurationContainer>(new(context.Configuration));

                    try
                    {
                        foreach (var item in loadContext.ModuleSources)
                        {
                            services.LoadModule(item.Key, item.Value);
                        }

                        foreach (var item in loadContext.OptionActions)
                        {
                            services.OptionModuleLoadBuilder(item);
                        }

                        services.ModuleLoadComplete();
                    }
                    finally
                    {
                        services.RemoveObjectAccessor<IConfigurationContainer>();
                        services.RemoveObjectAccessor<IHostBuilderContainer>();
                    }
                });
            }

            return hostBuilder;
        }

        internal static IHostBuilder InternalOptionModuleLoadBuilder(this IHostBuilder hostBuilder, Action<ModuleLoadOptions> optionAction)
        {
            if (optionAction is null)
            {
                throw new ArgumentNullException(nameof(optionAction));
            }

            if (hostBuilder.Properties.TryGetValue(HostBuilderPropertiesKey, out var storedLoadContext))
            {
                if (storedLoadContext is HostBuilderModuleLoadContext loadContext)
                {
                    loadContext.Option(optionAction);
                }
                else
                {
                    throw new ModularityException($"The key '{HostBuilderPropertiesKey}' stored in {nameof(IHostBuilder)}.{nameof(IHostBuilder.Properties)} is invalid. Don't change it.");
                }
            }
            return hostBuilder;
        }

        #endregion Internal 方法
    }
}