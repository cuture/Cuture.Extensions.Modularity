using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块引导选项
    /// </summary>
    public class ModulesBootstrapOptions
    {
        #region Public 属性

        /// <summary>
        /// 异步关闭时的超时时间
        /// </summary>
        public TimeSpan? ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(60);

        #endregion Public 属性
    }
}