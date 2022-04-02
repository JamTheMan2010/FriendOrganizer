using Autofac;
using FriendOrganizer.UI.StartUp;
using System;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootStrapper = new BootStrapper();
            var container = bootStrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, 
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please inform the admin."
                + Environment.NewLine + e.Exception.Message, "Unexpected Error");

            e.Handled = true;

        }
    }
}
