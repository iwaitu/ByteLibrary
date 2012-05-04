﻿using System.Web.Mvc;
using Byte.Library.Web;
using Xunit;

namespace Byte.Library.UnitTests.Web
{
    public class HtmlHelperExtensionsTests
    {
        [Fact]
        public void Id_generated_matches_property_name()
        {
            var htmlHelper = HtmlHelperTestHelpers.CreateMockHtmlHelper<Foo>(new ViewDataDictionary());
            string id = htmlHelper.IdFor<Foo, string>(f => f.Title);

            Assert.Equal("Title", id);
        }

        private class Foo
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }
    }
}
