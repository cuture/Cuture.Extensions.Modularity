﻿// <Auto-Generated></Auto-Generated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace OtherModuleSystemAdaptSample
{
    public class AbpAdaptedServiceRegistrar : DefaultServiceRegistrar
    {
        private Volo.Abp.DependencyInjection.DefaultConventionalRegistrar _defaultConventionalRegistrar = new();

        private readonly HashSet<Assembly> _processedAssemblies = new HashSet<Assembly>();

        public override void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var moduleLoaderBuilder = services.GetSingletonServiceInstance<IModuleLoaderBuilder>();
            //获取abp模块程序集列表
            var moduleAssemblies = moduleLoaderBuilder.ModuleSources.SelectMany(m => m.GetModules())
                                                                    .Where(m => Volo.Abp.Modularity.AbpModule.IsAbpModule(m))
                                                                    .Select(m => m.Assembly)
                                                                    .Distinct();

            foreach (var item in moduleAssemblies)
            {
                //加载所有abp相关的程序集
                AutoAddAbpAssemblyLink(services, item);
            }
        }

        private void AutoAddAbpAssemblyLink(IServiceCollection services, Assembly assembly)
        {
            if (_processedAssemblies.Contains(assembly))
            {
                return;
            }
            _processedAssemblies.Add(assembly);

            _defaultConventionalRegistrar.AddAssembly(services, assembly);
            assembly.GetTypes()
                    .Where(m => Volo.Abp.Modularity.AbpModule.IsAbpModule(m))
                    .SelectMany(m => m.GetCustomAttributes().OfType<IDependedTypesProvider>().SelectMany(provider => provider.GetDependedTypes()))
                    .Select(m => m.Assembly)
                    .Distinct()
                    .ToList()
                    .ForEach(m => AutoAddAbpAssemblyLink(services, m));
        }

        public override void AddType(IServiceCollection services, Type type)
        {
            _defaultConventionalRegistrar.AddType(services, type);
        }
    }
}
