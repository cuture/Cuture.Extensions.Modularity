using System;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 禁止自动注册模块所在程序集中导出的服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DisableAssemblyServicesRegisterAttribute : Attribute
    {
        #region Public 构造函数

        /// <inheritdoc cref="DisableAssemblyServicesRegisterAttribute"/>
        public DisableAssemblyServicesRegisterAttribute()
        {
        }

        #endregion Public 构造函数
    }
}