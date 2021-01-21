using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 基于程序集的 <inheritdoc cref="IModuleSource"/>
    /// </summary>
    public class AssemblyModuleSource : AssemblyModuleSourceBase
    {
        #region Public 属性

        /// <summary>
        /// 原始程序集列表
        /// </summary>
        public IReadOnlyList<Assembly> OriginAssemblies { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="AssemblyModuleSource"/>
        /// </summary>
        /// <param name="assemblies"></param>
        public AssemblyModuleSource(params Assembly[] assemblies)
        {
            OriginAssemblies = assemblies ?? Array.Empty<Assembly>();
        }

        #endregion Public 构造函数

        #region Protected 方法

        /// <inheritdoc/>
        protected override IEnumerable<Assembly> InternalGetAssemblies() => OriginAssemblies;

        #endregion Protected 方法
    }
}