﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InfinitTools.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
                return Visibility.Visible;
            
            return (bool)value == true ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                return false;

            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}
