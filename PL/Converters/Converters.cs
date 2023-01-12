using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BO;

namespace PL.Converters
{
    public class CategoryColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value == null ? Brushes.White : (Category)value switch
            {
                Category.PHONE => Brushes.LightSeaGreen,
                Category.TABLET => Brushes.Cornsilk,
                Category.SCREEN => Brushes.Coral,
                Category.LAPTOP => Brushes.Violet,
                Category.SMART_WATCH => Brushes.AliceBlue,
                _ => Brushes.White
            };
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }

    public class VisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }
    public class notVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }
}
