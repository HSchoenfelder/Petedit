using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a converter that is used on the LoadedEvent and packages the current width and height of 
    /// the presentation area into the command parameter of a hooked up command.
    /// </summary>
    public class LoadedEventConverter : IValueConverter
    {
        /// <summary>
        /// Converts the current width and height of the element that raised a LoadedEvent into a Point.
        /// </summary>
        /// <param name="value">The event data.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used.</param>
        /// <returns>A Point that contains the current widht and height of the element that raised the LoadedEvent. 
        /// null if the conversion failed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoutedEventArgs e = value as RoutedEventArgs;
            FrameworkElement source = e.Source as FrameworkElement;

            if (e != null && source != null)
            {
                return new Point(source.ActualWidth, source.ActualHeight);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
