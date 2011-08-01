using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte.Library.Game.Xna.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 vector1, Vector2 vector2, Color color)
        {
            var origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = vector2 - vector1;
            var scale = new Vector2(1.0f, diff.Length() / texture.Height);
            float angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            spriteBatch.Draw(texture, vector1, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
        }

        public static void DrawOutlinedString(
            this SpriteBatch spriteBatch, SpriteFont font, string text, Color backColor, Color frontColor, Vector2 position)
        {
            spriteBatch.DrawString(
                font,
                text,
                position + new Vector2(1, 1),
                backColor,
                0,
                new Vector2(0,0),
                1,
                SpriteEffects.None,
                1f);

            spriteBatch.DrawString(
                font,
                text,
                position + new Vector2(-1, -1),
                backColor,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                1f);

            spriteBatch.DrawString(
                font,
                text,
                position + new Vector2(-1, 1),
                backColor,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                1f);

            spriteBatch.DrawString(
                font,
                text,
                position + new Vector2(1, -1),
                backColor,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                1f);

            spriteBatch.DrawString(
                font,
                text,
                position,
                frontColor,
                0,
                new Vector2(0, 0),
                1,
                SpriteEffects.None,
                1f);
        }
    }
}
