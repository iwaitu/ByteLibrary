﻿using System;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;
using MvcContrib.PortableAreas;
using Raven.Client;
using Raven.Client.Document;

namespace Byte.Library.Web
{
    public abstract class AreaRegistrationBase : PortableAreaRegistration
    {
        private static bool storeRegistered;

        public override string AreaName
        {
            get { return this.GetType().Namespace; }
        }

        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            var state = context.State as ApplicationState;
            if (state == null)
            {
                throw new Exception("ApplicationState could not be read.");
            }

            this.RegisterStore(state.Container);
            this.RegisterTypes(state.Container);
            this.RegisterMappings();
            this.AssertMappingsValid();
            this.RegisterAssetRoutes(context);
            this.RegisterRoutes(context);
            this.RegisterAreaEmbeddedResources();
        }

        private void RegisterStore(IUnityContainer container)
        {
            if(storeRegistered)
            {
                return;
            }

            var store = CreateStore();
            container.RegisterInstance<IDocumentStore>(store);

            storeRegistered = true;
        }

        private static IDocumentStore CreateStore()
        {
            var store = new DocumentStore()
            {
                ConnectionStringName = "RavenDB"
            };

            store.Initialize();

            return store;
        }

        protected virtual void RegisterTypes(IUnityContainer container)
        {
        }

        protected virtual void RegisterMappings()
        {
        }

        private void AssertMappingsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        private void RegisterAssetRoutes(AreaRegistrationContext context)
        {
            this.RegisterAssetsRoute(context, "images");
            this.RegisterAssetsRoute(context, "scripts");
            this.RegisterAssetsRoute(context, "scripts/lib");
            this.RegisterAssetsRoute(context, "styles");

        }

        private void RegisterAssetsRoute(AreaRegistrationContext context, string folderName)
        {
            string url = string.Format("Assets/{0}/{{resourceName}}", folderName);

            context.MapRoute(
                this.AreaName + "-Assets-" + folderName.Replace('/', '-'),
                this.AreaRoutePrefix + "/" + url,
                new
                {
                    controller = "EmbeddedResource",
                    action = "Index",
                    resourcePath = ("Assets." + folderName.Replace('/', '.'))
                },
                null,
                new string[1] { "MvcContrib.PortableAreas" });
        }

        protected virtual void RegisterRoutes(AreaRegistrationContext context)
        {
        }
    }
}
