
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_5))]
    public class MultiLinkDependsModuleRoute2_4 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute2_5Async))]
    public class MultiLinkDependsModuleRoute2_4Async : AsyncCountableModuleBase
    {
    }

}
