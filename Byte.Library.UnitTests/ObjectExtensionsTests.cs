using Xunit;

namespace Byte.Library.UnitTests
{
    public class ObjectExtensionsTests
    {
        private class Alpha { }
        private class Beta : Alpha { }
        private class Charlie { }

        [Fact]
        public void Object_treated_as_type_given_to_AsIf_executes_action()
        {
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
