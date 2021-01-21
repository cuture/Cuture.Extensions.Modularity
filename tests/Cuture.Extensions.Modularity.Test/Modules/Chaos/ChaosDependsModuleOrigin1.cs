
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule1))]
    public class ChaosDependsModuleOrigin1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule1Async))]
    public class ChaosDependsModuleOrigin1Async : AsyncCountableModuleBase
    {
    }

}
