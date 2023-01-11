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

    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
       
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
                (bool)value ? Visibility.Hidden : Visibility.Visible;
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
                throw new NotImplementedException();
       
    }
}
