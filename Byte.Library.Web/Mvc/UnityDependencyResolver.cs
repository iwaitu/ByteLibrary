using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Byte.Library.Web.Mvc
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            try
            {
                return this.container.Resolve(serviceType);
            }
            catch
            {
                //TODO: Once logging infrastructure is in place, log this.
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch
            {
                //TODO: Once logging infrastructure is in place, log this.
                return Enumerable.Empty<object>();
            }
        }
    }
}
