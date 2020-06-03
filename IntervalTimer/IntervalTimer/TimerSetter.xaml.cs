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
using System.Windows.Shapes;

namespace IntervalTimer
{
    /// <summary>
    /// TimerSetter.xaml の相互作用ロジック
    /// </summary>
    public partial class TimerSetter : Window
    {

        private MainWindow MainWindowInstance;

        public TimerSetter(MainWindow Instance)
        {
            InitializeComponent();
            MainWindowInstance = Instance;
        }

        //HACK:あろうことに、TimerSetterクラスからMainWindowクラスのメンバ変数を直接書き換えている。
        //本来は、どうにかしてそうしない方法を模索すべきなのだろうが、今回は時間がないためこうしている。
        private void WhenTimerSettingOKButtonPressed(object sender, RoutedEventArgs e)
        {
            int TimerOneSeconds = 0;
            int TimerTwoSeconds = 0;

            TimerOneSeconds = int.Parse(Timer1Minutes.Text) * 60 + int.Parse(Timer1Seconds.Text);
            TimerTwoSeconds = int.Parse(Timer2Minutes.Text) * 60 + int.Parse(Timer2Seconds.Text);

            MainWindowInstance.MaxAllTimerRepatedTimes = int.Parse(RepeatedTimes.Text);

            MainWindowInstance.TimerInitialSeconds[0] = TimerOneSeconds;
            MainWindowInstance.TimerInitialSeconds[1] = TimerTwoSeconds;

            Close();

        }
    }
}
