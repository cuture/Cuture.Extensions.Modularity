
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_2))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_2Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_3Async : AsyncCountableModuleBase
    {
    }

}
