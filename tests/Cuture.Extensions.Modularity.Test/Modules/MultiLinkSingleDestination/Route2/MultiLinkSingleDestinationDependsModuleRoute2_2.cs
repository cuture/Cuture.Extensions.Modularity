
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_1))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_1Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_2Async : AsyncCountableModuleBase
    {
    }

}
