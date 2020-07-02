using System.Windows;

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

            // more than 2 monitors we need to ask to select which monitor
            MonitorSelection select = new MonitorSelection();
            if (select.ShowDialog() ?? false)
            {
                ClockWindow mainView = new ClockWindow((select.SelectVocals == 1), select.SelectedMonitor);
                //mainView.Show();
                mainView.Closed += (o, eventArgs) => Shutdown();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
