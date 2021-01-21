using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System
{
    internal static class IEnumerableExtensions
    {
        #region Public 方法

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable is null
                   || !enumerable.Any();
        }

        #endregion Public 方法
    }
}