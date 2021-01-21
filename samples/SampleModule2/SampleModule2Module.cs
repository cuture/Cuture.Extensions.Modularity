using System;
using System.Threading.Tasks;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

using SampleModule4;

namespace SampleModule2
{
    [DependsOn(
        typeof(SampleModule4.SampleModule4Module)
        )]
    [AutoRegisterServicesInAssembly]
    public class SampleModule2Module : AppModule, IOnPostApplicationInitializationAsync
    {
        public async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            var helloable = context.ServiceProvider.GetRequiredService<IHelloable>();
            if (helloable is Module2Helloable module2Helloable)
            {
                await module2Helloable.InitAsync();
            }
        }
    }
}
