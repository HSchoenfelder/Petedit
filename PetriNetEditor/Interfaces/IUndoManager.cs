using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    public interface IUndoManager
    {
        #region properties
        /// <summary> 
        /// Gets or sets the UndoExecuter which performs undo and redo operations.
        /// </summary>
        UndoExecuter UndoTarget { get; set; }

        /// <summary> 
        /// Gets or sets the amount of right shift during the current create operation. 
        /// </summary>
        double CreateRightShift { get; set; }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current create operation. 
        /// </summary>
        double CreateDownShift { get; set; }

        /// <summary> 
        /// Gets or sets the amount of right shift during the current size change operation. 
        /// </summary>
        double SizeChangeRightShift { get; set; }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current size change operation. 
        /// </summary>
        double SizeChangeDownShift { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the undo stack is currently empty.
        /// </summary>
        bool UndoStackEmpty { get; }

        /// <summary>
        /// Gets or sets a value which indicates whether a rectangle select operation is
        /// currently in progress.
        /// </summary>
        bool Selecting { get; set; }

        /// <summary>
        /// Gets or sets a value which indicates whether an arc drawing operation is
        /// currently in progress.
        /// </summary>
        bool Drawing { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Creates the complete parameters for a move undo operation.
        /// </summary>
        /// <param name="xOffset">The x-offset by which the elements were moved.</param>
        /// <param name="yOffset">The y-offset by which the elements were moved.</param>
        /// <param name="xShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="yShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="viewWidth">The view width before the move.</param>
        /// <param name="viewHeight">The view height before the move.</param>
        void CreateMoveUndoParams(double xOffset, double yOffset, double xShift, double yShift,
                                  double viewWidth, double viewHeight);

        /// <summary>
        /// Creates the complete parameters for a move redo operation.
        /// </summary>
        /// <param name="xOffset">The x-offset by which the elements were moved.</param>
        /// <param name="yOffset">The y-offset by which the elements were moved.</param>
        /// <param name="xShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="yShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="viewWidth">The view width before the move.</param>
        /// <param name="viewHeight">The view height before the move.</param>
        void CreateMoveRedoParams(double xOffset, double yOffset, double xShift, double yShift,
                                  double viewWidth, double viewHeight);

        /// <summary>
        /// Stores the id of an element that is part of a move operation for an undo operation.
        /// </summary>
        /// <param name="id">The id of the element that is part of a move operation.</param>
        void AddMoveUndoId(String id);

        /// <summary>
        /// Stores the id of an element that is part of a move operation for a redo operation.
        /// </summary>
        /// <param name="id">The id of the element that is part of a move operation.</param>
        void AddMoveRedoId(String id);

        /// <summary>
        /// Pushes the move undo operation to the undo stack.
        /// </summary>
        void PushMoveUndo();

        /// <summary>
        /// Pushes the move redo operation to the redo stack.
        /// </summary>
        void PushMoveRedo();

        /// <summary>
        /// Adds the complete parameters for a name change undo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the name change has been performed.</param>
        /// <param name="name">The name of the node before the change.</param>
        /// <param name="viewWidth">The view width before the change.</param>
        /// <param name="viewHeight">The view height before the change.</param>
        /// <param name="nfWidth">The width of the name field before the change.</param>
        /// <param name="nfHeight">The height of the name field before the change.</param>
        /// <param name="xPos">The x-coordinate of the node before the change.</param>
        /// <param name="yPos">The y-coordinate of the node before the change.</param>
        void AddNameChangeUndoParams(String id, String name, double viewWidth, double viewHeight,
                                     double nfWidth, double nfHeight, double xPos, double yPos);

        /// <summary>
        /// Adds the complete parameters for a name change redo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the name change has been performed.</param>
        /// <param name="name">The name of the node before the change.</param>
        /// <param name="viewWidth">The view width before the change.</param>
        /// <param name="viewHeight">The view height before the change.</param>
        /// <param name="nfWidth">The width of the name field before the change.</param>
        /// <param name="nfHeight">The height of the name field before the change.</param>
        /// <param name="xPos">The x-coordinate of the node before the change.</param>
        /// <param name="yPos">The y-coordinate of the node before the change.</param>
        void AddNameChangeRedoParams(String id, String name, double viewWidth, double viewHeight,
                                     double nfWidth, double nfHeight, double xPos, double yPos);

        /// <summary>
        /// Pushes the name change undo operation to the undo stack.
        /// </summary>
        void PushNameChangeUndo();

        /// <summary>
        /// Pushes the name change redo operation to the redo stack.
        /// </summary>
        void PushNameChangeRedo();

        /// <summary>
        /// Stores the provided list of ids for a select undo operation.
        /// </summary>
        /// <param name="ids">The list of ids to store for the select undo operation.</param>
        void AddSelectUndoIds(IList<String> ids);

        /// <summary>
        /// Stores the provided list of ids for a select redo operation.
        /// </summary>
        /// <param name="ids">The list of ids to store for the select redo operation.</param>
        void AddSelectRedoIds(IList<String> ids);

        /// <summary>
        /// Pushes the select undo operation to the undo stack.
        /// </summary>
        void PushSelectUndo();

        /// <summary>
        /// Pushes the select redo operation to the redo stack.
        /// </summary>
        void PushSelectRedo();

        /// <summary>
        /// Stores node info for a delete undo operation.
        /// </summary>
        /// <param name="id">The id of the deleted node.</param>
        /// <param name="name">The name of the deleted node.</param>
        /// <param name="x">The x-coordinate of the deleted node.</param>
        /// <param name="y">The y-coordinate of the deleted node.</param>
        /// <param name="tokenCount">The token count of the deleted node.</param>
        void AddDeleteUndoNodeInfo(String id, String name, int x, int y, int? tokenCount);

        /// <summary>
        /// Stores arc info for a delete undo operation.
        /// </summary>
        /// <param name="id">The id of the deleted arc.</param>
        /// <param name="sourceId">The id of the source of the deleted arc.</param>
        /// <param name="targetId">The id of the target of the deleted arc.</param>
        /// <param name="selected">The selected state of the deleted arc.</param>
        void AddDeleteUndoArcInfo(String id, String sourceId, String targetId, bool selected);

        /// <summary>
        /// Stores the id of an element for a delete redo operation.
        /// </summary>
        /// <param name="id">The id of the element that has been restored.</param>
        void AddDeleteRedoId(String id);

        /// <summary>
        /// Pushes the delete undo operation to the undo stack.
        /// </summary>
        void PushDeleteUndo();

        /// <summary>
        /// Pushes the delete redo operation to the redo stack.
        /// </summary>
        void PushDeleteRedo();

        /// <summary>
        /// Stores the id of an element for a create undo operation.
        /// </summary>
        /// <param name="id">The id of the element that has been created.</param>
        void AddCreateUndoId(String id);

        /// <summary>
        /// Stores the view size before a create operation for a create undo operation.
        /// </summary>
        /// <param name="viewWidth">The view width before the create operation.</param>
        /// <param name="viewHeight">The view height before the create operation.</param>
        void AddCreateUndoViewSize(double viewWidth, double viewHeight);

        /// <summary>
        /// Stores node info for a create redo operation.
        /// </summary>
        /// <param name="id">The id of the deleted node.</param>
        /// <param name="name">The name of the deleted node.</param>
        /// <param name="x">The x-coordinate of the deleted node.</param>
        /// <param name="y">The y-coordinate of the deleted node.</param>
        /// <param name="tokenCount">The token count of the deleted node.</param>
        void AddCreateRedoNodeInfo(String id, String name, int x, int y, int? tokenCount);

        /// <summary>
        /// Stores arc info for a create redo operation.
        /// </summary>
        /// <param name="id">The id of the deleted arc.</param>
        /// <param name="sourceId">The id of the source of the deleted arc.</param>
        /// <param name="targetId">The id of the target of the deleted arc.</param>
        /// <param name="selected">The selected state of the deleted arc.</param>
        void AddCreateRedoArcInfo(String id, String sourceId, String targetId, bool selected);

        /// <summary>
        /// Stores the view size before a create undo operation as well as the necessary amount of node shift for
        /// a create redo operation.
        /// </summary>
        /// <param name="viewWidth">The view width before the create undo operation.</param>
        /// <param name="viewHeight">The view height before the create undo operation.</param>
        /// <param name="rightShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="downShift">The amount by which elements had to be shifted in y-direction.</param>
        void AddCreateRedoParams(double viewWidth, double viewHeight, double rightShift, double downShift);

        /// <summary>
        /// Adds shift and pushes the create undo operation to the undo stack.
        /// </summary>
        void PushCreateUndo();

        /// <summary>
        /// Pushes the create redo operation the the redo stack.
        /// </summary>
        void PushCreateRedo();

        /// <summary>
        /// Adds the complete parameters for a token change undo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token change has been performed.</param>
        /// <param name="count">The token count of the node before the operation.</param>
        void AddTokenChangeUndoParams(String id, int count);

        /// <summary>
        /// Adds the complete parameters for a token change redo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token change has been performed.</param>
        /// <param name="count">The token count of the node before the operation.</param>
        void AddTokenChangeRedoParams(String id, int count);

        /// <summary>
        /// Pushes the token change undo operation to the undo stack.
        /// </summary>
        void PushTokenChangeUndo();

        /// <summary>
        /// Pushes the token change redo operation to the redo stack.
        /// </summary>
        void PushTokenChangeRedo();

        /// <summary>
        /// Stores the id of an element for a transition undo operation.
        /// </summary>
        /// <param name="id">The id of the node the transition has been performed on.</param>
        void AddTransitionUndoId(String id);

        /// <summary>
        /// Stores the id of an element for a transition redo operation.
        /// </summary>
        /// <param name="id">The id of the node the transition undo has been performed on.</param>
        void AddTransitionRedoId(String id);

        /// <summary>
        /// Pushes the transition undo operation to the undo stack.
        /// </summary>
        void PushTransitionUndo();

        /// <summary>
        /// Pushes the transition redo operation to the redo stack.
        /// </summary>
        void PushTransitionRedo();

        /// <summary>
        /// Adds the complete parameters for a size change undo operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the size change operation.</param>
        /// <param name="viewWidth">The view width before the size change operation.</param>
        /// <param name="viewHeight">The view height before the size change operation.</param>
        void AddSizeChangeUndoParams(int sizeFactor, double viewWidth, double viewHeight);

        /// <summary>
        /// Adds the complete parameters for a size change redo operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the size change undo operation.</param>
        /// <param name="viewWidth">The view width before the size change undo operation.</param>
        /// <param name="viewHeight">The view height before the size change undo operation.</param>
        /// <param name="rightShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="downShift">The amount by which elements had to be shifted in y-direction.</param>
        void AddSizeChangeRedoParams(int sizeFactor, double viewWidth, double viewHeight,
                                     double rightShift, double downShift);

        /// <summary>
        /// Adds shift and pushes the size change undo operation to the undo stack.
        /// </summary>
        void PushSizeChangeUndo();

        /// <summary>
        /// Pushes the size change redo operation to the redo stack.
        /// </summary>
        void PushSizeChangeRedo();

        /// <summary>
        /// Clears the redo stack.
        /// </summary>
        void ClearRedoStack();

        /// <summary>
        /// Clears the undo stack.
        /// </summary>
        void ClearUndoStack();
        #endregion
    }
}
