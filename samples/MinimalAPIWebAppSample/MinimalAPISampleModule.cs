﻿// <Auto-Generated></Auto-Generated>

using Cuture.Extensions.Modularity;

namespace MinimalAPIWebAppSample
{
    [DependsOn(
        typeof(SampleModule2.SampleModule2Module),
        typeof(SampleModule1.SampleModule1Module)
        )]
    public class MinimalAPISampleModule : AppModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Console.WriteLine("Slim Startup Class ConfigureServices");

            var services = context.Services;
            var configure = services.GetConfiguration();

            Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {configure["ASPNETCORE_ENVIRONMENT"]}");

            services.AddControllers();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.WriteLine("Slim Startup Class OnApplicationInitialization");

            var serviceProvider = context.ServiceProvider;

            //使用ObjectAccessor获取IApplicationBuilder
            var app = serviceProvider.GetObjectAccessorValue<IApplicationBuilder>();
            //使用初始化上下文获取IApplicationBuilder
            app = context.Value<IApplicationBuilder>("app");

            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        }
    }
}