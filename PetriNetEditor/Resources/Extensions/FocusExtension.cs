using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines attached properties that can be used for focus management.
    /// </summary>
    public static class FocusExtension
    {
        /// <summary> 
        /// Gets a value that indicates whether the dependency object is supposed to have keyboard focus. 
        /// </summary>
        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsActiveProperty);
        }

        /// <summary> 
        /// Sets a value that indicates whether the dependency object is supposed to have keyboard focus. 
        /// </summary>
        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }

        /// <summary> The IsActive AttachedProperty. </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", 
            typeof(bool), 
            typeof(FocusExtension),
            new UIPropertyMetadata(false, OnIsActivePropertyChanged));

        /// <summary>
        /// Called when the IsActive property changes. Enables focus on and has the dependency object grab keyboard
        /// focus if the value is true and shifts focus away and disables focus if the value is false.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new value.</param>
        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focusable = true;
                uie.Focus();
            }
            else
            {
                //uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                uie.Focusable = false;
            }
        }
    }
}
