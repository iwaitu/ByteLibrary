using System;
using Byte.Library.Game.Xna;
using Microsoft.Xna.Framework;
using Xunit;

namespace Byte.Library.Game.UnitTests.Xna
{
    public class DirectionTest
    {
        private const int decimalPrecision = 2;
        private const float rightAngleDegrees = 90;
        private const float rightAngleRadians = 1.57F;

        [Fact]
        public void Given_degrees_the_correct_radians_are_produced()
        {
            float radians = Direction.DegreesToRadians(rightAngleDegrees);

            Assert.Equal(rightAngleRadians, radians, decimalPrecision);
        }

        [Fact]
        public void Given_radians_the_correct_degrees_are_produced()
        {
            float degrees = Direction.RadiansToDegrees(rightAngleRadians);
            double roundedDegrees = Math.Round(degrees);

            Assert.Equal(rightAngleDegrees, roundedDegrees);
        }

        [Fact]
        public void Given_radians_the_correct_vector_is_produced()
        {
            Vector2 vector = Direction.RadiansToVector(rightAngleRadians);

            Assert.Equal(Direction.RightVector.X, Math.Round(vector.X));
            Assert.Equal(Direction.RightVector.Y, Math.Round(vector.Y));
        }

        [Fact]
        public void Given_degrees_the_correct_vector_is_produced()
        {
            Vector2 vector = Direction.DegreesToVector(rightAngleDegrees);

            Assert.Equal(Direction.RightVector.X, Math.Round(vector.X));
            Assert.Equal(Direction.RightVector.Y, Math.Round(vector.Y));
        }
    }
}
