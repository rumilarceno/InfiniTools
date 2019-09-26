using System;
using System.Globalization;
using System.Windows.Data;

namespace InfinitTools.Converters
{
    public class ExtensionToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return false;

            return (int)value == 0 ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
                return 0;

            return (bool)value == true ? 1 : 0;
        }
    }
}
