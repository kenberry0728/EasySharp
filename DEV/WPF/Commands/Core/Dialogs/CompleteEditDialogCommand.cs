using EasySharpStandard.Validations.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EasySharpWpf.Commands.Core.Dialogs
{
    public class CompleteEditDialogCommand : CommandBase, ICommand
    {
        private readonly Window window;

        public CompleteEditDialogCommand(Window window)
        {
            this.window = window;
        }

        public CompleteEditDialogCommand(Window window, Func<object, bool> canExecute) : base(canExecute)
        {
            this.window = window;
        }

        public bool CanExecute(object parameter)
        {
            return this.CanExecuteFunc(parameter);
        }

        public void Execute(object model)
        {
            var validationResults = model.Validate();
            if (validationResults.Any())
            {
                MessageBox.Show(
                    string.Join(
                        Environment.NewLine,
                        validationResults.Select(r => r.ErrorMessage)));
            }
            else
            {
                this.window.DialogResult = true;
            }
        }
    }
}
