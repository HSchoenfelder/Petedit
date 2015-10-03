using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides logic for the manipulation of visual elements in the petrinet.
    /// </summary>
    public interface IElementManager
    {
        #region events
        /// <summary> Occurs when a modification is made to the model. </summary>
        event EventHandler Modified;

        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when a modification is made to ViewWidth of ViewHeight. </summary>
        event ViewSizeChangedEventHandler ViewSizeChanged;

        /// <summary> Occurs when the state change block is to be set or released. </summary>
        event BlockStateChangedEventHandler BlockStateChanged;

        /// <summary> Occurs when the drawing state changed. </summary>
        event DrawingStateChangedEventHandler DrawingStateChanged;
        #endregion

        #region properties
        /// <summary> Gets or sets the current global draw size. </summary>
        int DrawSize { get; set; }

        /// <summary> Gets or sets the current global arrowhead size. </summary>
        int ArrowheadSize { get; set; }

        /// <summary> 
        /// Gets or sets the width of the visual presentation of the petrinet. 
        /// </summary>
        double ViewWidth { get; set; }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        double ViewHeight { get; set; }

        /// <summary>
        /// Gets or sets the id of the VisualNode that initiated the current drag operation.
        /// </summary>
        String DragSourceId { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Removes the brother relationship with the brother of the Arc with the specified id 
        /// if there is one.
        /// </summary>
        void RemoveBrother(String arcId);

        /// <summary>
        /// Adjusts the size of the visual presentation of the petrinet to fit the positioning of all
        /// nodes. To be called after the visual presentation of a node has been repositioned.
        /// </summary>
        /// <param name="id">The id of the node that requested the adjustment.</param>
        /// <param name="callType">A value that indicates what kind of operation is responsible for this 
        /// call.</param>
        void AdjustViewSize(String id, UndoRedoOps callType);

        /// <summary>
        /// Repositions a visual arc to match a visual node at the specified coordinates.
        /// </summary>
        /// <param name="arcId">The id of the visual arc to be repositioned.</param>
        /// <param name="nodeId">The id of the visual node that requests the operation.</param>
        /// <param name="xPos">The new x-coordinate of the visual node.</param>
        /// <param name="yPos">The new y-coordinate of the visual node.</param>
        void RepositionArc(String arcId, String nodeId, double xPos, double yPos);

        /// <summary>
        /// Creates a brother relationship with the inverse of the arc with the specified id, 
        /// if there should be one. 
        /// </summary>
        void TryCreateBrother(String arcId);

        /// <summary>
        /// Handles the TokensChangedEvent of the model. Sets the modfied token count to the
        /// respective visual node.
        /// </summary>
        /// <param name="e">The TokensChangedEventArgs containing the event data.</param>
        void HandleModelTokensChanged(TokensChangedEventArgs e);

        /// <summary>
        /// Handles the TransitionStateChangedEvent of the model. Sets the enabled state of the
        /// respective visual node.
        /// </summary>
        /// <param name="e">The TransitionStateChangedEventArgs containing the event data.</param>
        void HandleModelTransitionStateChanged(TransitionStateChangedEventArgs e);
        #endregion
    }
}
