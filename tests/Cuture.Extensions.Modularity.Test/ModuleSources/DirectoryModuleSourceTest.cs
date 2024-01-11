using System.IO;
using System.Linq;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test.ModuleSources;

[TestClass]
public class DirectoryModuleSourceTest : FileBaseModuleSourceTestBase
{
    #region Public 方法

    [TestMethod]
    public void GeneralLoad_Depth0()
    {
        LoadDirectory(0, 0);
    }

    [TestMethod]
    public void GeneralLoad_Depth1()
    {
        LoadDirectory(1, 5);
    }

    [TestMethod]
    public void LoadIntoServices()
    {
        var directoryModuleSource = LoadDirectory(5, 5);

        Services.LoadModule(directoryModuleSource, options => options.AddModuleAsService = true).ModuleLoadComplete();

        var assembly = GetAssemblyByFile(GetSampleModule1FilePath());

        var modules = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(assembly.GetTypes().FirstOrDefault(m => typeof(IAppModule).IsAssignableFrom(m)));

        var sortedModules = AppModuleDependencyUtil.SortModuleDescriptors(modules);

        foreach (var module in sortedModules)
        {
            Assert.IsTrue(Services.Any(m => m.ServiceType == module.Type));
        }
    }

    [TestMethod]
    public void LoadWithFilter_1()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            DirectoryFilter = m => false
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(0, modules.Length);
    }

    [TestMethod]
    public void LoadWithFilter_2()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            FileFilter = m => false
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(0, modules.Length);
    }

    [TestMethod]
    public void LoadWithFilter_3()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            AssemblyFilter = m => false,
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(0, modules.Length);
    }

    [TestMethod]
    public void LoadWithFilter_4()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            FileFilter = m => m.Contains(Path.GetFileName(GetSampleModule1FilePath()))
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    [TestMethod]
    public void LoadWithFilter_5()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            AssemblyFilter = m => m.Location.Contains(Path.GetFileName(GetSampleModule1FilePath()))
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    [TestMethod]
    public void LoadWithFilter_6()
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = 5,
            TypeFilter = m => m.FullName.Contains(Path.GetFileNameWithoutExtension(GetSampleModule1FilePath()))
        };

        var modules = directoryModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    #endregion Public 方法

    #region Private 方法

    private static string GetModuleDirectory()
    {
        return "./plugins";
    }

    private static DirectoryModuleSource LoadDirectory(int searchDepth, int moduleCount)
    {
        var directoryModuleSource = new DirectoryModuleSource(GetModuleDirectory())
        {
            SearchDepth = searchDepth,
        };

        Assert.AreEqual(1, directoryModuleSource.OriginDirectories.Count);

        var modules = directoryModuleSource.GetModules().ToArray();
        if (moduleCount > -1)
        {
            Assert.AreEqual(moduleCount, modules.Length);
        }
        if (moduleCount > 0)
        {
            Assert.AreEqual("SampleModule1Module", modules.First().Name);
        }

        return directoryModuleSource;
    }

    #endregion Private 方法
}
