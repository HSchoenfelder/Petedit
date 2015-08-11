using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to return a value of 11 scaled by the provided value.
    /// </summary>
    public class FontSizeConverter : IValueConverter
    {
        /// <summary>
        /// Returns a value of 11 scaled by the provided value.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A value of 11 scaled by the provided value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 11 * (int)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
