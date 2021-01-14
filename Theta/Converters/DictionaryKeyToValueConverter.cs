using System;
using System.Globalization;
using Theta.Constants;
using Xamarin.Forms;

namespace Theta.Converters
{
    public class DictionaryKeyToValueConverter : BindableObject, IValueConverter
    {
        public static BindableProperty ConstantValueProperty = BindableProperty.Create("ConstantValue", typeof(int), typeof(DictionaryKeyToValueConverter));

        public int ConstantValue
        {
            get => (int)GetValue(ConstantValueProperty);
            set => SetValue(ConstantValueProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return "--";

            if(ConstantValue == 1)
            {
                return DictionariesConstants.NodeTypes[(int)value];
            }
            else if(ConstantValue == 2)
            {
                return DictionariesConstants.Statuses[(int)value];
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
