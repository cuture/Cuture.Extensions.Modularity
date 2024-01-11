using System;
using System.Collections.Generic;

namespace DependencyInjection.Modularity.Test;

public static class IEnumerableExtensions
{
    #region Public 方法

    public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
    {
        int index = -1;
        foreach (var item in enumerable)
        {
            index++;
            if (func(item))
            {
                return index;
            }
        }
        return -1;
    }

    #endregion Public 方法
}
