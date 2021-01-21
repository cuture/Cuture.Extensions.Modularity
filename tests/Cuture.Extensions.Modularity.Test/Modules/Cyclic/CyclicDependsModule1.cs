
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute1_1))]
    public class CyclicDependsModule1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute1_1Async))]
    public class CyclicDependsModule1Async : AsyncCountableModuleBase
    {
    }

}
