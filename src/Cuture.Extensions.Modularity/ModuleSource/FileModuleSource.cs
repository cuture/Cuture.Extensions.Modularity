using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// �����ļ��� <inheritdoc cref="IModuleSource"/>
    /// </summary>
    public class FileModuleSource : FileModuleSourceBase
    {
        #region Public ����

        /// <summary>
        /// ԭʼģ���ļ�·���б�
        /// </summary>
        public IReadOnlyList<string> OriginFilePaths { get; }

        #endregion Public ����

        #region Public ���캯��

        /// <summary>
        /// <inheritdoc cref="FileModuleSource"/>
        /// </summary>
        /// <param name="filePaths"></param>
        public FileModuleSource(params string[] filePaths)
        {
            OriginFilePaths = filePaths ?? Array.Empty<string>();
        }

        #endregion Public ���캯��

        #region Protected ����

        /// <inheritdoc/>
        protected override IEnumerable<string> InternalGetFiles() => OriginFilePaths;

        #endregion Protected ����
    }
}