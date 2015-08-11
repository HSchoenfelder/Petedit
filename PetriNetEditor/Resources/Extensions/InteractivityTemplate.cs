using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents a DataTemplate that can be used to set InteractivityItems in a style.
    /// </summary>
    public class InteractivityTemplate : DataTemplate
    {

    }

    /// <summary>
    /// This class represents a FrameworkElement that can be used as an InteractivityTemplate in a style
    /// and that allows interactivity triggers to be set on it.
    /// </summary>
    public class InteractivityItems : FrameworkElement
    {
        /// <summary> Store for the Triggers property. </summary>
        private List<System.Windows.Interactivity.TriggerBase> _triggers;

        /// <summary>
        /// Gets the list of triggers to be set with the style.
        /// </summary>
        new public List<System.Windows.Interactivity.TriggerBase> Triggers
        {
            get
            {
                if (_triggers == null)
                    _triggers = new List<System.Windows.Interactivity.TriggerBase>();
                return _triggers;
            }
        }

        /// <summary> Gets the template on which the interactivity items are set. </summary>
        public static InteractivityTemplate GetTemplate(DependencyObject obj)
        {
            return (InteractivityTemplate)obj.GetValue(TemplateProperty);
        }

        /// <summary> Sets the template on which the interactivity items are set. </summary>
        public static void SetTemplate(DependencyObject obj, InteractivityTemplate value)
        {
            obj.SetValue(TemplateProperty, value);
        }

        /// <summary> The Template AttachedProperty. </summary>
        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.RegisterAttached("Template",
            typeof(InteractivityTemplate),
            typeof(InteractivityItems),
            new PropertyMetadata(default(InteractivityTemplate), OnTemplateChanged));

        /// <summary>
        /// Called when the Template property changes. Adds the stored triggers to the dependency object
        /// on which the template was set.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new template.</param>
        private static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InteractivityTemplate dt = (InteractivityTemplate)e.NewValue;
            dt.Seal();
            InteractivityItems ih = (InteractivityItems)dt.LoadContent();
            System.Windows.Interactivity.TriggerCollection tc = Interaction.GetTriggers(d);

            foreach (System.Windows.Interactivity.TriggerBase trigger in ih.Triggers)
                tc.Add(trigger);
        }
    }
}
