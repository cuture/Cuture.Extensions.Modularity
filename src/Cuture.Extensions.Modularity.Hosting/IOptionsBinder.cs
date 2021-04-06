using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 选项绑定器
    /// </summary>
    public interface IOptionsBinder
    {
        #region Public 方法

        /// <summary>
        /// 绑定程序集内的Options类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        void BindOptionsInAssembly(IServiceCollection services, Assembly assembly);

        #endregion Public 方法
    }
}