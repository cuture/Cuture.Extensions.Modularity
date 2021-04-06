using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 默认的 <see cref="IOptionsBinder"/> 实现
    /// </summary>
    public class DefaultOptionsBinder : IOptionsBinder
    {
        #region Private 字段

        private readonly Type _optionsBaseType = typeof(IOptions<object>);
        private readonly MethodInfo _optionsExtensionMethod;

        #endregion Private 字段

        #region Protected 属性

        /// <inheritdoc cref="OptionsBindOptions"/>
        protected OptionsBindOptions AutoBindOptions { get; }

        #endregion Protected 属性

        #region Public 构造函数

        /// <inheritdoc cref="DefaultOptionsBinder"/>
        public DefaultOptionsBinder(OptionsBindOptions options)
        {
            AutoBindOptions = options ?? throw new ArgumentNullException(nameof(options));
            _optionsExtensionMethod = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new[] { typeof(IServiceCollection), typeof(IConfiguration) })!;
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public virtual void BindOptionsInAssembly(IServiceCollection services, Assembly assembly)
        {
            var configuration = services.GetConfiguration();

            if (configuration is null)
            {
                throw new ModularityException($"Cannot auto bind options with out any {nameof(IConfiguration)} in {nameof(IServiceCollection)}");
            }

            services.AddOptions();

            var optionsTypes = FindOptionsTypes(assembly);

            foreach (var optionsType in optionsTypes)
            {
                var optionsConfiguration = GetOptionsConfiguration(configuration, optionsType);

                ConfigureOptions(services, optionsType, optionsConfiguration);
            }
        }

        #endregion Public 方法

        #region Protected 方法

        /// <summary>
        /// 配置options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsType"></param>
        /// <param name="configuration"></param>
        protected void ConfigureOptions(IServiceCollection services, Type optionsType, IConfigurationSection configuration)
        {
            var arguments = new object[] { services, configuration };

            var methodInfo = _optionsExtensionMethod.MakeGenericMethod(optionsType);

            methodInfo.Invoke(null, arguments);
        }

        /// <summary>
        /// 查找程序集中的配置类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        protected virtual IEnumerable<Type> FindOptionsTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                           .Where(m => m.IsClass && !m.IsAbstract && !m.IsGenericType && _optionsBaseType.IsAssignableFrom(m))
                           .ToArray();
        }

        /// <summary>
        /// 获取Options的配置节点
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="optionsType"></param>
        /// <returns></returns>
        protected virtual IConfigurationSection GetOptionsConfiguration(IConfiguration configuration, Type optionsType)
        {
            var targetConfigurationKey = GetOptionsConfiguretionSectionKey(optionsType);

            if (targetConfigurationKey is null)
            {
                throw new ModularityException($"cannot auto find {nameof(IConfiguration)} for option type {optionsType}.cannot auto bind it.");
            }

            var targetConfiguration = configuration.GetSection(targetConfigurationKey);
            return targetConfiguration;
        }

        /// <summary>
        /// 获取Options的配置节点Key
        /// </summary>
        /// <param name="optionsType"></param>
        /// <returns></returns>
        protected virtual string? GetOptionsConfiguretionSectionKey(Type optionsType)
        {
            if (optionsType.GetCustomAttribute<ConfigurationSectionKeyAttribute>() is ConfigurationSectionKeyAttribute sectionKeyAttribute)
            {
                return sectionKeyAttribute.Key;
            }

            string key;

            if (AutoBindOptions.UseFullNamespaceAsPath)
            {
                if (optionsType.FullName is null)
                {
                    throw new ModularityException($"there is no namespace founded with option type {optionsType}.cannot auto bind it.");
                }
                key = optionsType.FullName.Replace('.', ':');
            }
            else
            {
                key = optionsType.Name;
            }

            if (AutoBindOptions.RemoveOptionsSuffix
                && key.EndsWith("Options", StringComparison.OrdinalIgnoreCase))
            {
#pragma warning disable IDE0057 // 使用范围运算符
                key = key.Substring(0, key.Length - 7);
#pragma warning restore IDE0057 // 使用范围运算符
            }

            if (!string.IsNullOrEmpty(AutoBindOptions.PathPrefix))
            {
                key = $"{AutoBindOptions.PathPrefix}:{key}";
            }

            return key;
        }

        #endregion Protected 方法
    }
}