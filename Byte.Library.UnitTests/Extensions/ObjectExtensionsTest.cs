using Byte.Library.Extensions;
using Xunit;

namespace Byte.Library.UnitTests.Extensions
{
    public class ObjectExtensionsTest
    {
        private class Alpha { }
        private class Beta : Alpha { }
        private class Charlie { }

        [Fact]
        public void Object_treated_as_type_given_to_AsIf_executes_action()
        {
            var alpha = new Alpha();
            var beta = new Beta();

            bool executed = false;

            beta.AsIf<Alpha>(b =>
                {
                    executed = true;
                });

            Assert.True(executed);
        }

        [Fact]
        public void Object_treated_as_different_type_given_to_AsIf_does_not_execute_action()
        {
            var alpha = new Alpha();
            var beta = new Beta();
            var charlie = new Charlie();

            bool executed = false;

            charlie.AsIf<Alpha>(b =>
            {
                executed = true;
            });

            Assert.False(executed);
        }
    }
}
