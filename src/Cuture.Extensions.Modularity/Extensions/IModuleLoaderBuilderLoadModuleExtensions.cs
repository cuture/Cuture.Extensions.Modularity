using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IModuleLoaderBuilder"/>
/// </summary>
public static class IModuleLoaderBuilderLoadModuleExtensions
{
    #region LoadModule

    #region type

    /// <summary>
    /// 增加要加载的模块
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    /// <param name="moduleLoaderBuilder"></param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AddModule<TModule>(this IModuleLoaderBuilder moduleLoaderBuilder) where TModule : IAppModule
    {
        return moduleLoaderBuilder.AddModule(typeof(TModule));
    }

    /// <summary>
    /// 增加要加载的模块
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AddModule(this IModuleLoaderBuilder moduleLoaderBuilder, params Type[] types)
    {
        return moduleLoaderBuilder.InternalAddModule(new TypeModuleSource(types));
    }

    /// <summary>
    /// 增加要加载的模块源
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="moduleSource"></param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AddModule(this IModuleLoaderBuilder moduleLoaderBuilder, IModuleSource moduleSource)
    {
        return moduleLoaderBuilder.InternalAddModule(moduleSource);
    }

    #endregion type

    #region Directory

    /// <inheritdoc cref="AddModuleDirectory(IModuleLoaderBuilder, Action{DirectoryModuleSource}?, string[])"/>
    public static IModuleLoaderBuilder AddModuleDirectory(this IModuleLoaderBuilder moduleLoaderBuilder, params string[] directories)
    {
        return moduleLoaderBuilder.AddModuleDirectory(null, directories);
    }

    /// <summary>
    /// 增加要加载模块的目录
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="sourceOptionAction"></param>
    /// <param name="directories"></param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AddModuleDirectory(this IModuleLoaderBuilder moduleLoaderBuilder, Action<DirectoryModuleSource>? sourceOptionAction, params string[] directories)
    {
        var source = new DirectoryModuleSource(directories);

        sourceOptionAction?.Invoke(source);

        return moduleLoaderBuilder.InternalAddModule(source);
    }

    #endregion Directory

    #region File

    /// <inheritdoc cref="AddModuleFile(IModuleLoaderBuilder, Action{FileModuleSource}?, string[])"/>
    public static IModuleLoaderBuilder AddModuleFile(this IModuleLoaderBuilder moduleLoaderBuilder, params string[] files)
    {
        return moduleLoaderBuilder.AddModuleFile(null, files);
    }

    /// <summary>
    /// 增加要加载的模块文件
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="sourceOptionAction"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AddModuleFile(this IModuleLoaderBuilder moduleLoaderBuilder, Action<FileModuleSource>? sourceOptionAction, params string[] files)
    {
        var source = new FileModuleSource(files);

        sourceOptionAction?.Invoke(source);

        return moduleLoaderBuilder.InternalAddModule(source);
    }

    #endregion File

    #endregion LoadModule

    #region ModuleLoadComplete

    /// <inheritdoc cref="ModuleLoadCompleteAsync(IModuleLoaderBuilder)"/>
    public static IServiceCollection ModuleLoadComplete(this IModuleLoaderBuilder moduleLoaderBuilder)
    {
        return moduleLoaderBuilder.ModuleLoadCompleteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 模块加载完成（执行此方法后不能再进行模块追加）
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <returns></returns>
    public static async Task<IServiceCollection> ModuleLoadCompleteAsync(this IModuleLoaderBuilder moduleLoaderBuilder)
    {
        var services = moduleLoaderBuilder.Services;
        var moduleLoader = moduleLoaderBuilder.Build();

        var bootstrapper = moduleLoader.BuildBootstrapper();

        var context = new ServiceConfigurationContext(services);

        await bootstrapper.ConfigureServicesAsync(context).ConfigureAwait(false);

        services.AddSingleton<IModulesBootstrapper>(bootstrapper);

        services.Remove<IServiceRegistrar>();
        services.Remove<IModuleLoaderBuilder>();

        return services;
    }

    #endregion ModuleLoadComplete

    #region Private 方法

    private static IModuleLoaderBuilder InternalAddModule(this IModuleLoaderBuilder moduleLoaderBuilder, IModuleSource moduleSource)
    {
        moduleLoaderBuilder.AddModuleSource(moduleSource);

        return moduleLoaderBuilder;
    }

    #endregion Private 方法
}
