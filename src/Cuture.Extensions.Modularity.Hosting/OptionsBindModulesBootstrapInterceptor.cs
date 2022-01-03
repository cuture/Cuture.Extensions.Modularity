using System;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 自动绑定<see cref="IOptions{TOptions}"/>的<inheritdoc cref="IModulesBootstrapInterceptor"/>
    /// </summary>
    public class OptionsBindModulesBootstrapInterceptor : IModulesBootstrapInterceptor
    {
        #region Private 字段

        private readonly IOptionsBinder _optionsBinder;

        #endregion Private 字段

        #region Public 构造函数

        /// <inheritdoc cref="OptionsBindModulesBootstrapInterceptor"/>
        public OptionsBindModulesBootstrapInterceptor(IOptionsBinder optionsBinder)
        {
            _optionsBinder = optionsBinder ?? throw new ArgumentNullException(nameof(optionsBinder));
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public Task<bool> ConfigureModuleServicesAsync(ServiceConfigurationContext context, object moduleInstance)
        {
            _optionsBinder.BindOptionsInAssembly(context.Services, moduleInstance.GetType().Assembly);
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> RegisteringServicesInAssemblyAsync(IServiceCollection services, Assembly assembly) => Task.FromResult(true);

        #endregion Public 方法
    }
}