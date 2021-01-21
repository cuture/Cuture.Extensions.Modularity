
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule6))]
    public class ChildDependsModule5 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule6Async))]
    public class ChildDependsModule5Async : AsyncCountableModuleBase
    {
    }

}
