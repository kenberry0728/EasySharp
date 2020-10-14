using System.Collections.Generic;
using System.Linq;

namespace EasySharp.DesignPatterns.StrategyPattern
{
    public abstract class ParameterStrategyPatternBase<TParemeter> : List<IStrategy<TParemeter>>
    {
        protected abstract IStrategy<TParemeter> GetStrategy(TParemeter context);

        public void Execute(TParemeter paremeter)
        {
            var targetStrategy = this.GetStrategy(paremeter);
            targetStrategy?.Execute(paremeter);
        }
    }

    public abstract class ParameterStrategyPatternBase<TParameter, TReturn> : List<IStrategy<TParameter, TReturn>>
    {
        protected abstract IStrategy<TParameter, TReturn> GetStrategy(TParameter context);

        public TReturn Execute(TParameter parameter)
        {
            var targetStrategy = this.GetStrategy(parameter);
            if (targetStrategy == null)
            {
                return default;
            }

            return targetStrategy.Execute(parameter);
        }
    }
}