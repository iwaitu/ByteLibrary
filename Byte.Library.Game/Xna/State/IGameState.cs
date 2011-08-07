using Microsoft.Xna.Framework;

namespace Byte.Library.Game.Xna.State
{
    public interface IGameState : IDrawable, IUpdateable
    {
        string Name { get; }
    }
}
