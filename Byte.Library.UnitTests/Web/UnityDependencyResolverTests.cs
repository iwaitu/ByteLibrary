using System.Collections.Generic;
using Byte.Library.Web;
using Microsoft.Practices.Unity;
using Xunit;

namespace Byte.Library.UnitTests.Web
{
    public class UnityDependencyResolverTests
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
