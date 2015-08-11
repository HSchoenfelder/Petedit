using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Contains values for the three possible draw modes.
    /// </summary>
    public enum DrawMode
    {
        /// <summary> Draw places. </summary>
        Drawplace,
        /// <summary> Draw transitions. </summary>
        Drawtrans,
        /// <summary> Draw a selection rectangle. </summary>
        Select
    }

    /// <summary>
    /// Contains values for the three possible node modes.
    /// </summary>
    public enum NodeMode
    {
        /// <summary> Perform move and select operations. </summary>
        Movenode,
        /// <summary> Perform arc draw operations. </summary>
        Drawarc,
        /// <summary> Perform token and transition operations. </summary>
        Manipulate
    }

    /// <summary>
    /// Contains the possible types a visual node can take in the petrinet.
    /// </summary>
    public enum NodeType
    {
        /// <summary> The node is a place. </summary>
        Place,
        /// <summary> The node is a transition. </summary>
        Transition
    }

    /// <summary>
    /// Contains values for the four segments of a rectangular structure.
    /// </summary>
    public enum Corners
    {
        /// <summary> The bottom right segment of the rectangle. </summary>
        BottomRight,
        /// <summary> The bottom left segment of the rectangle. </summary>
        BottomLeft,
        /// <summary> The top right segment of the rectangle. </summary>
        TopRight,
        /// <summary> The top left segment of the rectangle. </summary>
        TopLeft
    }

    /// <summary>
    /// Contains values for all undoable / redoable operations.
    /// </summary>
    public enum UndoRedoOps
    {
        /// <summary> A move operation. </summary>
        Move,
        /// <summary> A name change operation. </summary>
        ChangeName,
        /// <summary> A select operation. </summary>
        Select,
        /// <summary> A delete operation. </summary>
        Delete,
        /// <summary> A create operation. </summary>
        Create,
        /// <summary> A token set operation on a place. </summary>
        SetTokens,
        /// <summary> An activate operation on a transition. </summary>
        Activate,
        /// <summary> A size change operation. </summary>
        ChangeSize,
        /// <summary> No operation. </summary>
        None
    }
}
