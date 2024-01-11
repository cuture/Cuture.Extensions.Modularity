namespace Cuture.Extensions.Modularity;

/// <summary>
/// 模块描述
/// </summary>
public interface IModuleDescriptor : IEquatable<IModuleDescriptor>
{
    #region Public 属性

    /// <summary>
    /// 当前模块依赖的模块
    /// </summary>
    IReadOnlyList<IModuleDescriptor> Dependencies { get; }

    /// <summary>
    /// 依赖当前模块的模块
    /// </summary>
    IReadOnlyList<IModuleDescriptor> Dependents { get; }

    /// <summary>
    /// 模块类型
    /// </summary>
    Type Type { get; }

    #endregion Public 属性

    #region Public 方法

    /// <summary>
    /// 添加当前模块依赖的模块
    /// </summary>
    /// <param name="moduleDescriptors"></param>
    /// <returns>当前对象</returns>
    IModuleDescriptor AddDependencies(IEnumerable<IModuleDescriptor> moduleDescriptors);

    /// <summary>
    /// 添加依赖当前模块的模块
    /// </summary>
    /// <param name="moduleDescriptor"></param>
    /// <returns>当前对象</returns>
    IModuleDescriptor AddDependent(IModuleDescriptor moduleDescriptor);

    #endregion Public 方法
}
