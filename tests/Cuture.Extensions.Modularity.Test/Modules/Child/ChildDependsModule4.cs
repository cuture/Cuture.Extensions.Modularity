
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule5))]
    public class ChildDependsModule4 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule5Async))]
    public class ChildDependsModule4Async : AsyncCountableModuleBase
    {
    }

}
