using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 自动绑定<see cref="IOptions{TOptions}"/>的<inheritdoc cref="IModulesBootstrapInterceptor"/>
    /// </summary>
    public class OptionsAutoBindModulesBootstrapInterceptor : IModulesBootstrapInterceptor
    {
        #region Private 字段

        private readonly Type _optionsBaseType = typeof(IOptions<object>);
        private readonly MethodInfo _optionsExtensionMethod;

        #endregion Private 字段

        #region Public 构造函数

        /// <inheritdoc cref="OptionsAutoBindModulesBootstrapInterceptor"/>
        public OptionsAutoBindModulesBootstrapInterceptor()
        {
            _optionsExtensionMethod = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new[] { typeof(IServiceCollection), typeof(IConfiguration) })!;
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public Task<bool> RegisteringServicesInAssemblyAsync(IServiceCollection services, Assembly assembly)
        {
            var configuration = services.GetConfiguration();

            if (configuration is null)
            {
                throw new ModularityException($"Cannot auto bind options with out any {nameof(IConfiguration)} in {nameof(IServiceCollection)}");
            }

            services.AddOptions().Configure<string>(configuration);

            var optionsTypes = assembly.GetTypes()
                                       .Where(m => m.IsClass && !m.IsAbstract && !m.IsGenericType && _optionsBaseType.IsAssignableFrom(m))
                                       .ToArray();

            var arguments = new object[2];
            arguments[0] = services;

            foreach (var optionsType in optionsTypes)
            {
                var targetConfiguration = GetOptionsConfiguretionSection(optionsType, configuration);

                //if (targetConfiguration is null)
                //{
                //    throw new ModularityException($"cannot auto find {nameof(IConfiguration)} for option type {optionsType}.cannot auto bind it.");
                //}

                arguments[1] = targetConfiguration;

                var methodInfo = _optionsExtensionMethod.MakeGenericMethod(optionsType);

                methodInfo.Invoke(null, arguments);
            }

            return Task.FromResult(true);
        }

        #endregion Public 方法

        #region Protected 方法

        protected virtual IConfiguration GetOptionsConfiguretionSection(Type optionsType, IConfiguration configuration)
        {
            if (optionsType.FullName is null)
            {
                throw new ModularityException($"there is no namespace founded with option type {optionsType}.cannot auto bind it.");
            }
            var path = optionsType.FullName.Replace('.', ':');

            return configuration.GetSection(path);
        }

        #endregion Protected 方法
    }
}