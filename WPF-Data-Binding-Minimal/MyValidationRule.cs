using System.Globalization;
using System.Windows.Controls;

namespace WPF_Data_Binding_Minimal
{
    public class MyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str == null)
            {
                return new ValidationResult(false, "is null or not a string");
            }
            if ("".Equals(str))
            {
                return new ValidationResult(false, "is empty");
            }
            return new ValidationResult(true, null);
        }
    }

    public class NotAValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str != null && str.Contains("a"))
            {
                return new ValidationResult(false, "contains 'a'");
            }
            return new ValidationResult(true, null);
        }
    }

    public class NotBValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str != null && str.Contains("b"))
            {
                return new ValidationResult(false, "contains 'b'");
            }
            return new ValidationResult(true, null);
        }
    }
}