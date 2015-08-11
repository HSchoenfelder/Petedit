using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class implements a nullable Point.
    /// </summary>
    public class NPoint
    {
        /// <summary> The x-value of the NPoint. </summary>
        public double X;

        /// <summary> The y-value of the NPoint. </summary>
        public double Y;

        /// <summary>
        /// Initializes a new instance of the NPoint class with the provided values.
        /// </summary>
        /// <param name="x">The x-value of the new NPoint.</param>
        /// <param name="y">The y-value of the new NPoint.</param>
        public NPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Implicitly converts an NPoint to a Point.
        /// </summary>
        /// <param name="npoint">The NPoint to convert.</param>
        /// <returns>The resulting Point.</returns>
        public static implicit operator Point(NPoint npoint)
        {
            if (npoint == null)
                throw new ArgumentNullException("npoint", "Cannot convert to Point from null!");
            return new Point(npoint.X, npoint.Y);
        }

        /// <summary>
        /// Implicitly converts a Point to an NPoint.
        /// </summary>
        /// <param name="point">The Point to convert.</param>
        /// <returns>The resulting NPoint.</returns>
        public static implicit operator NPoint(Point point)
        {
            return new NPoint(point.X, point.Y);
        }

        /// <summary>
        /// Returns a string representation of the NPoint.
        /// </summary>
        /// <returns>A string representation of the NPoint.</returns>
        public override string ToString()
        {
            return X + "; " + Y;
        }
    }
}
