namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 模块加载选项
    /// </summary>
    public class ModuleLoadOptions
    {
        #region Private 字段

        private ModulesBootstrapOptions? _bootstrapOptions;

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        /// 将模块注册到DI中
        /// </summary>
        public bool AddModuleAsService { get; set; } = false;

        /// <summary>
        /// <inheritdoc cref="ModulesBootstrapOptions"/>
        /// </summary>
        public ModulesBootstrapOptions BootstrapOptions
        {
            get
            {
                if (_bootstrapOptions is null)
                {
                    _bootstrapOptions = new ModulesBootstrapOptions();
                }
                return _bootstrapOptions;
            }
            set => _bootstrapOptions = value;
        }

        #endregion Public 属性
    }
}