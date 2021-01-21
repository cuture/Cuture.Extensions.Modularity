﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using Cuture.Extensions.Modularity;

namespace System
{
    /// <summary>
    ///
    /// </summary>
    internal static class TypeExtensions
    {
        #region Public 方法

        /// <summary>
        /// 调用无参构造函数构造实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConstructInstance<T>(this Type type) where T : class
        {
            return (T)type.ConstructInstance();
        }

        /// <inheritdoc cref="ConstructInstance{T}(Type)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object ConstructInstance(this Type type)
        {
            try
            {
                return Activator.CreateInstance(type)!;
            }
            catch (Exception ex)
            {
                throw new ModularityException($"Class {type.FullName} must has a no argument constructor. Please check it.", ex);
            }
        }

        /// <summary>
        /// 获取模块直接依赖的模块类型
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> GetDirectDependedModuleTypes(this Type moduleType)
        {
            moduleType.ThrowIfNotStandardAppModule();

            return moduleType.GetCustomAttributes()
                             .OfType<IDependedModuleTypesProvider>()
                             .SelectMany(m => m.GetDependedModuleTypes())
                             .Distinct()
                             .ToList();
        }

        /// <summary>
        /// 获取类型的导出服务类型提供器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExportServicesProvider[] GetExportServicesProviders(this Type type)
        {
            if (!type.IsClass
                || type.IsInterface
                || type.IsAbstract
                || type.IsGenericType)
            {
                return Array.Empty<IExportServicesProvider>();
            }

            return type.GetCustomAttributes(typeof(IExportServicesProvider), false)
                       .OfType<IExportServicesProvider>()
                       .ToArray();
        }

        /// <summary>
        /// 判断类型是不是标准的应用模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStandardAppModule(this Type type)
        {
            if (!type.IsClass
                || type.IsInterface
                || type.IsAbstract
                || type.IsGenericType)
            {
                return false;
            }

            return typeof(IAppModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// 是否应该自动注册类型所在程序集中导出的服务
        /// </summary>
        /// <param name="type"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShouldRegisterServicesInAssembly(this Type type)
        {
            return type.IsDefined(typeof(AutoRegisterServicesInAssemblyAttribute), true);
        }

        /// <summary>
        /// 当类型没有继承指定类型时抛出异常
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="type"></param>
        /// <param name="paramName"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNotInherit<TBase>(this Type type, string? paramName = null)
        {
            var baseType = typeof(TBase);
            if (!baseType.IsAssignableFrom(type))
            {
                var msg = $"{type.FullName} must inherit {baseType.FullName}";
                if (paramName is null)
                {
                    throw new ArgumentException(msg);
                }
                else
                {
                    throw new ArgumentException(msg, paramName);
                }
            }
        }

        /// <summary>
        /// 当类型不是标准的应用模块时抛出异常
        /// </summary>
        /// <param name="type"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNotStandardAppModule(this Type type)
        {
            if (!IsStandardAppModule(type))
            {
                throw new NotAppModuleException(type);
            }
        }

        #endregion Public 方法
    }
}