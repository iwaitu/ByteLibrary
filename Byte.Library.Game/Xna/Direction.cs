using System;
using Microsoft.Xna.Framework;

namespace Byte.Library.Game.Xna
{
    public static class Direction
    {
        public static readonly Vector2 UpVector = new Vector2(0, -1);
        public static readonly Vector2 RightVector = new Vector2(1, 0);
        public static readonly Vector2 DownVector = new Vector2(0, 1);
        public static readonly Vector2 LeftVector = new Vector2(-1, 0);

        public static Vector2 RadiansToVector(float radians)
        {
            return new Vector2((float)Math.Sin(radians), -(float)Math.Cos(radians));
        }

        public static Vector2 DegreesToVector(float degrees)
        {
            float radians = DegreesToRadians(degrees);
            return RadiansToVector(radians);
        }

        public static float RadiansToDegrees(float radians)
        {
            return radians * 180 / (float)Math.PI;
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180;
        }
    }
}
