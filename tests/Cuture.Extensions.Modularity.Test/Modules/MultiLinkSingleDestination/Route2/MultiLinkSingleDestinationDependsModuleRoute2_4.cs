
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_3))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_4 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_3Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_4Async : AsyncCountableModuleBase
    {
    }

}
