using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.Extensions.Options;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// 自动绑定<see cref="IOptions{TOptions}"/>的<inheritdoc cref="IModulesBootstrapInterceptor"/>
    /// </summary>
    internal class CustomKeyOptionsAutoBindModulesBootstrapInterceptor : OptionsAutoBindModulesBootstrapInterceptor
    {
        #region Private 字段

        private readonly Func<Assembly, IEnumerable<Type>>? _findOptionsTypesFunc;
        private readonly Func<Type, string?>? _sectionKeyGetFunc;

        #endregion Private 字段

        #region Public 构造函数

        public CustomKeyOptionsAutoBindModulesBootstrapInterceptor(Func<Assembly, IEnumerable<Type>>? findOptionsTypesFunc, Func<Type, string?>? sectionKeyGetFunc) : base(new OptionsAutoBindOptions())
        {
            _findOptionsTypesFunc = findOptionsTypesFunc;
            _sectionKeyGetFunc = sectionKeyGetFunc;
        }

        #endregion Public 构造函数

        #region Protected 方法

        protected override IEnumerable<Type> FindOptionsTypes(Assembly assembly) => _findOptionsTypesFunc?.Invoke(assembly) ?? base.FindOptionsTypes(assembly);

        protected override string? GetOptionsConfiguretionSectionKey(Type optionsType) => _sectionKeyGetFunc?.Invoke(optionsType) ?? base.GetOptionsConfiguretionSectionKey(optionsType);

        #endregion Protected 方法
    }
}