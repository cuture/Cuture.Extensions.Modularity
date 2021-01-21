using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 导出服务类型提供器
    /// </summary>
    public interface IExportServicesProvider
    {
        #region Public 属性

        /// <summary>
        /// <inheritdoc cref="Modularity.AddDIMode"/>
        /// </summary>
        AddDIMode AddDIMode { get; }

        /// <summary>
        /// 导出类型发现器类型
        /// <para/>
        /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
        /// </summary>
        Type? ExportTypeDiscovererType { get; }

        /// <summary>
        /// 导出服务的生命周期
        /// </summary>
        ServiceLifetime Lifetime { get; }

        #endregion Public 属性

        #region Public 方法

        /// <summary>
        /// 获取导出的服务类型
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        IEnumerable<Type> GetExportServiceTypes(Type targetType);

        #endregion Public 方法
    }
}