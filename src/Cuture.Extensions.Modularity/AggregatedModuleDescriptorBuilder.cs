using System;
using System.Collections.Generic;
using System.Linq;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 聚合的<inheritdoc cref="IModuleDescriptorBuilder"/>
    /// </summary>
    public class AggregatedModuleDescriptorBuilder : IModuleDescriptorBuilder
    {
        #region Private 字段

        private List<IModuleDescriptorBuilder> _descriptorBuilders;

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        ///
        /// </summary>
        public IReadOnlyList<IModuleDescriptorBuilder> DescriptorBuilders { get => _descriptorBuilders; protected set => _descriptorBuilders = value.ToList(); }

        #endregion Public 属性

        #region Public 构造函数

        /// <inheritdoc cref="AggregatedModuleDescriptorBuilder"/>
        public AggregatedModuleDescriptorBuilder()
        {
            _descriptorBuilders = new List<IModuleDescriptorBuilder>()
            {
                DefaultModuleDescriptorBuilder.Default
            };
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <summary>
        /// 添加一个构建器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public bool AppendBuilder(IModuleDescriptorBuilder? builder)
        {
            if (builder is null)
            {
                return false;
            }

            if (!_descriptorBuilders.Any(m => m.GetType().Equals(builder.GetType())))
            {
                _descriptorBuilders.Add(builder);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool CanCreate(Type type)
        {
            foreach (var builder in DescriptorBuilders)
            {
                if (builder.CanCreate(type))
                {
                    return true;
                }
            }
            return false;
        }

        /// <inheritdoc/>
        public IModuleDescriptor Create(Type moduleType)
        {
            foreach (var builder in DescriptorBuilders)
            {
                if (builder.CanCreate(moduleType))
                {
                    return builder.Create(moduleType);
                }
            }
            throw new ModularityException($"cannot build {nameof(IModuleDescriptor)} from type {moduleType.FullName}");
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetDependedModuleTypes(Type moduleType)
        {
            foreach (var builder in DescriptorBuilders)
            {
                if (builder.CanCreate(moduleType))
                {
                    return builder.GetDependedModuleTypes(moduleType).Distinct();
                }
            }
            throw new ModularityException($"cannot get depended module types from type {moduleType.FullName}");
        }

        #endregion Public 方法
    }
}