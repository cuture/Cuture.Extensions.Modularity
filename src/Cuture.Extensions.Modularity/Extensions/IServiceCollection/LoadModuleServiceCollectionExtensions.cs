using System;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class LoadModuleServiceCollectionExtensions
    {
        #region LoadModule

        #region Load

        /// <summary>
        /// 加载指定类型的模块
        /// </summary>
        /// <typeparam name="TModule">模块类型</typeparam>
        /// <param name="services"></param>
        /// <param name="optionAction"></param>
        /// <returns></returns>
        public static IModuleLoaderBuilder LoadModule<TModule>(this IServiceCollection services, Action<ModuleLoadOptions>? optionAction = null) where TModule : IAppModule
        {
            return services.InternalAddModuleSource(new TypeModuleSource(typeof(TModule)), optionAction);
        }

        /// <summary>
        /// 加载模块源的模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleSource"></param>
        /// <param name="optionAction"></param>
        /// <returns></returns>
        public static IModuleLoaderBuilder LoadModule(this IServiceCollection services, IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
        {
            if (moduleSource is null)
            {
                throw new ArgumentNullException(nameof(moduleSource));
            }

            return services.InternalAddModuleSource(moduleSource, optionAction);
        }

        #endregion Load

        #region Directory

        /// <inheritdoc cref="LoadModuleDirectory(IServiceCollection, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleDirectory(this IServiceCollection services, params string[] directories)
        {
            return services.LoadModuleDirectory(null, null, directories);
        }

        /// <inheritdoc cref="LoadModuleDirectory(IServiceCollection, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleDirectory(this IServiceCollection services, Action<ModuleLoadOptions>? optionAction, params string[] directories)
        {
            return services.LoadModuleDirectory(null, optionAction, directories);
        }

        /// <inheritdoc cref="LoadModuleDirectory(IServiceCollection, Action{DirectoryModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleDirectory(this IServiceCollection services, Action<DirectoryModuleSource>? sourceOptionAction, params string[] directories)
        {
            return services.LoadModuleDirectory(sourceOptionAction, null, directories);
        }

        /// <summary>
        /// 加载指定目录下的模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sourceOptionAction">目录源配置</param>
        /// <param name="optionAction">加载配置</param>
        /// <param name="directories">目录列表</param>
        /// <returns></returns>
        public static IModuleLoaderBuilder LoadModuleDirectory(this IServiceCollection services, Action<DirectoryModuleSource>? sourceOptionAction, Action<ModuleLoadOptions>? optionAction, params string[] directories)
        {
            var source = new DirectoryModuleSource(directories);

            sourceOptionAction?.Invoke(source);

            return services.InternalAddModuleSource(source, optionAction);
        }

        #endregion Directory

        #region File

        /// <inheritdoc cref="LoadModuleFile(IServiceCollection, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleFile(this IServiceCollection services, params string[] files)
        {
            return services.LoadModuleFile(null, null, files);
        }

        /// <inheritdoc cref="LoadModuleFile(IServiceCollection, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleFile(this IServiceCollection services, Action<ModuleLoadOptions>? optionAction, params string[] files)
        {
            return services.LoadModuleFile(null, optionAction, files);
        }

        /// <inheritdoc cref="LoadModuleFile(IServiceCollection, Action{FileModuleSource}?, Action{ModuleLoadOptions}?, string[])"/>
        public static IModuleLoaderBuilder LoadModuleFile(this IServiceCollection services, Action<FileModuleSource>? sourceOptionAction, params string[] files)
        {
            return services.LoadModuleFile(sourceOptionAction, null, files);
        }

        /// <summary>
        /// 加载指定的文件模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sourceOptionAction">目录源配置</param>
        /// <param name="optionAction">加载配置</param>
        /// <param name="files">模块文件列表</param>
        /// <returns></returns>
        public static IModuleLoaderBuilder LoadModuleFile(this IServiceCollection services, Action<FileModuleSource>? sourceOptionAction, Action<ModuleLoadOptions>? optionAction, params string[] files)
        {
            var source = new FileModuleSource(files);

            sourceOptionAction?.Invoke(source);

            return services.InternalAddModuleSource(source, optionAction);
        }

        #endregion File

        #endregion LoadModule

        #region ModuleLoadComplete

        /// <summary>
        /// <inheritdoc cref="IModuleLoaderBuilderLoadModuleExtensions.ModuleLoadComplete(IModuleLoaderBuilder)"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ModuleLoadComplete(this IServiceCollection services)
        {
            services.InternalConfigureModuleLoad(builder => builder.ModuleLoadComplete());
            return services;
        }

        /// <summary>
        /// <inheritdoc cref="IModuleLoaderBuilderLoadModuleExtensions.ModuleLoadCompleteAsync(IModuleLoaderBuilder)"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Task<IServiceCollection> ModuleLoadCompleteAsync(this IServiceCollection services)
        {
            var moduleLoaderBuilder = services.InternalGetOrInitModuleLoadBuilder();

            return moduleLoaderBuilder.ModuleLoadCompleteAsync();
        }

        #endregion ModuleLoadComplete

        #region Option

        /// <summary>
        /// 配置ModuleLoadBuilder选项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionAction"></param>
        /// <returns></returns>
        public static IModuleLoaderBuilder OptionModuleLoadBuilder(this IServiceCollection services, Action<ModuleLoadOptions> optionAction)
        {
            return services.InternalGetOrInitModuleLoadBuilder(optionAction);
        }

        #endregion Option

        #region Internal

        internal static IModuleLoaderBuilder InternalAddModuleSource(this IServiceCollection services, IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
        {
            return services.InternalConfigureModuleLoad(builder => builder.AddModuleSource(moduleSource), optionAction);
        }

        internal static IModuleLoaderBuilder InternalConfigureModuleLoad(this IServiceCollection services, Action<IModuleLoaderBuilder> buildAction, Action<ModuleLoadOptions>? optionAction = null)
        {
            var moduleLoaderBuilder = services.InternalGetOrInitModuleLoadBuilder(optionAction);
            buildAction(moduleLoaderBuilder);
            return moduleLoaderBuilder;
        }

        internal static IModuleLoaderBuilder InternalGetOrInitModuleLoadBuilder(this IServiceCollection services, Action<ModuleLoadOptions>? optionAction = null)
        {
            if (services.GetSingletonServiceInstance<IModuleLoaderBuilder>() is IModuleLoaderBuilder moduleLoaderBuilder
                && moduleLoaderBuilder != null)
            {
                optionAction?.Invoke(moduleLoaderBuilder.ModuleLoadOptions);
            }
            else
            {
                moduleLoaderBuilder = services.InternalInitModuleLoaderBuilder(optionAction);
            }

            return moduleLoaderBuilder;
        }

        private static IModuleLoaderBuilder InternalInitModuleLoaderBuilder(this IServiceCollection services, Action<ModuleLoadOptions>? optionAction)
        {
            if (services.GetSingletonServiceInstance<IModulesBootstrapper>() is IModulesBootstrapper modulesBootstrapper
                && modulesBootstrapper != null)
            {
                throw new ModularityException("Modules already initialized. Should not append modules after initialized.");
            }

            var moduleLoadOptions = new ModuleLoadOptions();
            optionAction?.Invoke(moduleLoadOptions);

            var moduleLoaderBuilder = new ModuleLoaderBuilder(services, moduleLoadOptions);

            services.AddSingleton<IServiceRegistrar>(new DefaultServiceRegistrar());

            services.Replace(ServiceDescriptor.Singleton<IModuleLoaderBuilder>(moduleLoaderBuilder));

            return moduleLoaderBuilder;
        }

        #endregion Internal
    }
}