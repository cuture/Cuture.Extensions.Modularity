using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extensions for <see cref="IHost"/>
    /// </summary>
    public static class IHostExtensions
    {
        #region Initialization

        /// <summary>
        /// 初始化<see cref="IHost"/>中的模块
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IModulesBootstrapper InitializationModules(this IHost host)
        {
            return host.InitializationModulesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 初始化<see cref="IHost"/>中的模块
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task<IModulesBootstrapper> InitializationModulesAsync(this IHost host)
        {
            var serviceProvider = host.Services;

            var bootstrapper = await serviceProvider.InitializationModulesAsync().ConfigureAwait(false);

            return bootstrapper;
        }

        #endregion Initialization
    }
}