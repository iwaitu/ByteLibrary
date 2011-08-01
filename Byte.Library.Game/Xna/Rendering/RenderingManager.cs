using System;
using System.Collections.Generic;
using System.Linq;

namespace Byte.Library.Game.Xna.Rendering
{
    public class RenderingManager<TState>
    {
        private IDictionary<TState, ICollection<IRenderer>> stateToRenderers;
        private static object stateLock = new object();

        public TState VisibleState { get; private set; }

        public IEnumerable<IRenderer> Renderers
        {
            get
            {
                lock (stateLock)
                {
                    return this.stateToRenderers
                        .SelectMany(keyVal => keyVal.Value);
                }
            }
        }

        public RenderingManager()
        {
            if (!typeof(TState).IsEnum)
            {
                throw new ArgumentException("Only enum types are supported.");
            }

            this.stateToRenderers = new Dictionary<TState, ICollection<IRenderer>>();

            foreach (TState state in EnumHelper.GetValues<TState>())
            {
                this.stateToRenderers.Add(state, new List<IRenderer>());
            }

            if (default(TState) != null)
            {
                this.SetVisibleState(default(TState));
            }
        }

        public void SetVisibleState(TState state)
        {
            lock (stateLock)
            {
                foreach (TState thisState in EnumHelper.GetValues<TState>())
                {
                    foreach (IRenderer renderer in this.stateToRenderers[thisState])
                    {
                        if (EqualityComparer<TState>.Default.Equals(state, thisState))
                        {
                            renderer.Visible = true;
                        }
                        else
                        {
                            renderer.Visible = false;
                        }
                    }
                }

                this.VisibleState = state;
            }
        }

        public void AttachRenderer(TState state, IRenderer renderer)
        {
            lock (stateLock)
            {
                this.stateToRenderers[state].Add(renderer);
            }
        }

        public IEnumerable<IRenderer> GetRenderersForState(TState state)
        {
            lock (stateLock)
            {
                return this.stateToRenderers[state];
            }
        }
    }
}
