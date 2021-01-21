using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cuture.Extensions.Modularity;

using Microsoft.Extensions.DependencyInjection;

namespace OtherModuleSystemAdaptSample
{
    public class AbpAdaptedExportServicesProvider : IExportServicesProvider
    {
        class ExposedServiceTypesProvider : Volo.Abp.DependencyInjection.IExposedServiceTypesProvider
        {
            private readonly Type[] _types;

            public ExposedServiceTypesProvider(Type targetType)
            {
                _types = new[] { targetType };
            }

            public Type[] GetExposedServiceTypes(Type targetType)
            {
                return _types;
            }
        }

        public AddDIMode AddDIMode { get; set; }

        public Type ExportTypeDiscovererType { get; set; }

        public ServiceLifetime Lifetime { get; set; }

        public IEnumerable<Type> GetExportServiceTypes(Type targetType)
        {
            return targetType.GetCustomAttributes(true)
                             .OfType<Volo.Abp.DependencyInjection.IExposedServiceTypesProvider>()
                             .DefaultIfEmpty(new ExposedServiceTypesProvider(targetType))
                             .SelectMany(p => p.GetExposedServiceTypes(targetType))
                             .Distinct()
                             .ToList();
        }
    }
}
