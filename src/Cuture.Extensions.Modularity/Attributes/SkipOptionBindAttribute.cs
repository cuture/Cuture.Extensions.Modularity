using System;

namespace Microsoft.Extensions.Options
{
    /// <summary>
    /// 不进行选项绑定
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SkipOptionBindAttribute : Attribute
    {
    }
}