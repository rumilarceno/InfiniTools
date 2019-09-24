using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InfinitTools.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return Visibility.Hidden;

            return (int)value < 1 ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                return 0;

            return (Visibility)value == Visibility.Visible ? 1 : 0;
        }
    }
}
