using System;

using Cuture.Extensions.Modularity;

namespace SampleModule5
{
    [DependsOn(typeof(SampleModule2.SampleModule2Module))]
    [AutoRegisterServicesInAssembly]
    public class SampleModule5Module : IAppModule, IOnApplicationShutdown
    {
        public void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            Console.WriteLine("OnApplicationShutdown SampleModule5Module");
        }
    }
}
