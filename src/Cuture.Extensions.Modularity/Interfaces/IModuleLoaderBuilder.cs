using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// ModuleLoader构建器
    /// </summary>
    public interface IModuleLoaderBuilder
    {
        #region Public 属性

        /// <inheritdoc cref="ModuleLoadOptions"/>
        public ModuleLoadOptions ModuleLoadOptions { get; }

        /// <summary>
        /// 模块源列表
        /// </summary>
        public IReadOnlyList<IModuleSource> ModuleSources { get; }

        /// <summary>
        ///
        /// </summary>
        public IServiceCollection Services { get; }

        #endregion Public 属性

        #region Public 方法

        /// <summary>
        /// 添加模块源
        /// </summary>
        /// <param name="moduleSource"></param>
        void AddModuleSource(IModuleSource moduleSource);

        /// <summary>
        /// 构建ModuleLoader
        /// </summary>
        /// <returns></returns>
        IModuleLoader Build();

        #endregion Public 方法
    }
}