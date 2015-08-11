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
    /// This class defines a converter that can be used to add a specific integer value to the provided value.
    /// </summary>
    public class AddConverter : IValueConverter
    {
        /// <summary>
        /// Adds a specific integer value to the provided value.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The value to add as a string.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>The original value increased by the value provided with the converter parameter.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + Int32.Parse((String)parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
