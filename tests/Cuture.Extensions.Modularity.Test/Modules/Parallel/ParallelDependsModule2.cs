
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule))]
    public class ParallelDependsModule2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModuleAsync))]
    public class ParallelDependsModule2Async : AsyncCountableModuleBase
    {
    }

}
