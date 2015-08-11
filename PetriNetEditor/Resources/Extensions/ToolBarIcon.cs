using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents an Image that is displayed with reduced opacity when its IsEnabled property
    /// changes to false, allowing it to be used as a toolbar icon that reflects the executability of the
    /// corresponding option.
    /// </summary>
    public class ToolBarIcon : Image
    {
        /// <summary>
        /// Initializes the ToolBarIcon class with the property metadata of the IsEnabled property overridden,
        /// so that OnIsEnabledChanged() is called every time the property value changes.
        /// </summary>
        static ToolBarIcon()
        {
            IsEnabledProperty.OverrideMetadata(typeof(ToolBarIcon),
                                               new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsEnabledChanged)));
        }

        /// <summary>
        /// Called when the IsEnabled property changes. Reduces the opacity of the ToolBarIcon to 0.3 if it has been disabled
        /// and sets it to 1 if it has been enabled.
        /// </summary>
        /// <param name="d">The ToolBarIcon in which the change occurred.</param>
        /// <param name="e">The event data that contains the new state.</param>
        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToolBarIcon toolBarIcon = d as ToolBarIcon;
            bool isEnabled = (bool)e.NewValue;
            if (toolBarIcon != null)
            {
                if (!isEnabled)
                {
                    BitmapImage image = new BitmapImage(new Uri(toolBarIcon.Source.ToString()));
                    toolBarIcon.Source = new FormatConvertedBitmap(image, PixelFormats.Gray32Float, null, 100);
                    toolBarIcon.OpacityMask = new ImageBrush(image);
                    toolBarIcon.Opacity = 0.3;
                }
                else
                {
                    toolBarIcon.Source = ((FormatConvertedBitmap)toolBarIcon.Source).Source;
                    toolBarIcon.OpacityMask = null;
                    toolBarIcon.Opacity = 1;
                }
            }
        }
    }
}
