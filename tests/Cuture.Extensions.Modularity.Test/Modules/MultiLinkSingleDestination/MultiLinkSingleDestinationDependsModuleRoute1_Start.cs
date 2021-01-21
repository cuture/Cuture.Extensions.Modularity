
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_3))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_Start : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute1_3Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_StartAsync : AsyncCountableModuleBase
    {
    }

}
