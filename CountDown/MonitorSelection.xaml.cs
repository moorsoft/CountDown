using CountDown.Properties;
using CountDown.Utils;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class MonitorSelection : Window
    {
        public Monitor SelectedMonitor { get; set; }

        public int SelectVocals { get; set; } = 0;

        public List<Monitor> MonitorList { get; set; } = new List<Monitor>();

        public MonitorSelection()
        {
            InitializeComponent();
            this.DataContext = this;

            int i = 0;
            foreach(var s in Screen.AllScreens)
            {
                Rectangle resolution = s.Bounds;
                MonitorList.Add(new Monitor() { MonitorNumber = i, MonitorName = $"Monitor {i+1}", Screen = s, ScreenCapture = ScreenUtils.CaptureScreen(s.WorkingArea.Left, s.WorkingArea.Top, s.WorkingArea.Width, s.WorkingArea.Height) });
                i++;
            }
            SelectedMonitor = MonitorList[Settings.Default.MonitorNumber];
            SelectVocals = Settings.Default.MusicType;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Settings.Default.MonitorNumber = SelectedMonitor.MonitorNumber;
            Settings.Default.MusicType = SelectVocals;
            Settings.Default.Save();
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
