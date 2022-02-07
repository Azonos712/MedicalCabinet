using System.Windows;

namespace MedicalCabinet.UI.Helpers
{
    public static class UIElementPropertyExtension
    {
        public static void ToggleVisibility(this UIElement control, int length)
        {
            if (length > 0)
                control.Visibility = Visibility.Collapsed;
            else
                control.Visibility = Visibility.Visible;
        }
    }
}
