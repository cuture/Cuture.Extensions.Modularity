﻿// <Auto-Generated></Auto-Generated>

using System;
using System.Collections.Generic;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

using SampleModule2;

using SampleModule4;

using Volo.Abp.Json;

namespace OtherModuleSystemAdaptSample
{
    [DependsOn(
        typeof(SampleModule2.SampleModule2Module),
        typeof(SampleModule1.SampleModule1Module)
        )]
    public class AdaptSampleModule : AppModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            var configure = ServiceCollectionConfigurationExtensions.GetConfiguration(services);

            Console.WriteLine($"Current User: {configure["USERNAME"] ?? configure["USER"]}");
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Console.WriteLine("已注册的服务：");
            foreach (var item in context.Services)
            {
                Console.WriteLine(item.ServiceType);
            }
            Console.WriteLine("--------------------------\r\n");
            base.PostConfigureServices(context);
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

            var dic = new Dictionary<string, string>() {
                {"key1","value1" },
                {"key2","value2" },
                {"key3","value3" },
                {"key4","value4" },
            };

            var jsonSerializer = serviceProvider.GetRequiredService<IJsonSerializerProvider>();

            Console.WriteLine($"这是使用abp的{nameof(IJsonSerializerProvider)}序列化的字符串: { jsonSerializer.Serialize(dic)}\r\n");

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("注意：abp模块集成未在生成环境下进行严格测试，请自行测试确保正确性！！");
            Console.ForegroundColor = color;
        }
    }
}
