using System;
using Byte.Library.Game.Xna.State;
using Xunit;

namespace Byte.Library.Game.UnitTests.Xna.State
{
    public class StateManagerTest
    {
        private long GetCurrentMillis()
        {
            //System.Environment.TickCount
            return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        [Fact]
        public void SwitchState_tracks_changes_properly()
        {
            var alphaState = new TestState();
            var betaState = new TestState();
            var charlieState = new TestState();

            var stateManager = new StateManager(alphaState, this.GetCurrentMillis());

            Assert.Equal(alphaState, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);

            stateManager.SetState(charlieState, this.GetCurrentMillis());

            Assert.Equal(charlieState, stateManager.CurrentState);
            Assert.Equal(alphaState, stateManager.LastState);

            stateManager.SetState(betaState, this.GetCurrentMillis());

            Assert.Equal(betaState, stateManager.CurrentState);
            Assert.Equal(charlieState, stateManager.LastState);
        }

        [Fact]
        public void SwitchState_delay_prevents_change()
        {
            var alphaState = new TestState();
            var betaState = new TestState();

            long staticMillis = this.GetCurrentMillis();

            var stateManager = new StateManager(alphaState, staticMillis, 5000);

            Assert.Equal(alphaState, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);

            stateManager.SetState(betaState, staticMillis);

            Assert.Equal(alphaState, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);
        }

        [Fact]
        public void RevertState_rolls_back_state()
        {
            var alphaState = new TestState();
            var betaState = new TestState();

            var stateManager = new StateManager(alphaState, this.GetCurrentMillis());
            stateManager.RevertState(this.GetCurrentMillis());

            Assert.Equal(alphaState, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);

            stateManager.SetState(betaState, this.GetCurrentMillis());

            Assert.Equal(betaState, stateManager.CurrentState);
            Assert.Equal(alphaState, stateManager.LastState);

            stateManager.RevertState(this.GetCurrentMillis());

            Assert.Equal(alphaState, stateManager.CurrentState);
            Assert.Equal(betaState, stateManager.LastState);
        }
    }
}
