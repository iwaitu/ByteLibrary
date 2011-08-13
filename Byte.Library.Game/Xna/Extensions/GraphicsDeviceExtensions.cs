using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte.Library.Game.Xna.Extensions
{
    public static class GraphicsDeviceExtensions
    {
        public static Vector2 GetScreenOrigin(this GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
            int screenHeight = graphicsDevice.PresentationParameters.BackBufferHeight;

            return new Vector2(screenWidth / 2, screenHeight / 2);
        }
    }
}
