using CountDown.ViewModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {

        public ClockWindow(bool PlayVocal, Monitor monitor)
        {
            InitializeComponent();

            var viewport = monitor.Screen.WorkingArea;
            this.Left = viewport.Left;

            DataContext = new MainVM(PlayVocal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var senderWindow = sender as Window;
            senderWindow.WindowState = WindowState.Maximized;
        }
    }
}
