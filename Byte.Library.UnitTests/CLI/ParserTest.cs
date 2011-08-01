using Byte.Library.CLI;
using Xunit;

namespace Byte.Library.UnitTests.CLI
{
    public class ParserTest
    {
        [Fact]
        public void Test()
        {
            string[] arguments = { "-alphaKey", "alphaValue", "--betaKey", "betaValue", "-charlieKey=charlieValue" };
            
            string[] requiredArguments = { "alphaKey" };
            string[] optionalArguments = { "betaKey", "charlieKey" };

            var parser = new Parser(Options.CaseSensitive, optionalArguments, requiredArguments);
            parser.Parse(arguments);

            Assert.True(parser.IsSet("alphaKey"));
            Assert.True(parser.IsSet("betaKey"));
            Assert.True(parser.IsSet("charlieKey"));

            Assert.False(parser.IsSet("deltaKey"));
            Assert.False(parser.IsSet("alphakey"));

            Assert.Equal("alphaValue", parser.Get("alphaKey"));
            Assert.Equal("betaValue", parser.Get("betaKey"));
            Assert.Equal("charlieValue", parser.Get("charlieKey"));
        }
    }
}
