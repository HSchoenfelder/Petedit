using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that can be used to package the coordinates of a mouse button down event 
    /// and the current state of the Shift modifier key into the command parameter of a hooked up command.
    /// </summary>
    public class CanvasModifierMouseButtonEventConverter : IValueConverter
    {
        /// <summary>
        /// Converts the coordinates of a mouse button down event and the current state of the Shift modifier key 
        /// into an object array.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>An object array that contains the coordinates of the mouse button down event and
        /// the current state of the Shift modifier key. null if the conversion failed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MouseButtonEventArgs e = value as MouseButtonEventArgs;
            Canvas sender = parameter as Canvas;

            if (e != null && sender != null)
            {
                return new object[] { new Point(e.GetPosition(sender).X, e.GetPosition(sender).Y),
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
