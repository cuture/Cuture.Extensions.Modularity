
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    //回到路径2上
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_2))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    //回到路径2上
    [DependsOn(typeof(MultiLinkSingleDestinationDependsModuleRoute2_2Async))]
    public class MultiLinkSingleDestinationDependsModuleRoute3_1Async : AsyncCountableModuleBase
    {
    }

}
