using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块源
    /// </summary>
    public interface IModuleSource
    {
        #region Public 属性

        /// <inheritdoc cref="IModuleDescriptorBuilder"/>
        IModuleDescriptorBuilder? DescriptorBuilder { get; }

        #endregion Public 属性

        #region Public 方法

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetModules();

        #endregion Public 方法
    }
}