using System;
using System.Windows.Input;

namespace EasySharpStandardMvvm.Commands.Core
{
    public class DelegateCommand : CommandBase, ICommand
    {
        private readonly Action<object> executeFunc;

        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
            : base(canExecute)
        {
            this.executeFunc = execute;
        }

        public DelegateCommand(Action<object> execute)
            : this((param) => true, execute)
        {
        }

        public bool CanExecute(object parameter)
        {
            return this.CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            this.executeFunc(parameter);
        }
    }
}
