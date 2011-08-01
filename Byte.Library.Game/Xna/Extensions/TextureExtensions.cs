using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte.Library.Game.Xna.Extensions
{
    public static class TextureExtensions
    {
        public static Vector2 GetOrigin(this Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public static Texture2D CreateFilledRectangleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            var texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { color });

            return texture;
        }
    }
}
