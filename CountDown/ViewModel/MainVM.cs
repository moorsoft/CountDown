using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace CountDown.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        const int CountDownMinutes = 5;
        const int CountDownSeconds = CountDownMinutes * 60;

        readonly DateTime MeetingStartTime;

        readonly DispatcherTimer timer = new DispatcherTimer();

        readonly MediaPlayer mediaPlayer = new MediaPlayer();
        readonly string[] MusicFiles = { };
        readonly Random random = new Random();

        // With vocals
        //readonly string musicUrl = "https://pubmedia.jw-api.org/GETPUBMEDIALINKS?output=json&pub=sjjc&fileformat=MP3%2CAAC&alllangs=0&langwritten=E&txtCMSLang=E";
        // With music only
        //readonly string musicAndVocalsUrl = "https://pubmedia.jw-api.org/GETPUBMEDIALINKS?output=json&pub=sjjm&fileformat=MP3%2CAAC&alllangs=0&langwritten=E&txtCMSLang=E";

        public MainVM(bool PlayVocal)
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
            //MeetingStartTime = DateTime.Now.AddMinutes(CountDownMinutes).AddSeconds(2);
            //MeetingStartTime = DateTime.Now.AddSeconds(5);

            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "JWLibrary");
            if (Directory.Exists(folder))
            {
                MusicFiles = System.IO.Directory.GetFiles(folder, (PlayVocal ? "sjjc*.mp3" : "sjjm*.mp3"));

                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                PlayNewMedia();
            }
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PlayNewMedia();
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

        private void PlayNewMedia()
        {
            if (MusicFiles?.Length > 0)
            {
                string filename = MusicFiles[random.Next(MusicFiles.Length)];
                mediaPlayer.Open(new Uri(filename));
                mediaPlayer.Play();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //ProgressValue += 1;
            TimeSpan timeToGo = MeetingStartTime - DateTime.Now;
            Visible = timeToGo.TotalSeconds < CountDownSeconds;
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
                    mediaPlayer.Stop();
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
