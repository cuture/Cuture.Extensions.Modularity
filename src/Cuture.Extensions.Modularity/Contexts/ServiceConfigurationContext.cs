using System;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 服务配置上下文
    /// </summary>
    public class ServiceConfigurationContext : StoreableContextBase
    {
        #region Public 属性

        /// <summary>
        /// ServiceCollection
        /// </summary>
        public IServiceCollection Services { get; protected set; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ServiceConfigurationContext"/>
        /// </summary>
        /// <param name="services"></param>
        public ServiceConfigurationContext(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        #endregion Public 构造函数
    }
}