﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace Byte.Library.Web
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly Type type;
        private readonly IUnityContainer container;

        public UnityInstanceProvider(Type type, IUnityContainer container)
        {
            this.type = type;
            this.container = container;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.container.Resolve(this.type);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}
