using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
    internal static class ICollectionExtensions
    {
        #region Public 方法

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddIfNotContains<T>(this ICollection<T> collection, T item)
        {
            if (collection.Contains(item))
            {
                return false;
            }

            collection.Add(item);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection is null
                   || collection.Count == 0;
        }

        #endregion Public 方法
    }
}