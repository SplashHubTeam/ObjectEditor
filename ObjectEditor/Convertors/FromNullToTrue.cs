using System;
using System.Globalization;
using System.Windows.Data;

namespace ObjectEditor.Convertors
{
	public static class NotNull
	{
		public static readonly IValueConverter ToTrue = new NotNullToTrueConverter();
	}

	public class NotNullToTrueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
