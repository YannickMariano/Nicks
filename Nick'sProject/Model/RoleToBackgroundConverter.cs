using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Nick_sProject.Model
{
    public class RoleToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = value?.ToString()?.ToLower();
            return role switch
            {
                "serveur" => new SolidColorBrush(Color.FromRgb(255, 235, 205)), // fond orangé clair
                "caissier" => new SolidColorBrush(Color.FromRgb(255, 225, 225)), // fond rosé clair
                _ => Brushes.LightGray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
