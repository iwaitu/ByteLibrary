using System;
using Byte.Library.Game.Xna.State;
using Microsoft.Xna.Framework;

namespace Byte.Library.Game.UnitTests.Xna.State
{
    public class TestState : IGameState
    {
        private int updateOrder;
        private int drawOrder;
        private bool enabled;
        private bool visible;

        public string Name
        {
            get { return "Test"; }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
                if (this.EnabledChanged != null)
                {
                    this.EnabledChanged(this, new EventArgs());
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
                if (this.VisibleChanged != null)
                {
                    this.VisibleChanged(this, new EventArgs());
                }
            }
        }

        public int UpdateOrder
        {
            get
            {
                return this.updateOrder;
            }
            set
            {
                this.updateOrder = value;
                if (this.UpdateOrderChanged != null)
                {
                    this.UpdateOrderChanged(this, new EventArgs());
                }
            }
        }

        public int DrawOrder
        {
            get
            {
                return this.drawOrder;
            }
            set
            {
                this.drawOrder = value;
                if (this.DrawOrderChanged != null)
                {
                    this.DrawOrderChanged(this, new EventArgs());
                }
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public TestState()
        {
            this.Enabled = true;
            this.Visible = true;
            this.UpdateOrder = 0;
            this.DrawOrder = 0;
        }

        public void Draw(GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
