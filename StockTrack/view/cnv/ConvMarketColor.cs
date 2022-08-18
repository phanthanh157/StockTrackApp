using System;
using System.Globalization;
using System.Windows.Data;
using StockTrack.model;

namespace StockTrack.view.cnv
{
    class ConvMarketColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typeColor = (JugdeColor)value;
            string color = "Gray";

            switch (typeColor)
            {
                case JugdeColor.Red:
                    color = "Red";
                    break;
                case JugdeColor.Yellow:
                    color = "Yellow";
                    break;
                case JugdeColor.Green:
                    color = "LawnGreen";
                    break;
                case JugdeColor.Cyan:
                    color = "Cyan";
                    break;
                case JugdeColor.Pink:
                    color = "Pink";
                    break;
                case JugdeColor.Gray:
                    break;
                default:
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
