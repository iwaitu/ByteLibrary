using Byte.Library.Game.Xna.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte.Library.Game.UnitTests.Xna.Helpers
{
    internal class TestRenderer : IRenderer
    {
        public bool Visible { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
