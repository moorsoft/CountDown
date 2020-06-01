using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace CountDown.Converters
{
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class BoolToVisibilityConverter : MarkupExtension,  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool swapParameters = false;
            if (parameter is string parameterString)
            {
                if (parameterString.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    swapParameters = true;
                }
            }
            if (value is bool b)
            {
                if (b)
                {
                    return (b != swapParameters ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
                }
            }

            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
