
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_1))]
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_1))]
    [DependsOn(typeof(MultiLinkDependsModuleRoute3_1))]
    public class SingleStartMultiLinkDependsModule : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_1Async))]
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_1Async))]
    [DependsOn(typeof(MultiLinkDependsModuleRoute3_1Async))]
    public class SingleStartMultiLinkDependsModuleAsync : AsyncCountableModuleBase
    {
    }

}
