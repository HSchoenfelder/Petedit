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
    /// This class defines a converter that can be used to package the coordinates of a mouse move event
    /// into the command parameter of a hooked up command.
    /// </summary>
    public class MouseMoveEventConverter : IValueConverter
    {
        /// <summary>
        /// Converts the coordinates of a mouse move event into a Point.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A Point that contains the coordinates of the mouse move event. null if the 
        /// conversion failed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MouseEventArgs e = value as MouseEventArgs;
            Canvas sender = parameter as Canvas;

            if (e != null && sender != null)
            {
                return new Point(e.GetPosition(sender).X, e.GetPosition(sender).Y);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
