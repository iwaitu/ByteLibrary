using System;
using System.Collections.Generic;

namespace ByteLibrary.State
{
    public class StateManager<TState> where TState : struct
    {
        private object stateHistoryLock = new object();
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
                lock (this.stateHistoryLock)
                {
                    if (this.stateHistory.Count > 1)
                    {
                        return this.stateHistory.Last.Previous.Value;
                    }
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
            lock (this.stateHistoryLock)
            {
                if (this.DelayHasElapsed(currentMillis))
                {
                    this.PerformStateChange(state, currentMillis);
                }
            }
        }

        public void RevertState(long currentMillis)
        {
            lock (this.stateHistoryLock)
            {
                if (this.stateHistory.Count > 1 && this.DelayHasElapsed(currentMillis))
                {
                    this.PerformStateChange(this.stateHistory.Last.Previous.Value, currentMillis);
                }
            }
        }

        private bool DelayHasElapsed(long currentMillis)
        {
            return (currentMillis - this.lastStateChangeMillis >= this.delayMillis);
        }

        private void PerformStateChange(TState state, long currentMillis)
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
}
