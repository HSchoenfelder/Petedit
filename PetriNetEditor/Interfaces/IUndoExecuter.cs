using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides methods to perform undo and redo operations on the petrinet.
    /// </summary>
    public interface IUndoExecuter
    {
        #region events
        /// <summary> Occurs when a modification is made to the model. </summary>
        event EventHandler Modified;

        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when a modification is made to ViewWidth of ViewHeight. </summary>
        event ViewSizeChangedEventHandler ViewSizeChanged;

        /// <summary> Occurs when a modification is made to SizeFactor. </summary>
        event SizeFactorChangedEventHandler SizeFactorChanged;
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the factor with which the BaseSize is to be multiplied for scaling.
        /// </summary>
        int SizeFactor { get; set; }

        /// <summary> 
        /// Gets or sets the width of the visual presentation of the petrinet. 
        /// </summary>
        double ViewWidth { get; set; }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        double ViewHeight { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Undoes a move operation.
        /// </summary>
        /// <param name="ids">A list with the ids of the visual elements that were moved.</param>
        /// <param name="offset">The move offset.</param>
        /// <param name="viewSize">The view size before the move.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction</param>
        void UndoMove(IList<String> ids, Point offset, Point viewSize, Point shift);

        /// <summary>
        /// Redoes a move operation.
        /// </summary>
        /// <param name="ids">A list with the ids of the visual elements that were moved.</param>
        /// <param name="offset">The move offset.</param>
        /// <param name="viewSize">The view size before the move.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        void RedoMove(IList<String> ids, Point offset, Point viewSize, Point shift);

        /// <summary>
        /// Undoes a name change operation.
        /// </summary>
        /// <param name="id">The id of the node that had its name changed.</param>
        /// <param name="name">The previous name of the node.</param>
        /// <param name="viewSize">The view size before the name change.</param>
        /// <param name="nfSize">The size of the name field before the name change.</param>
        /// <param name="nodePos">The position of the node before the name change.</param>
        void UndoNameChange(String id, String name, Point viewSize, Point nfSize, Point nodePos);

        /// <summary>
        /// Redoes a name change operation.
        /// </summary>
        /// <param name="id">The id of the node that had its name changed.</param>
        /// <param name="name">The previous name of the node.</param>
        /// <param name="viewSize">The view size before the name change.</param>
        /// <param name="nfSize">The size of the name field before the name change.</param>
        /// <param name="nodePos">The position of the node before the name change.</param>
        void RedoNameChange(String id, String name, Point viewSize, Point nfSize, Point nodePos);

        /// <summary>
        /// Undoes a select operation.
        /// </summary>
        /// <param name="selectIds">A list with the ids of the visual elements that whose selection
        /// state was changed.</param>
        void UndoSelect(List<String> selectIds);

        /// <summary>
        /// Redoes a select operation.
        /// </summary>
        /// <param name="selectIds">A list with the ids of the visual elements that whose selection
        /// state was changed.</param>
        void RedoSelect(List<String> selectIds);

        /// <summary>
        /// Undoes a delete operation.
        /// </summary>
        /// <param name="nodes">A list that contains node info for the deleted nodes.</param>
        /// <param name="arcs">A list that contains arc info for the deleted arcs.</param>
        void UndoDelete(List<NodeInfo> nodes, List<ArcInfo> arcs);

        /// <summary>
        /// Redoes a delete operation.
        /// </summary>
        /// <param name="ids">A list that a list with the ids of the elements that were deleted.</param>
        void RedoDelete(List<String> ids);

        /// <summary>
        /// Undoes a create operation.
        /// </summary>
        /// <param name="id">The id of the element that was created.</param>
        /// <param name="viewSize">The view size before the element was created.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        void UndoCreate(String id, Point viewSize, Point shift);

        /// <summary>
        /// Redoes a create operation.
        /// </summary>
        /// <param name="node">Node info about the node that is to be restored.</param>
        /// <param name="arc">Arc info about the arc that is to be restored.</param>
        /// <param name="viewSize">The view size before the element was removed.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        void RedoCreate(NodeInfo node, ArcInfo arc, Point viewSize, Point shift);

        /// <summary>
        /// Undoes a token change operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token count has been changed.</param>
        /// <param name="tokenCount">The previous token count of the node.</param>
        void UndoTokenChange(String id, int tokenCount);

        /// <summary>
        /// Redoes a token change operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token count has been changed.</param>
        /// <param name="tokenCount">The previous token count of the node.</param>
        void RedoTokenChange(String id, int tokenCount);

        /// <summary>
        /// Undoes a transition operation.
        /// </summary>
        /// <param name="id">The id of the node on which the transition has been performed.</param>
        void UndoTransition(String id);

        /// <summary>
        /// Redoes a transition operation.
        /// </summary>
        /// <param name="id">The id of the node on which the transition has been performed.</param>
        void RedoTransition(String id);

        /// <summary>
        /// Undoes a size change operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the change.</param>
        /// <param name="viewSize">The view size before the change.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        void UndoSizeChange(int sizeFactor, Point viewSize, Point shift);

        /// <summary>
        /// Redoes a size change operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the change.</param>
        /// <param name="viewSize">The view size before the change.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        void RedoSizeChange(int sizeFactor, Point viewSize, Point shift);
        #endregion
    }
}
