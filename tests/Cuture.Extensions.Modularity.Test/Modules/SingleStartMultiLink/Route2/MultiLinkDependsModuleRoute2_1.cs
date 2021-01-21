
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_2))]
    public class MultiLinkDependsModuleRoute2_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_2Async))]
    public class MultiLinkDependsModuleRoute2_1Async : AsyncCountableModuleBase
    {
    }

}
