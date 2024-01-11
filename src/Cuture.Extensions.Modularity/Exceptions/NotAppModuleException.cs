namespace Cuture.Extensions.Modularity;

/// <summary>
/// 给定类型不是<see cref="IAppModule"/>的异常
/// </summary>
public class NotAppModuleException : ModularityException
{
    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="NotAppModuleException"/>
    /// </summary>
    /// <param name="errorType">类型</param>
    public NotAppModuleException(Type errorType) : base($"the type - {errorType} is not an AppModule.")
    {
    }

    #endregion Public 构造函数
}
