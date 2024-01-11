namespace System;

/// <summary>
/// 循环依赖异常
/// </summary>
public class CyclicDependencyException : Exception
{
    #region Public 属性

    /// <summary>
    /// 发现循环依赖的对象
    /// </summary>
    public object Item { get; set; }

    #endregion Public 属性

    #region Public 构造函数

    /// <inheritdoc cref="CyclicDependencyException"/>
    public CyclicDependencyException(object item)
    {
        Item = item;
    }

    #endregion Public 构造函数
}
