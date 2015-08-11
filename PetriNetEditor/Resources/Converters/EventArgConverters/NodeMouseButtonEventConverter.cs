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
    /// This class defines a converter that can be used to package the coordinates of a mouse button down event
    /// relative to the center of the node on which the event occurred and the current state of the Shift modifier 
    /// key into the command parameter of a hooked up command alongside the id of the node that triggered the event.
    /// </summary>
    public class NodeMouseButtonEventConverter : IValueConverter
    {
        /// <summary>
        /// Converts the coordinates of a mouse button down event relative to the center of the node on which the 
        /// event occurred and the current state of the Shift modifier key into an object array together with the
        /// id of the node that triggered the event.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter that contains the node id.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>An object array that contains the id of the node that triggered the event, the coordinates of the 
        /// mouse button event relative to the center of the node on which the event occurred and the current state of 
        /// the Shift modifier key. null if the conversion failed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MouseButtonEventArgs e = value as MouseButtonEventArgs;
            FrameworkElement source = e.Source as FrameworkElement;

            if (e != null && source != null)
            {
                return new object[] { (String)parameter, new Point(e.GetPosition(source).X, e.GetPosition(source).Y),
                                      Keyboard.Modifiers == ModifierKeys.Shift };
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
