using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <inheritdoc cref="IModuleLoader"/>
    public class ModuleLoader : IModuleLoader
    {
        #region Private 字段

        private readonly ModuleLoadOptions _options;

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        /// 模块依赖的根
        /// </summary>
        public IReadOnlyList<IModuleDescriptor> ModuleDependencyRoots { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public IServiceCollection Services { get; protected set; }

        /// <summary>
        /// 已排序的模块描述
        /// </summary>
        public IReadOnlyList<IModuleDescriptor> SortedModuleDescriptors { get; protected set; }

        /// <summary>
        /// 已排序的模块类型列表
        /// </summary>
        public IReadOnlyList<Type> SortedModuleTypes { get; protected set; }

        #endregion Public 属性

        #region Public 构造函数

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// <inheritdoc cref="ModuleLoader"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleTypes"></param>
        /// <param name="descriptorBuilder"></param>
        /// <param name="options"></param>
        public ModuleLoader(IServiceCollection services, IEnumerable<Type> moduleTypes, IModuleDescriptorBuilder descriptorBuilder, ModuleLoadOptions options)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (moduleTypes.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(moduleTypes));
            }

            OrganizationModule(moduleTypes, descriptorBuilder);
        }

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public virtual IModulesBootstrapper BuildBootstrapper()
        {
            var moduleInstances = SortedModuleTypes.Select(m => m.ConstructInstance())
                                                   .ToArray();

            if (_options.AddModuleAsService)
            {
                foreach (var module in moduleInstances)
                {
                    Services.Add(ServiceDescriptor.Singleton(module.GetType(), module));
                }
            }

            var modulesBootstrapInterceptor = new AggregatedModulesBootstrapInterceptor(_options.ModulesBootstrapInterceptors);

            return new ModulesBootstrapper(moduleInstances, modulesBootstrapInterceptor, _options.BootstrapOptions);
        }

        #endregion Public 方法

        #region Protected 方法

        /// <summary>
        /// 组织模块
        /// </summary>
        /// <param name="moduleTypes"></param>
        /// <param name="descriptorBuilder"></param>
        protected virtual void OrganizationModule(IEnumerable<Type> moduleTypes, IModuleDescriptorBuilder descriptorBuilder)
        {
            try
            {
                var allModuleDescriptors = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(moduleTypes, descriptorBuilder);

                SortedModuleDescriptors = SortModuleDescriptors(allModuleDescriptors).ToList().AsReadOnly();

                SortedModuleTypes = SortedModuleDescriptors.Select(m => m.Type).ToList().AsReadOnly();
                ModuleDependencyRoots = SortedModuleDescriptors.Where(m => m.Dependents.IsNullOrEmpty()).ToList().AsReadOnly();
            }
            catch (ModularityException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ModularityException("SortModuleDescriptors Fail", ex);
            }
        }

        /// <summary>
        /// 排序模块描述列表
        /// </summary>
        /// <returns></returns>
        protected virtual IModuleDescriptor[] SortModuleDescriptors(IEnumerable<IModuleDescriptor> moduleDescriptors)
        {
            return AppModuleDependencyUtil.SortModuleDescriptors(moduleDescriptors);
        }

        #endregion Protected 方法
    }
}