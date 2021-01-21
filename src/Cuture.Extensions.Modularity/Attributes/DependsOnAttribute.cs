using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 依赖模块描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedModuleTypesProvider
    {
        #region Protected 字段

        /// <summary>
        /// 依赖的模块类型
        /// </summary>
        protected readonly Type[] DependedModuleTypes;

        #endregion Protected 字段

        #region Public 构造函数

        /// <summary>
        /// 依赖模块描述
        /// </summary>
        /// <param name="dependedModuleTypes">依赖的模块类型</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes ?? Array.Empty<Type>();
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public Type[] GetDependedModuleTypes() => DependedModuleTypes?.Clone() as Type[] ?? Array.Empty<Type>();

        #endregion Public 方法
    }
}