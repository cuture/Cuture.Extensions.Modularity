
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule8))]
    public class ChaosDependsModule7 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModule8Async))]
    public class ChaosDependsModule7Async : AsyncCountableModuleBase
    {
    }

}
