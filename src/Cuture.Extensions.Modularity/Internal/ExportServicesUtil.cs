namespace Cuture.Extensions.Modularity;

internal static class ExportServicesUtil
{
    #region Public 方法

    /// <summary>
    /// 获取导出的服务类型
    /// </summary>
    /// <param name="exportServicesProvider"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetServiceExportTypes(this IExportServicesProvider exportServicesProvider, Type targetType)
    {
        return exportServicesProvider.GetExportServiceTypes(targetType).Distinct();
    }

    #endregion Public 方法
}
