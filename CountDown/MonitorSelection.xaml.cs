using System.Windows;
using System.Windows.Controls;

using Screen = System.Windows.Forms.Screen;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MonitorSelection : Window
    {
        public int SelectedMonitor { get; private set; } = 2;
        public MonitorSelection()
        {
            InitializeComponent();

            BtnMonitor2.Visibility = (Screen.AllScreens.Length > 1 ? Visibility.Visible : Visibility.Collapsed);
            BtnMonitor3.Visibility = (Screen.AllScreens.Length > 2 ? Visibility.Visible : Visibility.Collapsed);
            BtnMonitor4.Visibility = (Screen.AllScreens.Length > 3 ? Visibility.Visible : Visibility.Collapsed);
            BtnMonitor5.Visibility = (Screen.AllScreens.Length > 4 ? Visibility.Visible : Visibility.Collapsed);
        }

        private void BtnMonitor_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse((sender as Button).Tag.ToString(), out int monitor))
            {
                SelectedMonitor = monitor;
                DialogResult = true;
            }
        }
    }
}
