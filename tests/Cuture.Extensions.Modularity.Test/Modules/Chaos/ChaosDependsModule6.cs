
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule7), typeof(ChaosDependsModule9))]
    public class ChaosDependsModule6 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule7Async), typeof(ChaosDependsModule9Async))]
    public class ChaosDependsModule6Async : AsyncCountableModuleBase
    {
    }

}
