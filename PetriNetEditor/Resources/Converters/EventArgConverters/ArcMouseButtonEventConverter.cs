using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to package the current state of the Shift modifier 
    /// key into the command parameter of a hooked up command together with the id of the element on which
    /// the event was raised.
    /// </summary>
    public class ArcMouseButtonEventConverter : IValueConverter
    {
        /// <summary>
        /// Returns the current state of the Shift modifier key as a bool value.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The id of the element on which the event was raised.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A the id of the element on which the event was raised and a bool value indicating the 
        /// current state of the Shift modifier key. null if the conversion failed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MouseButtonEventArgs e = value as MouseButtonEventArgs;
            FrameworkElement source = e.Source as FrameworkElement;

            if (source != null)
            {
                return new object[] { (String)parameter, Keyboard.Modifiers == ModifierKeys.Shift };
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
