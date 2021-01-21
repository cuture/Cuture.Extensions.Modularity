
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_3))]
    public class MultiLinkDependsModuleRoute1_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute1_3Async))]
    public class MultiLinkDependsModuleRoute1_2Async : AsyncCountableModuleBase
    {
    }

}
