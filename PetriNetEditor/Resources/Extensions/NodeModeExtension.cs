using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines an attached property that allows a NodeMode to be set.
    /// </summary>
    public class NodeModeExtension : DependencyObject
    {
        /// <summary> Gets the associated node mode of the dependency object. </summary>
        public static NodeMode GetAssociatedNodeMode(DependencyObject obj)
        {
            return (NodeMode)obj.GetValue(AssociatedNodeModeProperty);
        }

        /// <summary> Sets the associated node mode of the dependency object. </summary>
        public static void SetAssociatedNodeMode(DependencyObject obj, NodeMode value)
        {
            obj.SetValue(AssociatedNodeModeProperty, value);
        }

        /// <summary> The AssociatedNodeMode AttachedProperty. </summary>
        public static readonly DependencyProperty AssociatedNodeModeProperty =
            DependencyProperty.RegisterAttached("AssociatedNodeMode",
            typeof(NodeMode),
            typeof(NodeModeExtension));
    }
}
