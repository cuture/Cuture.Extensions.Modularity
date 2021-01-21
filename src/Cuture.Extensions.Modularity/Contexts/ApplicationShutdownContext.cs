using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 应用程序关闭下文
    /// </summary>
    public class ApplicationShutdownContext
    {
        #region Public 属性

        /// <summary>
        ///
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ApplicationShutdownContext"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationShutdownContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion Public 构造函数
    }
}