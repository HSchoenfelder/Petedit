using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides data related to the ViewSizeChanged event.
    /// </summary>
    public class ViewSizeChangedEventArgs : EventArgs
    {
        /// <summary> Gets the new view width. </summary>
        public double ViewWidth { get; protected set; }

        /// <summary> Gets the new view height. </summary>
        public double ViewHeight { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the ViewSizeChangedEventArgs class with the specified view 
        /// width and height.
        /// </summary>
        /// <param name="viewWidth">The new view width associated with the event.</param>
        /// <param name="viewHeight">The new view height associated with the event.</param>
        public ViewSizeChangedEventArgs(double viewWidth, double viewHeight)
        {
            ViewWidth = viewWidth;
            ViewHeight = viewHeight;
        }
    }
}
