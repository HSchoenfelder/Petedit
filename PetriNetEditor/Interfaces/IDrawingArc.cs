using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Represents a visual arc that is used for drawing operations.
    /// </summary>
    public interface IDrawingArc
    {
        /// <summary> Sets the draw size of this DrawingArc. </summary>
        int DrawSize { set; }

        /// <summary> Sets the size of the arrowhead of this DrawingArc. </summary>
        int ArrowheadSize { set; }

        /// <summary>
        /// Sets a value which indicates whether this DrawingArc is currently visible in the
        /// graphical presentation.
        /// </summary>
        bool Visible { set; }

        /// <summary>
        /// Sets a value which indicates whether this DrawingArc is valid and ready for display.
        /// </summary>
        bool IsValid { set; }

        /// <summary>
        /// Sets a value which indicates whether this DrawingArc is currently beyond
        /// the bounds of the visual presentation and requires a scroll operation.
        /// </summary>
        bool IsBeyondEdge { set; }

        /// <summary> Sets the type of the source node of this DrawingArc. </summary>
        NodeType SourceType { set; }

        /// <summary>
        /// Sets the coordinates of the center of the source of this DrawingArc.
        /// </summary>
        NPoint SourceNodePos { set; }

        /// <summary>
        /// Sets the coordinates of the center of the target of this DrawingArc.
        /// </summary>
        NPoint TargetNodePos { set; }

        /// <summary>
        /// Sets the coordinates of the actual end point of this DrawingArc.
        /// </summary>
        NPoint ActualTarget { set; }

        /// <summary>
        /// Creates a new visual arc that is a deep copy of the current DrawingArc.
        /// </summary>
        /// <returns>A new object that is a copy of this DrawingArc.</returns>
        object Clone();
    }
}
