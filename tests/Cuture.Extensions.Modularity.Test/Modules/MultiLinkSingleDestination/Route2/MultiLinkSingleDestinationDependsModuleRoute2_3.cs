
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_2))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_2Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_3Async : AsyncCountableModuleBase
    {
    }

}
