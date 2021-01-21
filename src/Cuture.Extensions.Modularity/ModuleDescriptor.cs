using System;
using System.Collections.Generic;
using System.Linq;

namespace Cuture.Extensions.Modularity
{
    /// <summary>
    /// ģ������
    /// </summary>
    public class ModuleDescriptor : IModuleDescriptor
    {
        #region Private �ֶ�

        private List<IModuleDescriptor> _dependencies = new List<IModuleDescriptor>();
        private List<IModuleDescriptor> _dependents = new List<IModuleDescriptor>();

        #endregion Private �ֶ�

        #region Public ����

        /// <inheritdoc/>
        public virtual IReadOnlyList<IModuleDescriptor> Dependencies { get => _dependencies; protected set => _dependencies = value.ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<IModuleDescriptor> Dependents { get => _dependents; protected set => _dependents = value.ToList(); }

        /// <inheritdoc/>
        public virtual Type Type { get; protected set; }

        #endregion Public ����

        #region Public ���캯��

        /// <summary>
        /// <inheritdoc cref="ModuleDescriptor"/>
        /// </summary>
        /// <param name="moduleType">ģ������</param>
        public ModuleDescriptor(Type moduleType)
        {
            Type = moduleType ?? throw new ArgumentNullException(nameof(moduleType));
        }

        /// <summary>
        /// <inheritdoc cref="ModuleDescriptor"/>
        /// </summary>
        /// <param name="moduleType">ģ������</param>
        /// <param name="dependencies">������ģ��</param>
        public ModuleDescriptor(Type? moduleType, params IModuleDescriptor[] dependencies)
        {
            Type = moduleType ?? throw new ArgumentNullException(nameof(moduleType));
            Dependencies = dependencies ?? Array.Empty<ModuleDescriptor>();
        }

        #endregion Public ���캯��

        #region Public ����

        /// <inheritdoc/>
        public IModuleDescriptor AddDependencies(IEnumerable<IModuleDescriptor> moduleDescriptors)
        {
            foreach (var item in moduleDescriptors)
            {
                _dependencies.AddIfNotContains(item);
            }

            return this;
        }

        /// <inheritdoc/>
        public IModuleDescriptor AddDependent(IModuleDescriptor moduleDescriptor)
        {
            _dependents.AddIfNotContains(moduleDescriptor);
            return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Module: {Type.FullName}. Dependencies Count: {Dependencies.Count}";
        }

        #region Compare

        /// <inheritdoc/>
        public static bool operator !=(ModuleDescriptor left, ModuleDescriptor right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public static bool operator ==(ModuleDescriptor left, ModuleDescriptor right)
        {
            return EqualityComparer<ModuleDescriptor>.Default.Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(IModuleDescriptor? other)
        {
            return Type is not null && Type == other?.Type;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            return Equals(obj as IModuleDescriptor);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Type.GetHashCode();

        #endregion Compare

        #endregion Public ����
    }
}