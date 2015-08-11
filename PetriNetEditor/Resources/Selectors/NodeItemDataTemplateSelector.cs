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
    /// This class defines a DataTemplateSelector for presentation nodes.
    /// </summary>
    public class NodeItemDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Returns the placeTemplate if the item is a place and the transTemplate if the item
        /// is a transition.
        /// </summary>
        /// <param name="item">The item for which to select the data template.</param>
        /// <param name="container">The item container.</param>
        /// <returns>The selected data template.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            KeyValuePair<String, IVisualNode> pair = (KeyValuePair<String, IVisualNode>)item;
            IVisualNode vNode = pair.Value;
            
            if (element != null && vNode != null)
	        {
                switch (vNode.NodeType)
                {
                    case NodeType.Place:
                        return element.FindResource("placeTemplate") as DataTemplate;
                    case NodeType.Transition:
                        return element.FindResource("transTemplate") as DataTemplate;
                    default:
                        break;
                }
	        }
            return null;
        }
    }
}
