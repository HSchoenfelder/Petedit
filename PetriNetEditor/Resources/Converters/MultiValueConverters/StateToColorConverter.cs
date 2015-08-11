using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to express node states with different colors.
    /// </summary>
    public class StateToColorConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts node states to colors.
        /// </summary>
        /// <param name="values">The provided values.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A Brush of the color that results from the current state.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush b = Brushes.Black;

            if (values.Length == 3)
            {
                // IsHighlighted
                if ((bool)values[0])
                    b = Brushes.Red;
                // IsDrawTarget
                if ((bool)values[1])
                    b = Brushes.Red;
                // Selected
                if ((bool)values[2])
                    b = Brushes.DeepSkyBlue;
            }
            return b;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
