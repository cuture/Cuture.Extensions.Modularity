using System;
using System.Linq;
using System.Runtime.CompilerServices;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ObjectAccessorServiceCollectionExtensions
    {
        #region ObjectAccessor

        /// <summary>
        /// 添加单例对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, T? value = null) where T : class => services.AddObjectAccessor<T>(ServiceLifetime.Singleton, value);

        /// <summary>
        /// 添加对象访问器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, ServiceLifetime lifetime, T? value = null) where T : class
        {
            if (services.Any(service => service.ServiceType == typeof(IObjectAccessor<T>) && service.Lifetime == lifetime))
            {
                throw new Exception($"the same service ObjectAccessor<{typeof(T).FullName}> was already registered.");
            }

            var serviceDescriptor = lifetime switch
            {
                ServiceLifetime.Singleton => ServiceDescriptor.Singleton<IObjectAccessor<T>>(new ObjectAccessor<T>(value)),
                ServiceLifetime.Scoped => ServiceDescriptor.Scoped<IObjectAccessor<T>>(_ => new ObjectAccessor<T>(value)),
                _ => throw new ArgumentException($"ServiceLifetime: {lifetime} is a valueless value.", nameof(lifetime)),
            };

            services.Insert(0, serviceDescriptor);

            return services;
        }

        /// <summary>
        /// 获取对象访问器（必须为单例模式的注册类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IObjectAccessor<T> GetObjectAccessor<T>(this IServiceCollection services) where T : class
        {
            var serviceDescriptor = services.FirstOrDefault(m => m.ServiceType == typeof(IObjectAccessor<T>));
            if (serviceDescriptor is null
                || serviceDescriptor.Lifetime != ServiceLifetime.Singleton)
            {
                throw new InvalidOperationException($"before get ObjectAccessor<{typeof(T).Name}> from ServiceCollection must regist as a singleton service.");
            }
            return (serviceDescriptor.ImplementationInstance as IObjectAccessor<T>)!;
        }

        /// <summary>
        /// 获取对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetObjectAccessorValue<T>(this IServiceCollection services) where T : class
        {
            return services.GetObjectAccessor<T>().Value;
        }

        /// <summary>
        /// 移除已注册的对象访问器服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RemoveObjectAccessor<T>(this IServiceCollection services) where T : class
        {
            return services.RemoveAll<IObjectAccessor<T>>();
        }

        /// <summary>
        /// 移除对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? RemoveObjectAccessorValue<T>(this IServiceCollection services) where T : class
        {
            var objectAccessor = services.GetObjectAccessor<T>();
            var value = objectAccessor.Value;
            objectAccessor.Value = null;
            return value;
        }

        /// <summary>
        /// 设置对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetObjectAccessorValue<T>(this IServiceCollection services, T value) where T : class
        {
            services.GetObjectAccessor<T>().Value = value;
        }

        #region try

        /// <summary>
        /// 获取对象访问器（必须为单例模式的注册类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static bool TryGetObjectAccessor<T>(this IServiceCollection services, out IObjectAccessor<T>? accessor) where T : class
        {
            var serviceDescriptor = services.FirstOrDefault(m => m.ServiceType == typeof(IObjectAccessor<T>));
            if (serviceDescriptor is null
                || serviceDescriptor.Lifetime != ServiceLifetime.Singleton)
            {
                accessor = null;
                return false;
            }
            accessor = serviceDescriptor.ImplementationInstance as IObjectAccessor<T>;
            return accessor is not null;
        }

        /// <summary>
        /// 获取对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObjectAccessorValue<T>(this IServiceCollection services, out T? value) where T : class
        {
            if (services.TryGetObjectAccessor<T>(out var accessor)
                && accessor is not null)
            {
                value = accessor.Value;
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// 移除对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="removedValue"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemoveObjectAccessorValue<T>(this IServiceCollection services, out T? removedValue) where T : class
        {
            if (services.TryGetObjectAccessor<T>(out var accessor)
                && accessor is not null)
            {
                removedValue = accessor.Value;
                accessor.Value = null;
                return true;
            }
            removedValue = null;
            return false;
        }

        /// <summary>
        /// 设置对象访问器的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySetObjectAccessorValue<T>(this IServiceCollection services, T value) where T : class
        {
            if (services.TryGetObjectAccessor<T>(out var accessor)
                && accessor is not null)
            {
                accessor.Value = value;
                return true;
            }
            return false;
        }

        #endregion try

        #endregion ObjectAccessor
    }
}