using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class IServiceCollectionExtensions
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
        /// <inheritdoc cref="IModuleLoaderBuilderExtensions.ModuleLoadComplete(IModuleLoaderBuilder)"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ModuleLoadComplete(this IServiceCollection services)
        {
            services.InternalConfigureModuleLoad(builder => builder.ModuleLoadComplete());
            return services;
        }

        /// <summary>
        /// <inheritdoc cref="IModuleLoaderBuilderExtensions.ModuleLoadCompleteAsync(IModuleLoaderBuilder)"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Task<IServiceCollection> ModuleLoadCompleteAsync(this IServiceCollection services)
        {
            var moduleLoaderBuilder = services.InternalGetOrInitModuleLoadBuilder();

            return moduleLoaderBuilder.ModuleLoadCompleteAsync();
        }

        #endregion ModuleLoadComplete

        #region ObjectAccessor

        /// <summary>
        /// 添加单例对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, T? value = null) where T : class => services.AddObjectAccessor<T>(ServiceLifetime.Singleton, value);

        /// <summary>
        /// 添加对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, ServiceLifetime lifetime, T? value = null) where T : class
        {
            if (services.Any(service => service.ServiceType == typeof(ObjectAccessor<T>) && service.Lifetime == lifetime))
            {
                throw new Exception($"the same service ObjectAccessor<{typeof(T).FullName}> was already registered.");
            }

            var serviceDescriptor = lifetime switch
            {
                ServiceLifetime.Singleton => ServiceDescriptor.Singleton<ObjectAccessor<T>>(new ObjectAccessor<T>(value)),
                ServiceLifetime.Scoped => ServiceDescriptor.Scoped<ObjectAccessor<T>>(_ => new ObjectAccessor<T>(value)),
                _ => throw new ArgumentException($"ServiceLifetime: {lifetime} is a valueless value.", nameof(lifetime)),
            };

            services.Insert(0, serviceDescriptor);

            return services;
        }

        /// <summary>
        /// 获取对象访问器（必须为单例模式的注册类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ObjectAccessor<T> GetObjectAccessor<T>(this IServiceCollection services) where T : class
        {
            var serviceDescriptor = services.FirstOrDefault(m => m.ServiceType == typeof(ObjectAccessor<T>));
            if (serviceDescriptor is null
                || serviceDescriptor.Lifetime != ServiceLifetime.Singleton)
            {
                throw new InvalidOperationException($"before get ObjectAccessor<{typeof(T).Name}> from ServiceCollection must regist as a singleton service.");
            }
            return (serviceDescriptor.ImplementationInstance as ObjectAccessor<T>)!;
        }

        /// <summary>
        /// 获取对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetObjectAccessorValue<T>(this IServiceCollection services) where T : class
        {
            return services.GetObjectAccessor<T>().Value;
        }

        /// <summary>
        /// 移除已注册的对象访问器服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RemoveObjectAccessor<T>(this IServiceCollection services) where T : class
        {
            return services.RemoveAll<ObjectAccessor<T>>();
        }

        /// <summary>
        /// 移除对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? RemoveObjectAccessorValue<T>(this IServiceCollection services) where T : class
        {
            var objectAccessor = services.GetObjectAccessor<T>();
            var value = objectAccessor.Value;
            objectAccessor.Value = null;
            return value;
        }

        /// <summary>
        /// 设置对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetObjectAccessorValue<T>(this IServiceCollection services, T value) where T : class
        {
            services.GetObjectAccessor<T>().Value = value;
        }

        #endregion ObjectAccessor

        #region GetSingletonServiceInstance

        /// <summary>
        /// 获取集合中单例服务的的实例，获取实例失败时抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetRequiredSingletonServiceInstance<T>(this IServiceCollection services) where T : class
        {
            return services.GetSingletonServiceInstance<T>()
                    ?? throw new InvalidOperationException($"type of {typeof(T).FullName}'s singleton service instance not found in serviceCollection.");
        }

        /// <summary>
        /// 获取集合中单例服务的的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T? GetSingletonServiceInstance<T>(this IServiceCollection services) where T : class
        {
            var serviceDescriptor = services.FirstOrDefault(t => t.ServiceType == typeof(T));

            return serviceDescriptor?.ImplementationInstance as T;
        }

        #endregion GetSingletonServiceInstance

        #region RemoveService

        /// <summary>
        /// 从服务集合中移除指定类型的服务
        /// </summary>
        /// <typeparam name="T">要移除的服务类型</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static int Remove<T>(this IServiceCollection services) where T : class
        {
            var descriptors = services.Where(m => m.ServiceType == typeof(T)).ToArray();
            return descriptors.Count(descriptor => services.Remove(descriptor));
        }

        #endregion RemoveService

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
                if (optionAction != null)
                {
                    throw new ModularityException($"{nameof(ModuleLoadOptions)} already set. cannot set it twice. please set it at first time when load module.");
                }
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