
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule1))]
    public class CyclicDependsModuleRoute1_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule1Async))]
    public class CyclicDependsModuleRoute1_1Async : AsyncCountableModuleBase
    {
    }

}
