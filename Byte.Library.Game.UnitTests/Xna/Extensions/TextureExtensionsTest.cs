using Byte.Library.Game.Xna.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xunit;

namespace Byte.Library.Game.UnitTests.Xna.Extensions
{
    public class TextureExtensionsTest
    {
        [Fact]
        public void GetOrigin_correctly_identifies_texture_origin()
        {
            using (var game = new Microsoft.Xna.Framework.Game())
            {
                var graphicsDeviceManager = new GraphicsDeviceManager(game);

                graphicsDeviceManager.PreferredBackBufferWidth = 1024;
                graphicsDeviceManager.PreferredBackBufferHeight = 768;
                graphicsDeviceManager.IsFullScreen = false;
                graphicsDeviceManager.ApplyChanges();

                var texture = new Texture2D(graphicsDeviceManager.GraphicsDevice, 100, 100);

                Vector2 origin = texture.GetOrigin();
                var expectedOrigin = new Vector2(50, 50);

                Assert.Equal(expectedOrigin, origin);
            }            
        }
    }
}
