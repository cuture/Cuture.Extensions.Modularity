using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
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

        #endregion Public 方法
    }
}