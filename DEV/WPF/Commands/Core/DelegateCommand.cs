using System;
using System.Windows.Input;

namespace EasySharpWpf.Commands.Core
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public DelegateCommand(Action<object> execute)
            : this((param) => true, execute)
        {
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void NotifyCanExecuteChanged(EventArgs e)
        {
            this.CanExecuteChanged?.Invoke(this, e);
        }
    }
}
