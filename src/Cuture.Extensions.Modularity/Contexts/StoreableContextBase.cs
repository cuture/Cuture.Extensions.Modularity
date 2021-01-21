using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 可以存放数据的Context基类
    /// </summary>
    public abstract class StoreableContextBase
    {
        #region Public 属性

        /// <summary>
        /// 储存的传递项
        /// </summary>
        public Dictionary<string, object?> Items { get; protected set; } = new Dictionary<string, object?>();

        #endregion Public 属性

        #region Public 方法

        /// <summary>
        /// 存放值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value) where T : class
        {
#if NETSTANDARD2_0
            if (Items.ContainsKey(key))
            {
                return false;
            }
            Items.Add(key, value);
            return true;
#else
            return Items.TryAdd(key, value);
#endif
        }

        /// <summary>
        /// 获取存放的值，如果不存在则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? Value<T>(string key) where T : class
        {
            if (Items.TryGetValue(key, out var value))
            {
                return (T?)value;
            }
            return null;
        }

        #endregion Public 方法
    }
}