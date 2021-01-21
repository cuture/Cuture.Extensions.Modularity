using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <inheritdoc cref="IModuleLoaderBuilder"/>
    public class ModuleLoaderBuilder : IModuleLoaderBuilder
    {
        #region Private 字段

        private readonly List<IModuleSource> _moduleSources = new List<IModuleSource>();
        private readonly ModuleLoadOptions _options;

        #endregion Private 字段

        #region Public 属性

        /// <inheritdoc/>
        public IReadOnlyList<IModuleSource> ModuleSources => _moduleSources.AsReadOnly();

        /// <inheritdoc/>
        public IServiceCollection Services { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ModuleLoaderBuilder"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public ModuleLoaderBuilder(IServiceCollection services, ModuleLoadOptions options)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public void AddModuleSource(IModuleSource moduleSource)
        {
            _moduleSources.Add(moduleSource);
        }

        /// <inheritdoc/>
        public IModuleLoader Build()
        {
            var moduleTypes = _moduleSources.SelectMany(m => m.GetModules()).ToArray();

            var descriptorBuilder = new AggregatedModuleDescriptorBuilder();

            foreach (var source in _moduleSources)
            {
                descriptorBuilder.AppendBuilder(source.DescriptorBuilder);
            }

            return new ModuleLoader(Services, moduleTypes, descriptorBuilder, _options);
        }

        #endregion Public 方法
    }
}