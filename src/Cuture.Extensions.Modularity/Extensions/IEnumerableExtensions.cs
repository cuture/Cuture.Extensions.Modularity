using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    ///
    /// </summary>
    public static class IEnumerableExtensions
    {
        /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
         *      http://en.wikipedia.org/wiki/Topological_sorting
         */

        #region Public 方法

        /// <summary>
        /// 根据依赖进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies">获取依赖的委托</param>
        /// <returns></returns>
        public static T[] SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies) where T : notnull
        {
            if (source.IsNullOrEmpty())
            {
                return Array.Empty<T>();
            }

            List<T> sorted = new();
            Dictionary<T, bool> visited = new();

            foreach (var item in source)
            {
                VisitItemDependency(item, getDependencies, sorted, visited);
            }
            return sorted.ToArray();
        }

        #endregion Public 方法

        #region Internal 方法

        internal static void VisitItemDependency<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited) where T : notnull
        {
            if (visited.TryGetValue(item, out var inProcess))
            {
                if (inProcess)
                {
                    throw new CyclicDependencyException(item);
                }
                return;
            }

            visited[item] = true;

            var dependencies = getDependencies(item);

            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    VisitItemDependency(dependency, getDependencies, sorted, visited);
                }
            }

            sorted.Add(item);

            visited[item] = false;
        }

        #endregion Internal 方法
    }
}