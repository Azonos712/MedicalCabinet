using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MedicalCabinet.UI.UserControls
{
    public partial class TimeControl : UserControl
    {
        public TimeControl()
        {
            InitializeComponent();
            mmTxt.ItemsSource = Enumerable.Range(0, 24).Select(x => IntToTime(x));
            ddTxt.ItemsSource = Enumerable.Range(0, 60).Select(x => IntToTime(x));
            yyTxt.ItemsSource = Enumerable.Range(0, 60).Select(x => IntToTime(x));
        }

        public string Hours {
            get { return (string)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }
        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register("Hours", typeof(string), typeof(TimeControl), new UIPropertyMetadata("", new PropertyChangedCallback(OnTimeChanged), new CoerceValueCallback(CorrectValue)));

        public string Minutes {
            get { return (string)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes", typeof(string), typeof(TimeControl), new UIPropertyMetadata("", new PropertyChangedCallback(OnTimeChanged), new CoerceValueCallback(CorrectValue)));

        public string Seconds {
            get { return (string)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }

        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register("Seconds", typeof(string), typeof(TimeControl), new UIPropertyMetadata("", new PropertyChangedCallback(OnTimeChanged), new CoerceValueCallback(CorrectValue)));

        private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TimeControl control = obj as TimeControl;
            //control.Value = new TimeSpan(int.Parse(control.Hours), int.Parse(control.Minutes), int.Parse(control.Seconds));
        }

        private static object CorrectValue(DependencyObject d, object baseValue)
        {
            if (baseValue.ToString() == "")
                return "00";

            int num = Convert.ToInt32(baseValue);

            return IntToTime(num);
        }

        private static string IntToTime(int num)
        {
            if (num < 10)
                return "0" + num;

            return num.ToString();
        }
    }
}
