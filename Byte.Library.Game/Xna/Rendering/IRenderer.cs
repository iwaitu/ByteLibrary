using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte.Library.Game.Xna.Rendering
{
    public interface IRenderer
    {
        bool Visible { get; set; }

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
