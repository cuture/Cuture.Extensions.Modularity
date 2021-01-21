
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule9))]
    public class ChildDependsModule8 : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(ChildDependsModule9Async))]
    public class ChildDependsModule8Async : AsyncCountableModuleBase
    {
    }

}
