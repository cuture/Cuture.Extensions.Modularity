using System;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 以 <see cref="ServiceLifetime.Scoped"/> 为生命周期导出服务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExportScopedServicesAttribute : ExportServicesAttribute
    {
        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ExportScopedServicesAttribute"/>
        /// </summary>
        /// <param name="exportTypes"></param>
        public ExportScopedServicesAttribute(params Type[] exportTypes) : base(ServiceLifetime.Scoped, exportTypes)
        {
        }

        /// <summary>
        /// <inheritdoc cref="ExportScopedServicesAttribute"/>
        /// </summary>
        /// <param name="addMode">添加到DI容器的方式</param>
        /// <param name="exportTypes"></param>
        public ExportScopedServicesAttribute(AddDIMode addMode, params Type[] exportTypes) : base(ServiceLifetime.Scoped, addMode, exportTypes)
        {
        }

        /// <summary>
        /// <inheritdoc cref="ExportScopedServicesAttribute"/>
        /// </summary>
        /// <param name="exportTypeDiscovererType">
        /// 导出类型发现器类型
        /// <para/>
        /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
        /// </param>
        /// <param name="addMode">添加到DI容器的方式</param>
        public ExportScopedServicesAttribute(Type exportTypeDiscovererType, AddDIMode addMode) : base(ServiceLifetime.Scoped, exportTypeDiscovererType, addMode)
        {
        }

        #endregion Public 构造函数
    }
}