namespace Cuture.Extensions.Modularity;

/// <summary>
/// 基于目录的 <inheritdoc cref="IModuleSource"/>
/// </summary>
public class DirectoryModuleSource : FileModuleSourceBase
{
    #region Public 字段

    /// <summary>
    /// 最大搜索深度
    /// </summary>
    public const int MaxSearchDepth = 16;

    /// <summary>
    /// 最小搜索深度
    /// </summary>
    public const int MinSearchDepth = 0;

    #endregion Public 字段

    #region Private 字段

    private int _searchDepth = MinSearchDepth;

    #endregion Private 字段

    #region Public 属性

    /// <summary>
    /// 目录筛选委托
    /// </summary>
    public Func<string, bool>? DirectoryFilter { get; set; }

    /// <summary>
    /// 原始的目录
    /// </summary>
    public IReadOnlyList<string> OriginDirectories { get; }

    /// <summary>
    /// 搜索深度
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

    #endregion Public 属性

    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="DirectoryModuleSource"/>
    /// </summary>
    /// <param name="directories">目录</param>
    public DirectoryModuleSource(params string[] directories)
    {
        OriginDirectories = directories ?? Array.Empty<string>();
    }

    #endregion Public 构造函数

    #region Protected 方法

    /// <inheritdoc/>
    protected override IEnumerable<string> InternalGetFiles()
    {
        return OriginDirectories.SelectMany(m => SearchValidDirectories(m, SearchDepth)).SelectMany(m => SearchAssemblyFiles(m));
    }

    /// <summary>
    /// 查找目录下的程序集文件
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
    /// 查找有效的文件夹
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

    #endregion Protected 方法
}
