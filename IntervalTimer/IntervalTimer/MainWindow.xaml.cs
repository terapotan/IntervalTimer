using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IntervalTimer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private int TimerElapsedSeconds = 0;
        private DispatcherTimer Timer;
        
        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
        }

        private void WhenTimerSettingButtonPressed(object sender, RoutedEventArgs e)
        {
            var WindowInstance = new TimerSetter();
            WindowInstance.ShowDialog();
        }

        private void WhenTimerStartButtonPressed(object sender, RoutedEventArgs e)
        {
            Timer.Start();
        }

        private string GenerateTimerString(int Seconds)
        {
            int DisplayedMinutes = 0;
            int DisplayedSeconds = 0;

            DisplayedMinutes = Seconds / 60;
            DisplayedSeconds = Seconds - DisplayedMinutes * 60;

            return DisplayedMinutes.ToString() + ":" + DisplayedSeconds.ToString();

        }

        private void SetupTimer()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(1.0),
            };

            Timer.Tick += (s,e) =>
            {
                TimerElapsedSeconds += 1;
                TimerString.Text = GenerateTimerString(TimerElapsedSeconds);
            };
        }
    }
}
