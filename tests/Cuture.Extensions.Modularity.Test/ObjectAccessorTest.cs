using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjection.Modularity.Test
{
    [TestClass]
    public class ObjectAccessorTest : DIContainerTestBase
    {
        #region Public 方法

        [TestMethod]
        public void OperateInServiceCollectionWithOutAddService()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Services.GetObjectAccessor<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.GetObjectAccessorValue<TargetClass>());
            //Assert.ThrowsException<InvalidOperationException>(() => Services.RemoveObjectAccessor<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.RemoveObjectAccessorValue<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.SetObjectAccessorValue<TargetClass>(null));
        }

        [TestMethod]
        public void OperateInServiceProviderWithOutAddService()
        {
            var serviceProvider = Services.BuildServiceProvider();

            Assert.ThrowsException<InvalidOperationException>(() => serviceProvider.GetObjectAccessor<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => serviceProvider.GetObjectAccessorValue<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => serviceProvider.RemoveObjectAccessorValue<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => serviceProvider.SetObjectAccessorValue<TargetClass>(null));
        }

        [TestMethod]
        public void ScopedWithServiceCollection()
        {
            Services.AddObjectAccessor<TargetClass>(ServiceLifetime.Scoped);

            Assert.ThrowsException<InvalidOperationException>(() => Services.GetObjectAccessor<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.GetObjectAccessorValue<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.RemoveObjectAccessorValue<TargetClass>());
            Assert.ThrowsException<InvalidOperationException>(() => Services.SetObjectAccessorValue<TargetClass>(null));
        }

        [TestMethod]
        public void ScopedWithServiceProvider()
        {
            Services.AddObjectAccessor<TargetClass>(ServiceLifetime.Scoped);

            var rootServiceProvider = Services.BuildServiceProvider();

            var rootTarget = new TargetClass();
            rootServiceProvider.SetObjectAccessorValue<TargetClass>(rootTarget);
            var rootAccessor = rootServiceProvider.GetObjectAccessor<TargetClass>();
            Assert.AreEqual(rootTarget, rootAccessor.Value);

            using (var serviceScope1 = rootServiceProvider.CreateScope())
            {
                var scope1Accessor = serviceScope1.ServiceProvider.GetObjectAccessor<TargetClass>();
                Assert.AreEqual(null, scope1Accessor.Value);
                serviceScope1.ServiceProvider.SetObjectAccessorValue<TargetClass>(rootTarget);
                Assert.AreEqual(rootTarget, scope1Accessor.Value);

                using (var serviceScope2 = rootServiceProvider.CreateScope())
                {
                    var scope2Accessor = serviceScope2.ServiceProvider.GetObjectAccessor<TargetClass>();
                    Assert.AreEqual(null, scope2Accessor.Value);
                }
            }
        }

        [TestMethod]
        public void SingletonWithServiceCollection()
        {
            Services.AddObjectAccessor<TargetClass>();
            var accessor = Services.GetObjectAccessor<TargetClass>();
            Assert.IsNotNull(accessor);

            var target = new TargetClass();
            accessor.Value = target;
            accessor = Services.GetObjectAccessor<TargetClass>();
            Assert.AreEqual(target, accessor.Value);

            var accessorValue = Services.GetObjectAccessorValue<TargetClass>();
            Assert.AreEqual(accessor.Value, accessorValue);

            target = new TargetClass();
            Services.SetObjectAccessorValue<TargetClass>(target);
            Assert.AreEqual(target, accessor.Value);

            Services.RemoveObjectAccessorValue<TargetClass>();
            Assert.AreEqual(null, accessor.Value);

            Services.RemoveObjectAccessor<TargetClass>();

            Assert.ThrowsException<InvalidOperationException>(() => Services.GetObjectAccessor<TargetClass>());
        }

        [TestMethod]
        public void SingletonWithServiceProvider()
        {
            var target = new TargetClass();

            Services.AddObjectAccessor<TargetClass>(target);

            var serviceProvider = Services.BuildServiceProvider();

            var accessor = serviceProvider.GetObjectAccessor<TargetClass>();
            var accessorValue = serviceProvider.GetObjectAccessorValue<TargetClass>();
            Assert.AreEqual(target, accessor.Value);
            Assert.AreEqual(target, accessorValue);

            var removedValue = serviceProvider.RemoveObjectAccessorValue<TargetClass>();
            accessorValue = serviceProvider.GetObjectAccessorValue<TargetClass>();
            Assert.AreEqual(target, removedValue);
            Assert.AreEqual(null, accessor.Value);
            Assert.AreEqual(null, accessorValue);
        }

        [TestMethod]
        public void TransientWithServiceCollection()
        {
            Assert.ThrowsException<ArgumentException>(() => Services.AddObjectAccessor<TargetClass>(ServiceLifetime.Transient));
        }

        #endregion Public 方法

        #region Private 类

        private class TargetClass
        {
        }

        #endregion Private 类
    }
}