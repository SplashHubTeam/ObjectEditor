using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace EnumEditor.Convertors
{
    public static class List
    {
        public new static readonly IValueConverter ToString = new ListToStringConverter(", ");
    }

    public class ListToStringConverter : IValueConverter
    {
        public string Separator { get; set; }

        public ListToStringConverter() { }

        public ListToStringConverter(string separator)
        {
            Separator = separator;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var list = value as IList;
            if (list == null) return null;

            var res = "";

            foreach (var item in list)
            {
                if (res.Length > 0) res += Separator;
                res += item;
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            return null;
        }
    }
}
