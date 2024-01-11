namespace Cuture.Extensions.Modularity;

/// <summary>
/// 自动注册服务异常
/// </summary>
public class AutoRegisterServiceException : ModularityException
{
    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="AutoRegisterServiceException"/>
    /// </summary>
    /// <param name="moduleType">模块类型</param>
    /// <param name="serviceRegistrarType">服务注册器类型</param>
    /// <param name="innerException"></param>
    public AutoRegisterServiceException(Type moduleType, Type serviceRegistrarType, Exception? innerException = null) : base($"Auto register services fail with module: {moduleType.FullName} at assembly file: {moduleType.Assembly.Location} with {nameof(IServiceRegistrar)}: {serviceRegistrarType.FullName}", innerException)
    {
    }

    #endregion Public 构造函数
}
