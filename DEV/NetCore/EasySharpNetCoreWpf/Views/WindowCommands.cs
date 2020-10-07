using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Input;

namespace EasySharpNetCoreWpf.Views
{
    public static class WindowBehaviours
    {
        public static readonly DependencyProperty CloseWindowCommandProperty =
            DependencyProperty.Register(
                "CloseWindow",
                typeof(ICommand),
                typeof(Window),
                new PropertyMetadata(OnCloseWindowCommandPropertyChanged));

        public static ICommand GetCloseWindowCommand(DependencyObject obj)
        {
            return (ICommand)obj?.GetValue(CloseWindowCommandProperty);
        }

        public static void SetCloseWindowCommand(DependencyObject obj, ICommand value)
        {
            obj?.SetValue(CloseWindowCommandProperty, value);
        }

        private static void OnCloseWindowCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            Contract.Requires<ArgumentException>(window != null, "must be window");

            window.CommandBindings.Add(new CommandBinding(e.NewValue as ICommand));
        }
    }
}
