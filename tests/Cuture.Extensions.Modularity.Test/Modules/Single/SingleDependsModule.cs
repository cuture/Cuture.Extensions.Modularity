using Cuture.Extensions.Modularity;

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleDependsModule1), typeof(SingleDependsModule2))]
    public class SingleDependsModule : CountableModuleBase
    {
    }
}

namespace DependencyInjection.Modularity.Test
{
    [DependsOn(typeof(SingleDependsModule1Async), typeof(SingleDependsModule2Async))]
    public class SingleDependsModuleAsync : AsyncCountableModuleBase
    {
    }
}
