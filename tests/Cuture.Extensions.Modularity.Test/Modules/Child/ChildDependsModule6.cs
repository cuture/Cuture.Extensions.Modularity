
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule7))]
    public class ChildDependsModule6 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule7Async))]
    public class ChildDependsModule6Async : AsyncCountableModuleBase
    {
    }

}
