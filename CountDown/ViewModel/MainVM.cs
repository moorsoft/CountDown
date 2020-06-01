using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CountDown.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        const int CountDownMinutes = 5;
        const int CountDownSeconds = CountDownMinutes * 60;

        readonly DateTime MeetingStartTime;

        readonly DispatcherTimer timer = new DispatcherTimer();

        public MainVM()
        {
            MeetingStartTime = DateTime.Now;
            if (DateTime.Now.Minute > 30)
            {
                MeetingStartTime = new DateTime(MeetingStartTime.Year, MeetingStartTime.Month, MeetingStartTime.Day, MeetingStartTime.Hour, 0, 0).AddHours(1);
            }
            else
            {
                MeetingStartTime = new DateTime(MeetingStartTime.Year, MeetingStartTime.Month, MeetingStartTime.Day, MeetingStartTime.Hour, 0, 0).AddMinutes(30);
            }
            //MeetingStartTime = DateTime.Now.AddMinutes(CountDownMinutes).AddSeconds(5);
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private double progressValue = 0;
        public double ProgressValue
        {
            get => progressValue;
            set
            {
                if (progressValue != value)
                {
                    progressValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double ProgressTotal { get; } = CountDownSeconds;

        private string downTime = "5:00";
        public string DownTime
        {
            get => downTime;
            set
            {
                if (downTime != value)
                {
                    downTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool visible = false;
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnPropertyChanged();
                }
            }
        }

        private double opacity = 0;
        public double Opacity
        {
            get => opacity;
            set
            {
                if (opacity != value)
                {
                    opacity = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //ProgressValue += 1;
            TimeSpan timeToGo = MeetingStartTime - DateTime.Now;
            Visible = timeToGo.TotalSeconds < CountDownSeconds;
            Opacity = (Visible ? 1 : 0);
            if (Visible)
            {
                if (timeToGo.TotalSeconds > 0)
                {
                    ProgressValue = CountDownSeconds - timeToGo.TotalSeconds;
                    DownTime = timeToGo.ToString(@"mm\:ss");
                }
                else
                {
                    ProgressValue = CountDownSeconds;
                    DownTime = "0:00";
                    timer.Stop();
                    Application.Current.Shutdown();
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
