using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceProvider"/>
    /// </summary>
    public static class IServiceProviderExtensions
    {
        #region InitializationModules

        #region Sync

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider)"/>
        public static IModulesBootstrapper InitializationModulesWithOutHostLifetime(this IServiceProvider serviceProvider)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, object[])"/>
        public static IModulesBootstrapper InitializationModulesWithOutHostLifetime(this IServiceProvider serviceProvider, params object[] items)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, IEnumerable{KeyValuePair{string, object?}}?)"/>
        public static IModulesBootstrapper InitializationModulesWithOutHostLifetime(this IServiceProvider serviceProvider, IEnumerable<KeyValuePair<string, object?>>? items = null)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, IDictionary{string, object?}?)"/>
        public static IModulesBootstrapper InitializationModulesWithOutHostLifetime(this IServiceProvider serviceProvider, IDictionary<string, object?>? items = null)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(items).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        #endregion Sync

        #region Async

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, IDictionary{string, object?}?)"/>
        public static Task<IModulesBootstrapper> InitializationModulesWithOutHostLifetimeAsync(this IServiceProvider serviceProvider)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(null as IDictionary<string, object?>);
        }

        /// <summary>
        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, IDictionary{string, object?}?)"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="items">初始化上下文中的传递项
        /// <para/>
        /// 排列需要为 string,object,string,object,string,object...
        /// <para/>
        /// 即 key,value,key,value,key,value...
        /// </param>
        /// <returns></returns>
        public static Task<IModulesBootstrapper> InitializationModulesWithOutHostLifetimeAsync(this IServiceProvider serviceProvider, params object[] items)
        {
            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(ParseParamsToDictionary(items));
        }

        /// <inheritdoc cref="InitializationModulesWithOutHostLifetimeAsync(IServiceProvider, IDictionary{string, object?}?)"/>
        public static Task<IModulesBootstrapper> InitializationModulesWithOutHostLifetimeAsync(this IServiceProvider serviceProvider, IEnumerable<KeyValuePair<string, object?>>? items = null)
        {
            var initItems = items.IsNullOrEmpty()
                                ? null
                                : items as IDictionary<string, object?> ?? items!.ToDictionary(m => m.Key, m => m.Value)!;

            return serviceProvider.InitializationModulesWithOutHostLifetimeAsync(initItems);
        }

        /// <summary>
        /// 初始化模块
        /// <para/>
        /// 没有主机生命周期控制 - 需要手动调用<see cref="ShutdownModules"/>以关闭模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="items">初始化上下文中的传递项</param>
        /// <returns></returns>
        public static async Task<IModulesBootstrapper> InitializationModulesWithOutHostLifetimeAsync(this IServiceProvider serviceProvider, IDictionary<string, object?>? items = null)
        {
            var bootstrapper = serviceProvider.GetService<IModulesBootstrapper>();

            if (bootstrapper is null)
            {
                throw new ModularityException($"must load modules first before initialization modules. please check module is loaded and {nameof(IServiceCollectionExtensions.ModuleLoadComplete)} has been invoked.");
            }

            var initializationContext = new ApplicationInitializationContext(serviceProvider, items);

            await bootstrapper.InitializationAsync(initializationContext).ConfigureAwait(false);

            return bootstrapper;
        }

        #endregion Async

        private static Dictionary<string, object?>? ParseParamsToDictionary(object[] items)
        {
            if (items.IsNullOrEmpty())
            {
                return null;
            }

            if (items.Length % 2 != 0)
            {
                throw new ArgumentException("The number of items length must be even.");
            }

            var result = new Dictionary<string, object?>();
            for (int i = 0; i < items.Length; i += 2)
            {
                if (items[i] is not string)
                {
                    throw new ArgumentException("The items type must ordered as [string,object,string,object,string,object.....]. namely [key,value,key,value,key,value.....].");
                }
                result.Add((items[i] as string)!, items[i + 1]);
            }

            return result;
        }

        #endregion InitializationModules

        #region ShutdownModules

        /// <summary>
        /// 停止应用模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider ShutdownModules(this IServiceProvider serviceProvider)
        {
            return serviceProvider.ShutdownModulesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 停止应用模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IServiceProvider> ShutdownModulesAsync(this IServiceProvider serviceProvider)
        {
            var bootstrapper = serviceProvider.GetRequiredService<IModulesBootstrapper>();

            var shutdownContext = new ApplicationShutdownContext(serviceProvider);
            await bootstrapper.ShutdownAsync(shutdownContext).ConfigureAwait(false);

            return serviceProvider;
        }

        #endregion ShutdownModules

        #region ObjectAccessor

        /// <summary>
        /// 获取对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObjectAccessor<T> GetObjectAccessor<T>(this IServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetRequiredService<ObjectAccessor<T>>();
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
            return serviceProvider.GetRequiredService<ObjectAccessor<T>>().Value;
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
            var accessor = serviceProvider.GetRequiredService<ObjectAccessor<T>>();
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
            serviceProvider.GetRequiredService<ObjectAccessor<T>>().Value = value;
        }

        #endregion ObjectAccessor
    }
}