using ChatApp.Views;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Windows;

namespace ChatApp
{
    public class Bootstrap : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }


    }
}
