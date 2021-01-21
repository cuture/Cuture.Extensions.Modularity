
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule4), typeof(ChaosDependsModule9))]
    [DependsOn(typeof(ChaosDependsModule5), typeof(ChaosDependsModule7))]
    public class ChaosDependsModule3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule4Async), typeof(ChaosDependsModule9Async))]
    [DependsOn(typeof(ChaosDependsModule5Async), typeof(ChaosDependsModule7Async))]
    public class ChaosDependsModule3Async : AsyncCountableModuleBase
    {
    }

}
