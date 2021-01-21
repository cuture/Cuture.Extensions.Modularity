
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule3))]
    [DependsOn(typeof(ChaosDependsModule6))]
    public class ChaosDependsModuleOrigin2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule3Async))]
    [DependsOn(typeof(ChaosDependsModule6Async))]
    public class ChaosDependsModuleOrigin2Async : AsyncCountableModuleBase
    {
    }

}
