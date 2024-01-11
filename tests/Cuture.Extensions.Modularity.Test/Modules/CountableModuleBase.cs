using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test;

public abstract class CountableModuleBase : AppModule
{
    #region Private 字段

    private static readonly DiagnosticSource _diagnosticSource = new DiagnosticListener("ModuleMethodInvoked");
    private static readonly Random _random = new Random();

    #endregion Private 字段

    #region Public 属性

    public List<KeyValuePair<string, string>> MethodLog { get; set; } = new();

    #endregion Public 属性

    #region Public 构造函数

    public CountableModuleBase()
    {
        LogMethodInvoked();
    }

    #endregion Public 构造函数

    #region Public 方法

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        base.ConfigureServices(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        base.OnApplicationInitialization(context);
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        LogMethodInvoked();
        base.OnApplicationShutdown(context);
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        base.OnPostApplicationInitialization(context);
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        LogMethodInvoked();
        base.OnPreApplicationInitialization(context);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        base.PostConfigureServices(context);
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        LogMethodInvoked();
        base.PreConfigureServices(context);
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
        await Task.Delay(_random.Next(0, 500));
    }

    #endregion Protected 方法
}
