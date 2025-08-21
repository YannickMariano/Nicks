using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NicksProject.View.Converters
{
    public class StatutCommandeToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string statut = value?.ToString()?.Trim().ToLowerInvariant() ?? "";
            return statut switch
            {
                "a consommer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#154D71")),
                "a récupérer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1C352D")),
                "a livrer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#932F67")),
                _ => Brushes.LightGray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
