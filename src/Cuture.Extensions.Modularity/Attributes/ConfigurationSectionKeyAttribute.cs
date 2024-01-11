namespace Microsoft.Extensions.Options;

/// <summary>
/// 指定配置节点的键
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ConfigurationSectionKeyAttribute : Attribute
{
    #region Public 属性

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; }

    #endregion Public 属性

    #region Public 构造函数

    /// <summary>
    /// <inheritdoc cref="ConfigurationSectionKeyAttribute"/>
    /// </summary>
    /// <param name="key">键</param>
    public ConfigurationSectionKeyAttribute(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException($"“{nameof(key)}”不能为 null 或空白。", nameof(key));
        }

        Key = key;
    }

    #endregion Public 构造函数
}
