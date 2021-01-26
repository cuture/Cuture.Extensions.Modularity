using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 聚合的<inheritdoc cref="IModuleDescriptorBuilder"/>
    /// </summary>
    public class AggregatedModulesBootstrapInterceptor : IModulesBootstrapInterceptor
    {
        #region Private 字段

        private List<IModulesBootstrapInterceptor> _modulesBootstrapInterceptors;

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        ///
        /// </summary>
        public IReadOnlyList<IModulesBootstrapInterceptor> ModulesBootstrapInterceptors { get => _modulesBootstrapInterceptors; protected set => _modulesBootstrapInterceptors = value.ToList(); }

        #endregion Public 属性

        #region Public 构造函数

        /// <inheritdoc cref="AggregatedModulesBootstrapInterceptor"/>
        public AggregatedModulesBootstrapInterceptor(IEnumerable<IModulesBootstrapInterceptor>? interceptors = null)
        {
            if (interceptors is null)
            {
                _modulesBootstrapInterceptors = new List<IModulesBootstrapInterceptor>();
            }
            else
            {
                _modulesBootstrapInterceptors = new List<IModulesBootstrapInterceptor>(interceptors);
            }
        }

        #endregion Public 构造函数

        #region Public 方法

        /// <inheritdoc/>
        public async Task<bool> RegisteringServicesInAssemblyAsync(IServiceCollection services, Assembly assembly)
        {
            foreach (var item in ModulesBootstrapInterceptors)
            {
                if (!await item.RegisteringServicesInAssemblyAsync(services, assembly))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion Public 方法
    }
}