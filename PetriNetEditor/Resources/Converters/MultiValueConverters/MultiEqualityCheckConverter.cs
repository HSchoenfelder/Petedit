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
    /// This class defines a converter that can be used to check if first of the provided values is the same
    /// as the second.
    /// </summary>
    public class MultiEqualityCheckConverter : IMultiValueConverter
    {
        /// <summary>
        /// Checks if first of the provided values is the same as the second.
        /// </summary>
        /// <param name="values">The provided values.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>true if the first of the provided values equals the second; otherwise false.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].Equals(values[1]))
                return true;
            else
                return false;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
