namespace Cuture.Extensions.Modularity;

/// <summary>
/// 导出类型发现器
/// </summary>
public interface IExportTypeDiscoverer
{
    #region Public 方法

    /// <summary>
    /// 发现导出类型
    /// </summary>
    /// <param name="targetType">目标类型</param>
    /// <returns></returns>
    IEnumerable<Type> DiscoveryExportTypes(Type targetType);

    #endregion Public 方法
}
