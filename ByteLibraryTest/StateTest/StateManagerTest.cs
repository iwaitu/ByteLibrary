using System;
using ByteLibrary.State;
using Xunit;

namespace ByteLibraryTest.StateTest
{
    public class StateManagerTest
    {
        enum TestEnum
        {
            Alpha,
            Beta,
            Charlie
        };

        private long GetCurrentMillis()
        {
            return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        [Fact]
        public void SwitchState_tracks_changes_properly()
        {
            var stateManager = new StateManager<TestEnum>(TestEnum.Alpha, this.GetCurrentMillis());

            Assert.Equal(TestEnum.Alpha, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);
            
            stateManager.SwitchState(TestEnum.Charlie, this.GetCurrentMillis());

            Assert.Equal(TestEnum.Charlie, stateManager.CurrentState);
            Assert.Equal(TestEnum.Alpha, stateManager.LastState);

            stateManager.SwitchState(TestEnum.Beta, this.GetCurrentMillis());

            Assert.Equal(TestEnum.Beta, stateManager.CurrentState);
            Assert.Equal(TestEnum.Charlie, stateManager.LastState);
        }

        [Fact]
        public void SwitchState_delay_prevents_change()
        {
            long sameCurrentMillis = this.GetCurrentMillis();

            var stateManager = new StateManager<TestEnum>(TestEnum.Alpha, this.GetCurrentMillis(), 5000);

            Assert.Equal(TestEnum.Alpha, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);

            stateManager.SwitchState(TestEnum.Charlie, sameCurrentMillis);

            Assert.Equal(TestEnum.Alpha, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);
        }

        [Fact]
        public void StateManager_throws_if_not_enum_type()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    var stateManager = new StateManager<int>(3, this.GetCurrentMillis());
                });
        }

        [Fact]
        public void RevertState_rolls_back_state()
        {
            var stateManager = new StateManager<TestEnum>(TestEnum.Alpha, this.GetCurrentMillis());

            stateManager.RevertState(this.GetCurrentMillis());

            Assert.Equal(TestEnum.Alpha, stateManager.CurrentState);
            Assert.Null(stateManager.LastState);

            stateManager.SwitchState(TestEnum.Charlie, this.GetCurrentMillis());

            Assert.Equal(TestEnum.Charlie, stateManager.CurrentState);
            Assert.Equal(TestEnum.Alpha, stateManager.LastState);

            stateManager.RevertState(this.GetCurrentMillis());

            Assert.Equal(TestEnum.Alpha, stateManager.CurrentState);
            Assert.Equal(TestEnum.Charlie, stateManager.LastState);
        }
    }
}
