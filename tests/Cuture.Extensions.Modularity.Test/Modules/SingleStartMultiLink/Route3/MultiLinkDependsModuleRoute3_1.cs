
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute3_2))]
    public class MultiLinkDependsModuleRoute3_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(MultiLinkDependsModuleRoute3_2Async))]
    public class MultiLinkDependsModuleRoute3_1Async : AsyncCountableModuleBase
    {
    }

}
