using System.Collections.Generic;
using ByteLibrary.Mvc;
using Microsoft.Practices.Unity;
using Xunit;

namespace ByteLibraryTest.MvcTest
{
    public class UnityDependencyResolverTest
    {
        [Fact]
        public void Resolves_instance_from_container()
        {
            var container = new UnityContainer();
            container.RegisterType<ICollection<string>, List<string>>(new InjectionConstructor());

            var dependencyResolver = new UnityDependencyResolver(container);
            object dependency = dependencyResolver.GetService(typeof(ICollection<string>));

            Assert.IsType(typeof(List<string>), dependency);
        }
    }
}
