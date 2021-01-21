
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_3))]
    public class CyclicDependsModuleRoute2_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute2_3Async))]
    public class CyclicDependsModuleRoute2_2Async : AsyncCountableModuleBase
    {
    }

}
