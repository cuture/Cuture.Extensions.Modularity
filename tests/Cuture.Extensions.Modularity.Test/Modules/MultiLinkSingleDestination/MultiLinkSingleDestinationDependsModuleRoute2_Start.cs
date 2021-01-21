
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_5))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_Start : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_5Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_StartAsync : AsyncCountableModuleBase
    {
    }

}
