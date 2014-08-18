using System.Windows.Controls;

namespace ObjectEditor.Validation
{
    public class IntRule : ValidationRule
    {
        public int Max { get; set; }
        public int Min { get; set; }

        public IntRule()
        {
            Min = int.MinValue;
            Max = int.MaxValue;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int intValue = 0;
            
            try
            {
                if (!string.IsNullOrWhiteSpace((string)value))
                {
                    intValue = int.Parse((string)value, cultureInfo);
                }
            }
            catch
            {
                return new ValidationResult(false, "not correct value");
            }
            if (intValue < Min || intValue > Max)
            {
                return new ValidationResult(false, string.Format("value is not in range [{0}..{1}]", Min, Max));
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
