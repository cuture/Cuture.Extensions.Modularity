
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_4))]
    public class MultiLinkDependsModuleRoute2_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_4Async))]
    public class MultiLinkDependsModuleRoute2_3Async : AsyncCountableModuleBase
    {
    }

}
