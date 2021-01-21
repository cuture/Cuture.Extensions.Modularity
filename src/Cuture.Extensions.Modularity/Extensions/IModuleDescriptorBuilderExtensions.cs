using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    ///
    /// </summary>
    public static class IModuleDescriptorBuilderExtensions
    {
        #region Mermaid

        internal static readonly StringBuilder EmptyStringBuilder = new StringBuilder(0);

        /// <summary>
        /// 根据已组织的多个模块描述，获取其 Mermaid 关系字符串，并移除重复路径
        /// </summary>
        /// <param name="organizedModuleDescriptors"></param>
        /// <param name="displayFullTypeName">显示完整类型名</param>
        /// <returns></returns>
        public static string ToMermaidString(this IEnumerable<IModuleDescriptor> organizedModuleDescriptors, bool displayFullTypeName = false)
        {
            var allMermaidString = string.Join(Environment.NewLine, organizedModuleDescriptors.Select(m => m.ToMermaidString(displayFullTypeName)));
            return RemoveMermaidDuplicatePaths(allMermaidString);
        }

        /// <summary>
        /// 根据已组织的多个模块描述，获取其 Mermaid 关系字符串，并移除重复路径
        /// </summary>
        /// <param name="organizedModuleDescriptors"></param>
        /// <param name="getTypeDisplayNameFunc">获取类型展示名的委托</param>
        /// <returns></returns>
        public static string ToMermaidString(this IEnumerable<IModuleDescriptor> organizedModuleDescriptors, Func<Type, string> getTypeDisplayNameFunc)
        {
            var allMermaidString = string.Join(Environment.NewLine, organizedModuleDescriptors.Select(m => m.ToMermaidString(getTypeDisplayNameFunc)));
            return RemoveMermaidDuplicatePaths(allMermaidString);
        }

        /// <summary>
        /// 根据已组织的模块描述，获取其 Mermaid 关系字符串
        /// </summary>
        /// <param name="organizedModuleDescriptor"></param>
        /// <param name="displayFullTypeName">显示完整类型名</param>
        /// <returns></returns>
        public static string ToMermaidString(this IModuleDescriptor organizedModuleDescriptor, bool displayFullTypeName = false)
        {
            HashSet<IModuleDescriptor> processed = new();
            return organizedModuleDescriptor.InternalToMermaidString(processed, displayFullTypeName ? type => type.FullName : type => type.Name).ToString();
        }

        /// <summary>
        /// 根据已组织的模块描述，获取其 Mermaid 关系字符串
        /// </summary>
        /// <param name="organizedModuleDescriptor"></param>
        /// <param name="getTypeDisplayNameFunc">获取类型展示名的委托</param>
        /// <returns></returns>
        public static string ToMermaidString(this IModuleDescriptor organizedModuleDescriptor, Func<Type, string> getTypeDisplayNameFunc)
        {
            HashSet<IModuleDescriptor> processed = new();
            return organizedModuleDescriptor.InternalToMermaidString(processed, getTypeDisplayNameFunc).ToString();
        }

        internal static StringBuilder InternalToMermaidString(this IModuleDescriptor descriptor, HashSet<IModuleDescriptor> processed, Func<Type, string> getTypeDisplayNameFunc)
        {
            if (processed.Contains(descriptor)
                || descriptor.Dependencies.IsNullOrEmpty())
            {
                return EmptyStringBuilder;
            }

            var builder = new StringBuilder();

            var typeName = getTypeDisplayNameFunc(descriptor.Type);

            foreach (var dependency in descriptor.Dependencies)
            {
                var dependenceTypeName = getTypeDisplayNameFunc(dependency.Type);

                builder.AppendLine($"{typeName}[\"{typeName}\"] --> {dependenceTypeName}[\"{dependenceTypeName}\"]");

                processed.Add(dependency);

                builder.Append(dependency.InternalToMermaidString(processed, getTypeDisplayNameFunc));
            }

            return builder;
        }

        /// <summary>
        /// 移除Mermaid中的重复路径
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static string RemoveMermaidDuplicatePaths(string content)
        {
            var lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Distinct();

            return string.Join(Environment.NewLine, lines);
        }

        #endregion Mermaid
    }
}