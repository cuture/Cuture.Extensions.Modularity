using Microsoft.Extensions.DependencyInjection;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 添加到DI容器的方式
    /// </summary>
    public enum AddDIMode
    {
        /// <summary>
        /// 默认 <see cref="Add"/>
        /// </summary>
        Default = Add,

        /// <summary>
        /// 直接添加
        /// <para/>
        /// 注入时使用 <see cref="IServiceCollection"/>.Add
        /// </summary>
        Add = 0,

        /// <summary>
        /// 尝试添加
        /// <para/>
        /// 注入时使用 <see cref="IServiceCollection"/>.TryAdd
        /// </summary>
        TryAdd = 1,

        /// <summary>
        /// 替换
        /// <para/>
        /// 注入时使用 <see cref="IServiceCollection"/>.Replace
        /// </summary>
        Replace = 2,
    }
}