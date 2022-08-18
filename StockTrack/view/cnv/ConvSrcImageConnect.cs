using System;
using System.Globalization;
using System.Windows.Data;

namespace StockTrack.view.cnv
{
    class ConvSrcImageConnect : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typeColor = (StatusConnected)value;
            string src = "./image/bullet_red.png";

            if (typeColor == StatusConnected.Connected)
            {
                src = "./image/bullet_green.png";
            }

            return src;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
