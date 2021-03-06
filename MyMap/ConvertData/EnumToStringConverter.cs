using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyMap.ConvertData
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string string_model = string.Empty;

            if (value != null)
                string_model = (value).ToString();

            return string_model;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
