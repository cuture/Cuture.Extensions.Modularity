using System;
using System.Threading;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

namespace SampleModule1;

[DependsOn(
    typeof(SampleModule2.SampleModule2Module)
    )]
public class SampleModule1Module : AppModule, IOnApplicationShutdownAsync
{
    public async Task OnApplicationShutdownAsync(ApplicationShutdownContext context, CancellationToken token)
    {
        Console.WriteLine($"Wait {nameof(SampleModule1Module)} a moment.");
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}
