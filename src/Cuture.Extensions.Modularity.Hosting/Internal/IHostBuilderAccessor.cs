using System;

using Microsoft.Extensions.Hosting;

namespace Cuture.Extensions.Modularity.Internal;

internal class IHostBuilderContainer
{
    #region Public 属性

    public IHostBuilder Value { get; set; }

    #endregion Public 属性

    #region Public 构造函数

    public IHostBuilderContainer(IHostBuilder value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion Public 构造函数
}
