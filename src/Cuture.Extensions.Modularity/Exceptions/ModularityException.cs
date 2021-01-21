using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块化异常
    /// </summary>
    public class ModularityException : Exception
    {
        #region Public 构造函数

        /// <inheritdoc cref="ModularityException"/>
        public ModularityException()
        {
        }

        /// <summary>
        /// <inheritdoc cref="ModularityException"/>
        /// </summary>
        /// <param name="message"></param>
        public ModularityException(string? message) : base(message)
        {
        }

        /// <summary>
        /// <inheritdoc cref="ModularityException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ModularityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        #endregion Public 构造函数
    }
}