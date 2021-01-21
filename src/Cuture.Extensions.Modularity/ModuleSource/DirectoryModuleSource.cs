using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// ����Ŀ¼�� <inheritdoc cref="IModuleSource"/>
    /// </summary>
    public class DirectoryModuleSource : FileModuleSourceBase
    {
        #region Public �ֶ�

        /// <summary>
        /// ����������
        /// </summary>
        public const int MaxSearchDepth = 16;

        /// <summary>
        /// ��С�������
        /// </summary>
        public const int MinSearchDepth = 0;

        #endregion Public �ֶ�

        #region Private �ֶ�

        private int _searchDepth = MinSearchDepth;

        #endregion Private �ֶ�

        #region Public ����

        /// <summary>
        /// Ŀ¼ɸѡί��
        /// </summary>
        public Func<string, bool>? DirectoryFilter { get; set; }

        /// <summary>
        /// ԭʼ��Ŀ¼
        /// </summary>
        public IReadOnlyList<string> OriginDirectories { get; }

        /// <summary>
        /// �������
        /// </summary>
        public int SearchDepth
        {
            get => _searchDepth;
            set
            {
                if (value < 0 || value > 16)
                {
                    throw new ArgumentOutOfRangeException(nameof(SearchDepth), value, $"searchDepth must between {MinSearchDepth} and {MaxSearchDepth}");
                }
                _searchDepth = value;
            }
        }

        #endregion Public ����

        #region Public ���캯��

        /// <summary>
        /// <inheritdoc cref="DirectoryModuleSource"/>
        /// </summary>
        /// <param name="directories">Ŀ¼</param>
        public DirectoryModuleSource(params string[] directories)
        {
            OriginDirectories = directories ?? Array.Empty<string>();
        }

        #endregion Public ���캯��

        #region Protected ����

        /// <inheritdoc/>
        protected override IEnumerable<string> InternalGetFiles()
        {
            return OriginDirectories.SelectMany(m => SearchValidDirectories(m, SearchDepth)).SelectMany(m => SearchAssemblyFiles(m));
        }

        /// <summary>
        /// ����Ŀ¼�µĳ����ļ�
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        protected virtual IEnumerable<string> SearchAssemblyFiles(string directory)
        {
            var files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly)
                                 .Where(m => m.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
                                             || m.EndsWith(".exe", StringComparison.OrdinalIgnoreCase));
            return files;
        }

        /// <summary>
        /// ������Ч���ļ���
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="searchDepth"></param>
        /// <returns></returns>
        protected virtual IEnumerable<string> SearchValidDirectories(string directory, int searchDepth)
        {
            if (searchDepth <= 0)
            {
                return new[] { directory };
            }

            var subDirectories = Directory.EnumerateDirectories(directory, "*", SearchOption.TopDirectoryOnly).ToArray();

            if (subDirectories.Length == 0)
            {
                return Array.Empty<string>();
            }

            var filteredDirectories = (DirectoryFilter != null
                                        ? subDirectories.Where(DirectoryFilter)
                                        : subDirectories
                                       ).ToArray();
            searchDepth--;

            List<string> allDirectories = new(filteredDirectories);

            foreach (var subDirectory in filteredDirectories)
            {
                allDirectories.AddRange(SearchValidDirectories(subDirectory, searchDepth));
            }

            return allDirectories;
        }

        #endregion Protected ����
    }
}