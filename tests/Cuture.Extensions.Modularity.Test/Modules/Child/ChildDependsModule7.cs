
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule8))]
    public class ChildDependsModule7 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule8Async))]
    public class ChildDependsModule7Async : AsyncCountableModuleBase
    {
    }

}
