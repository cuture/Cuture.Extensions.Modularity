using System.Collections;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    internal sealed class HostBuilderModuleSourceCollection : IEnumerable<IModuleSource>
    {
        #region Private 字段

        private readonly List<IModuleSource> _moduleSources = new List<IModuleSource>();

        #endregion Private 字段

        #region Public 方法

        /// <summary>
        /// 将模块源添加到构建集合中
        /// </summary>
        /// <param name="moduleSource"></param>
        public void Add(IModuleSource moduleSource)
        {
            _moduleSources.Add(moduleSource);
        }

        /// <inheritdoc/>
        public IEnumerator<IModuleSource> GetEnumerator() => _moduleSources.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => _moduleSources.GetEnumerator();

        #endregion Public 方法
    }
}