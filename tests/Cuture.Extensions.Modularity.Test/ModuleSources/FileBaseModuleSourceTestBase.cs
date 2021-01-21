using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test.ModuleSources
{
    /// <summary>
    /// 基于文件的ModuleSource测试基类
    /// </summary>
    [TestClass]
    public abstract class FileBaseModuleSourceTestBase : DIContainerTestBase
    {
        #region Protected 方法

        protected static Assembly GetAssemblyByFile(string filePath)
        {
            return Assembly.LoadFrom(filePath);
        }

        protected static string GetSampleModule1FilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./plugins/linked/SampleModule1.dll");
        }

        protected static string GetSampleModule2FilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./plugins/linked/SampleModule2.dll");
        }

        protected static string GetSampleModule3FilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./plugins/linked/SampleModule3.dll");
        }

        protected static string GetSampleModule4FilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./plugins/linked/SampleModule4.dll");
        }

        protected static string GetSampleModule5FilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./plugins/linked/SampleModule5.dll");
        }

        protected virtual void AutoResolutionsModuleDllMissing()
        {
            //处理dll的加载
            var dllFiles = Directory.EnumerateFiles("./plugins/linked/", "*.dll").ToArray();
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                if (dllFiles.FirstOrDefault(m => m.Contains($"{e.Name.Split(',')[0]}.dll")) is string dllFile)
                {
                    Console.WriteLine($"自动处理了关联Assembly加载：{e.Name} File：{dllFile}");
                    dllFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllFile);
                    return Assembly.LoadFrom(dllFile);
                }
                return null;
            };
        }

        #endregion Protected 方法
    }
}