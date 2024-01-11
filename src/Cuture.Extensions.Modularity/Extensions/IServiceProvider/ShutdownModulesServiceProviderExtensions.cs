using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ShutdownModulesServiceProviderExtensions
{
    #region ShutdownModules

    /// <summary>
    /// 停止应用模块
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IServiceProvider ShutdownModules(this IServiceProvider serviceProvider)
    {
        return serviceProvider.ShutdownModulesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 停止应用模块
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static async Task<IServiceProvider> ShutdownModulesAsync(this IServiceProvider serviceProvider)
    {
        var bootstrapper = serviceProvider.GetRequiredService<IModulesBootstrapper>();

        var shutdownContext = new ApplicationShutdownContext(serviceProvider);
        await bootstrapper.ShutdownAsync(shutdownContext).ConfigureAwait(false);

        return serviceProvider;
    }

    #endregion ShutdownModules
}
