
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_4))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_5 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_4Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute2_5Async : AsyncCountableModuleBase
    {
    }

}
