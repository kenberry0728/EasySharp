﻿using System;

namespace EasySharpWpf.Commands.Core
{
    public class CommandBase
    {
        public event EventHandler CanExecuteChanged;

        public CommandBase(Func<object, bool> canExecute)
        {
            this.CanExecuteFunc = canExecute;
        }

        public CommandBase()
        {
            this.CanExecuteFunc = x => true;
        }

        protected Func<object, bool> CanExecuteFunc { get; }

        public void NotifyCanExecuteChanged(object sender, EventArgs e)
        {
            this.CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
