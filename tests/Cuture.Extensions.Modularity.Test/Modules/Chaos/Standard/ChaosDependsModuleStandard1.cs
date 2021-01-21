
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModuleStandard2))]
    public class ChaosDependsModuleStandard1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModuleStandard2Async))]
    public class ChaosDependsModuleStandard1Async : AsyncCountableModuleBase
    {
    }

}
