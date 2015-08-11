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
    /// This class defines a StyleSelector for a custom context menu.
    /// </summary>
    public class CMItemContainerStyleSelector : StyleSelector
    {
        /// <summary> Store for the TitleStyle property. </summary>
        private Style _titleStyle;

        /// <summary> Gets of sets the title style. </summary>
        public Style TitleStyle
        {
            get { return _titleStyle; }
            set { _titleStyle = value; }
        }

        /// <summary>
        /// Selects the title style if the item is a TitleItem, otherwise stays with the default style.
        /// </summary>
        /// <param name="item">The item for which to select the style.</param>
        /// <param name="container">The item container.</param>
        /// <returns>The selected style.</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is TitleItem)
                return _titleStyle;

            return null;
        }
    }
}
