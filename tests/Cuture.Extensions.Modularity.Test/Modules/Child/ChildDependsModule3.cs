
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule4))]
    public class ChildDependsModule3 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule4Async))]
    public class ChildDependsModule3Async : AsyncCountableModuleBase
    {
    }

}
