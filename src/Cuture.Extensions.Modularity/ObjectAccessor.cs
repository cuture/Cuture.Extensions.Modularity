using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 对象访问器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectAccessor<T> : IDisposable where T : class
    {
        #region Public 属性

        /// <summary>
        /// 对象
        /// </summary>
        T? Value { get; set; }

        #endregion Public 属性
    }

    /// <inheritdoc/>
    public class ObjectAccessor<T> : IObjectAccessor<T> where T : class
    {
        #region Private 字段

        private bool _disposedValue;

        #endregion Private 字段

        #region Public 属性

        /// <inheritdoc/>
        public T? Value { get; set; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// 对象访问器
        /// </summary>
        public ObjectAccessor()
        {
        }

        /// <summary>
        /// 对象访问器
        /// </summary>
        /// <param name="value"></param>
        public ObjectAccessor(T? value)
        {
            Value = value;
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public void Dispose()
        {
            DoDispose();
            GC.SuppressFinalize(this);
        }

        #endregion Public 方法

        #region Protected 方法

        /// <inheritdoc cref="Dispose"/>
        protected virtual void DoDispose()
        {
            if (!_disposedValue)
            {
                if (Value is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                Value = null;

                _disposedValue = true;
            }
        }

        #endregion Protected 方法
    }
}