
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    // 不设置依赖
    [DependsOn()]
    public class MultiLinkDependsModuleRoute3_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    // 不设置依赖
    [DependsOn()]
    public class MultiLinkDependsModuleRoute3_2Async : AsyncCountableModuleBase
    {
    }

}
