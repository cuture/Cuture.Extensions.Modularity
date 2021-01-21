
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule4), typeof(ChaosDependsModule9))]
    [DependsOn(typeof(ChaosDependsModule7))]
    public class ChaosDependsModuleStandard3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule4Async), typeof(ChaosDependsModule9Async))]
    [DependsOn(typeof(ChaosDependsModule7Async))]
    public class ChaosDependsModuleStandard3Async : AsyncCountableModuleBase
    {
    }

}
