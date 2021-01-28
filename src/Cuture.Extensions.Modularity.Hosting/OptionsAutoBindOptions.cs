using Microsoft.Extensions.Configuration;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// Options自动绑定选项
    /// </summary>
    public class OptionsAutoBindOptions
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