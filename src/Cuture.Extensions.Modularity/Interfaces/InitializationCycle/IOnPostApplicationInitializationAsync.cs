﻿// <Auto-Generated></Auto-Generated>

using System.Threading.Tasks;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// <inheritdoc cref="IAppModule"/>生命周期接口 - <inheritdoc cref="OnPostApplicationInitializationAsync"/>
    /// </summary>
    public interface IOnPostApplicationInitializationAsync
    {
        /// <summary>
        /// 初始化应用后（包含异步操作）
        /// </summary>
        /// <param name="context"></param>
        Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context);
    }
}
