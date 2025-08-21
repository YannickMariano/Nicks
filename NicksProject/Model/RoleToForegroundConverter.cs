using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NicksProject.Model
{
    public class RoleToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = value?.ToString()?.ToLower();
            return role switch
            {
                "serveur" => new SolidColorBrush(Color.FromRgb(239, 130, 13)), // orange
                "caissier" => new SolidColorBrush(Color.FromRgb(178, 49, 44)), // rouge foncé
                _ => Brushes.Black
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
