using System.Reflection;

using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class AutoBindOptionsServiceCollectionExtensions
{
    #region Public 方法

    /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder)"/>
    public static IServiceCollection AutoBindModuleOptions(this IServiceCollection services)
    {
        var moduleLoaderBuilder = services.GetRequiredSingletonServiceInstance<IModuleLoaderBuilder>();

        moduleLoaderBuilder.AutoBindModuleOptions();

        return services;
    }

    /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Action{OptionsBindOptions}?)"/>
    public static IServiceCollection AutoBindModuleOptions(this IServiceCollection services, Action<OptionsBindOptions>? optionAction = null)
    {
        var moduleLoaderBuilder = services.GetRequiredSingletonServiceInstance<IModuleLoaderBuilder>();

        moduleLoaderBuilder.AutoBindModuleOptions(optionAction);

        return services;
    }

    /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, Func{Assembly, IEnumerable{Type}}?, Func{Type, string?}?)"/>
    public static IServiceCollection AutoBindModuleOptions(this IServiceCollection services, Func<Assembly, IEnumerable<Type>>? findOptionsTypesFunc = null, Func<Type, string?>? sectionKeyGetFunc = null)
    {
        var moduleLoaderBuilder = services.GetRequiredSingletonServiceInstance<IModuleLoaderBuilder>();

        moduleLoaderBuilder.AutoBindModuleOptions(findOptionsTypesFunc, sectionKeyGetFunc);

        return services;
    }

    /// <inheritdoc cref="AutoBindOptionsModuleLoaderBuilderExtensions.AutoBindModuleOptions(IModuleLoaderBuilder, IOptionsBinder)"/>
    public static IServiceCollection AutoBindModuleOptions(this IServiceCollection services, IOptionsBinder optionsBinder)
    {
        var moduleLoaderBuilder = services.GetRequiredSingletonServiceInstance<IModuleLoaderBuilder>();

        moduleLoaderBuilder.AutoBindModuleOptions(optionsBinder);

        return services;
    }

    #endregion Public 方法
}
