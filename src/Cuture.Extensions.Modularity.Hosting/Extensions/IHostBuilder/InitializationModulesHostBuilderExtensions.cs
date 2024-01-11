namespace Microsoft.Extensions.Hosting;

/// <summary>
///
/// </summary>
public static class InitializationModulesHostBuilderExtensions
{
    #region Public 方法

    /// <summary>
    /// 构建Host，并初始化模块
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <returns></returns>
    public static IHost InitializationModules(this IHostBuilder hostBuilder)
    {
        return hostBuilder.InitializationModulesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <inheritdoc cref="InitializationModules"/>
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <returns></returns>
    public static async Task<IHost> InitializationModulesAsync(this IHostBuilder hostBuilder)
    {
        var host = hostBuilder.Build();

        await host.InitializationModulesAsync().ConfigureAwait(false);

        return host;
    }

    #endregion Public 方法
}
