using System.Runtime.CompilerServices;

using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ObjectAccessorServiceProviderExtensions
{
    #region ObjectAccessor

    /// <summary>
    /// 获取对象访问器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IObjectAccessor<T> GetObjectAccessor<T>(this IServiceProvider serviceProvider) where T : class
    {
        return serviceProvider.GetRequiredService<IObjectAccessor<T>>();
    }

    /// <summary>
    /// 获取对象访问器的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetObjectAccessorValue<T>(this IServiceProvider serviceProvider) where T : class
    {
        return serviceProvider.GetRequiredService<IObjectAccessor<T>>().Value;
    }

    /// <summary>
    /// 清空对象访问器的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? RemoveObjectAccessorValue<T>(this IServiceProvider serviceProvider) where T : class
    {
        var accessor = serviceProvider.GetRequiredService<IObjectAccessor<T>>();
        if (accessor != null)
        {
            var value = accessor.Value;
            accessor.Value = null;
            return value;
        }
        return null;
    }

    /// <summary>
    /// 设置对象访问器的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetObjectAccessorValue<T>(this IServiceProvider serviceProvider, T value) where T : class
    {
        serviceProvider.GetRequiredService<IObjectAccessor<T>>().Value = value;
    }

    #endregion ObjectAccessor
}
