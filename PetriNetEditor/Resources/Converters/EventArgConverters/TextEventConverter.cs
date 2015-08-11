using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to package the changed text of a TextChangedEvent 
    /// into the command parameter of a hooked up command alongside the id of the node that triggered the event.
    /// </summary>
    public class TextEventConverter : IValueConverter
    {
        /// <summary>
        /// Returns the id and the changed text of the element that raised a TextChangedEvent.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter that contains the node id.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>An Array that contains the id and the changed text of the element that raised a 
        /// TextChangedEvent. null if the conversion failed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoutedEventArgs e = value as RoutedEventArgs;
            if (e != null)
            {
                TextBox tb = e.Source as TextBox;
                if (tb != null)
                    return new object[] { (String)parameter, tb.Text };
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
