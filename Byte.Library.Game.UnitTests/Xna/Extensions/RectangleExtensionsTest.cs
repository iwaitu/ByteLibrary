using Byte.Library.Game.Xna.Extensions;
using Microsoft.Xna.Framework;
using Xunit;

namespace Byte.Library.Game.UnitTests.Xna.Extensions
{
    public class RectangleExtensionsTest
    {
        [Fact]
        public void TopLeft_extension_produces_correct_vector()
        {
            var rectangle = new Rectangle(0, 0, 500, 500);

            var topLeft = rectangle.TopLeft();
            var topLeftExpected = new Vector2(0, 0);

            Assert.Equal(topLeftExpected, topLeft);
        }

        [Fact]
        public void TopRight_extension_produces_correct_vector()
        {
            var rectangle = new Rectangle(0, 0, 500, 500);

            var topRight = rectangle.TopRight();
            var topRightExpected = new Vector2(500, 0);

            Assert.Equal(topRightExpected, topRight);
        }

        [Fact]
        public void BottomLeft_extension_produces_correct_vector()
        {
            var rectangle = new Rectangle(0, 0, 500, 500);

            var bottomLeft = rectangle.BottomLeft();
            var bottomLeftExpected = new Vector2(0, 500);

            Assert.Equal(bottomLeftExpected, bottomLeft);
        }

        [Fact]
        public void BottomRight_extension_produces_correct_vector()
        {
            var rectangle = new Rectangle(0, 0, 500, 500);

            var bottomRight = rectangle.BottomRight();
            var bottomRightExpected = new Vector2(500, 500);

            Assert.Equal(bottomRightExpected, bottomRight);
        }
    }
}
