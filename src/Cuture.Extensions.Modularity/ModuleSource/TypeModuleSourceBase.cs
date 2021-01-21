using System;
using System.Collections.Generic;
using System.Linq;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 基于类型的<inheritdoc cref="IModuleSource"/>基类
    /// </summary>
    public abstract class TypeModuleSourceBase : IModuleSource
    {
        #region Public 属性

        /// <inheritdoc/>
        public IModuleDescriptorBuilder? DescriptorBuilder { get; set; }

        /// <summary>
        /// 类型过滤
        /// </summary>
        public Func<Type, bool>? TypeFilter { get; set; }

        #endregion Public 属性

        #region Public 方法

        /// <inheritdoc/>
        public virtual IEnumerable<Type> GetModules()
        {
            var types = TypeFilter != null
                         ? InternalGetTypes().Where(TypeFilter)
                         : InternalGetTypes();

            return InternalGetModules(types);
        }

        #endregion Public 方法

        #region Protected 方法

        /// <summary>
        /// 从类型中获取模块类型
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        protected IEnumerable<Type> InternalGetModules(IEnumerable<Type> types)
        {
            var descriptorBuilder = DescriptorBuilder ?? DefaultModuleDescriptorBuilder.Default;

            List<Type> modules = new();

            foreach (var type in types)
            {
                if (descriptorBuilder.CanCreate(type)
                    && !modules.Contains(type))
                {
                    modules.Add(type);
                }
            }

            return modules;
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<Type> InternalGetTypes();

        #endregion Protected 方法
    }
}