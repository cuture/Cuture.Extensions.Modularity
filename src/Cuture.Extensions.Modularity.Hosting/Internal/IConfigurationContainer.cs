using Microsoft.Extensions.Configuration;

namespace Cuture.Extensions.Modularity.Internal;

internal class IConfigurationContainer
{
    #region Public 属性

    public IConfiguration Value { get; set; }

    #endregion Public 属性

    #region Public 构造函数

    public IConfigurationContainer(IConfiguration value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion Public 构造函数
}
