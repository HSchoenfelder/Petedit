using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides methods that perform various geometrical calculations.
    /// </summary>
    public static class Calculations
    {
        /// <summary>
        /// Gets the distance between two points p1 and p2 in a plane.
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>The distance between the the two points.</returns>
        public static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) +
                   Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// Gets the clockwise angle in degrees by which a line between the points p1 and p2 has been
        /// rotated out of the horizontal position. The method assumes that point p1 is the leftmost
        /// point.
        /// </summary>
        /// <param name="p1">The leftmost point for the angle calculation.</param>
        /// <param name="p2">The rightmost point for the angle calculation.</param>
        /// <returns>The clockwise angle in degrees by which a line between the points has been
        /// rotated out of the horizontal position.</returns>
        public static double GetAngle(Point p1, Point p2)
        {
            double radianAngle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            return radianAngle * (180.0 / Math.PI);
        }

        /// <summary>
        /// Given two points that are separated by a vertical line whose coordinates on the x-axis are denoted
        /// by the value edge, calculates the difference between the y-coordinate of the intersection of the 
        /// direct line between the two points with the vertical line and the y-coordinate of the point denoted 
        /// by target.
        /// </summary>
        /// <param name="startPos">The first point.</param>
        /// <param name="target">The second point.</param>
        /// <param name="edge">The x-coordinate of the vertical line.</param>
        /// <returns></returns>
        public static double GetClippingCorrectionY(Point startPos, Point target, double edge)
        {
            Point relativeTarget = new Point(Math.Abs(startPos.X - target.X), startPos.Y - target.Y);
            return relativeTarget.Y - (Math.Abs((edge - startPos.X)) / relativeTarget.X * relativeTarget.Y);
        }

        /// <summary>
        /// Given two points that are separated by a horizontal line whose coordinates on the y-axis are denoted
        /// by the value edge, calculates the difference between the x-coordinate of the intersection of the 
        /// direct line between the two points with the horizontal line and the x-coordinate of the point denoted 
        /// by target.
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="target"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static double GetClippingCorrectionX(Point startPos, Point target, double edge)
        {
            Point relativeTarget = new Point(startPos.X - target.X, Math.Abs(startPos.Y - target.Y));
            return relativeTarget.X - (Math.Abs((edge - startPos.Y)) / relativeTarget.Y * relativeTarget.X);
        }
    }
}
