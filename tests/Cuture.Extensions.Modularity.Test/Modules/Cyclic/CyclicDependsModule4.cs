
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule4))]
    public class CyclicDependsModule4 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(CyclicDependsModule4Async))]
    public class CyclicDependsModule4Async : AsyncCountableModuleBase
    {
    }

}
