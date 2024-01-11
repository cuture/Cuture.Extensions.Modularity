namespace Cuture.Extensions.Modularity;

/// <summary>
/// 依赖模块类型提供者
/// </summary>
public interface IDependedModuleTypesProvider
{
    #region Public 方法

    /// <summary>
    /// 获取依赖的模块类型
    /// </summary>
    /// <returns></returns>
    Type[] GetDependedModuleTypes();

    #endregion Public 方法
}
