
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_2))]
    public class CyclicDependsModuleRoute3_1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_2Async))]
    public class CyclicDependsModuleRoute3_1Async : AsyncCountableModuleBase
    {
    }

}
