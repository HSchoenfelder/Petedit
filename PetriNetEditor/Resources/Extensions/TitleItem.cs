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
    /// This class represents a menu item that may be used as a title item in a menu.
    /// </summary>
    public class TitleItem : MenuItem
    {
        /// <summary> Gets or sets the title of the menu item. </summary>
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary> The Title DependencyProperty. </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
            "Title",
            typeof(String),
            typeof(TitleItem));
    }
}
