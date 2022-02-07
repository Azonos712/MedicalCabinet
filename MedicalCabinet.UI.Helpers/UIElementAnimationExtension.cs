using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MedicalCabinet.UI.Helpers
{
    public static class UIElementAnimationExtension
    {
        public static void OpacityAnimation(this FrameworkElement element, double to, TimeSpan duration)
        {
            DoubleAnimation animation = new() { To = to, Duration = duration };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        public static void MarginAnimation(this FrameworkElement element, Thickness to, TimeSpan duration)
        {
            ThicknessAnimation animation = new() { To = to, Duration = duration };
            element.BeginAnimation(FrameworkElement.MarginProperty, animation);
        }

        public static void ForegroundColorAnimation(this TextBlock element, Color to, TimeSpan duration)
        {
            ColorAnimation animation = new() { To = to, Duration = duration };
            element.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public static void ForegroundColorAnimation(this Control element, Color to, TimeSpan duration)
        {
            element.Foreground = element.Foreground.Clone();
            ColorAnimation animation = new() { To = to, Duration = duration };
            element.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public static void BackgroundColorAnimation(this Control element, Color to, TimeSpan duration)
        {
            element.Background = element.Foreground.Clone(); //new SolidColorBrush((element.Background as SolidColorBrush).Color);
            ColorAnimation animation = new() { To = to, Duration = duration };
            element.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        //public static void BorderBrushColorAnimation(this Control element, Color from, Color to, TimeSpan duration)
        //{
        //    element.BorderBrush = new SolidColorBrush(from);
        //    ColorAnimation animation = new ColorAnimation { To = to, Duration = duration, AutoReverse = true };
        //    element.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        //}
    }
}
