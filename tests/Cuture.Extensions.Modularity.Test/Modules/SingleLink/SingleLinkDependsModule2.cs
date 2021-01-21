
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule3))]
    public class SingleLinkDependsModule2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule3Async))]
    public class SingleLinkDependsModule2Async : AsyncCountableModuleBase
    {
    }

}
