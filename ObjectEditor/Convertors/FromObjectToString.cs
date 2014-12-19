using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectEditor.Convertors
{
    public static class FromObject
    {
        public new static IValueConverter ToString = new ToStringConvertor();
    }

    public class ToStringConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
