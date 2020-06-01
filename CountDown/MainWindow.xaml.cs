using CountDown.Extensions;
using CountDown.ViewModel;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Screen targetScreen = (Screen.AllScreens.Length > 1) ? Screen.AllScreens[1] : Screen.AllScreens[0];

            var viewport = targetScreen.WorkingArea;
            this.Top = viewport.Top;
            this.Left = viewport.Left;
            this.Width = viewport.Width;
            this.Height = viewport.Height;

            using (Bitmap bmp = new Bitmap((int)viewport.Width, (int)viewport.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen((int)this.Left, (int)this.Top, 0, 0, bmp.Size);
                }
                FirstColumn.Width = new GridLength(viewport.Width / 5.5);
                FirstRow.Height = new GridLength(viewport.Width / 5.5);
                progress.Width = viewport.Width / 6.7;
                progress.Height = viewport.Width / 6.7;
                textBlock.FontSize = viewport.Width / 25;

                Background = new ImageBrush(bmp.ToBitmapSource());
            }
            DataContext = new MainVM();
        }
    }
}
