using Cuture.Extensions.Modularity;

using SampleModule2;

namespace SampleModule5
{
    //使用Scoped确保每个请求不一样
    [ExportScopedServices]
    //[ExportScopedServices(typeof(IRequestRandomProvider))]
    public class SRRProvider : IRequestRandomProvider
    {
        public int Random()
        {
            //使用GetHashCode，验证每次请求不同实例
            return this.GetHashCode();
        }
    }
}