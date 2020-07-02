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
        public int MonitorIndex { get; set; } = 0;

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
                MonitorList.Add(new Monitor() { MonitorName = $"Monitor {i+1}", Screen = s, ScreenCapture = ScreenUtils.CaptureScreen(s.WorkingArea.Left, s.WorkingArea.Top, s.WorkingArea.Width, s.WorkingArea.Height) });
                // MonitorList.Add(new Monitor() { MonitorName = $"Monitor {i + 1}", Screen = s });
                i++;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }

    public class Monitor
    {
        public string MonitorName { get; set; }

        public Screen Screen { get; set; }

        public ImageBrush ScreenCapture { get; set; }
    }
}
