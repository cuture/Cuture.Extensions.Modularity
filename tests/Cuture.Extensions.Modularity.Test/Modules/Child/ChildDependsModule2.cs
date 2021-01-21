
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule3))]
    public class ChildDependsModule2 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule3Async))]
    public class ChildDependsModule2Async : AsyncCountableModuleBase
    {
    }

}
