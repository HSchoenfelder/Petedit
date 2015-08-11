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
    /// This class defines a converter that can be used to package multiple values into an object array.
    /// </summary>
    public class MultiToArrayConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the individual values into an object array.
        /// </summary>
        /// <param name="values">The provided values.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>An object array that contains the individual values.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToArray();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
