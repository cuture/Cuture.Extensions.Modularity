
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleDependsModule))]
    public class ParallelDependsModule1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleDependsModuleAsync))]
    public class ParallelDependsModule1Async : AsyncCountableModuleBase
    {
    }

}
