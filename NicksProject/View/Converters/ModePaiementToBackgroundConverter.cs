using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NicksProject.View.Converters
{
    public class ModePaiementToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mode = value?.ToString()?.Trim().ToLowerInvariant() ?? "";
            return mode switch
            {
                "espèce" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EED9D9")),
                "mobile money" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0D5")),
                _ => Brushes.LightGray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
