using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to convert an integer value to a Visibility value.
    /// Visible will be returned only of the value is not 0 or 1.
    /// </summary>
    public class TextBlockVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer value to a Visibility value.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>Visible if the value was not 0 or 1; otherwise Collapsed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;
            return count == 0 || count == 1 ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
