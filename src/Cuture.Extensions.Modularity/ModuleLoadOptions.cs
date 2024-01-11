namespace Cuture.Extensions.Modularity;

/// <summary>
/// 模块加载选项
/// </summary>
public class ModuleLoadOptions
{
    #region Private 字段

    private ModulesBootstrapOptions? _bootstrapOptions;

    #endregion Private 字段

    #region Public 属性

    /// <summary>
    /// 将模块注册到DI中
    /// </summary>
    public bool AddModuleAsService { get; set; } = false;

    /// <summary>
    /// <inheritdoc cref="ModulesBootstrapOptions"/>
    /// </summary>
    public ModulesBootstrapOptions BootstrapOptions
    {
        get
        {
            return _bootstrapOptions ??= new ModulesBootstrapOptions();
        }
        set => _bootstrapOptions = value;
    }

    /// <summary>
    /// <inheritdoc cref="IModulesBootstrapInterceptor"/>列表
    /// </summary>
    public List<IModulesBootstrapInterceptor> ModulesBootstrapInterceptors { get; } = [];

    #endregion Public 属性
}
