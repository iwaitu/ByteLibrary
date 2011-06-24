using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace ByteLibrary.Mvc
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

        //TODO: I do not yet understand the use of this.
        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new System.NotImplementedException();
        }
    }
}
