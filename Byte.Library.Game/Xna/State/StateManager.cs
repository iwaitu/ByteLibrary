using System.Collections.Generic;

namespace Byte.Library.Game.Xna.State
{
    public class StateManager
    {
        private static readonly int InitialMaxHistory = 10;

        private object stateHistoryLock = new object();
        private LinkedList<IGameState> stateHistory;
        private long delayMillis;
        private long lastStateChangeMillis;

        public int MaxHistory { get; set; }

        public IGameState CurrentState
        {
            get
            {
                return this.stateHistory.Last.Value;
            }
        }

        public IGameState LastState
        {
            get
            {
                lock (this.stateHistoryLock)
                {
                    if (this.stateHistory.Count > 1)
                    {
                        return this.stateHistory.Last.Previous.Value;
                    }

                    return null;
                }
            }
        }

        public delegate void StateChangedHandler(IGameState state);
        public event StateChangedHandler StateChanged;

        public StateManager(IGameState initialState, long currentMillis, long delayMillis = 0)
        {
            this.lastStateChangeMillis = currentMillis;
            this.delayMillis = delayMillis;
            this.MaxHistory = InitialMaxHistory;
            this.stateHistory = new LinkedList<IGameState>();

            this.stateHistory.AddLast(initialState);
        }

        public bool SetState(IGameState state, long currentMillis)
        {
            lock (this.stateHistoryLock)
            {
                if (this.DelayHasElapsed(currentMillis))
                {
                    this.PerformStateChange(state, currentMillis);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RevertState(long currentMillis)
        {
            lock (this.stateHistoryLock)
            {
                if (this.stateHistory.Count > 1 && this.DelayHasElapsed(currentMillis))
                {
                    this.PerformStateChange(this.stateHistory.Last.Previous.Value, currentMillis);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool DelayHasElapsed(long currentMillis)
        {
            return (currentMillis - this.lastStateChangeMillis >= this.delayMillis);
        }

        private void PerformStateChange(IGameState state, long currentMillis)
        {
            this.stateHistory.AddLast(state);

            if (this.stateHistory.Count > this.MaxHistory)
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
