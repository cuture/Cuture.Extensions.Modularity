namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public interface IModuleLoader
    {
        #region Public 方法

        /// <summary>
        /// 构建<see cref="IModuleDescriptor"/>
        /// </summary>
        /// <returns></returns>
        IModulesBootstrapper BuildBootstrapper();

        #endregion Public 方法
    }
}