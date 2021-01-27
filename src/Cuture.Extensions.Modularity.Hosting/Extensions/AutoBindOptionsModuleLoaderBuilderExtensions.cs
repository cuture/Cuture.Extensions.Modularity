using Cuture.Extensions.Modularity;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class AutoBindOptionsModuleLoaderBuilderExtensions
    {
        #region Public 方法

        /// <summary>
        /// 自动查找标记了<see cref="AutoRegisterServicesInAssemblyAttribute"/>的模块中继承了<see cref="IOptions{TOptions}"/>的类。
        /// <para/>
        /// 使用其完整名称为路径，在<see cref="IConfiguration"/>查找节点，并绑定值。
        /// <para/>
        /// 如 A 类命名空间为 B.C.D.E.F ，则<see cref="IConfiguration"/>查找路径为 B:C:D:E:F:A
        /// <para/>
        /// Note: 构建过程中必须有可访问的<see cref="IConfiguration"/> !!!
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IModuleLoaderBuilder AutoBindModuleOptions(this IModuleLoaderBuilder moduleLoaderBuilder)
        {
            moduleLoaderBuilder.ModuleLoadOptions.ModulesBootstrapInterceptors.Add(new OptionsAutoBindModulesBootstrapInterceptor());

            return moduleLoaderBuilder;
        }

        #endregion Public 方法
    }
}