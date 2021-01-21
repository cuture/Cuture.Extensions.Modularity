
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_1))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_1Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_2Async : AsyncCountableModuleBase
    {
    }

}
