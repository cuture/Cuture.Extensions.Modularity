using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 导出服务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExportServicesAttribute : Attribute, IExportServicesProvider
    {
        #region Protected 属性

        /// <summary>
        /// 当前特性标记的类型
        /// </summary>
        protected Type? TargetType { get; set; }

        #endregion Protected 属性

        #region Public 属性

        /// <summary>
        /// <inheritdoc cref="Modularity.AddDIMode"/>
        /// <para/>
        /// 默认为 <see cref="AddDIMode.Add"/>
        /// </summary>
        public AddDIMode AddDIMode { get; set; } = AddDIMode.Add;

        /// <summary>
        /// 导出类型发现器类型
        /// <para/>
        /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
        /// </summary>
        public Type? ExportTypeDiscovererType { get; }

        /// <summary>
        /// 导出的服务
        /// </summary>
        public Type[] ExportTypes { get; protected set; }

        /// <summary>
        /// 导出服务的生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        #endregion Public 属性

        #region Public 构造函数

        /// <summary>
        /// <inheritdoc cref="ExportServicesAttribute"/>
        /// </summary>
        /// <param name="lifetime">导出服务的生命周期</param>
        /// <param name="exportTypes"></param>
        public ExportServicesAttribute(ServiceLifetime lifetime, params Type[] exportTypes)
        {
            Lifetime = lifetime;
            ExportTypes = exportTypes.IsNullOrEmpty()
                            ? throw new ArgumentOutOfRangeException(nameof(exportTypes), 0, "exportTypes cannot be less than 1 item.")
                            : exportTypes;
        }

        /// <summary>
        /// <inheritdoc cref="ExportServicesAttribute"/>
        /// </summary>
        /// <param name="lifetime">导出服务的生命周期</param>
        /// <param name="addMode">添加到DI容器的方式</param>
        /// <param name="exportTypes"></param>
        public ExportServicesAttribute(ServiceLifetime lifetime, AddDIMode addMode, params Type[] exportTypes) : this(lifetime, exportTypes)
        {
            AddDIMode = addMode;
        }

        /// <summary>
        /// <inheritdoc cref="ExportServicesAttribute"/>
        /// </summary>
        /// <param name="lifetime">导出服务的生命周期</param>
        /// <param name="exportTypeDiscovererType">
        /// 导出类型发现器类型
        /// <para/>
        /// 需继承类型 <see cref="IExportTypeDiscoverer"/>
        /// </param>
        /// <param name="addMode">添加到DI容器的方式</param>
        public ExportServicesAttribute(ServiceLifetime lifetime, Type exportTypeDiscovererType, AddDIMode addMode)
        {
            Lifetime = lifetime;
            ExportTypeDiscovererType = exportTypeDiscovererType ?? throw new ArgumentNullException(nameof(exportTypeDiscovererType));

            exportTypeDiscovererType.ThrowIfNotInherit<IExportTypeDiscoverer>(nameof(exportTypeDiscovererType));

            ExportTypes = Array.Empty<Type>();

            AddDIMode = addMode;
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public virtual IEnumerable<Type> GetExportServiceTypes(Type targetType)
        {
            TargetType = targetType;

            if (ExportTypeDiscovererType is null)
            {
                return ExportTypes;
            }
            else
            {
                var exportTypeDiscoverer = ExportTypeDiscovererType.ConstructInstance<IExportTypeDiscoverer>();

                if (ExportTypes.IsNullOrEmpty())
                {
                    return exportTypeDiscoverer.DiscoveryExportTypes(targetType);
                }
                else
                {
                    return ExportTypes.Concat(exportTypeDiscoverer.DiscoveryExportTypes(targetType));
                }
            }
        }

        #endregion Public 方法
    }
}