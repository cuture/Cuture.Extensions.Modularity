// <Auto-Generated></Auto-Generated>

using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApiHostSample
{
    public class Startup
    {
#if SlimStartup

        //精简的Startup类示例
        //将Asp.net的ConfigureServices和Configure操作都移动到模块中进行配置

        public void ConfigureServices(IServiceCollection services)
        {
            //IApplicationBuilder无法注入到DI容器，想访问时需要额外操作.
            //可以使用ObjectAccessor实现访问，或者使用初始化上下文传递

            //添加IApplicationBuilder的访问器，以便可以在模块中进行访问
            services.AddObjectAccessor<IApplicationBuilder>();

            var module3Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleModule3.dll");
            var module5Directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            //加载模块
            services.LoadModule<AspnetSampleModule>()
                    .AddModuleFile(module3Path) //从文件加载
                    .AddModuleDirectory(source =>
                    {
                        source.SearchDepth = 5;    //设置文件夹搜索深度
                    }, module5Directory)  //从文件夹加载
                    .AutoBindModuleOptions()    //自动使用 IConfiguration 绑定标记了 AutoRegisterServicesInAssemblyAttribute 的模块中继承了 IOptions<TOptions> 的类
                    .ModuleLoadComplete();  //完成加载
        }

        public void Configure(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

            //为IApplicationBuilder的访问器设置值，以便可以在模块中进行访问
            serviceProvider.SetObjectAccessorValue(app);

            //将IApplicationBuilder作为键app，传递到初始化上下文
            serviceProvider.InitializationModules("app", app);

            //移除对象访问器的值，使其不再引用IApplicationBuilder
            serviceProvider.RemoveObjectAccessorValue<IApplicationBuilder>();
        }

#else

        //普通的Startup类示例
        //Asp.net默认的配置方式

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //在Asp.net配置完成后加载模块
            services.LoadModule<AspnetSampleModule>().ModuleLoadComplete();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //在Configure中初始化模块
            app.ApplicationServices.InitializationModules();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

#endif
    }
}
