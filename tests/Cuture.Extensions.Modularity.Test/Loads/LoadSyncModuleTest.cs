﻿// <Auto-Generated></Auto-Generated>

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test.Loads;

[TestClass]
public class LoadSyncModuleTest : LoadTestBase
{
    [TestMethod]
    [DataRow(typeof(ChaosDependsModuleOrigin1), typeof(ChaosDependsModuleOrigin2), typeof(ChaosDependsModuleStandard1), typeof(ChaosDependsModuleStandard2), typeof(ChaosDependsModuleStandard3))]  //Chaos
    [DataRow(typeof(ChildDependsModule1), typeof(ChildDependsModule3), typeof(ChildDependsModule5), typeof(ChildDependsModule7), typeof(ChildDependsModule9))]  //Child
    [DataRow(typeof(MultiLinkSingleDestinationDependsModuleRoute1_Start), typeof(MultiLinkSingleDestinationDependsModuleRoute2_Start), typeof(MultiLinkSingleDestinationDependsModuleRoute3_Start))] //MultiLinkSingleDestination
    [DataRow(typeof(NoneDependsModule))] //NoneDepends
    [DataRow(typeof(ParallelDependsModule1), typeof(ParallelDependsModule2))] //Parallel
    [DataRow(typeof(SingleDependsModule))] //SingleLayer
    [DataRow(typeof(SingleLinkDependsModule))] //SingleLink
    [DataRow(typeof(SingleStartMultiLinkDependsModule))] //SingleStartMultiLink
    public void Check(params Type[] types)
    {
        LoadInitModules(types);
        ShutdownModules();

        AutoCheckModuleConfigureOrder(types);
    }
}
