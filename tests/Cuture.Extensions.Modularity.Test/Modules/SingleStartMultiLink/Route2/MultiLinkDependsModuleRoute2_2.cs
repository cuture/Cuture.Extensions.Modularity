
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_3))]
    public class MultiLinkDependsModuleRoute2_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_3Async))]
    public class MultiLinkDependsModuleRoute2_2Async : AsyncCountableModuleBase
    {
    }

}
