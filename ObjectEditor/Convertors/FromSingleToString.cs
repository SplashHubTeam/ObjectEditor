using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectEditor.Convertors
{
    public class FromSingle
    {
        public static IValueConverter ToDouble = new SingleToStringConverter();
    }

    public class SingleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !Single.IsNaN((float)value))
            {
                var calInfo = CultureInfo.InvariantCulture;
                return ((Single)value).ToString("F4", calInfo);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float res;
            var calInfo = CultureInfo.InvariantCulture;
            return Single.TryParse((string)value, NumberStyles.Any, calInfo, out res) ? res : 0;
        }
    }
}
