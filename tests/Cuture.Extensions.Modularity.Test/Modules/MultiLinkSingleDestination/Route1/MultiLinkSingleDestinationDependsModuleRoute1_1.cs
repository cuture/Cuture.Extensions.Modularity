
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(NoneDependsModule))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(NoneDependsModuleAsync))]
    public class MultiLinkSingleDestinationDependsModuleRoute1_1Async : AsyncCountableModuleBase
    {
    }

}
