
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule2))]
    public class SingleLinkDependsModule1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule2Async))]
    public class SingleLinkDependsModule1Async : AsyncCountableModuleBase
    {
    }

}
