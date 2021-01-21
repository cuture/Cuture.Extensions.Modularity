
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_1))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_1Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_2Async : AsyncCountableModuleBase
    {
    }

}
