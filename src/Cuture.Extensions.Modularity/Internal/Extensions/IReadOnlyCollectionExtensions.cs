using System.Collections.Generic;

namespace System
{
    /// <summary>
    ///
    /// </summary>
    internal static class IReadOnlyCollectionExtensions
    {
        #region Public 方法

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            return collection is null
                   || collection.Count == 0;
        }

        #endregion Public 方法
    }
}