using Cuture.Extensions.Modularity;

using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class InitializationModulesServiceProviderExtensions
{
    #region InitializationModules

    #region Sync

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    public static IModulesBootstrapper InitializationModules(this IServiceProvider serviceProvider)
    {
        return serviceProvider.InitializationModulesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, object[])"/>
    public static IModulesBootstrapper InitializationModules(this IServiceProvider serviceProvider, params object[] items)
    {
        return serviceProvider.InitializationModulesAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    public static IModulesBootstrapper InitializationModules(this IServiceProvider serviceProvider, IEnumerable<KeyValuePair<string, object?>>? items = null)
    {
        return serviceProvider.InitializationModulesAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    public static IModulesBootstrapper InitializationModules(this IServiceProvider serviceProvider, IDictionary<string, object?>? items = null)
    {
        return serviceProvider.InitializationModulesAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    #endregion Sync

    #region Async

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    public static Task<IModulesBootstrapper> InitializationModulesAsync(this IServiceProvider serviceProvider)
    {
        return serviceProvider.InternalInitializationModulesAsync(() => serviceProvider.InitializationModulesWithOutHostLifetimeAsync());
    }

    /// <summary>
    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="items">初始化上下文中的传递项
    /// <para/>
    /// 排列需要为 string,object,string,object,string,object...
    /// <para/>
    /// 即 key,value,key,value,key,value...
    /// </param>
    /// <returns></returns>
    public static Task<IModulesBootstrapper> InitializationModulesAsync(this IServiceProvider serviceProvider, params object[] items)
    {
        return serviceProvider.InternalInitializationModulesAsync(() => serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items));
    }

    /// <inheritdoc cref="InitializationModulesAsync(IServiceProvider, IDictionary{string, object?}?)"/>
    public static Task<IModulesBootstrapper> InitializationModulesAsync(this IServiceProvider serviceProvider, IEnumerable<KeyValuePair<string, object?>>? items = null)
    {
        return serviceProvider.InternalInitializationModulesAsync(() => serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items));
    }

    /// <summary>
    /// 初始化模块
    /// <para/>
    /// 会在<see cref="IHost"/>关闭时 - <see cref="IHostApplicationLifetime.ApplicationStopping"/>取消时关闭模块
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="items">初始化上下文中的传递项</param>
    /// <returns></returns>
    public static Task<IModulesBootstrapper> InitializationModulesAsync(this IServiceProvider serviceProvider, IDictionary<string, object?>? items = null)
    {
        return serviceProvider.InternalInitializationModulesAsync(() => serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items));
    }

    #endregion Async

    private static async Task<IModulesBootstrapper> InternalInitializationModulesAsync(this IServiceProvider serviceProvider, Func<Task> initTaskFunc)
    {
        var bootstrapper = serviceProvider.GetService<IModulesBootstrapper>();

        if (bootstrapper is null)
        {
            throw new ModularityException($"must load modules first before initialization modules. please check module is loaded and {nameof(IModuleLoaderBuilderLoadModuleExtensions.ModuleLoadComplete)} has been invoked.");
        }

        var applicationLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            var shutdownContext = new ApplicationShutdownContext(serviceProvider);
            bootstrapper.ShutdownAsync(shutdownContext).ConfigureAwait(false).GetAwaiter().GetResult();
        });

        await initTaskFunc().ConfigureAwait(false);

        return bootstrapper;
    }

    #endregion InitializationModules
}
