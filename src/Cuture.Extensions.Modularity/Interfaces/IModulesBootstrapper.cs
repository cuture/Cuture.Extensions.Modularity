using System.Threading.Tasks;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块引导
    /// </summary>
    public interface IModulesBootstrapper
    {
        #region Public 方法

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ConfigureServicesAsync(ServiceConfigurationContext context);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task InitializationAsync(ApplicationInitializationContext context);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ShutdownAsync(ApplicationShutdownContext context);

        #endregion Public 方法
    }
}