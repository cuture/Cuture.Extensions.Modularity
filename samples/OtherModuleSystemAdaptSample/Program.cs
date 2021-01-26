using System;
using System.IO;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.Hosting;

namespace OtherModuleSystemAdaptSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var module3Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleModule3.dll");
            var module5Directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            var abpModuleDescriptors = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(typeof(Volo.Abp.Json.AbpJsonModule), new AbpAdaptedModuleDescriptorBuilder());

            var mermaidString = abpModuleDescriptors.ToMermaidString(type => type.IsGenericType ? type.GenericTypeArguments[0].FullName : type.FullName);

            Console.WriteLine($"\r\n---------Mermaid DependencyRoots Start----------\r\n{mermaidString}\r\n---------Mermaid DependencyRoots End----------\r\n");

            var abpModuleSource = new TypeModuleSource(typeof(Volo.Abp.Json.AbpJsonModule)) { DescriptorBuilder = new AbpAdaptedModuleDescriptorBuilder() };

            //直接在HostBuilder加载，或者使用ConfigureServices进行加载
            Host.CreateDefaultBuilder(args)
                .LoadModule<AdaptSampleModule>()
                .LoadModuleFile(module3Path) //从文件加载
                .LoadModule(abpModuleSource)
                .LoadModuleDirectory(source =>
                {
                    source.SearchDepth = 5;    //设置文件夹搜索深度
                }, module5Directory)  //从文件夹加载
                .UseConsoleLifetime()
                .InitializationModules()
                .Run();
        }
    }
}
