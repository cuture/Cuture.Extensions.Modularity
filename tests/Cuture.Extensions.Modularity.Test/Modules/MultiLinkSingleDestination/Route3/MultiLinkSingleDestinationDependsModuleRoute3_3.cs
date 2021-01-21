
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_2))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute3_2Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_3Async : AsyncCountableModuleBase
    {
    }

}
