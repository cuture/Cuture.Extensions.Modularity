
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    // 不设置依赖
    [DependsOn(null)]
    public class MultiLinkDependsModuleRoute1_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    // 不设置依赖
    [DependsOn(null)]
    public class MultiLinkDependsModuleRoute1_3Async : AsyncCountableModuleBase
    {
    }

}
