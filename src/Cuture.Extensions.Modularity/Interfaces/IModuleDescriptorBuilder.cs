using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块描述构建器
    /// </summary>
    public interface IModuleDescriptorBuilder
    {
        #region Public 方法

        /// <summary>
        /// 检查指定类型是否可以创建模块描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanCreate(Type type);

        /// <summary>
        /// 为指定模块类型创建模块描述
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        IModuleDescriptor Create(Type moduleType);

        /// <summary>
        /// 获取指定模块依赖的模块类型
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        IEnumerable<Type> GetDependedModuleTypes(Type moduleType);

        #endregion Public 方法
    }
}