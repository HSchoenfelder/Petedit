using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines attached properties that can be used for mouse capturing.
    /// </summary>
    public class CaptureExtension
    {
        /// <summary> Gets a value that indicates whether the dependency object is capturing the mouse. </summary>
        public static bool GetIsCapturing(UIElement element)
        {
            return (bool)element.GetValue(IsCapturingProperty);
        }

        /// <summary> Sets a value that indicates whether the dependency object is capturing the mouse. </summary>
        public static void SetIsCapturing(UIElement element, bool value)
        {
            element.SetValue(IsCapturingProperty, value);
        }

        /// <summary> The IsCapturing AttachedProperty. </summary>
        public static readonly DependencyProperty IsCapturingProperty =
            DependencyProperty.RegisterAttached("IsCapturing",
            typeof(bool),
            typeof(CaptureExtension),
            new PropertyMetadata(false, OnIsCapturingPropertyChanged));

        /// <summary>
        /// Called when the IsCaptured property changes. Has the dependency object capture the mouse if the value 
        /// is true and release mouse capture if the value is false.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new value.</param>
        private static void OnIsCapturingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                Mouse.Capture(uie);
            }
            else
            {
                Mouse.Capture(null);
            }
        }
    }
}
