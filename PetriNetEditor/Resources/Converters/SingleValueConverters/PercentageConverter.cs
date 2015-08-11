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
    /// This class defines a converter that can be used to return a value that equals a certain percentage
    /// of the original value.
    /// </summary>
    public class PercentageConverter : IValueConverter
    {
        /// <summary>
        /// Returns a value that is calculated by taking the percentage specified by the converter parameter
        /// of the provided value.
        /// </summary>
        /// <param name="value">The provided value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A value that is calculated by taking the percentage specified by the converter parameter
        /// of the provided value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
