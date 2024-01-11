namespace Cuture.Extensions.Modularity;

/// <summary>
/// 默认的<inheritdoc cref="IModuleDescriptorBuilder"/>
/// </summary>
public class DefaultModuleDescriptorBuilder : IModuleDescriptorBuilder
{
    #region Public 字段

    /// <summary>
    /// 默认实例
    /// </summary>
    public static readonly DefaultModuleDescriptorBuilder Default = new();

    #endregion Public 字段

    #region Public 方法

    /// <inheritdoc/>
    public virtual bool CanCreate(Type type)
    {
        return type.IsStandardAppModule();
    }

    /// <inheritdoc/>
    public virtual IModuleDescriptor Create(Type moduleType)
    {
        moduleType.ThrowIfNotStandardAppModule();
        return new ModuleDescriptor(moduleType);
    }

    /// <inheritdoc/>
    public virtual IEnumerable<Type> GetDependedModuleTypes(Type moduleType)
    {
        return moduleType.GetDirectDependedModuleTypes();
    }

    #endregion Public 方法
}
