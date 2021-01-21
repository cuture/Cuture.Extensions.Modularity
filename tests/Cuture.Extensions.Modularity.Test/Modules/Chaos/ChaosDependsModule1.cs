
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule2), typeof(ChaosDependsModule3), typeof(ChaosDependsModule4))]
    public class ChaosDependsModule1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule2Async), typeof(ChaosDependsModule3Async), typeof(ChaosDependsModule4Async))]
    public class ChaosDependsModule1Async : AsyncCountableModuleBase
    {
    }

}
