
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_2))]
    public class MultiLinkDependsModuleRoute1_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_2Async))]
    public class MultiLinkDependsModuleRoute1_1Async : AsyncCountableModuleBase
    {
    }

}
