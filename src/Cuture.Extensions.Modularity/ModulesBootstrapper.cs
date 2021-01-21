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
        /// <param name="options"></param>
        public ModulesBootstrapper(object[] moduleInstances, ModulesBootstrapOptions options)
        {
            if (moduleInstances.IsNullOrEmpty())
            {
                throw new ModularityException($"{nameof(moduleInstances)} cannot be null or empty.");
            }

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
                if (module is IPreConfigureServices preConfigureServices)
                {
                    preConfigureServices.PreConfigureServices(context);
                }
                if (module is IPreConfigureServicesAsync preConfigureServicesAsync)
                {
                    await preConfigureServicesAsync.PreConfigureServicesAsync(context);
                }
            }

            AutoRegisterModuleServicesContext autoRegisterContext = new(context.Services);

            foreach (var module in Modules)
            {
                AutoRegisterModuleServices(module, autoRegisterContext);
                if (module is IConfigureServices configureServices)
                {
                    configureServices.ConfigureServices(context);
                }
                if (module is IConfigureServicesAsync configureServicesAsync)
                {
                    await configureServicesAsync.ConfigureServicesAsync(context);
                }
            }

            foreach (var module in Modules)
            {
                if (module is IPostConfigureServices postConfigureServices)
                {
                    postConfigureServices.PostConfigureServices(context);
                }
                if (module is IPostConfigureServicesAsync postConfigureServicesAsync)
                {
                    await postConfigureServicesAsync.PostConfigureServicesAsync(context);
                }
            }
        }

        /// <inheritdoc/>
        public virtual async Task InitializationAsync(ApplicationInitializationContext context)
        {
            foreach (var module in Modules)
            {
                if (module is IOnPreApplicationInitialization preApplicationInitialization)
                {
                    preApplicationInitialization.OnPreApplicationInitialization(context);
                }
                if (module is IOnPreApplicationInitializationAsync onPreApplicationInitializationAsync)
                {
                    await onPreApplicationInitializationAsync.OnPreApplicationInitializationAsync(context);
                }
            }

            foreach (var module in Modules)
            {
                if (module is IOnApplicationInitialization onApplicationInitialization)
                {
                    onApplicationInitialization.OnApplicationInitialization(context);
                }
                if (module is IOnApplicationInitializationAsync onApplicationInitializationAsync)
                {
                    await onApplicationInitializationAsync.OnApplicationInitializationAsync(context);
                }
            }

            foreach (var module in Modules)
            {
                if (module is IOnPostApplicationInitialization onPostApplicationInitialization)
                {
                    onPostApplicationInitialization.OnPostApplicationInitialization(context);
                }
                if (module is IOnPostApplicationInitializationAsync onPostApplicationInitializationAsync)
                {
                    await onPostApplicationInitializationAsync.OnPostApplicationInitializationAsync(context);
                }
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

                if (module is IOnApplicationShutdown onApplicationShutdown)
                {
                    onApplicationShutdown.OnApplicationShutdown(context);
                }
                if (module is IOnApplicationShutdownAsync onApplicationShutdownAsync)
                {
                    await onApplicationShutdownAsync.OnApplicationShutdownAsync(context, token);
                }
            }
        }

        #endregion Public 方法

        #region Protected 方法

        /// <summary>
        /// 自动注册模块所在程序集中导出的服务
        /// </summary>
        /// <param name="moduleInstance"></param>
        /// <param name="context"></param>
        protected virtual void AutoRegisterModuleServices(object moduleInstance, AutoRegisterModuleServicesContext context)
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