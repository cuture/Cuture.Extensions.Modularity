using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test;

public abstract class AsyncCountableModuleBase : AsyncAppModule
{
    #region Private 字段

    private static readonly DiagnosticSource _diagnosticSource = new DiagnosticListener("ModuleMethodInvoked");
    private static readonly Random _random = new Random();

    #endregion Private 字段

    #region Public 属性

    public List<KeyValuePair<string, string>> MethodLog { get; set; } = [];

    #endregion Public 属性

    #region Public 构造函数

    public AsyncCountableModuleBase()
    {
        LogMethodInvoked();
    }

    #endregion Public 构造函数

    #region Public 方法

    public override async Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        await base.ConfigureServicesAsync(context);
        await RandomWaitAsync();
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        await base.OnApplicationInitializationAsync(context);
        await RandomWaitAsync();
    }

    public override async Task OnApplicationShutdownAsync(ApplicationShutdownContext context, CancellationToken token)
    {
        LogMethodInvoked();
        await base.OnApplicationShutdownAsync(context, token);
        await RandomWaitAsync();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        await base.OnPostApplicationInitializationAsync(context);
        await RandomWaitAsync();
    }

    public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        await base.OnPreApplicationInitializationAsync(context);
        await RandomWaitAsync();
    }

    public override async Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        await base.PostConfigureServicesAsync(context);
        await RandomWaitAsync();
    }

    public override async Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        await base.PreConfigureServicesAsync(context);
        await RandomWaitAsync();
    }

    #endregion Public 方法

    #region Protected 方法

    protected void LogMethodInvoked([CallerMemberName] string methodName = null)
    {
        if (_diagnosticSource.IsEnabled("MethodLog"))
        {
            var typeName = GetType().Name;
            _diagnosticSource.Write("MethodLog", new { typeName, methodName });
        }
    }

    protected async Task RandomWaitAsync()
    {
        if (_random.Next() % 5 < 4)
        {
            //大多是时候不模拟等待
            await Task.CompletedTask;
            return;
        }
        await Task.Delay(_random.Next(0, 100));
    }

    #endregion Protected 方法
}
