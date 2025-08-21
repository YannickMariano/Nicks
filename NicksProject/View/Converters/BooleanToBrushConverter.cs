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
    public class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                // Couleur verte quand sélectionné
                return new SolidColorBrush(Color.FromRgb(139, 195, 74)); // #8BC34A
            }

            // Couleur grise quand non sélectionné
            return new SolidColorBrush(Color.FromRgb(102, 102, 102)); // #666
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
