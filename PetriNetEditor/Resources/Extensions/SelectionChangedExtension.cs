using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace PetriNetEditor
{
    /// <summary>
    /// This class allows a Command to be set on a ComboBox. The Command is fired when the dropdown menu of
    /// the ComboBox closes.
    /// </summary>
    public class SelectionChangedExtension
    {
        /// <summary> Gets the associated Command of the dependency object. </summary>
        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        /// <summary> Sets the associated Command of the dependency object. </summary>
        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }

        /// <summary> The Command AttachedProperty. </summary>
        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.RegisterAttached("Command", 
            typeof(ICommand),
            typeof(SelectionChangedExtension), 
            new PropertyMetadata(OnCommandPropertyChanged));
        
        /// <summary>
        /// Called when the Command property changes. Registers an event handler to the DropDownClosed event of
        /// the ComboBox. The registered event handler is responsible for invoking the Command.
        /// </summary>
        /// <param name="depObj">The dependency object on which the change occurred.</param>
        /// <param name="args">The event data that contains the new value.</param>
        public static void OnCommandPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ComboBox cb = (ComboBox)depObj;
            if (cb != null)
            {
                cb.DropDownClosed += new EventHandler(DropDownClosed);
            }
        }
        
        /// <summary>
        /// Called when the DropDownClosed event of the ComboBox occurs. Invokes the associated Command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
                ICommand command = cb.GetValue(CommandProperty) as ICommand;
                if (command != null)
                {
                    command.Execute(cb.SelectedItem);
                }
            }
        }
    }
}
