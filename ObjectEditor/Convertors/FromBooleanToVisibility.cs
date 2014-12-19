using System;
using System.Windows;
using System.Windows.Data;

namespace ObjectEditor.Convertors
{
    public static class False
    {
        public static readonly IValueConverter ToCollapsed = new BooleanToVisibilityConvertor(Visibility.Visible, Visibility.Collapsed);
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConvertor : IValueConverter
    {
        Visibility True;
        Visibility False;

        public BooleanToVisibilityConvertor(Visibility t, Visibility f)
        {
            True = t;
            False = f;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Visibility)value != Visibility.Visible;
        }
    }
}

