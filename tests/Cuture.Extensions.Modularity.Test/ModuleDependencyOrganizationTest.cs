using System;
using System.Collections.Generic;
using System.Linq;

using Cuture.Extensions.Modularity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test;

[TestClass]
public class ModuleDependencyOrganizationTest
{
    #region Public 方法

    [TestMethod]
    public void ChaosDepends()
    {
        var types = new[] { typeof(ChaosDependsModuleOrigin1), typeof(ChaosDependsModuleOrigin2), typeof(ChaosDependsModuleStandard1), typeof(ChaosDependsModuleStandard2), typeof(ChaosDependsModuleStandard3) };

        var moduleDescriptors = CheckAndGetModuleDependencyRoots(14, 3, types);

        CheckDependencyDepth(moduleDescriptors[0], 8);
        CheckDependencyDepth(moduleDescriptors[1], 6);
        CheckDependencyDepth(moduleDescriptors[2], 5);
    }

    [TestMethod]
    public void ChildDepends()
    {
        var types = new[] {
            typeof(ChildDependsModule1),
            typeof(ChildDependsModule3),
            typeof(ChildDependsModule5),
            typeof(ChildDependsModule7),
            typeof(ChildDependsModule9),
        };

        var moduleDescriptors = CheckAndGetModuleDependencyRoots(9, 1, types);
        CheckDependencyDepth(moduleDescriptors.First(), 9);
    }

    [TestMethod]
    public void CyclicDepends()
    {
        Assert.ThrowsException<CyclicDependencyException>(() => CheckAndGetModuleDependencyRoots(-1, -1, typeof(CyclicDependsModule1)));
        Assert.ThrowsException<CyclicDependencyException>(() => CheckAndGetModuleDependencyRoots(-1, -1, typeof(CyclicDependsModule2)));
        Assert.ThrowsException<CyclicDependencyException>(() => CheckAndGetModuleDependencyRoots(-1, -1, typeof(CyclicDependsModule3)));
        Assert.ThrowsException<CyclicDependencyException>(() => CheckAndGetModuleDependencyRoots(-1, -1, typeof(CyclicDependsModule4)));
    }

    [TestMethod]
    public void MultiLinkSingleDestinationDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(15, 3, typeof(MultiLinkSingleDestinationDependsModuleRoute1_Start), typeof(MultiLinkSingleDestinationDependsModuleRoute2_Start), typeof(MultiLinkSingleDestinationDependsModuleRoute3_Start));

        CheckDependencyDepth(moduleDescriptors[0], 5);
        CheckDependencyDepth(moduleDescriptors[1], 7);
        CheckDependencyDepth(moduleDescriptors[2], 7);
    }

    [TestMethod]
    public void NoneDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(1, 1, typeof(NoneDependsModule));

        CheckDependencyDepth(moduleDescriptors.First(), 1);
    }

    [TestMethod]
    public void ParallelDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(9, 2, typeof(ParallelDependsModule1), typeof(ParallelDependsModule2));
        CheckDependencyDepth(moduleDescriptors.First(), 3);
        CheckDependencyDepth(moduleDescriptors.Last(), 5);
    }

    [TestMethod]
    public void SingleLayerDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(3, 1, typeof(SingleDependsModule));

        CheckDependencyDepth(moduleDescriptors.First(), 2);

        Assert.AreEqual(2, moduleDescriptors.First().Dependencies.Count);
    }

    [TestMethod]
    public void SingleLinkDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(4, 1, typeof(SingleLinkDependsModule));
        CheckDependencyDepth(moduleDescriptors.First(), 4);
    }

    [TestMethod]
    public void SingleStartMultiLinkDepends()
    {
        var moduleDescriptors = CheckAndGetModuleDependencyRoots(11, 1, typeof(SingleStartMultiLinkDependsModule));

        var moduleDescriptor = moduleDescriptors.First();

        CheckDependencyDepth(moduleDescriptor, 6);

        Assert.AreEqual(3, moduleDescriptor.Dependencies.Count);

        CheckDependencyDepth(moduleDescriptor.Dependencies[0], 3);
        CheckDependencyDepth(moduleDescriptor.Dependencies[1], 5);
        CheckDependencyDepth(moduleDescriptor.Dependencies[2], 2);
    }

    #endregion Public 方法

    #region func

    /// <summary>
    /// 获取模块的依赖根
    /// </summary>
    /// <param name="sortedModuleCount">排序后的模块数量</param>
    /// <param name="dependencyRootCount">依赖根的数量</param>
    /// <param name="moduleTypes"></param>
    /// <returns></returns>
    private static IModuleDescriptor[] CheckAndGetModuleDependencyRoots(int sortedModuleCount, int dependencyRootCount, params Type[] moduleTypes)
    {
        //模块的顺序不应该影响模块数量，但是初始化顺序是可以不同的。。。
        var reversedRoots = GetModuleDependencyRoots(sortedModuleCount, dependencyRootCount, moduleTypes.Reverse().ToArray());
        var roots = GetModuleDependencyRoots(sortedModuleCount, dependencyRootCount, moduleTypes);

        foreach (var item in reversedRoots)
        {
            Assert.IsTrue(roots.Contains(item));
        }

        return roots;
    }

    /// <summary>
    /// 检查依赖深度
    /// </summary>
    /// <param name="moduleDescriptor"></param>
    /// <param name="depth"></param>
    private static void CheckDependencyDepth(IModuleDescriptor moduleDescriptor, int depth)
    {
        Assert.AreEqual(depth, GetMaxDependencyDepth(moduleDescriptor));
    }

    /// <summary>
    /// 依赖链检查
    /// </summary>
    /// <param name="moduleDescriptors"></param>
    private static void DependencyLinkCheck(IEnumerable<IModuleDescriptor> moduleDescriptors)
    {
        foreach (var moduleDescriptor in moduleDescriptors)
        {
            DependencyNodeCheck(moduleDescriptor);
        }
    }

    /// <summary>
    /// 检查节点
    /// </summary>
    /// <param name="moduleDescriptor"></param>
    private static void DependencyNodeCheck(IModuleDescriptor moduleDescriptor)
    {
        if (moduleDescriptor.Dependencies.Count > 0)
        {
            var dependencies = DefaultModuleDescriptorBuilder.Default.GetDependedModuleTypes(moduleDescriptor.Type);

            Assert.AreEqual(dependencies.Count(), moduleDescriptor.Dependencies.Count);

            foreach (var item in dependencies)
            {
                Assert.IsTrue(moduleDescriptor.Dependencies.Select(md => md.Type).Contains(item));
            }

            foreach (var dependency in moduleDescriptor.Dependencies)
            {
                Assert.IsTrue(dependency.Dependents.Contains(moduleDescriptor));
                DependencyNodeCheck(dependency);
            }
        }
    }

    /// <summary>
    /// 获取最大深度
    /// </summary>
    /// <param name="moduleDescriptor"></param>
    /// <returns></returns>
    private static int GetMaxDependencyDepth(IModuleDescriptor moduleDescriptor)
    {
        if (moduleDescriptor.Dependencies.Count == 0)
        {
            return 1;
        }
        else
        {
            return 1 + moduleDescriptor.Dependencies.Max(GetMaxDependencyDepth);
        }
    }

    /// <summary>
    /// 获取模块的依赖根
    /// </summary>
    /// <param name="sortedModuleCount">排序后的模块数量</param>
    /// <param name="dependencyRootCount">依赖根的数量</param>
    /// <param name="types"></param>
    /// <returns></returns>
    private static IModuleDescriptor[] GetModuleDependencyRoots(int sortedModuleCount, int dependencyRootCount, params Type[] types)
    {
        var moduleDependencyRoots = AppModuleDependencyUtil.GetModuleDependencyRoots(types);
        if (dependencyRootCount >= 0)
        {
            Assert.AreEqual(dependencyRootCount, moduleDependencyRoots.Length);
            DependencyLinkCheck(moduleDependencyRoots);
        }

        Console.WriteLine($"\r\n---------Mermaid DependencyRoots Start----------\r\n{moduleDependencyRoots.ToMermaidString()}\r\n---------Mermaid DependencyRoots End----------\r\n");

        var allModuleDescriptors = AppModuleDependencyUtil.FindAllDependedModuleDescriptors(types);

        Console.WriteLine($"\r\n---------Mermaid allModuleDescriptors Start----------\r\n{allModuleDescriptors.ToMermaidString()}\r\n---------Mermaid allModuleDescriptors End----------\r\n");

        var sortedModuleDescriptors = AppModuleDependencyUtil.SortModuleDescriptors(allModuleDescriptors);

        if (sortedModuleCount >= 0)
        {
            Assert.AreEqual(sortedModuleCount, sortedModuleDescriptors.Length);
            DependencyLinkCheck(allModuleDescriptors);
        }

        Console.WriteLine($"\r\n---------sortedModuleTypes Start----------\r\n{string.Join("\r\n", sortedModuleDescriptors.Select(m => m.Type.Name))}\r\n---------sortedModuleTypes End----------\r\n");

        return moduleDependencyRoots;
    }

    #endregion func
}
