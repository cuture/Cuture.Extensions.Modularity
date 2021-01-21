
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_1))]
    public class CyclicDependsModule2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_1Async))]
    public class CyclicDependsModule2Async : AsyncCountableModuleBase
    {
    }

}
