using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PetriNetEditor
{
    /// <summary>
    /// This class defines a Canvas whose left half is hittest invisible.
    /// </summary>
    public class PartialCanvas : Canvas
    {
        /// <summary>
        /// Ensures that a HitTestResult is only returned for the right half of the Canvas.
        /// </summary>
        /// <param name="hitTestParameters">The HitTestParameters.</param>
        /// <returns>The PointHitTestResult, if the hit was over the right half of the Canvas,
        /// null otherwise.</returns>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            Point hitPoint = hitTestParameters.HitPoint;
            if (hitPoint.X > ActualWidth / 2)
                return new PointHitTestResult(this, hitPoint);

            return null;
        }
    }
}
