using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 基于类型的 <inheritdoc cref="IModuleSource"/>
    /// </summary>
    public class TypeModuleSource : TypeModuleSourceBase
    {
        #region Public 属性

        /// <summary>
        /// 原始模块类型列表
        /// </summary>
        public IReadOnlyList<Type> OriginModuleTypes { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="TypeModuleSource"/>
        /// </summary>
        /// <param name="moduleTypes"></param>
        public TypeModuleSource(params Type[] moduleTypes)
        {
            OriginModuleTypes = moduleTypes ?? Array.Empty<Type>();
        }

        #endregion Public 构造函数

        #region Protected 方法

        /// <inheritdoc/>
        protected override IEnumerable<Type> InternalGetTypes() => OriginModuleTypes;

        #endregion Protected 方法
    }
}