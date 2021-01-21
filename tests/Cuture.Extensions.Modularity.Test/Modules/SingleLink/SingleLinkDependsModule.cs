
using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule1))]
    public class SingleLinkDependsModule : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleLinkDependsModule1Async))]
    public class SingleLinkDependsModuleAsync : AsyncCountableModuleBase
    {
    }

}
