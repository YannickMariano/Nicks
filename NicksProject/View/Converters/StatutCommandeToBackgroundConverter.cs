using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NicksProject.View.Converters
{
    public class StatutCommandeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string statut = value?.ToString()?.Trim().ToLowerInvariant() ?? "";
            return statut switch
            {
                "a consommer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBDCE5")),
                "a récupérer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6B28B")),
                "a livrer" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5C9B0")),
                _ => Brushes.Gray

            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
