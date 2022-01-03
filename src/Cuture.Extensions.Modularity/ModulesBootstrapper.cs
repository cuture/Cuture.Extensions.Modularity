using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块引导
    /// </summary>
    public class ModulesBootstrapper : IModulesBootstrapper
    {
        #region Private 字段

        private readonly IModulesBootstrapInterceptor _modulesBootstrapInterceptor;

        private readonly ModulesBootstrapOptions _options;

        #endregion Private 字段

        #region Protected 属性

        /// <summary>
        /// 模块实例列表（已按依赖顺序排序）
        /// </summary>
        protected virtual IReadOnlyList<object> Modules { get; }

        #endregion Protected 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ModulesBootstrapper"/>
        /// </summary>
        /// <param name="moduleInstances"></param>
        /// <param name="modulesBootstrapInterceptor"></param>
        /// <param name="options"></param>
        public ModulesBootstrapper(object[] moduleInstances, IModulesBootstrapInterceptor modulesBootstrapInterceptor, ModulesBootstrapOptions options)
        {
            if (moduleInstances.IsNullOrEmpty())
            {
                throw new ModularityException($"{nameof(moduleInstances)} cannot be null or empty.");
            }
            _modulesBootstrapInterceptor = modulesBootstrapInterceptor ?? throw new ArgumentNullException(nameof(modulesBootstrapInterceptor));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            Modules = moduleInstances.ToList().AsReadOnly();
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public virtual async Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var module in Modules)
            {
                await PreConfigureModuleServicesAsync(context, module);
            }

            AutoRegisterModuleServicesContext autoRegisterContext = new(context.Services);

            foreach (var module in Modules)
            {
                await ConfigureModuleServicesAsync(context, autoRegisterContext, module);
            }

            foreach (var module in Modules)
            {
                await PostConfigureModuleServicesAsync(context, module);
            }
        }

        /// <inheritdoc/>
        public virtual async Task InitializationAsync(ApplicationInitializationContext context)
        {
            foreach (var module in Modules)
            {
                await OnPreApplicationInitializationAsync(context, module);
            }

            foreach (var module in Modules)
            {
                await OnApplicationInitializationAsync(context, module);
            }

            foreach (var module in Modules)
            {
                await OnPostApplicationInitializationAsync(context, module);
            }
        }

        /// <inheritdoc/>
        public virtual async Task ShutdownAsync(ApplicationShutdownContext context)
        {
            var timeOut = _options.ShutdownTimeout ?? TimeSpan.FromSeconds(60);
            using var timeoutCTS = new CancellationTokenSource(timeOut);

            var token = timeoutCTS.Token;

            for (int i = Modules.Count - 1; i >= 0; i--)
            {
                var module = Modules[i];

                await OnApplicationShutdownAsync(context, module, token);
            }
        }

        #endregion Public 方法

        #region Protected 方法

        #region Lifecycle

        /// <inheritdoc cref="IConfigureServices"/>
        protected virtual async Task ConfigureModuleServicesAsync(ServiceConfigurationContext context, AutoRegisterModuleServicesContext autoRegisterContext, object moduleInstance)
        {
            await AutoRegisterModuleServicesAsync(moduleInstance, autoRegisterContext);

            if (!await _modulesBootstrapInterceptor.ConfigureModuleServicesAsync(context, moduleInstance))
            {
                return;
            }

            if (moduleInstance is IConfigureServices configureServices)
            {
                configureServices.ConfigureServices(context);
            }
            if (moduleInstance is IConfigureServicesAsync configureServicesAsync)
            {
                await configureServicesAsync.ConfigureServicesAsync(context);
            }
        }

        /// <inheritdoc cref="IPostConfigureServices"/>
        protected virtual async Task PostConfigureModuleServicesAsync(ServiceConfigurationContext context, object moduleInstance)
        {
            if (moduleInstance is IPostConfigureServices postConfigureServices)
            {
                postConfigureServices.PostConfigureServices(context);
            }
            if (moduleInstance is IPostConfigureServicesAsync postConfigureServicesAsync)
            {
                await postConfigureServicesAsync.PostConfigureServicesAsync(context);
            }
        }

        /// <inheritdoc cref="IPreConfigureServices"/>
        protected virtual async Task PreConfigureModuleServicesAsync(ServiceConfigurationContext context, object moduleInstance)
        {
            if (moduleInstance is IPreConfigureServices preConfigureServices)
            {
                preConfigureServices.PreConfigureServices(context);
            }
            if (moduleInstance is IPreConfigureServicesAsync preConfigureServicesAsync)
            {
                await preConfigureServicesAsync.PreConfigureServicesAsync(context);
            }
        }

        /// <inheritdoc cref="IOnApplicationInitialization"/>
        private static async Task OnApplicationInitializationAsync(ApplicationInitializationContext context, object moduleInstance)
        {
            if (moduleInstance is IOnApplicationInitialization onApplicationInitialization)
            {
                onApplicationInitialization.OnApplicationInitialization(context);
            }
            if (moduleInstance is IOnApplicationInitializationAsync onApplicationInitializationAsync)
            {
                await onApplicationInitializationAsync.OnApplicationInitializationAsync(context);
            }
        }

        /// <inheritdoc cref="IOnApplicationShutdown"/>
        private static async Task OnApplicationShutdownAsync(ApplicationShutdownContext context, object moduleInstance, CancellationToken token)
        {
            if (moduleInstance is IOnApplicationShutdown onApplicationShutdown)
            {
                onApplicationShutdown.OnApplicationShutdown(context);
            }
            if (moduleInstance is IOnApplicationShutdownAsync onApplicationShutdownAsync)
            {
                await onApplicationShutdownAsync.OnApplicationShutdownAsync(context, token);
            }
        }

        /// <inheritdoc cref="IOnPostApplicationInitialization"/>
        private static async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context, object moduleInstance)
        {
            if (moduleInstance is IOnPostApplicationInitialization onPostApplicationInitialization)
            {
                onPostApplicationInitialization.OnPostApplicationInitialization(context);
            }
            if (moduleInstance is IOnPostApplicationInitializationAsync onPostApplicationInitializationAsync)
            {
                await onPostApplicationInitializationAsync.OnPostApplicationInitializationAsync(context);
            }
        }

        /// <inheritdoc cref="IOnPreApplicationInitialization"/>
        private static async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context, object moduleInstance)
        {
            if (moduleInstance is IOnPreApplicationInitialization preApplicationInitialization)
            {
                preApplicationInitialization.OnPreApplicationInitialization(context);
            }
            if (moduleInstance is IOnPreApplicationInitializationAsync onPreApplicationInitializationAsync)
            {
                await onPreApplicationInitializationAsync.OnPreApplicationInitializationAsync(context);
            }
        }

        #endregion Lifecycle

        /// <summary>
        /// 自动注册模块所在程序集中导出的服务
        /// </summary>
        /// <param name="moduleInstance"></param>
        /// <param name="context"></param>
        protected virtual async Task AutoRegisterModuleServicesAsync(object moduleInstance, AutoRegisterModuleServicesContext context)
        {
            var moduleType = moduleInstance.GetType();
            if (!moduleType.ShouldRegisterServicesInAssembly())
            {
                return;
            }
            var assembly = moduleType.Assembly;

            if (context.ProcessedAssemblies.Contains(assembly))
            {
                return;
            }

            if (!await _modulesBootstrapInterceptor.RegisteringServicesInAssemblyAsync(context.Services, assembly))
            {
                return;
            }

            var autoRegisterServicesInAssemblyAttribute = moduleType.GetCustomAttribute<AutoRegisterServicesInAssemblyAttribute>();

            var serviceRegistrar = autoRegisterServicesInAssemblyAttribute?.ServiceRegistrarType is null
                                        ? context.Services.GetSingletonServiceInstance<IServiceRegistrar>() ?? throw new ModularityException($"cannot get {nameof(IServiceRegistrar)} instance in context's {nameof(IServiceCollection)}")
                                        : autoRegisterServicesInAssemblyAttribute.ServiceRegistrarType.ConstructInstance<IServiceRegistrar>();
            try
            {
                serviceRegistrar.AddAssembly(context.Services, assembly);
            }
            catch (Exception ex)
            {
                throw new AutoRegisterServiceException(moduleType, serviceRegistrar.GetType(), ex);
            }

            context.ProcessedAssemblies.Add(assembly);
        }

        #endregion Protected 方法
    }
}