using EasySharpStandard.SafeCodes.Core;
using System;
using System.Windows;
using System.Windows.Input;

namespace EasySharpWpf.Commands.Core
{
    public class CloseWindowCommand : CommandBase, ICommand
    {
        private readonly Window defaultWindow;

        public CloseWindowCommand(Window defaultWindow)
        {
            this.defaultWindow = defaultWindow;
        }

        public CloseWindowCommand(Window window, Func<object, bool> canExecute) : base(canExecute)
        {
            this.defaultWindow = window;
        }

        public CloseWindowCommand()
        {
        }

        public CloseWindowCommand(Func<object, bool> canExecute) : base(canExecute)
        {
        }

        public bool CanExecute(object parameter)
        {
            return this.TryGetCloseWindow(parameter, out _)
                   && this.CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            if(this.CanExecute(parameter)
                && this.TryGetCloseWindow(parameter, out var window))
            {
                window.Close();
            }
        }

        private bool TryGetCloseWindow(object parameter, out Window window)
        {
            if (parameter is Window parameterWindow)
            {
                window = parameterWindow;
                return true;
            }
            else if (this.defaultWindow != null)
            {
                window = this.defaultWindow;
                return true;
            }

            return Try.Failed(out window);
        }
    }
}
