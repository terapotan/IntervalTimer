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
    public delegate void TimerEndEventHandler(object Sender, EventArgs EventArgs);
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private int TimerDisplayedSeconds = 0;

        /// <summary>
        /// <para>
        /// 実行中のタイマーナンバーとは、設定画面に書いてあるタイマー1,2の番号のことである。
        /// </para>
        /// <para>
        /// もしこの変数の値が1であれば、それはタイマー1で設定された初期時間でカウントしていることを示す。
        /// </para>
        /// </summary>
        private int ExecutingTimerNumber = 0;

        /// <summary>
        /// 全てのタイマーを繰り返した回数を示す。
        /// タイマー1とタイマー2が設定されているのであれば、1,2,1,2と繰り返せば
        /// この変数には2が格納される。
        /// </summary>
        private int AllTimerRepeatedTimes = 0;

        /// <summary>
        /// 最大のタイマー繰り返し回数がここに格納される。
        /// AllTimerRepeatedTimesがこの変数の値を超えると、タイマーは終了する。
        /// (次のタイマーが始まらない)
        /// </summary>
        public int MaxAllTimerRepatedTimes = 3;
        private DispatcherTimer Timer;
        private event TimerEndEventHandler TimerEnd;

        /*HACK:本来はユーザー定義のデータを用いるべきである。
        なぜなら、このままではint型でありさえすればどんな値でもぶち込めるからである。
        が時間がないため、とりあえずこのようにしている。あとで修正が必要である。*/

        /// <summary>
        /// タイマーの初期表示秒数の配列。20,10,5であれば20,10,5秒のタイマーが連続で
        /// 始動することになる。
        /// </summary>
        public int[] TimerInitialSeconds;


        
        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();

            //TODO:ここら辺の初期化処理は、ダイアログからデータを受け取る時には変更すること。
            TimerInitialSeconds = new int[2];

            TimerInitialSeconds[0] = 5;
            TimerInitialSeconds[1] = 2;

            TimerEnd += new TimerEndEventHandler(TimerEndHandler);
        }


        private void WhenTimerSettingButtonPressed(object sender, RoutedEventArgs e)
        {
            var WindowInstance = new TimerSetter(this);
            WindowInstance.ShowDialog();
        }

        private void WhenTimerStartButtonPressed(object sender, RoutedEventArgs e)
        {
            InitializeTimer();
            TimerDisplayedSeconds = TimerInitialSeconds[0];
            Timer.Start();
        }

        /// <summary>
        /// ウィンドウに表示する経過秒数(01:23とか)の文字列を秒数(120秒など)から生成する。
        /// 例:Seconds=122 戻り値:"02:02"
        /// </summary>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        private string GenerateTimerString(int Seconds)
        {
            int DisplayedMinutes = 0;
            int DisplayedSeconds = 0;

            DisplayedMinutes = Seconds / 60;
            DisplayedSeconds = Seconds - DisplayedMinutes * 60;

            return DisplayedMinutes.ToString("00") + ":" + DisplayedSeconds.ToString("00");

        }

        private void SetupTimer()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(1.0),
            };

            Timer.Tick += (s,e) =>
            {
                TimerString.Text = GenerateTimerString(TimerDisplayedSeconds);
                TimerDisplayedSeconds -= 1;
                if(TimerDisplayedSeconds == -1)
                {
                    TimerEnd(this, EventArgs.Empty);
                }
            };
        }

        private void TimerEndHandler(object Sender, EventArgs EventArgs)
        {
            Timer.Stop();
            ExecutingTimerNumber += 1;

            if(ExecutingTimerNumber >= TimerInitialSeconds.Length)
            {
                AllTimerRepeatedTimes += 1;
                ExecutingTimerNumber = 0;
            }

            //最大繰り返し回数を超えている場合、次のタイマーを始動しない。
            if(AllTimerRepeatedTimes >= MaxAllTimerRepatedTimes)
            {
                return;
            }

            TimerDisplayedSeconds = TimerInitialSeconds[ExecutingTimerNumber];
            SetupTimer();
            Timer.Start();
        }

        /// <summary>
        /// タイマー動作に関わる変数を全て初期化する。
        /// 具体的には、TimerDisplaySeconds,ExecutingTimerNumber,AllTimerRepeatedTimesを0にセットする。
        /// </summary>
        private void InitializeTimer()
        {
            TimerDisplayedSeconds = 0;
            ExecutingTimerNumber = 0;
            AllTimerRepeatedTimes = 0;
        }
    }
}
