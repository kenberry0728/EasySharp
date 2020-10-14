using System.Collections.Generic;

namespace EasySharp.DesignPatterns.StrategyPattern
{
    public abstract class StateStrategyPatternBase<TState> : Dictionary<TState, IStrategy>
        where TState : struct
    {
        protected TState State { get; set; }

        public void Execute()
        {
            this.TryGetValue(State, out var strategy);
            strategy?.Execute();
        }
    }
}