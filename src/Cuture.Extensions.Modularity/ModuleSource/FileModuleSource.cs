using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 基于文件的 <inheritdoc cref="IModuleSource"/>
    /// </summary>
    public class FileModuleSource : FileModuleSourceBase
    {
        #region Public 属性

        /// <summary>
        /// 原始模块文件路径列表
        /// </summary>
        public IReadOnlyList<string> OriginFilePaths { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="FileModuleSource"/>
        /// </summary>
        /// <param name="filePaths"></param>
        public FileModuleSource(params string[] filePaths)
        {
            OriginFilePaths = filePaths ?? Array.Empty<string>();
        }

        #endregion Public 构造函数

        #region Protected 方法

        /// <inheritdoc/>
        protected override IEnumerable<string> InternalGetFiles() => OriginFilePaths;

        #endregion Protected 方法
    }
}