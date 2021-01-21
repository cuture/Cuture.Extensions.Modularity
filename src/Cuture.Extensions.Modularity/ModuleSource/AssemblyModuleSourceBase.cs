using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 基于程序集的<inheritdoc cref="IModuleSource"/>基类
    /// </summary>
    public abstract class AssemblyModuleSourceBase : TypeModuleSourceBase
    {
        #region Public 属性

        /// <summary>
        /// 程序集过滤
        /// </summary>
        public Func<Assembly, bool>? AssemblyFilter { get; set; }

        #endregion Public 属性

        #region Protected 方法

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<Assembly> InternalGetAssemblies();

        /// <inheritdoc/>
        protected override IEnumerable<Type> InternalGetTypes()
        {
            var assemblies = AssemblyFilter != null
                                ? InternalGetAssemblies().Where(AssemblyFilter)
                                : InternalGetAssemblies();

            List<Type> types = new();

            foreach (var assembly in assemblies)
            {
                try
                {
                    types.AddRange(assembly.GetTypes());
                }
                catch (Exception ex)
                {
                    throw new ModuleLoadException(assembly, ex);
                }
            }

            return types;
        }

        #endregion Protected 方法
    }
}