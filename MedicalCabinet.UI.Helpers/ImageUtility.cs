using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MedicalCabinet.UI.Helpers
{
    //TODO оптимизация загружаемых изображений
    public static class ImageUtility
    {
        public static BitmapImage PathToImageSource(string path)
        {
            return new BitmapImage(new Uri(path));
        }

        public static BitmapImage BytesToImageSource(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            //image.Freeze();
            return image;
        }

        //public static byte[] ImageSourceToBytes(BitmapImage imageSource)
        //{
        //    Stream stream = imageSource.StreamSource;
        //    byte[] buffer = null;
        //    if (stream != null && stream.Length > 0)
        //    {
        //        using (BinaryReader br = new BinaryReader(stream))
        //        {
        //            buffer = br.ReadBytes((int)stream.Length);
        //        }
        //    }

        //    return buffer;
        //}

        public static byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            byte[] bytes = null;

            if (imageSource is BitmapSource bitmapSource)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using var stream = new MemoryStream();
                encoder.Save(stream);
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }
}
