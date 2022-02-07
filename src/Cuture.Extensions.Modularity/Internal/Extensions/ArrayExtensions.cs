using System.Runtime.CompilerServices;

namespace System
{
    internal static class ArrayExtensions
    {
        #region Public 方法

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this T[]? array)
        {
            return array is null
                   || array.Length == 0;
        }

        #endregion Public 方法
    }
}