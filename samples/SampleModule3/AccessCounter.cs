using System.Threading;

using Cuture.Extensions.Modularity;

using SampleModule2;

namespace SampleModule3;

[ExportSingletonServices(typeof(IAccessCounter))]
public class AccessCounter : IAccessCounter
{
    private long _count;
    public long Count => _count;

    public void Add()
    {
        Interlocked.Increment(ref _count);
    }
}
