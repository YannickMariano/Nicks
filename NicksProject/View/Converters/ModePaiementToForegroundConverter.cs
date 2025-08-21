using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NicksProject.View.Converters
{
    public class ModePaiementToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mode = value?.ToString()?.Trim().ToLowerInvariant() ?? "";
            return mode switch
            {
                "espèce" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#831414")),
                "mobile money" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EA4A10")),
                _ => Brushes.Gray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
