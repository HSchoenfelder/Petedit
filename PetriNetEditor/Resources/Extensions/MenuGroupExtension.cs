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
    /// This class defines attached properties that allow for grouping of menu items.
    /// </summary>
    public class MenuGroupExtension : DependencyObject
    {
        /// <summary> The dictionary that manages the elements by groupnames. </summary>
        public static Dictionary<MenuItem, String> _elementToGroupNames = new Dictionary<MenuItem, String>();

        /// <summary> Gets a string that serves as the name of a group. </summary>
        public static String GetGroupName(MenuItem element)
        {
            return element.GetValue(GroupNameProperty).ToString();
        }

        /// <summary> Sets a string that serves as the name of a group. </summary>
        public static void SetGroupName(MenuItem element, String value)
        {
            element.SetValue(GroupNameProperty, value);
        }

        /// <summary> The GroupName AttachedProperty. </summary>
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached("GroupName",
                                        typeof(String),
                                        typeof(MenuGroupExtension),
                                        new PropertyMetadata(String.Empty, OnGroupNameChanged));

        /// <summary>
        /// Called when the GroupName property changes. Adds a menu item to the group associated with the 
        /// group name or changes its group association.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new and old value.</param>
        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Add an entry to the group name collection
            var menuItem = d as MenuItem;

            if (menuItem != null)
            {
                String newGroupName = e.NewValue.ToString();
                String oldGroupName = e.OldValue.ToString();
                if (String.IsNullOrEmpty(newGroupName))
                {
                    //Removing the toggle button from grouping
                    RemoveCheckboxFromGrouping(menuItem);
                }
                else
                {
                    //Switching to a new group
                    if (newGroupName != oldGroupName)
                    {
                        if (!String.IsNullOrEmpty(oldGroupName))
                        {
                            //Remove the old group mapping
                            RemoveCheckboxFromGrouping(menuItem);
                        }
                        _elementToGroupNames.Add(menuItem, e.NewValue.ToString());
                        menuItem.Checked += MenuItemChecked;
                        //menuItem.MouseLeftButtonDown += MenuItemClicked;
                        menuItem.AddHandler(MenuItem.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MenuItemClicked), true);
                    }
                }
            }
        }        

        /// <summary>
        /// Removes a menu item from the grouping.
        /// </summary>
        /// <param name="checkBox">The menu item to remove.</param>
        private static void RemoveCheckboxFromGrouping(MenuItem checkBox)
        {
            _elementToGroupNames.Remove(checkBox);
            checkBox.Checked -= MenuItemChecked;
            checkBox.MouseLeftButtonDown -= MenuItemClicked;
        }

        /// <summary>
        /// Handler for the MenuItemChecked event. Unchecks all other items in the group.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event data.</param>
        static void MenuItemChecked(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            foreach (var item in _elementToGroupNames)
            {
                if (item.Key != menuItem && item.Value == GetGroupName(menuItem))
                {
                    item.Key.ClearValue(MenuItem.IsCheckedProperty);
                }
            }
        }

        /// <summary>
        /// Handler for the MenuItemClicked event. Unchecks all other items in the group.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event data.</param>
        static void MenuItemClicked(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem.IsChecked)
                menuItem.ClearValue(MenuItem.IsCheckableProperty);
        }
    }  
}
