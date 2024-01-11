﻿// <Auto-Generated></Auto-Generated>

using System;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

using SampleModule2;

using SampleModule4;

namespace UseHostSample;

[DependsOn(
    typeof(SampleModule2.SampleModule2Module),
    typeof(SampleModule1.SampleModule1Module)
    )]
public class HostSampleModule : AppModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configure = services.GetConfiguration();

        Console.WriteLine($"Current User: {configure["USERNAME"] ?? configure["USER"]}");
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
        var serviceProvider = context.ServiceProvider;

        Console.WriteLine();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"------------- {i} -------------");
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var accessCounter = serviceScope.ServiceProvider.GetRequiredService<IAccessCounter>();
                accessCounter.Add();

                Console.WriteLine($"hello: {serviceScope.ServiceProvider.GetRequiredService<IHelloable>().SayHello()}");
                Console.WriteLine($"accessCount: {accessCounter.Count}");
                Console.WriteLine($"scopedRandom1: {serviceScope.ServiceProvider.GetRequiredService<IRequestRandomProvider>().Random()}");
                Console.WriteLine($"scopedRandom2: {serviceScope.ServiceProvider.GetRequiredService<IRequestRandomProvider>().Random()}");
                Console.WriteLine($"random1: {serviceScope.ServiceProvider.GetRequiredService<IRandomProvider>().Random()}");
                Console.WriteLine($"random3: {serviceScope.ServiceProvider.GetRequiredService<IRandomProvider>().Random()}");
            }
            Console.WriteLine("\r\n");
        }
    }
}
