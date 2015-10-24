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

    /// <summary>
    /// Contains values for all available command types.
    /// </summary>
    public enum CommandTypes
    {
        /// <summary> The size change command. </summary>
        SizeChangeCommand,
        /// <summary> The delete nodes command. </summary>
        DeleteNodesCommand,
        /// <summary> The select all command. </summary>
        SelectAllCommand,
        /// <summary> The loaded command. </summary>
        LoadedCommand,
        /// <summary> The new file command. </summary>
        NewFileCommand,
        /// <summary> The load file command. </summary>
        LoadFileCommand,
        /// <summary> The save file command. </summary>
        SaveFileCommand,
        /// <summary> The node mode change command. </summary>
        NodeModeChangeCommand,
        /// <summary> The name change click command. </summary>
        NameChangeClickCommand,
        /// <summary> The name field clicked command. </summary>
        NameFieldClickedCommand,
        /// <summary> The name confirmed command. </summary>
        NameConfirmedCommand,
        /// <summary> The name changed command. </summary>
        NameChangedCommand,
        /// <summary> The node mouse left button down command. </summary>
        NodeMouseLeftButtonDownCommand,
        /// <summary> The arc mouse left button down command. </summary>
        ArcMouseLeftButtonDownCommand,
        /// <summary> The node mouse left button down command. </summary>
        NodeMouseMoveCommand,
        /// <summary> The node mouse left button up command. </summary>
        NodeMouseLeftButtonUpCommand,
        /// <summary> The mouse left button up command for the element manager. </summary>
        MouseLeftButtonUpCommandEM,
        /// <summary> The tokens changed command. </summary>
        TokensChangedCommand,
        /// <summary> The perform transition command. </summary>
        PerformTransitionCommand,
        /// <summary> The draw mode change command. </summary>
        DrawModeChangeCommand,
        /// <summary> The mouse left button down command. </summary>
        MouseLeftButtonDownCommand,
        /// <summary> The select rect mouse move command. </summary>
        SelectRectMouseMoveCommand,
        /// <summary> The mouse left button up command for the workspace manager. </summary>
        MouseLeftButtonUpCommandWS,
        /// <summary> The undo command. </summary>
        UndoCommand,
        /// <summary> The redo command. </summary>
        RedoCommand
    }
}
