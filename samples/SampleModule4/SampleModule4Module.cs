using System;
using System.Threading;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

namespace SampleModule4;

public class SampleModule4Module : AppModule, IOnPostApplicationInitializationAsync
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IHelloable, Module4Helloable>();
    }

    public async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var helloable = context.ServiceProvider.GetRequiredService<IHelloable>();
        if (helloable is Module4Helloable module4Helloable)
        {
            await module4Helloable.InitAsync();
        }
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        Console.WriteLine($"Wait {nameof(SampleModule4Module)} a moment.");
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }
}
