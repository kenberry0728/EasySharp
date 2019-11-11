using System;
using System.Windows;

namespace EasySharpNetCoreWpf.Threading
{
    public static class WpfDispatcher
    {
        public static void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static void BeginInvoke(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }
    }
}
