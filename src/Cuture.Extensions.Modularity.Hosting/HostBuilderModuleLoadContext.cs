using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    internal sealed class HostBuilderModuleLoadContext
    {
        #region Private 字段

        private readonly List<KeyValuePair<IModuleSource, Action<ModuleLoadOptions>?>> _moduleSources = new();
        private readonly List<Action<ModuleLoadOptions>> _optionActions = new List<Action<ModuleLoadOptions>>();

        #endregion Private 字段

        #region Public 属性

        /// <summary>
        /// <see cref="IModuleSource"/>加载列表
        /// </summary>
        public IReadOnlyList<KeyValuePair<IModuleSource, Action<ModuleLoadOptions>?>> ModuleSources => _moduleSources;

        public IReadOnlyList<Action<ModuleLoadOptions>> OptionActions => _optionActions;

        #endregion Public 属性

        #region Public 方法

        /// <summary>
        /// 将模块源添加到构建集合中
        /// </summary>
        /// <param name="moduleSource"></param>
        /// <param name="optionAction">配置选项的委托</param>
        public void Add(IModuleSource moduleSource, Action<ModuleLoadOptions>? optionAction = null)
        {
            _moduleSources.Add(new KeyValuePair<IModuleSource, Action<ModuleLoadOptions>?>(moduleSource, optionAction));
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="optionAction"></param>
        public void Option(Action<ModuleLoadOptions> optionAction)
        {
            if (optionAction is null)
            {
                throw new ArgumentNullException(nameof(optionAction));
            }

            _optionActions.Add(optionAction);
        }

        #endregion Public 方法
    }
}