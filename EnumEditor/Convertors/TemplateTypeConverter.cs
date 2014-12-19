using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EnumEditor
{
    public class StringToListConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var text = value as string;

            if (text != null)
            {
                return new List<string>(text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            var templates = value as List<string>;

            if (templates != null && CanConvertTo(context, destinationType))
            {
                return string.Join(", ", templates);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
