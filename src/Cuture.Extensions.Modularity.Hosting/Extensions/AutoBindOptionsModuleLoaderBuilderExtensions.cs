using System.Reflection;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class AutoBindOptionsModuleLoaderBuilderExtensions
{
    #region Public 方法

    /// <inheritdoc cref="AutoBindModuleOptions(IModuleLoaderBuilder, Action{OptionsBindOptions}?)"/>
    public static IModuleLoaderBuilder AutoBindModuleOptions(this IModuleLoaderBuilder moduleLoaderBuilder)
    {
        return moduleLoaderBuilder.AutoBindModuleOptions(options => { });
    }

    /// <summary>
    /// 自动绑定标记了<see cref="AutoRegisterServicesInAssemblyAttribute"/>的模块中继承了<see cref="IOptions{TOptions}"/>的类。
    /// <para/>
    /// 默认情况下使用其完整名称为路径，在<see cref="IConfiguration"/>查找节点，并绑定值。
    /// <para/>
    /// 如 A 类命名空间为 B.C.D.E.F ，则<see cref="IConfiguration"/>查找路径为 B:C:D:E:F:A
    /// <para/>
    /// Note: 构建过程中必须有可访问的<see cref="IConfiguration"/> !!!
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="optionAction">配置选项的委托</param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AutoBindModuleOptions(this IModuleLoaderBuilder moduleLoaderBuilder, Action<OptionsBindOptions>? optionAction = null)
    {
        var options = new OptionsBindOptions();
        optionAction?.Invoke(options);

        moduleLoaderBuilder.ModuleLoadOptions.ModulesBootstrapInterceptors.Add(new OptionsBindModulesBootstrapInterceptor(new DefaultOptionsBinder(options)));

        return moduleLoaderBuilder;
    }

    /// <summary>
    /// 自动绑定模块程序集中的配置类
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="findOptionsTypesFunc">从程序集中获取需要绑定的类型的委托</param>
    /// <param name="sectionKeyGetFunc">获取配置类型对应的<see cref="IConfiguration"/>Key的委托</param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AutoBindModuleOptions(this IModuleLoaderBuilder moduleLoaderBuilder, Func<Assembly, IEnumerable<Type>>? findOptionsTypesFunc = null, Func<Type, string?>? sectionKeyGetFunc = null)
    {
        moduleLoaderBuilder.ModuleLoadOptions.ModulesBootstrapInterceptors.Add(new OptionsBindModulesBootstrapInterceptor(new CustomKeyGetOptionsBinder(findOptionsTypesFunc, sectionKeyGetFunc)));

        return moduleLoaderBuilder;
    }

    /// <summary>
    /// 使用指定的 <see cref="IOptionsBinder"/> 绑定模块程序集中的配置类
    /// </summary>
    /// <param name="moduleLoaderBuilder"></param>
    /// <param name="optionsBinder">选项绑定器</param>
    /// <returns></returns>
    public static IModuleLoaderBuilder AutoBindModuleOptions(this IModuleLoaderBuilder moduleLoaderBuilder, IOptionsBinder optionsBinder)
    {
        moduleLoaderBuilder.ModuleLoadOptions.ModulesBootstrapInterceptors.Add(new OptionsBindModulesBootstrapInterceptor(optionsBinder));

        return moduleLoaderBuilder;
    }

    #endregion Public 方法
}
