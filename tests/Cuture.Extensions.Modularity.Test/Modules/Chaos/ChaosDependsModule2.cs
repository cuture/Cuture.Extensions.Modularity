
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule3))]
    public class ChaosDependsModule2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule3Async))]
    public class ChaosDependsModule2Async : AsyncCountableModuleBase
    {
    }

}
