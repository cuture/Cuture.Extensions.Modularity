using Cuture.Extensions.Modularity;

using SampleModule2;

namespace SampleModule5;

//使用Transient确保每次获取不一样
[ExportTransientServices]
//[ExportTransientServices(typeof(IRandomProvider))]
public class TRProvider : IRandomProvider
{
    public int Random()
    {
        //使用GetHashCode，验证每次为不同实例
        return this.GetHashCode();
    }
}
