using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines attached properties that can be used for scroll management.
    /// </summary>
    public static class ScrollExtension
    {
        /// <summary> 
        /// Gets a value that indicates whether the dependency object is outside of the visible range. 
        /// </summary>
        public static bool GetNodeBeyondEdge(DependencyObject obj)
        {
            return (bool)obj.GetValue(NodeBeyondEdgeProperty);
        }

        /// <summary> 
        /// Sets a value that indicates whether the dependency object is outside of the visible range. 
        /// </summary>
        public static void SetNodeBeyondEdge(DependencyObject obj, bool value)
        {
            obj.SetValue(NodeBeyondEdgeProperty, value);
        }

        /// <summary> The NodeBeyondEdge AttachedProperty. </summary>
        public static readonly DependencyProperty NodeBeyondEdgeProperty =
            DependencyProperty.RegisterAttached("NodeBeyondEdge",
            typeof(bool),
            typeof(ScrollExtension),
            new UIPropertyMetadata(false, OnNodeBeyondEdgePropertyChanged));


        /// <summary> 
        /// Gets a value that indicates whether the dependency object is outside of the visible range. 
        /// </summary>
        public static bool GetArcBeyondEdge(DependencyObject obj)
        {
            return (bool)obj.GetValue(ArcBeyondEdgeProperty);
        }

        /// <summary> 
        /// Sets a value that indicates whether the dependency object is outside of the visible range. 
        /// </summary>
        public static void SetArcBeyondEdge(DependencyObject obj, bool value)
        {
            obj.SetValue(ArcBeyondEdgeProperty, value);
        }

        /// <summary> The ArcBeyondEdge AttachedProperty. </summary>
        public static readonly DependencyProperty ArcBeyondEdgeProperty =
            DependencyProperty.RegisterAttached("ArcBeyondEdge",
            typeof(bool),
            typeof(ScrollExtension),
            new UIPropertyMetadata(false, OnArcBeyondEdgePropertyChanged));


        /// <summary> 
        /// Gets a value that indicates which corner of the dependency object requires scrolling. 
        /// </summary>
        public static Corners GetRectBeyondEdge(DependencyObject obj)
        {
            return (Corners)obj.GetValue(RectBeyondEdgeProperty);
        }

        /// <summary> 
        /// Sets a value that indicates which corner of the dependency object requires scrolling. 
        /// </summary>
        public static void SetRectBeyondEdge(DependencyObject obj, Corners value)
        {
            obj.SetValue(RectBeyondEdgeProperty, value);
        }

        /// <summary> The RectBeyondEdge AttachedProperty. </summary>
        public static readonly DependencyProperty RectBeyondEdgeProperty =
            DependencyProperty.RegisterAttached("RectBeyondEdge",
            typeof(Corners),
            typeof(ScrollExtension),
            new UIPropertyMetadata(OnRectBeyondEdgePropertyChanged));


        /// <summary>
        /// Called when the NodeBeyondEdge property changes. Scrolls the corresponding element into view.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new value.</param>
        private static void OnNodeBeyondEdgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fwe = (FrameworkElement)d;
            if ((bool)e.NewValue)
            {
                fwe.BringIntoView();
            }
        }

        /// <summary>
        /// Called when the ArcBeyondEdge property changes. Scrolls the corresponding element into view by its tip.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new value.</param>
        private static void OnArcBeyondEdgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fwe = (FrameworkElement)d;
            if ((bool)e.NewValue)
            {
                fwe.BringIntoView(new System.Windows.Rect(fwe.ActualWidth, fwe.ActualHeight / 2, 1, 1));
            }
        }

        /// <summary>
        /// Called when the RectBeyondEdge property changes. Scrolls the corresponding element into view by the corner
        /// that requires scrolling.
        /// </summary>
        /// <param name="d">The dependency object on which the change occurred.</param>
        /// <param name="e">The event data that contains the new value.</param>
        private static void OnRectBeyondEdgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fwe = (FrameworkElement)d;
            switch ((Corners)e.NewValue)
            {
                case Corners.BottomRight:
                    fwe.BringIntoView(new System.Windows.Rect(fwe.ActualWidth, fwe.ActualHeight, 1, 1));
                    break;
                case Corners.BottomLeft:
                    fwe.BringIntoView(new System.Windows.Rect(-1, fwe.ActualHeight, 1, 1));
                    break;
                case Corners.TopRight:
                    fwe.BringIntoView(new System.Windows.Rect(fwe.ActualWidth, -1, 1, 1));
                    break;
                case Corners.TopLeft:
                    fwe.BringIntoView(new System.Windows.Rect(-1, -1, 1, 1));
                    break;
                default:
                    break;
            }
        }
    }
}
