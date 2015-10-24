using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class manages the undo and the redo queue.
    /// </summary>
    public class UndoManager : IUndoManager, IUndoManagerEx
    {
        #region fields

        /// <summary> Store for the UndoExecuter property. </summary>
        private IUndoExecuter _undoTarget;

        /// <summary> Store for the Undos property. </summary>
        private Stack<object> _undos = new Stack<object>();

        /// <summary> Store for the Redos property. </summary>
        private Stack<object> _redos = new Stack<object>();

        /// <summary> Store for the MoveUndoParams property. </summary>
        private object[] _moveUndoParams;

        /// <summary> Store for the MoveRedoParams property. </summary>
        private object[] _moveRedoParams;

        /// <summary> Store for the NameChangeUndoParams property. </summary>
        private object[] _nameChangeUndoParams;

        /// <summary> Store for the NameChangeRedoParams property. </summary>
        private object[] _nameChangeRedoParams;

        /// <summary> Store for the SelectUndoParams property. </summary>
        private object[] _selectUndoParams;

        /// <summary> Store for the SelectRedoParams property. </summary>
        private object[] _selectRedoParams;

        /// <summary> Store for the DeleteUndoParams property. </summary>
        private object[] _deleteUndoParams;

        /// <summary> Store for the DeleteRedoParams property. </summary>
        private object[] _deleteRedoParams;

        /// <summary> Store for the CreateUndoParams property. </summary>
        private object[] _createUndoParams;

        /// <summary> Store for the CreateRedoParams property. </summary>
        private object[] _createRedoParams;

        /// <summary> Store for the TokenChangeUndoParams property. </summary>
        private object[] _tokenChangeUndoParams;

        /// <summary> Store for the TokenChangeRedoParams property. </summary>
        private object[] _tokenChangeRedoParams;

        /// <summary> Store for the TransitionUndoParams property. </summary>
        private object[] _transitionUndoParams;

        /// <summary> Store for the TransitionRedoParams property. </summary>
        private object[] _transitionRedoParams;

        /// <summary> Store for the SizeChangeUndoParams property. </summary>
        private object[] _sizeChangeUndoParams;

        /// <summary> Store for the SizeChangeRedoParams property. </summary>
        private object[] _sizeChangeRedoParams;

        /// <summary> Store for the MoveRightShift property. </summary>
        private double _moveRightShift = 0;

        /// <summary> Store for the MoveDownShift property. </summary>
        private double _moveDownShift = 0;

        /// <summary> Store for the MoveUndoStart property. </summary>
        private Point _moveUndoStart;

        /// <summary> Store for the MoveRedoStart property. </summary>
        private Point _moveRedoStart;

        /// <summary> Store for the MoveUndoViewSize property. </summary>
        private Point _moveUndoViewSize;

        /// <summary> Store for the CreateRightShift property. </summary>
        private double _createRightShift = 0;

        /// <summary> Store for the CreateDownShift property. </summary>
        private double _createDownShift = 0;

        /// <summary> Store for the SizeChangeRightShift property. </summary>
        private double _sizeChangeRightShift = 0;

        /// <summary> Store for the SizeChangeDownShift property. </summary>
        private double _sizeChangeDownShift = 0;

        /// <summary> Store for the Selecting property. </summary>
        private bool _selecting;

        /// <summary> Store for the Drawing property. </summary>
        private bool _drawing;

        /// <summary> Store for the UndoCommand property. </summary>
        private readonly IDelegateCommand _undoCommand;

        /// <summary> Store for the RedoCommand property. </summary>
        private readonly IDelegateCommand _redoCommand;

        #endregion

        #region properties

        #region private
        /// <summary> Gets the stack that manages undo operations. </summary>
        private Stack<object> Undos
        {
            get { return _undos; }
        }

        /// <summary> Gets the stack that manages redo operations. </summary>
        private Stack<object> Redos
        {
            get { return _redos; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a move operation. 
        /// </summary>
        private object[] MoveUndoParams
        {
            get { return _moveUndoParams; }
            set { _moveUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a move operation. 
        /// </summary>
        private object[] MoveRedoParams
        {
            get { return _moveRedoParams; }
            set { _moveRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a name change operation. 
        /// </summary>
        private object[] NameChangeUndoParams
        {
            get { return _nameChangeUndoParams; }
            set { _nameChangeUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a name change operation. 
        /// </summary>
        private object[] NameChangeRedoParams
        {
            get { return _nameChangeRedoParams; }
            set { _nameChangeRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a select operation. 
        /// </summary>
        private object[] SelectUndoParams
        {
            get { return _selectUndoParams; }
            set { _selectUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a select operation. 
        /// </summary>
        private object[] SelectRedoParams
        {
            get { return _selectRedoParams; }
            set { _selectRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a delete operation. 
        /// </summary>
        private object[] DeleteUndoParams
        {
            get { return _deleteUndoParams; }
            set { _deleteUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a delete operation. 
        /// </summary>
        private object[] DeleteRedoParams
        {
            get { return _deleteRedoParams; }
            set { _deleteRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a create operation. 
        /// </summary>
        private object[] CreateUndoParams
        {
            get { return _createUndoParams; }
            set { _createUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a create operation. 
        /// </summary>
        private object[] CreateRedoParams
        {
            get { return _createRedoParams; }
            set { _createRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a token change operation. 
        /// </summary>
        private object[] TokenChangeUndoParams
        {
            get { return _tokenChangeUndoParams; }
            set { _tokenChangeUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a token change operation. 
        /// </summary>
        private object[] TokenChangeRedoParams
        {
            get { return _tokenChangeRedoParams; }
            set { _tokenChangeRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a transition operation. 
        /// </summary>
        private object[] TransitionUndoParams
        {
            get { return _transitionUndoParams; }
            set { _transitionUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a transition operation. 
        /// </summary>
        private object[] TransitionRedoParams
        {
            get { return _transitionRedoParams; }
            set { _transitionRedoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to undo a size change operation. 
        /// </summary>
        private object[] SizeChangeUndoParams
        {
            get { return _sizeChangeUndoParams; }
            set { _sizeChangeUndoParams = value; }
        }

        /// <summary> 
        /// Gets the list that manages the parameters needed to redo a size change operation. 
        /// </summary>
        private object[] SizeChangeRedoParams
        {
            get { return _sizeChangeRedoParams; }
            set { _sizeChangeRedoParams = value; }
        }
        #endregion

        #region public
        /// <summary> 
        /// Gets or sets the UndoExecuter which performs undo and redo operations.
        /// </summary>
        public IUndoExecuter UndoTarget
        {
            get { return _undoTarget; }
            set { _undoTarget = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the undo stack is currently empty.
        /// </summary>
        public bool UndoStackEmpty
        {
            get { return _undos.Count == 0; }
        }

        /// <summary> 
        /// Gets or sets the amount of right shift during the current move operation. 
        /// </summary>
        public double MoveRightShift
        {
            get { return _moveRightShift; }
            set { _moveRightShift = value; }
        }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current move operation. 
        /// </summary>
        public double MoveDownShift
        {
            get { return _moveDownShift; }
            set { _moveDownShift = value; }
        }

        /// <summary> 
        /// Gets or sets the starting point of the move operation to undo. 
        /// </summary>
        public Point MoveUndoStart
        {
            get { return _moveUndoStart; }
            set { _moveUndoStart = value; }
        }

        /// <summary> 
        /// Gets or sets the starting point of the move operation to redo. 
        /// </summary>
        public Point MoveRedoStart
        {
            get { return _moveRedoStart; }
            set { _moveRedoStart = value; }
        }

        /// <summary> 
        /// Gets or sets the size of the view at the start of the move operation to undo. 
        /// </summary>
        public Point MoveUndoViewSize
        {
            get { return _moveUndoViewSize; }
            set { _moveUndoViewSize = value; }
        }

        /// <summary> 
        /// Gets or sets the amount of right shift during the current create operation. 
        /// </summary>
        public double CreateRightShift
        {
            get { return _createRightShift; }
            set { _createRightShift = value; }
        }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current create operation. 
        /// </summary>
        public double CreateDownShift
        {
            get { return _createDownShift; }
            set { _createDownShift = value; }
        }

        /// <summary> 
        /// Gets or sets the amount of right shift during the current size change operation. 
        /// </summary>
        public double SizeChangeRightShift
        {
            get { return _sizeChangeRightShift; }
            set { _sizeChangeRightShift = value; }
        }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current size change operation. 
        /// </summary>
        public double SizeChangeDownShift
        {
            get { return _sizeChangeDownShift; }
            set { _sizeChangeDownShift = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether a rectangle select operation is
        /// currently in progress.
        /// </summary>
        public bool Selecting
        {
            get { return _selecting; }
            set { _selecting = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether an arc drawing operation is
        /// currently in progress.
        /// </summary>
        public bool Drawing
        {
            get { return _drawing; }
            set { _drawing = value; }
        }

        /// <summary> Gets the command that performs an undo operation. </summary>
        public IDelegateCommand UndoCommand
        {
            get { return _undoCommand; }
        }

        /// <summary> Gets the command that performs an redo operation. </summary>
        public IDelegateCommand RedoCommand
        {
            get { return _redoCommand; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new UndoManager.
        /// </summary>
        public UndoManager()
        {
            // for storage of id list, move offset, view size and right/down shift
            _moveUndoParams = new object[4];
            _moveRedoParams = new object[4];

            // for storage of id, name, view size, name field size and position
            _nameChangeUndoParams = new object[5];
            _nameChangeRedoParams = new object[5];

            // for storage of id list
            _selectUndoParams = new object[1];
            _selectRedoParams = new object[1];

            // for storage of node and arc info lists
            _deleteUndoParams = new object[2];
            // for storage of id list of nodes restored in the last undo operation
            _deleteRedoParams = new object[1];

            // for storage of most recently created element id, view size and right/down shift
            _createUndoParams = new object[3];
            // for storage of node and arc info for most recently deleted element, view size 
            // and right/down shift
            _createRedoParams = new object[5];

            // for storage of id and token count
            _tokenChangeUndoParams = new object[2];
            _tokenChangeRedoParams = new object[2];

            // for storage of id
            _transitionUndoParams = new object[1];
            _transitionRedoParams = new object[1];

            // for storage of size factor, view size and right/down shift
            _sizeChangeUndoParams = new object[3];
            _sizeChangeRedoParams = new object[3];

            // connect command handler
            CommandFactory commandFactory = new CommandFactory();
            _undoCommand = commandFactory.Create<String>(CommandTypes.UndoCommand, HandleUndo, CanUndo);
            _redoCommand = commandFactory.Create<String>(CommandTypes.RedoCommand, HandleRedo, CanRedo);
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Handles the UndoCommand. Delegates the undo operation to the UndoExecuter together with 
        /// the corresponding parameters.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void HandleUndo(String parameter)
        {
            object[] stackOp = (object[])Undos.Pop();
            UndoRedoOps operation = (UndoRedoOps)stackOp[0];
            object[] parameters = (object[])stackOp[1];
            switch (operation)
            {
                case UndoRedoOps.Move:
                    List<String> moveIds = (List<String>)parameters[0];
                    Point offset = (Point)parameters[1];
                    Point viewSize = (Point)parameters[2];
                    Point rightDownShift = (Point)parameters[3];
                    UndoTarget.UndoMove(moveIds, offset, viewSize, rightDownShift);
                    break;
                case UndoRedoOps.ChangeName:
                    String id = (String)parameters[0];
                    String name = (String)parameters[1];
                    viewSize = (Point)parameters[2];
                    Point nfSize = (Point)parameters[3];
                    Point nodePos = (Point)parameters[4];
                    UndoTarget.UndoNameChange(id, name, viewSize, nfSize, nodePos);
                    break;
                case UndoRedoOps.Select:
                    List<String> selectIds = (List<String>)parameters[0];
                    UndoTarget.UndoSelect(selectIds);
                    break;
                case UndoRedoOps.Delete:
                    List<NodeInfo> nodes = (List<NodeInfo>)parameters[0];
                    List<ArcInfo> arcs = (List<ArcInfo>)parameters[1];
                    UndoTarget.UndoDelete(nodes, arcs);
                    break;
                case UndoRedoOps.Create:
                    String createId = (String)parameters[0];
                    viewSize = (Point)parameters[1];
                    rightDownShift = (Point)parameters[2];
                    UndoTarget.UndoCreate(createId, viewSize, rightDownShift);
                    break;
                case UndoRedoOps.SetTokens:
                    String changeId = (String)parameters[0];
                    int tokenCount = (int)parameters[1];
                    UndoTarget.UndoTokenChange(changeId, tokenCount);
                    break;
                case UndoRedoOps.Activate:
                    String transId = (String)parameters[0];
                    UndoTarget.UndoTransition(transId);
                    break;
                case UndoRedoOps.ChangeSize:
                    int sizeFactor = (int)parameters[0];
                    viewSize = (Point)parameters[1];
                    rightDownShift = (Point)parameters[2];
                    UndoTarget.UndoSizeChange(sizeFactor, viewSize, rightDownShift);
                    break;
                default:
                    break;
            }
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Handles the RedoCommand. Delegates the redo operation to the UndoExecuter together with 
        /// the corresponding parameters.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void HandleRedo(String parameter)
        {
            object[] stackOp = (object[])Redos.Pop();
            UndoRedoOps operation = (UndoRedoOps)stackOp[0];
            object[] parameters = (object[])stackOp[1];
            switch (operation)
            {
                case UndoRedoOps.Move:
                    List<String> moveIds = (List<String>)parameters[0];
                    Point offset = (Point)parameters[1];
                    Point viewSize = (Point)parameters[2];
                    Point rightDownShift = (Point)parameters[3];
                    UndoTarget.RedoMove(moveIds, offset, viewSize, rightDownShift);
                    break;
                case UndoRedoOps.ChangeName:
                    String id = (String)parameters[0];
                    String name = (String)parameters[1];
                    viewSize = (Point)parameters[2];
                    Point nfSize = (Point)parameters[3];
                    Point nodePos = (Point)parameters[4];
                    UndoTarget.RedoNameChange(id, name, viewSize, nfSize, nodePos);
                    break;
                case UndoRedoOps.Select:
                    List<String> selectIds = (List<String>)parameters[0];
                    UndoTarget.RedoSelect(selectIds);
                    break;
                case UndoRedoOps.Delete:
                    List<String> deleteIds = (List<String>)parameters[0];
                    UndoTarget.RedoDelete(deleteIds);
                    break;
                case UndoRedoOps.Create:
                    NodeInfo nodeInfo = (NodeInfo)parameters[0];
                    ArcInfo arcInfo = (ArcInfo)parameters[1];
                    viewSize = (Point)parameters[2];
                    rightDownShift = (Point)parameters[3];
                    UndoTarget.RedoCreate(nodeInfo, arcInfo, viewSize, rightDownShift);
                    break;
                case UndoRedoOps.SetTokens:
                    String changeId = (String)parameters[0];
                    int tokenCount = (int)parameters[1];
                    UndoTarget.RedoTokenChange(changeId, tokenCount);
                    break;
                case UndoRedoOps.Activate:
                    String transId = (String)parameters[0];
                    UndoTarget.RedoTransition(transId);
                    break;
                case UndoRedoOps.ChangeSize:
                    int sizeFactor = (int)parameters[0];
                    viewSize = (Point)parameters[1];
                    rightDownShift = (Point)parameters[2];
                    UndoTarget.RedoSizeChange(sizeFactor, viewSize, rightDownShift);
                    break;
                default:
                    break;
            }
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines whether the UndoCommand is enabled or disabled.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if the command is enabled; otherwise false.</returns>
        private bool CanUndo(String parameter)
        {
            if (!Drawing && !Selecting)
                return Undos.Count > 0;
            else
                return false;
        }

        /// <summary>
        /// Determines whether the RedoCommand is enabled or disabled.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if the command is enabled; otherwise false.</returns>
        private bool CanRedo(String parameter)
        {
            if (!Drawing && !Selecting)
                return Redos.Count > 0;
            else
                return false;
        }
        #endregion

        #region public
        /// <summary>
        /// Stores the id of an element that is part of a move operation for an undo operation.
        /// </summary>
        /// <param name="id">The id of the element that is part of a move operation.</param>
        public void AddMoveUndoId(String id)
        {
            if (MoveUndoParams[0] == null)
                MoveUndoParams[0] = new List<String>();
            ((List<String>)MoveUndoParams[0]).Add(id);
        }

        /// <summary>
        /// Stores the id of an element that is part of a move operation for a redo operation.
        /// </summary>
        /// <param name="id">The id of the element that is part of a move operation.</param>
        public void AddMoveRedoId(String id)
        {
            if (MoveRedoParams[0] == null)
                MoveRedoParams[0] = new List<String>();
            ((List<String>)MoveRedoParams[0]).Add(id);
        }
        
        /// <summary>
        /// Assembles the complete parameters for a move undo operation.
        /// </summary>
        /// <param name="xNewPos">The x-coordinate of the position after the move.</param>
        /// <param name="yNewPos">The y-coordinate of the position after the move.</param>
        public void AssembleMoveUndoParams(double xNewPos, double yNewPos)
        {
            MoveUndoParams[1] = new Point(xNewPos - MoveUndoStart.X, yNewPos - MoveUndoStart.Y);
            MoveUndoParams[2] = MoveUndoViewSize;
            MoveUndoParams[3] = new Point(MoveRightShift, MoveDownShift);
        }

        /// <summary>
        /// Creates the complete parameters for a move undo operation.
        /// </summary>
        /// <param name="xOffset">The x-offset by which the elements were moved.</param>
        /// <param name="yOffset">The y-offset by which the elements were moved.</param>
        /// <param name="xShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="yShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="viewWidth">The view width before the move.</param>
        /// <param name="viewHeight">The view height before the move.</param>
        public void CreateMoveUndoParams(double xOffset, double yOffset, double xShift, double yShift,
                                         double viewWidth, double viewHeight)
        {
            MoveUndoParams[1] = new Point(xOffset, yOffset);
            MoveUndoParams[2] = new Point(viewWidth, viewHeight);
            MoveUndoParams[3] = new Point(xShift, yShift);
        }

        /// <summary>
        /// Creates the complete parameters for a move redo operation.
        /// </summary>
        /// <param name="xOffset">The x-offset by which the elements were moved.</param>
        /// <param name="yOffset">The y-offset by which the elements were moved.</param>
        /// <param name="xShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="yShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="viewWidth">The view width before the move.</param>
        /// <param name="viewHeight">The view height before the move.</param>
        public void CreateMoveRedoParams(double xOffset, double yOffset, double xShift, double yShift,
                                         double viewWidth, double viewHeight)
        {
            MoveRedoParams[1] = new Point(xOffset, yOffset);
            MoveRedoParams[2] = new Point(viewWidth, viewHeight);
            MoveRedoParams[3] = new Point(xShift, yShift);
        }

        /// <summary>
        /// Pushes the move undo operation to the undo stack.
        /// </summary>
        public void PushMoveUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.Move, MoveUndoParams });
            MoveUndoParams = new object[4];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the move redo operation to the redo stack.
        /// </summary>
        public void PushMoveRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.Move, MoveRedoParams });
            MoveRedoParams = new object[4];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Resets the move undo parameters for the next use.
        /// </summary>
        public void ClearMoveUndoParams()
        {
            MoveUndoParams = new object[4];
            MoveRightShift = 0;
            MoveDownShift = 0;
        }

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
        public void AddNameChangeUndoParams(String id, String name, double viewWidth, double viewHeight, 
                                            double nfWidth, double nfHeight, double xPos, double yPos)
        {
            NameChangeUndoParams[0] = id;
            NameChangeUndoParams[1] = name;
            NameChangeUndoParams[2] = new Point(viewWidth, viewHeight);
            NameChangeUndoParams[3] = new Point(nfWidth, nfHeight);
            NameChangeUndoParams[4] = new Point(xPos, yPos);
        }

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
        public void AddNameChangeRedoParams(String id, String name, double viewWidth, double viewHeight,
                                            double nfWidth, double nfHeight, double xPos, double yPos)
        {
            NameChangeRedoParams[0] = id;
            NameChangeRedoParams[1] = name;
            NameChangeRedoParams[2] = new Point(viewWidth, viewHeight);
            NameChangeRedoParams[3] = new Point(nfWidth, nfHeight);
            NameChangeRedoParams[4] = new Point(xPos, yPos);
        }

        /// <summary>
        /// Pushes the name change undo operation to the undo stack.
        /// </summary>
        public void PushNameChangeUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.ChangeName, NameChangeUndoParams });
            NameChangeUndoParams = new object[5];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the name change redo operation to the redo stack.
        /// </summary>
        public void PushNameChangeRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.ChangeName, NameChangeRedoParams });
            NameChangeRedoParams = new object[5];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Stores the provided list of ids for a select undo operation.
        /// </summary>
        /// <param name="ids">The list of ids to store for the select undo operation.</param>
        public void AddSelectUndoIds(IList<String> ids)
        {
            if (SelectUndoParams[0] == null)
                SelectUndoParams[0] = new List<String>();
            ((List<String>)SelectUndoParams[0]).AddRange(ids);
        }

        /// <summary>
        /// Stores the provided list of ids for a select redo operation.
        /// </summary>
        /// <param name="ids">The list of ids to store for the select redo operation.</param>
        public void AddSelectRedoIds(IList<String> ids)
        {
            if (SelectRedoParams[0] == null)
                SelectRedoParams[0] = new List<String>();
            ((List<String>)SelectRedoParams[0]).AddRange(ids);
        }

        /// <summary>
        /// Pushes the select undo operation to the undo stack.
        /// </summary>
        public void PushSelectUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.Select, SelectUndoParams });
            SelectUndoParams = new object[1];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the select redo operation to the redo stack.
        /// </summary>
        public void PushSelectRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.Select, SelectRedoParams });
            SelectRedoParams = new object[1];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Stores node info for a delete undo operation.
        /// </summary>
        /// <param name="id">The id of the deleted node.</param>
        /// <param name="name">The name of the deleted node.</param>
        /// <param name="x">The x-coordinate of the deleted node.</param>
        /// <param name="y">The y-coordinate of the deleted node.</param>
        /// <param name="tokenCount">The token count of the deleted node.</param>
        public void AddDeleteUndoNodeInfo(String id, String name, int x, int y, int? tokenCount)
        {
            if (DeleteUndoParams[0] == null)
                DeleteUndoParams[0] = new List<NodeInfo>();
            ((List<NodeInfo>)DeleteUndoParams[0]).Add(new NodeInfo(id, name, x, y, tokenCount));
        }
        
        /// <summary>
        /// Stores arc info for a delete undo operation.
        /// </summary>
        /// <param name="id">The id of the deleted arc.</param>
        /// <param name="sourceId">The id of the source of the deleted arc.</param>
        /// <param name="targetId">The id of the target of the deleted arc.</param>
        /// <param name="selected">The selected state of the deleted arc.</param>
        public void AddDeleteUndoArcInfo(String id, String sourceId, String targetId, bool selected)
        {
            if (DeleteUndoParams[1] == null)
                DeleteUndoParams[1] = new List<ArcInfo>();
            ((List<ArcInfo>)DeleteUndoParams[1]).Add(new ArcInfo(id, sourceId, targetId, selected));
        }

        /// <summary>
        /// Stores the id of an element for a delete redo operation.
        /// </summary>
        /// <param name="id">The id of the element that has been restored.</param>
        public void AddDeleteRedoId(String id)
        {
            if (DeleteRedoParams[0] == null)
                DeleteRedoParams[0] = new List<String>();
            ((List<String>)DeleteRedoParams[0]).Add(id);
        }

        /// <summary>
        /// Pushes the delete undo operation to the undo stack.
        /// </summary>
        public void PushDeleteUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.Delete, DeleteUndoParams });
            DeleteUndoParams = new object[2];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the delete redo operation to the redo stack.
        /// </summary>
        public void PushDeleteRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.Delete, DeleteRedoParams });
            DeleteRedoParams = new object[1];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Stores the id of an element for a create undo operation.
        /// </summary>
        /// <param name="id">The id of the element that has been created.</param>
        public void AddCreateUndoId(String id)
        {
            CreateUndoParams[0] = id;
        }

        /// <summary>
        /// Stores the view size before a create operation for a create undo operation.
        /// </summary>
        /// <param name="viewWidth">The view width before the create operation.</param>
        /// <param name="viewHeight">The view height before the create operation.</param>
        public void AddCreateUndoViewSize(double viewWidth, double viewHeight)
        {
            CreateUndoParams[1] = new Point(viewWidth, viewHeight);
        }

        /// <summary>
        /// Stores node info for a create redo operation.
        /// </summary>
        /// <param name="id">The id of the deleted node.</param>
        /// <param name="name">The name of the deleted node.</param>
        /// <param name="x">The x-coordinate of the deleted node.</param>
        /// <param name="y">The y-coordinate of the deleted node.</param>
        /// <param name="tokenCount">The token count of the deleted node.</param>
        public void AddCreateRedoNodeInfo(String id, String name, int x, int y, int? tokenCount)
        {
            CreateRedoParams[0] = new NodeInfo(id, name, x, y, tokenCount);
        }

        /// <summary>
        /// Stores arc info for a create redo operation.
        /// </summary>
        /// <param name="id">The id of the deleted arc.</param>
        /// <param name="sourceId">The id of the source of the deleted arc.</param>
        /// <param name="targetId">The id of the target of the deleted arc.</param>
        /// <param name="selected">The selected state of the deleted arc.</param>
        public void AddCreateRedoArcInfo(String id, String sourceId, String targetId, bool selected)
        {
            CreateRedoParams[1] = new ArcInfo(id, sourceId, targetId, selected);
        }

        /// <summary>
        /// Stores the view size before a create undo operation as well as the necessary amount of node shift for
        /// a create redo operation.
        /// </summary>
        /// <param name="viewWidth">The view width before the create undo operation.</param>
        /// <param name="viewHeight">The view height before the create undo operation.</param>
        /// <param name="rightShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="downShift">The amount by which elements had to be shifted in y-direction.</param>
        public void AddCreateRedoParams(double viewWidth, double viewHeight, double rightShift, double downShift)
        {
            CreateRedoParams[2] = new Point(viewWidth, viewHeight);
            CreateRedoParams[3] = new Point(rightShift, downShift);
        }

        /// <summary>
        /// Adds shift and pushes the create undo operation to the undo stack.
        /// </summary>
        public void PushCreateUndo()
        {
            CreateUndoParams[2] = new Point(CreateRightShift, CreateDownShift);
            CreateRightShift = 0;
            CreateDownShift = 0;
            Undos.Push(new object[2] { UndoRedoOps.Create, CreateUndoParams });
            CreateUndoParams = new object[3];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the create redo operation the the redo stack.
        /// </summary>
        public void PushCreateRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.Create, CreateRedoParams });
            CreateRedoParams = new object[4];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds the complete parameters for a token change undo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token change has been performed.</param>
        /// <param name="count">The token count of the node before the operation.</param>
        public void AddTokenChangeUndoParams(String id, int count)
        {
            TokenChangeUndoParams[0] = id;
            TokenChangeUndoParams[1] = count;
        }

        /// <summary>
        /// Adds the complete parameters for a token change redo operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token change has been performed.</param>
        /// <param name="count">The token count of the node before the operation.</param>
        public void AddTokenChangeRedoParams(String id, int count)
        {
            TokenChangeRedoParams[0] = id;
            TokenChangeRedoParams[1] = count;
        }

        /// <summary>
        /// Pushes the token change undo operation to the undo stack.
        /// </summary>
        public void PushTokenChangeUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.SetTokens, TokenChangeUndoParams });
            TokenChangeUndoParams = new object[2];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the token change redo operation to the redo stack.
        /// </summary>
        public void PushTokenChangeRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.SetTokens, TokenChangeRedoParams });
            TokenChangeRedoParams = new object[2];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Stores the id of an element for a transition undo operation.
        /// </summary>
        /// <param name="id">The id of the node the transition has been performed on.</param>
        public void AddTransitionUndoId(String id)
        {
            TransitionUndoParams[0] = id;
        }

        /// <summary>
        /// Stores the id of an element for a transition redo operation.
        /// </summary>
        /// <param name="id">The id of the node the transition undo has been performed on.</param>
        public void AddTransitionRedoId(String id)
        {
            TransitionRedoParams[0] = id;
        }

        /// <summary>
        /// Pushes the transition undo operation to the undo stack.
        /// </summary>
        public void PushTransitionUndo()
        {
            Undos.Push(new object[2] { UndoRedoOps.Activate, TransitionUndoParams });
            TransitionUndoParams = new object[1];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the transition redo operation to the redo stack.
        /// </summary>
        public void PushTransitionRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.Activate, TransitionRedoParams });
            TransitionRedoParams = new object[1];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Adds the complete parameters for a size change undo operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the size change operation.</param>
        /// <param name="viewWidth">The view width before the size change operation.</param>
        /// <param name="viewHeight">The view height before the size change operation.</param>
        public void AddSizeChangeUndoParams(int sizeFactor, double viewWidth, double viewHeight)
        {
            SizeChangeUndoParams[0] = sizeFactor;
            SizeChangeUndoParams[1] = new Point(viewWidth, viewHeight);
        }

        /// <summary>
        /// Adds the complete parameters for a size change redo operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the size change undo operation.</param>
        /// <param name="viewWidth">The view width before the size change undo operation.</param>
        /// <param name="viewHeight">The view height before the size change undo operation.</param>
        /// <param name="rightShift">The amount by which elements had to be shifted in x-direction.</param>
        /// <param name="downShift">The amount by which elements had to be shifted in y-direction.</param>
        public void AddSizeChangeRedoParams(int sizeFactor, double viewWidth, double viewHeight, 
                                            double rightShift, double downShift)
        {
            SizeChangeRedoParams[0] = sizeFactor;
            SizeChangeRedoParams[1] = new Point(viewWidth, viewHeight);
            SizeChangeRedoParams[2] = new Point(rightShift, downShift);
        }

        /// <summary>
        /// Adds shift and pushes the size change undo operation to the undo stack.
        /// </summary>
        public void PushSizeChangeUndo()
        {
            SizeChangeUndoParams[2] = new Point(SizeChangeRightShift, SizeChangeDownShift);
            SizeChangeRightShift = 0;
            SizeChangeDownShift = 0;
            Undos.Push(new object[2] { UndoRedoOps.ChangeSize, SizeChangeUndoParams });
            SizeChangeUndoParams = new object[3];
            UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Pushes the size change redo operation to the redo stack.
        /// </summary>
        public void PushSizeChangeRedo()
        {
            Redos.Push(new object[2] { UndoRedoOps.ChangeSize, SizeChangeRedoParams });
            SizeChangeRedoParams = new object[3];
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Clears the redo stack.
        /// </summary>
        public void ClearRedoStack()
        {
            Redos.Clear();
            RedoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Clears the undo stack.
        /// </summary>
        public void ClearUndoStack()
        {
            Undos.Clear();
            UndoCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #endregion
    }
}
