using Cuture.Extensions.Modularity.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cuture.Extensions.Modularity.Hosting;

/// <summary>
///
/// </summary>
internal static class InternalHostBuilderExtensions
{
    #region Public 字段

    /// <summary>
    /// HostBuilder 是否延时执行 ConfigureServices 的 key
    /// </summary>
    public const string HostBuilderDelayConfigureServicesPropertiesKey = "Cuture.Extensions.Modularity.HostBuilder.DelayConfigureServices";

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

        //检查是否是延时执行 ConfigureServices 的HostBuilder，非延时执行的HostBuilder需要抛出异常，避免功能不正常
        var isDelayConfigureServices = true;
        if (!hostBuilder.Properties.TryGetValue(HostBuilderDelayConfigureServicesPropertiesKey, out var isDelayConfigureServicesObject)
            || isDelayConfigureServicesObject is null)
        {
            var localCheckValue = false;
            hostBuilder.ConfigureServices((_, _) => localCheckValue = true);
            isDelayConfigureServices = !localCheckValue;
            hostBuilder.Properties.Add(HostBuilderDelayConfigureServicesPropertiesKey, isDelayConfigureServices);
        }
        else
        {
            isDelayConfigureServices = (bool)isDelayConfigureServicesObject;
        }

        if (!isDelayConfigureServices)
        {
            throw new ModularityException("The hostBuilder invoke `ConfigureServices` every time at `LoadModule()`. Modular functions may not perform correctly. Please use `IServiceCollection.LoadModule*()` instead of `IHostBuilder.LoadModule*()`.");
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
                var hasConfiguration = services.GetConfiguration() is not null;
                var removeIConfigurationContainer = false;
                if (!hasConfiguration)
                {
                    removeIConfigurationContainer = services.SetConfiguration(context.Configuration);
                }

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
                    if (removeIConfigurationContainer)
                    {
                        services.RemoveObjectAccessor<IConfigurationContainer>();
                    }
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
