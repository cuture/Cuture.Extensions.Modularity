
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_3))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_Start : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_3Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_StartAsync : AsyncCountableModuleBase
    {
    }

}
