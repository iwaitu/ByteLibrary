using System;
using System.Collections.Generic;

namespace ByteLibrary
{
    public class StateManager<TState> where TState : struct
    {
        private LinkedList<TState> stateHistory;
        private int maxHistory;
        private long delayMillis;
        private long lastStateChangeMillis;

        public delegate void StateChangedHandler(TState state);
        public event StateChangedHandler StateChanged;

        public TState CurrentState
        {
            get
            {
                return this.stateHistory.Last.Value;
            }
        }

        public Nullable<TState> LastState
        {
            get
            {
                if (this.stateHistory.Count > 1)
                {
                    return this.stateHistory.Last.Previous.Value;
                }

                return null;
            }
        }

        public StateManager(TState initialState, long currentMillis, long delayMillis = 0)
        {
            if (!typeof(TState).IsEnum || Enum.GetValues(typeof(TState)).Length < 2)
            {
                throw new ArgumentException(string.Format("{0} supports only Enum types of size 2 or greater.", this.GetType().ToString()));
            }

            this.lastStateChangeMillis = currentMillis;
            this.delayMillis = delayMillis;
            this.maxHistory = Enum.GetValues(typeof(TState)).Length;
            this.stateHistory = new LinkedList<TState>();
            this.stateHistory.AddLast(initialState);
        }

        public void SwitchState(TState state, long currentMillis)
        {
            if (currentMillis - this.lastStateChangeMillis >= this.delayMillis)
            {
                this.stateHistory.AddLast(state);

                if (this.stateHistory.Count > this.maxHistory)
                {
                    this.stateHistory.RemoveFirst();
                }

                if (this.StateChanged != null)
                {
                    this.StateChanged(this.CurrentState);
                }

                this.lastStateChangeMillis = currentMillis;
            }
        }

        public void RevertState(long currentMillis)
        {
            if (this.stateHistory.Count > 1)
            {
                this.SwitchState(this.stateHistory.Last.Previous.Value, currentMillis);
            }
        }
    }
}
