using CountDown.Properties;
using CountDown.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Screen = System.Windows.Forms.Screen;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MonitorSelection : Window, INotifyPropertyChanged
    {
        public Monitor SelectedMonitor { get; set; }

        public int SelectVocals { get; set; } = 0;

        public List<Monitor> MonitorList { get; set; } = new List<Monitor>();

        private string buttonText = "OK";
        public string ButtonText
        {
            get => buttonText;
            set
            {
                if (buttonText != value)
                {
                    buttonText = value;
                    OnPropertyChanged();
                }
            }
        }

        ClockWindow mainView;

        public MonitorSelection()
        {
            InitializeComponent();
            this.DataContext = this;

            int i = 0;
            foreach (var s in Screen.AllScreens)
            {
                Rectangle resolution = s.Bounds;
                MonitorList.Add(new Monitor() { MonitorNumber = i, MonitorName = $"Monitor {i + 1}", Screen = s, ScreenCapture = ScreenUtils.CaptureScreen(s.WorkingArea.Left, s.WorkingArea.Top, s.WorkingArea.Width, s.WorkingArea.Height) });
                i++;
            }
            SelectedMonitor = MonitorList[Settings.Default.MonitorNumber];
            SelectVocals = Settings.Default.MusicType;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (mainView == null)
            {
                Settings.Default.MonitorNumber = SelectedMonitor.MonitorNumber;
                Settings.Default.MusicType = SelectVocals;
                Settings.Default.Save();

                mainView = new ClockWindow((SelectVocals == 1), SelectedMonitor);
                ButtonText = "Cancel";
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            mainView?.Close();
        }
    }

    public class Monitor
    {
        public string MonitorName { get; set; }

        public int MonitorNumber { get; set; }

        public Screen Screen { get; set; }

        public ImageBrush ScreenCapture { get; set; }
    }
}
