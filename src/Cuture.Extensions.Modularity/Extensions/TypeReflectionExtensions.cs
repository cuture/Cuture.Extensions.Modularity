using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    ///
    /// </summary>
    public static class CutureExtensionsModularityTypeReflectionExtensions
    {
        #region Public 方法

        /// <summary>
        /// 获取类型最有可能的直接接口<para/>
        /// C#反射不能只获取代码中的直接继承接口，会获取间接继承的接口。通过一些简单的排除方法获取最有可能的接口，但不太能保证符合意图<para/>
        /// https://stackoverflow.com/questions/5318685/get-only-direct-interface-instead-of-all <para/>
        /// https://stackoverflow.com/questions/1613867/how-do-i-know-when-an-interface-is-directly-implemented-in-a-type-ignoring-inheri <para/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetMostLikelyDirectInterfaces(this Type type)
        {
            var interfaces = type.BaseType is null
                             ? type.GetInterfaces()
                             : type.GetInterfaces().Except(type.BaseType.GetInterfaces());
            return interfaces.Except(interfaces.SelectMany(t => t.GetInterfaces()));
        }

        #endregion Public 方法
    }
}