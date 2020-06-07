using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Screen = System.Windows.Forms.Screen;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MonitorSelection : Window
    {
        public int MonitorIndex { get; set; } = 0;

        public int SelectVocals { get; set; } = 0;

        public List<Monitor> MonitorList { get; set; } = new List<Monitor>();

        public MonitorSelection()
        {
            InitializeComponent();
            this.DataContext = this;

            for(int i = 1; i < Screen.AllScreens.Length; i++)
            {
                MonitorList.Add(new Monitor() { MonitorName = $"Monitor {i+1}" });
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
    }
}
