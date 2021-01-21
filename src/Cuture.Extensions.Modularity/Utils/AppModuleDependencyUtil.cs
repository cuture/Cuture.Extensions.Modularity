using System;
using System.Collections.Generic;
using System.Linq;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块依赖工具
    /// </summary>
    public static class AppModuleDependencyUtil
    {
        #region Public 方法

        /// <summary>
        /// 找到所有的依赖模块描述
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="descriptorBuilder">模块描述创建器</param>
        /// <returns></returns>
        public static List<IModuleDescriptor> FindAllDependedModuleDescriptors(Type moduleType, IModuleDescriptorBuilder? descriptorBuilder = null)
        {
            Dictionary<Type, IModuleDescriptor> moduleDescriptorMap = new();

            CollectModuleDescriptors(moduleDescriptorMap, moduleType, descriptorBuilder ?? DefaultModuleDescriptorBuilder.Default);

            return moduleDescriptorMap.Values.ToList();
        }

        /// <summary>
        /// 找到所有的依赖模块描述
        /// </summary>
        /// <param name="moduleTypes"></param>
        /// <param name="descriptorBuilder">模块描述创建器</param>
        /// <returns></returns>
        public static List<IModuleDescriptor> FindAllDependedModuleDescriptors(IEnumerable<Type> moduleTypes, IModuleDescriptorBuilder? descriptorBuilder = null)
        {
            descriptorBuilder ??= DefaultModuleDescriptorBuilder.Default;
            Dictionary<Type, IModuleDescriptor> moduleDescriptorMap = new();

            foreach (var moduleType in moduleTypes)
            {
                CollectModuleDescriptors(moduleDescriptorMap, moduleType, descriptorBuilder);
            }

            return moduleDescriptorMap.Values.ToList();
        }

        /// <summary>
        /// 获取模块的依赖根
        /// </summary>
        /// <param name="moduleTypes"></param>
        /// <returns></returns>
        public static IModuleDescriptor[] GetModuleDependencyRoots(params Type[] moduleTypes)
        {
            return FindAllDependedModuleDescriptors(moduleTypes).Where(m => m.Dependents.IsNullOrEmpty())
                                                                .ToArray();
        }

        /// <summary>
        /// 排序模块描述列表
        /// </summary>
        /// <returns></returns>
        public static IModuleDescriptor[] SortModuleDescriptors(IEnumerable<IModuleDescriptor> moduleDescriptors)
        {
            return moduleDescriptors.SortByDependencies(m => m.Dependencies);
        }

        #endregion Public 方法

        #region Private 方法

        /// <summary>
        /// 收集模块
        /// </summary>
        /// <param name="moduleContainer"></param>
        /// <param name="moduleType"></param>
        /// <param name="descriptorBuilder"></param>
        private static IModuleDescriptor CollectModuleDescriptors(Dictionary<Type, IModuleDescriptor> moduleContainer, Type moduleType, IModuleDescriptorBuilder descriptorBuilder)
        {
            if (moduleContainer.TryGetValue(moduleType, out var existedDescriptor))
            {
                return existedDescriptor;
            }

            var descriptor = descriptorBuilder.Create(moduleType);
            moduleContainer.Add(moduleType, descriptor);

            var dependencies = descriptorBuilder.GetDependedModuleTypes(moduleType)
                                                .Select(dependedModuleType => CollectModuleDescriptors(moduleContainer, dependedModuleType, descriptorBuilder).AddDependent(descriptor));

            descriptor.AddDependencies(dependencies);

            return descriptor;
        }

        #endregion Private 方法
    }
}