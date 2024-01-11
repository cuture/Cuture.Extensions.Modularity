using System.Linq;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test.ModuleSources;

[TestClass]
public class FileModuleSourceTest : FileBaseModuleSourceTestBase
{
    #region Public 方法

    [TestMethod]
    public void GeneralLoad()
    {
        var filePath = GetSampleModule1FilePath();
        var fileModuleSource = new FileModuleSource(filePath);

        Assert.AreEqual(1, fileModuleSource.OriginFilePaths.Count);
        Assert.AreEqual(filePath, fileModuleSource.OriginFilePaths[0]);

        var modules = fileModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    [TestMethod]
    public void LoadIntoServices()
    {
        var filePath = GetSampleModule1FilePath();

        Services.LoadModule(new FileModuleSource(filePath), options => options.AddModuleAsService = true).ModuleLoadComplete();

        var assembly = GetAssemblyByFile(filePath);

        var modules = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(assembly.GetTypes().FirstOrDefault(m => typeof(IAppModule).IsAssignableFrom(m)));

        var sortedModules = AppModuleDependencyUtil.SortModuleDescriptors(modules);

        foreach (var module in sortedModules)
        {
            Assert.IsTrue(Services.Any(m => m.ServiceType == module.Type));
        }
    }

    [TestMethod]
    public void LoadWithFilter()
    {
        var filePaths = new[] {
            GetSampleModule1FilePath(),
            GetSampleModule2FilePath(),
            GetSampleModule3FilePath(),
            GetSampleModule4FilePath(),
            GetSampleModule5FilePath(),
        };

        var fileModuleSource = new FileModuleSource(filePaths)
        {
            FileFilter = m => m.Contains(GetSampleModule1FilePath())
        };

        Assert.AreEqual(5, fileModuleSource.OriginFilePaths.Count);
        Assert.IsTrue(filePaths.SequenceEqual(fileModuleSource.OriginFilePaths));

        var modules = fileModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    #endregion Public 方法
}
