﻿using System;
using System.Web.Mvc;
using Byte.Library.Data;
using Raven.Client;
using Raven.Client.Embedded;
using Xunit;

namespace Byte.Blog.Framework.UnitTests.Data
{
    public class RavenControllerTests
    {
        [Fact]
        public void Raven_session_is_not_opened_before_on_action_executing()
        {
            var store = GetTestStore();
            var controller = new TestableController(store);

            Assert.Null(controller.Session);
        }

        [Fact]
        public void Raven_session_is_opened_on_action_executing()
        {
            var store = GetTestStore();
            var controller = new TestableController(store);

            StartActionExecuting(controller);

            using (var session = controller.Session)
            {
                Assert.NotNull(session);
            }
        }

        [Fact]
        public void If_no_exception_occurred_then_changes_are_saved()
        {
            var testDocument = new Entry("entries/123") { Title = "foo" };

            var store = GetTestStore();

            using (var session = store.OpenSession())
            {
                session.Store(testDocument);
                session.SaveChanges();
            }

            var controller = new TestableController(store);

            StartActionExecuting(controller);

            using (var session = controller.Session)
            {
                var doc = session.Load<Entry>(testDocument.Id);
                doc.Title = "bar";

                FinishActionExecuted(controller);
            }

            using (var session = store.OpenSession())
            {
                var doc = session.Load<Entry>(testDocument.Id);
                Assert.Equal("bar", doc.Title);
            }
        }

        [Fact]
        public void If_exception_occurred_then_changes_are_not_saved()
        {
            var testDocument = new Entry("entries/123") { Title = "foo" };

            var store = GetTestStore();

            using (var session = store.OpenSession())
            {
                session.Store(testDocument);
                session.SaveChanges();
            }

            var controller = new TestableController(store);

            StartActionExecuting(controller);

            using (var session = controller.Session)
            {
                var doc = session.Load<Entry>(testDocument.Id);
                doc.Title = "bar";

                FinishActionExecuted(controller, true);
            }

            using (var session = store.OpenSession())
            {
                var doc = session.Load<Entry>(testDocument.Id);
                Assert.Equal("foo", doc.Title);
            }
        }

        private static void StartActionExecuting(TestableController controller)
        {
            var actionExecutingContext = new ActionExecutingContext();
            controller.OnActionExecuting(actionExecutingContext);
        }

        private static void FinishActionExecuted(TestableController controller, bool throwException = false)
        {
            var actionExecutedContext = new ActionExecutedContext();
            if (throwException)
            {
                actionExecutedContext.Exception = new Exception();
            }

            controller.OnActionExecuted(actionExecutedContext);
        }

        private class TestableController : RavenController
        {
            public new IDocumentSession Session
            {
                get { return base.session; }
            }

            public TestableController(IDocumentStore documentStore)
                : base(documentStore)
            {
            }

            public new void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
            }

            public new void OnActionExecuted(ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);
            }
        }

        private static IDocumentStore GetTestStore()
        {
            var store = new EmbeddableDocumentStore()
            {
                RunInMemory = true,
            };

            store.Initialize();

            return store;
        }
    }

    public class Entry
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public Entry(string id)
        {
            Id = id;
        }
    }
}
