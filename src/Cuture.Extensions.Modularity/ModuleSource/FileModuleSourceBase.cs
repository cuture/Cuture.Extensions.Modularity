using System.Reflection;

#if !NETSTANDARD2_0 && !NETSTANDARD2_1

using System.Runtime.Loader;

#endif

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 基于文件的 <inheritdoc cref="IModuleSource"/> 基类
/// </summary>
public abstract class FileModuleSourceBase : AssemblyModuleSourceBase
{
    #region Public 属性

    /// <summary>
    /// 文件筛选委托
    /// </summary>
    public Func<string, bool>? FileFilter { get; set; }

    #endregion Public 属性

    #region Protected 方法

    /// <summary>
    /// 获取程序集
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<Assembly> InternalGetAssemblies()
    {
        var files = FileFilter != null
                        ? InternalGetFiles().Where(FileFilter)
                        : InternalGetFiles();

        List<Assembly> assemblies = [];

        foreach (var file in files)
        {
            try
            {
                var rootedPath = file;
                if (!Path.IsPathRooted(rootedPath))
                {
                    rootedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, rootedPath);
                }

                assemblies.Add(LoadAssembly(rootedPath));
            }
            catch (Exception ex)
            {
                throw new ModuleLoadException(file, ex);
            }
        }

        return assemblies;
    }

    /// <summary>
    /// 获取文件
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<string> InternalGetFiles();

    /// <summary>
    /// 加载Assembly
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    protected virtual Assembly LoadAssembly(string filePath)
    {
#if NETSTANDARD2_0 || NETSTANDARD2_1
        //HACK 用Assembly.LoadFrom有没有问题。。
        return Assembly.LoadFrom(filePath);
#else
        return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
#endif
    }

    #endregion Protected 方法
}
