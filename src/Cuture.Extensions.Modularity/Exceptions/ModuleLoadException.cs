using System.Reflection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 模块加载异常
/// </summary>
public class ModuleLoadException : ModularityException
{
    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="ModuleLoadException"/>
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="innerException"></param>
    public ModuleLoadException(string path, Exception? innerException = null) : base($"load module fail with path: {path} .", innerException)
    {
    }

    /// <summary>
    /// <inheritdoc cref="ModuleLoadException"/>
    /// </summary>
    /// <param name="assembly">程序集</param>
    /// <param name="innerException"></param>
    public ModuleLoadException(Assembly assembly, Exception? innerException = null) : base($"load module fail with assembly: {assembly.FullName} . At: {assembly.Location}", innerException)
    {
    }

    #endregion Public 构造函数
}
