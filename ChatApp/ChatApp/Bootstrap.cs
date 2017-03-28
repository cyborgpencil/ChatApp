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
        public MainWindow MainWindow { get; set; }

        protected override DependencyObject CreateShell()
        {
            MainWindow = ServiceLocator.Current.GetInstance<MainWindow>();
            return MainWindow;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = MainWindow;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
        }

    }
}
