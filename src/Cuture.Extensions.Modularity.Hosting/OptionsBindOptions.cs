using Microsoft.Extensions.Configuration;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// Options自动绑定选项
    /// </summary>
    public class OptionsBindOptions
    {
        #region Private 字段

        private string? _pathPrefix;

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        /// 配置路径前缀
        /// </summary>
        public string? PathPrefix { get => _pathPrefix; set => _pathPrefix = value?.TrimEnd(':'); }

        /// <summary>
        /// 移除名称中的`Options`后缀
        /// </summary>
        public bool RemoveOptionsSuffix { get; set; } = false;

        /// <summary>
        /// 当启用 <see cref="UseFullNamespaceAsPath"/> 和 <see cref="RemoveOptionsSuffix"/> 时<para/>
        /// 简化配置的层次结构，移除连续重复的层级<para/>
        /// 默认为 false<para/>
        /// 例如：<para/>
        /// 配置类 SampleNamespace.Hello.HelloOptions 的匹配路径为 SampleNamespace:Hello:Hello <para/>
        /// 这会导致在配置文件中连续两层为 Hello <para/>
        /// 设置此选项为 true ，自动移除连续的 Hello ，最终其匹配路径为 SampleNamespace:Hello
        /// </summary>
        public bool SimplifiedHierarchy { get; set; } = false;

        /// <summary>
        /// 是否使用完整命名空间作为配置路径
        /// <para/>
        /// 当值为 true 时：
        /// <para/>
        /// 使用完整命名空间作为配置路径，在<see cref="IConfiguration"/>查找节点，并绑定值；
        /// <para/>
        /// 如 A 类命名空间为 B.C.D.E.F ，则<see cref="IConfiguration"/>查找路径为 B:C:D:E:F:A
        /// <para/>
        /// 当值为 false 时：直接使用类名进行查找
        /// <para/>
        /// 默认为 true
        /// </summary>
        public bool UseFullNamespaceAsPath { get; set; } = true;

        #endregion Public 属性
    }
}