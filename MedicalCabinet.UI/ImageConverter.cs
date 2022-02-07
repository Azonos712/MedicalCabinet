using MedicalCabinet.UI.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MedicalCabinet.UI
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] imageData = value as byte[];
            return ImageUtility.BytesToImageSource(imageData);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = value as ImageSource;
            return ImageUtility.ImageSourceToBytes(imageSource);
        }
    }
}
