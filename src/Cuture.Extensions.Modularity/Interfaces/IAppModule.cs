﻿// <Auto-Generated></Auto-Generated>

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 应用模块
    /// </summary>
    public interface IAppModule
    {
    }

    /// <summary>
    /// 异步初始化的应用模块
    /// </summary>
    public interface IAsyncAppModule :
        IAppModule,
        IPreConfigureServicesAsync,
        IConfigureServicesAsync,
        IPostConfigureServicesAsync,
        IOnPreApplicationInitializationAsync,
        IOnApplicationInitializationAsync,
        IOnPostApplicationInitializationAsync,
        IOnApplicationShutdownAsync
    {
    }

    /// <summary>
    /// 同步初始化的应用模块
    /// </summary>
    public interface ISyncAppModule :
        IAppModule,
        IPreConfigureServices,
        IConfigureServices,
        IPostConfigureServices,
        IOnPreApplicationInitialization,
        IOnApplicationInitialization,
        IOnPostApplicationInitialization,
        IOnApplicationShutdown
    {
    }
}