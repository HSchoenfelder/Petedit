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
    /// This class defines a converter that can be used to multiply the provided value by a specific integer.
    /// </summary>
    public class MultiplyConverter : IValueConverter
    {
        /// <summary>
        /// Multiplies the provided value by a specific integer.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The value to multiply with as a string.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>The original value multiplied by the value provided with the converter parameter.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value * Int32.Parse((String)parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
