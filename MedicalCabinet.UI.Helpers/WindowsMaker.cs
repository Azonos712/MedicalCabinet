using System.Windows;
using System.Windows.Controls;

namespace MedicalCabinet.UI.Helpers
{
    public class WindowsMaker
    {
        public static void ShowWindow(Window parent, Window child)
        {
            child.Show();
            parent.Close();
        }

        public static void ShowPopUp(Window window, Grid background)
        {
            background.Visibility = Visibility.Visible;
            window.ShowDialog();
            background.Visibility = Visibility.Collapsed;
        }
    }
}
