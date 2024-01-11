using System.Linq;
using System.Reflection;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test.ModuleSources;

[TestClass]
public class AssemblyModuleSourceTest : FileBaseModuleSourceTestBase
{
    #region Public 方法

    [TestMethod]
    public void GeneralLoad()
    {
        Assembly assembly = GetSampleModule1Assembly();
        var assemblyModuleSource = new AssemblyModuleSource(assembly);

        Assert.AreEqual(1, assemblyModuleSource.OriginAssemblies.Count);
        Assert.AreEqual(assembly, assemblyModuleSource.OriginAssemblies[0]);

        var modules = assemblyModuleSource.GetModules().ToArray();
        Assert.AreEqual(1, modules.Length);
        Assert.AreEqual("SampleModule1Module", modules.First().Name);
    }

    [TestInitialize]
    public override void InitDIContainer()
    {
        base.InitDIContainer();

        AutoResolutionsModuleDllMissing();
    }

    [TestMethod]
    public void LoadIntoServices()
    {
        Assembly assembly = GetSampleModule1Assembly();

        Services.LoadModule(new AssemblyModuleSource(assembly), options => options.AddModuleAsService = true).ModuleLoadComplete();

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
        Assembly assembly = GetSampleModule1Assembly();
        var assemblyModuleSource = new AssemblyModuleSource(assembly)
        {
            AssemblyFilter = m => false,
        };

        Assert.AreEqual(1, assemblyModuleSource.OriginAssemblies.Count);
        Assert.AreEqual(assembly, assemblyModuleSource.OriginAssemblies[0]);

        var modules = assemblyModuleSource.GetModules().ToArray();
        Assert.AreEqual(0, modules.Length);
    }

    #endregion Public 方法

    #region Protected 方法

    protected static Assembly GetSampleModule1Assembly()
    {
        return GetAssemblyByFile(GetSampleModule1FilePath());
    }

    #endregion Protected 方法
}
