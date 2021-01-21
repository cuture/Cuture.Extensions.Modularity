using System;
using System.Collections.Generic;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// ģ��Դ
    /// </summary>
    public interface IModuleSource
    {
        #region Public ����

        /// <inheritdoc cref="IModuleDescriptorBuilder"/>
        IModuleDescriptorBuilder? DescriptorBuilder { get; }

        #endregion Public ����

        #region Public ����

        /// <summary>
        /// ��ȡ����ģ��
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetModules();

        #endregion Public ����
    }
}