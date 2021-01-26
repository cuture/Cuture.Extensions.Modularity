using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class CutureServiceCollectionUtilExtensions
    {
        #region GetSingletonServiceInstance

        /// <summary>
        /// 获取集合中单例服务的的实例，获取实例失败时抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetRequiredSingletonServiceInstance<T>(this IServiceCollection services) where T : class
        {
            return services.GetSingletonServiceInstance<T>()
                    ?? throw new InvalidOperationException($"type of {typeof(T).FullName}'s singleton service instance not found in serviceCollection.");
        }

        /// <summary>
        /// 获取集合中单例服务的的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T? GetSingletonServiceInstance<T>(this IServiceCollection services) where T : class
        {
            var serviceDescriptor = services.FirstOrDefault(t => t.ServiceType == typeof(T));

            return serviceDescriptor?.ImplementationInstance as T;
        }

        #endregion GetSingletonServiceInstance

        #region RemoveService

        /// <summary>
        /// 从服务集合中移除指定类型的服务
        /// </summary>
        /// <typeparam name="T">要移除的服务类型</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static int Remove<T>(this IServiceCollection services) where T : class
        {
            var descriptors = services.Where(m => m.ServiceType == typeof(T)).ToArray();
            return descriptors.Count(descriptor => services.Remove(descriptor));
        }

        #endregion RemoveService
    }
}