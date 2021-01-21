
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{

    [DependsOn(typeof(CyclicDependsModuleRoute3_1))]
    public class CyclicDependsModuleRoute3_3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{

    [DependsOn(typeof(CyclicDependsModuleRoute3_1Async))]
    public class CyclicDependsModuleRoute3_3Async : AsyncCountableModuleBase
    {
    }

}
