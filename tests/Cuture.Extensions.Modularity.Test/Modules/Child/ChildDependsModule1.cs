
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule2))]
    public class ChildDependsModule1 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule2Async))]
    public class ChildDependsModule1Async : AsyncCountableModuleBase
    {
    }

}
