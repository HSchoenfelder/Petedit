using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines an attached property that allows a DrawMode to be set.
    /// </summary>
    public class DrawModeExtension : DependencyObject
    {
        /// <summary> Gets the associated draw mode of the dependency object. </summary>
        public static DrawMode GetAssociatedDrawMode(DependencyObject obj)
        {
            return (DrawMode)obj.GetValue(AssociatedDrawModeProperty);
        }

        /// <summary> Sets the associated draw mode of the dependency object. </summary>
        public static void SetAssociatedDrawMode(DependencyObject obj, DrawMode value)
        {
            obj.SetValue(AssociatedDrawModeProperty, value);
        }

        /// <summary> The AssociatedDrawMode AttachedProperty. </summary>
        public static readonly DependencyProperty AssociatedDrawModeProperty =
            DependencyProperty.RegisterAttached("AssociatedDrawMode",
            typeof(DrawMode),
            typeof(DrawModeExtension));
    }
}
