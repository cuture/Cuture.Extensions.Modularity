
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_2))]
    public class CyclicDependsModuleRoute2_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_2Async))]
    public class CyclicDependsModuleRoute2_1Async : AsyncCountableModuleBase
    {
    }

}
