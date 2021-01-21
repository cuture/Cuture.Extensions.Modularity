
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule6))]
    public class ChaosDependsModule5 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule6Async))]
    public class ChaosDependsModule5Async : AsyncCountableModuleBase
    {
    }

}
