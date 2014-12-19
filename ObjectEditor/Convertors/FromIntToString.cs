using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectEditor.Convertors
{
    public static class Int
    {
        public new static readonly IValueConverter ToString = new IntToStringConverter();
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int)
            {
                return ((int)value).ToString();
            }

            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int res;
            return int.TryParse((string)value, out res) ? res : 0;
        }
    }
}
