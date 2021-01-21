
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModuleStandard3))]
    public class ChaosDependsModuleStandard2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChaosDependsModuleStandard3Async))]
    public class ChaosDependsModuleStandard2Async : AsyncCountableModuleBase
    {
    }

}
