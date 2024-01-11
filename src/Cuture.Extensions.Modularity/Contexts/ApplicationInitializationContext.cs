namespace Cuture.Extensions.Modularity;

/// <summary>
/// 应用程序初始化上下文
/// </summary>
public class ApplicationInitializationContext : StoreableContextBase
{
    #region Public 属性

    /// <summary>
    ///
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    #endregion Public 属性

    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="ApplicationInitializationContext"/>
    /// </summary>
    /// <param name="serviceProvider">要初始化的 <see cref="IServiceProvider"/></param>
    /// <param name="items">要初始化到上下文中的传递项</param>
    public ApplicationInitializationContext(IServiceProvider serviceProvider, IDictionary<string, object?>? items = null)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Items = items is null
                    ? []
                    : new Dictionary<string, object?>(items);
    }

    #endregion Public 构造函数
}
