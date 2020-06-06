using System.Windows;

using Screen = System.Windows.Forms.Screen;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            ClockWindow mainView;
            // more than 2 monitors we need to ask to select which monitor
            if (Screen.AllScreens.Length > 2)
            {
                MonitorSelection select = new MonitorSelection();
                select.ShowDialog();

                mainView = new ClockWindow(select.SelectedMonitor);
                mainView.Show();
            }
            else
            {
                mainView = new ClockWindow();
                mainView.Show();
            }
            mainView.Closed += (o, eventArgs) => Shutdown();
        }
    }
}
