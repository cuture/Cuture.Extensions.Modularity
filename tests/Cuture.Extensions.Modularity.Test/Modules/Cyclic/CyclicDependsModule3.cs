
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_1))]
    public class CyclicDependsModule3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_1Async))]
    public class CyclicDependsModule3Async : AsyncCountableModuleBase
    {
    }

}
