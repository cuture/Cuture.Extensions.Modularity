using System.Reflection;

namespace Cuture.Extensions.Modularity;

/// <summary>
/// 自定义获取键的选项绑定器
/// </summary>
internal class CustomKeyGetOptionsBinder : DefaultOptionsBinder
{
    #region Private 字段

    private readonly Func<Assembly, IEnumerable<Type>>? _findOptionsTypesFunc;
    private readonly Func<Type, string?>? _sectionKeyGetFunc;

    #endregion Private 字段

    #region Public 构造函数

    /// <inheritdoc cref="CustomKeyGetOptionsBinder"/>
    public CustomKeyGetOptionsBinder(Func<Assembly, IEnumerable<Type>>? findOptionsTypesFunc, Func<Type, string?>? sectionKeyGetFunc) : base(new OptionsBindOptions())
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
