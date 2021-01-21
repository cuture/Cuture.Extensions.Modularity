
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule2))]
    public class CyclicDependsModuleRoute2_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule2Async))]
    public class CyclicDependsModuleRoute2_3Async : AsyncCountableModuleBase
    {
    }

}
