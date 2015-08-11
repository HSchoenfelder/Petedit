using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a DataTemplateSelector for presentation arcs.
    /// </summary>
    public class ArcItemDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Returns the oneWayArcTemplate if the item is a normal arc and the twoWayArcTemplate if the
        /// item is one of two arcs between the same two nodes.
        /// </summary>
        /// <param name="item">The item for which to select the data template.</param>
        /// <param name="container">The item container.</param>
        /// <returns>The selected data template.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            KeyValuePair<String, IVisualArc> pair = (KeyValuePair<String, IVisualArc>)item;
            IVisualArc vArc = pair.Value;

            if (element != null && vArc != null)
            {
                if (vArc.BrotherId != null)
                    return element.FindResource("twoWayArcTemplate") as DataTemplate;
                else
                    return element.FindResource("oneWayArcTemplate") as DataTemplate;
            }
            return null;
        }
    }
}
