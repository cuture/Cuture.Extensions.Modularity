
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_3))]
    public class CyclicDependsModuleRoute3_2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModuleRoute3_3Async))]
    public class CyclicDependsModuleRoute3_2Async : AsyncCountableModuleBase
    {
    }

}
