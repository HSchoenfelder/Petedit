using PetriNetModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides logic for the manipulation of visual elements in the petrinet.
    /// </summary>
    public class ElementManager : INotifyPropertyChanged
    {
        #region fields
        /// <summary> Store for the Model property. </summary>
        private ModelMain _model;

        /// <summary> Store for the ElementProvider property. </summary>
        private ElementProvider _elementProvider;

        /// <summary> Store for the SelectionManager property. </summary>
        private SelectionManager _selectionManager;

        /// <summary> Store for the UndoManager property. </summary>
        private UndoManager _undoManager;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize;

        /// <summary> Store for the ArrowheadSize property. </summary>
        private int _arrowheadSize;

        /// <summary> Store for the ViewWidth property. </summary>
        private double _viewWidth;

        /// <summary> Store for the ViewHeight property. </summary>
        private double _viewHeight;

        /// <summary> Store for the NodeMode property. </summary>
        private NodeMode _nodeMode = NodeMode.Movenode;

        /// <summary> Store for the MouseOverTarget property. </summary>
        private bool _mouseOverTarget;

        /// <summary> Store for the DrawSourceId property. </summary>
        private string _drawSourceId;

        /// <summary> Store for the DrawTargetId property. </summary>
        private string _drawTargetId;

        /// <summary> Store for the DragSourceId property. </summary>
        private string _dragSourceId;

        /// <summary> Store for the DrawArc property. </summary>
        private IDrawingArc _drawingArc;

        /// <summary> Store for the DragStart property. </summary>
        private NPoint _dragStart;

        /// <summary> Store for the KeepSelected property. </summary>
        private bool _keepSelected;

        /// <summary> Store for the NodeModeChangeCommand property. </summary>
        private readonly DelegateCommand<NodeMode> _nodeModeChangeCommand;

        /// <summary> Store for the NameChangeClickCommand property. </summary>
        private readonly DelegateCommand<String> _nameChangeClickCommand;

        /// <summary> Store for the NameFieldClickedCommand property. </summary>
        private readonly DelegateCommand<String> _nameFieldClickedCommand;

        /// <summary> Store for the NameConfirmedCommand property. </summary>
        private readonly DelegateCommand<String> _nameConfirmedCommand;

        /// <summary> Store for the NameChangedCommand property. </summary>
        private readonly DelegateCommand<String, String> _nameChangedCommand;

        /// <summary> Store for the NodeMouseLeftButtonDownCommand property. </summary>
        private readonly DelegateCommand<String, Point, bool> _nodeMouseLeftButtonDownCommand;

        /// <summary> Store for the ArcMouseLeftButtonDownCommand property. </summary>
        private DelegateCommand<String, bool> _arcMouseLeftButtonDownCommand;

        /// <summary> Store for the NodeMouseMoveCommand property. </summary>
        private readonly DelegateCommand<Point> _nodeMouseMoveCommand;

        /// <summary> Store for the NodeMouseLeftButtonUpCommand property. </summary>
        private readonly DelegateCommand<String, Point, bool> _nodeMouseLeftButtonUpCommand;

        /// <summary> Store for the MouseLeftButtonUpCommand property. </summary>
        private readonly DelegateCommand<Point> _mouseLeftButtonUpCommand;

        /// <summary> Store for the TokensChangedCommand property. </summary>
        private readonly DelegateCommand<String, String> _tokensChangedCommand;

        /// <summary> Store for the PerformTransitionCommand property. </summary>
        private readonly DelegateCommand<String> _performTransitionCommand;
        #endregion

        #region delegates
        public delegate void BlockStateChangedEventHandler(object source, StateChangedEventArgs e);

        public delegate void ViewSizeChangedEventHandler(object source, ViewSizeChangedEventArgs e);

        public delegate void DrawingStateChangedEventHandler(object source, StateChangedEventArgs e);
        #endregion

        #region events
        /// <summary> Occurs when a tracked property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Occurs when a modification is made to the model. </summary>
        public event EventHandler Modified;

        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        public event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when a modification is made to ViewWidth of ViewHeight. </summary>
        public event ViewSizeChangedEventHandler ViewSizeChanged;

        /// <summary> Occurs when the state change block is to be set or released. </summary>
        public event BlockStateChangedEventHandler BlockStateChanged;

        /// <summary> Occurs when the drawing state changed. </summary>
        public event DrawingStateChangedEventHandler DrawingStateChanged;
        #endregion

        #region properties

        #region private
        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private ModelMain Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Gets the element provider that enables access to individual elements of the petrinet.
        /// </summary>
        private ElementProvider ElementProvider
        {
            get { return _elementProvider; }
        }

        /// <summary> Gets the selection manager that provides access to select functions. </summary>
        private SelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary> Gets the UndoManager that manages undo and redo operations. </summary>
        private UndoManager UndoManager
        {
            get { return _undoManager; }
        }

        /// <summary>
        /// Gets or sets the id of the VisualNode that initiated the current draw operation.
        /// </summary>
        private String DrawSourceId
        {
            get { return _drawSourceId; }
            set
            {
                _drawSourceId = value;
                if (value != null)
                    BlockStateChanged(this, new StateChangedEventArgs(true));
                else
                    BlockStateChanged(this, new StateChangedEventArgs(false));
            }
        }

        /// <summary>
        /// Gets or sets the id of the VisualNode that serves as a draw target during a draw
        /// operation.
        /// </summary>
        private String DrawTargetId
        {
            get { return _drawTargetId; }
            set { _drawTargetId = value; }
        }

        /// <summary>
        /// Gets or sets the coordinates of the exact location from which a drag operation was initiated
        /// within the originating node.
        /// </summary>
        private NPoint DragStart
        {
            get { return _dragStart; }
            set { _dragStart = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether a deselection should not be performed
        /// upon MouseUp.
        /// </summary>
        private bool KeepSelected
        {
            get { return _keepSelected; }
            set { _keepSelected = value; }
        }
        #endregion

        #region public
        /// <summary> Gets or sets the current global draw size. </summary>
        public int DrawSize
        {
            get { return _drawSize; }
            set
            {
                if (value != _drawSize)
                {
                    _drawSize = value;
                    OnDrawSizeChanged(value);
                }
            }
        }

        /// <summary> Gets or sets the current global arrowhead size. </summary>
        public int ArrowheadSize
        {
            get { return _arrowheadSize; }
            set
            {
                if (value != _arrowheadSize)
                {
                    _arrowheadSize = value;
                    OnArrowheadSizeChanged(value);
                }
            }
        }

        /// <summary> 
        /// Gets or sets the width of the visual presentation of the petrinet. 
        /// </summary>
        public double ViewWidth
        {
            get { return _viewWidth; }
            set { _viewWidth = value; }
        }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        public double ViewHeight
        {
            get { return _viewHeight; }
            set { _viewHeight = value; }
        }

        /// <summary>
        /// Gets or sets the current NodeMode. Allows switching between move, draw and manipulate 
        /// mode.
        /// </summary>
        public NodeMode NodeMode
        {
            get { return _nodeMode; }
            set
            {
                if (_nodeMode != value)
                {
                    _nodeMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the id of the VisualNode that initiated the current drag operation.
        /// </summary>
        public String DragSourceId
        {
            get { return _dragSourceId; }
            set
            {
                _dragSourceId = value;
                if (value != null)
                    BlockStateChanged(this, new StateChangedEventArgs(true));
                else
                    BlockStateChanged(this, new StateChangedEventArgs(false));
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether the mouse pointer is currently 
        /// located over a potential target during a draw operation.
        /// </summary>
        public bool MouseOverTarget
        {
            get { return _mouseOverTarget; }
            set
            {
                if (_mouseOverTarget != value)
                {
                    _mouseOverTarget = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the visual arc instance that visually presents a arc drawing operation.
        /// </summary>
        public IDrawingArc DrawingArc
        {
            get { return _drawingArc; }
        }

        /// <summary> Gets the command that is executed when the node mode changes. </summary>
        public DelegateCommand<NodeMode> NodeModeChangeCommand
        {
            get { return _nodeModeChangeCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when a click on the name change button occurs.
        /// </summary>
        public DelegateCommand<String> NameChangeClickCommand
        {
            get { return _nameChangeClickCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when a click on the name field occurs.
        /// </summary>
        public DelegateCommand<String> NameFieldClickedCommand
        {
            get { return _nameFieldClickedCommand; }
        }

        /// <summary>
        /// Gets the command that is executed upon manual confirmation of an entered name.
        /// </summary>
        public DelegateCommand<String> NameConfirmedCommand
        {
            get { return _nameConfirmedCommand; }
        }

        /// <summary> Gets the command that is executed to finalize a name change. </summary>
        public DelegateCommand<String, String> NameChangedCommand
        {
            get { return _nameChangedCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonDown event occurs on the 
        /// visual presentation of the VisualNode.
        /// </summary>
        public DelegateCommand<String, Point, bool> NodeMouseLeftButtonDownCommand
        {
            get { return _nodeMouseLeftButtonDownCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonDown event occurs on the visual 
        /// presentation of the VisualArc.
        /// </summary>
        public DelegateCommand<String, bool> ArcMouseLeftButtonDownCommand
        {
            get { return _arcMouseLeftButtonDownCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseMove event occurs on the visual 
        /// presentation of the VisualNode.
        /// </summary>
        public DelegateCommand<Point> NodeMouseMoveCommand
        {
            get { return _nodeMouseMoveCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonUp event occurs on the 
        /// visual presentation of the VisualNode.
        /// </summary>
        public DelegateCommand<String, Point, bool> NodeMouseLeftButtonUpCommand
        {
            get { return _nodeMouseLeftButtonUpCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonUp event occurs on the 
        /// presentation area.
        /// </summary>
        public DelegateCommand<Point> MouseLeftButtonUpCommand
        {
            get { return _mouseLeftButtonUpCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the amount of tokens on the VisualNode is
        /// changed.
        /// </summary>
        public DelegateCommand<String, String> TokensChangedCommand
        {
            get { return _tokensChangedCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when a transition is performed on the VisualNode.
        /// </summary>
        public DelegateCommand<String> PerformTransitionCommand
        {
            get { return _performTransitionCommand; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new ElementManager with the specified references and the specified initial drawsize and
        /// arrowheadSize.
        /// </summary>
        /// <param name="elementProvider">Reference to the element provider.</param>
        /// <param name="selectionManager">Reference to the selection manager.</param>
        /// <param name="undoManager">Reference to the undo manager.</param>
        /// <param name="model">Reference to the model of the petrinet.</param>
        /// <param name="drawSize">The initial drawsize.</param>
        /// <param name="arrowheadSize">The initial arrowhead size.</param>
        public ElementManager(ElementProvider elementProvider, SelectionManager selectionManager, UndoManager undoManager, 
                              ModelMain model, int drawSize, int arrowheadSize)
        {
            _elementProvider = elementProvider;
            _selectionManager = selectionManager;
            _undoManager = undoManager;
            _model = model;
            _drawSize = drawSize;
            _arrowheadSize = arrowheadSize;
            _drawingArc = new VisualArc(DrawSize, ArrowheadSize, this, Model);

            _nodeModeChangeCommand = new DelegateCommand<NodeMode>(HandleNodeModeChange);
            _nameChangeClickCommand = new DelegateCommand<String>(HandleNameChangeClick);
            _nameFieldClickedCommand = new DelegateCommand<String>(HandleNameFieldClicked);
            _nameConfirmedCommand = new DelegateCommand<String>(HandleNameConfirmed);
            _nameChangedCommand = new DelegateCommand<String, String>(HandleNameChanged);
            _nodeMouseLeftButtonDownCommand = new DelegateCommand<String, Point, bool>(HandleNodeMouseLeftButtonDown);
            _arcMouseLeftButtonDownCommand = new DelegateCommand<String, bool>(HandleArcMouseLeftButtonDown);
            _nodeMouseMoveCommand = new DelegateCommand<Point>(HandleNodeMouseMove);
            _nodeMouseLeftButtonUpCommand = new DelegateCommand<String, Point, bool>(HandleNodeMouseLeftButtonUp);
            _mouseLeftButtonUpCommand = new DelegateCommand<Point>(HandleMouseLeftButtonUp);
            _tokensChangedCommand = new DelegateCommand<String, String>(HandleTokensChanged);
            _performTransitionCommand = new DelegateCommand<String>(HandlePerformTransition, CanPerformTransition);
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Notifies the view that a property of this ViewModel has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when the global DrawSize changes. Distributes the new draw size to all 
        /// elements that need it.
        /// </summary>
        /// <param name="newDrawSize">The new global draw size.</param>
        private void OnDrawSizeChanged(int newDrawSize)
        {
            DrawingArc.DrawSize = newDrawSize;
        }

        /// <summary>
        /// Called when the global ArrowheadSize changes. Distributes the new arrowhead size to
        /// all elements that need it.
        /// </summary>
        /// <param name="newArrowheadSize"></param>
        private void OnArrowheadSizeChanged(int newArrowheadSize)
        {
            DrawingArc.ArrowheadSize = newArrowheadSize;
        }

        /// <summary>
        /// Handles the NodeModeChangeCommand. Sets the new node mode.
        /// </summary>
        /// <param name="newMode">The new node mode.</param>
        private void HandleNodeModeChange(NodeMode newMode)
        {
            NodeMode = newMode;
        }

        /// <summary>
        /// Handles the NameChangeClickCommand. Sets up the name field for entering a name.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        private void HandleNameChangeClick(String nodeId)
        {
            INameField nField = ElementProvider.GetNameField(nodeId);
            IVisualNode node = ElementProvider.GetNode(nodeId);
            nField.DrawSize = DrawSize;
            nField.NameFieldVisible = true;
            nField.TextFieldActive = true;
            UndoManager.AddNameChangeUndoParams(nodeId, null, ViewWidth, ViewHeight, 0, 0, node.XPos, node.YPos);
        }

        /// <summary>
        /// Handles the NameFieldClickedCommand. Requests focus on the name field when it is clicked upon.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        private void HandleNameFieldClicked(String nodeId)
        {
            INameField nField = ElementProvider.GetNameField(nodeId);
            IVisualNode node = ElementProvider.GetNode(nodeId);
            nField.TextFieldActive = true;
            UndoManager.AddNameChangeUndoParams(nodeId, nField.Name, ViewWidth, ViewHeight, nField.Width, nField.Height,
                                                node.XPos, node.YPos);
        }

        /// <summary>
        /// Handles the NameConfirmedCommand. Removes focus from the name field once the input operation
        /// is completed.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        private void HandleNameConfirmed(String nodeId)
        {
            ElementProvider.GetNameField(nodeId).TextFieldActive = false;
        }

        /// <summary>
        /// Handles the NameChangedCommand. Performs the actual name change and writes the new name to the model.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        /// <param name="name">The new name.</param>
        private void HandleNameChanged(String nodeId, String name)
        {
            INameField nField = ElementProvider.GetNameField(nodeId);
            nField.TextFieldActive = false;
            nField.Name = name;
            Model.ChangeName(nodeId, name);
            if (name.Equals(""))
                nField.NameFieldVisible = false;
            UndoManager.PushNameChangeUndo();
            UndoManager.ClearRedoStack();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Handles the NodeMouseLeftButtonDownCommand. Selects the VisualNode with the given id and initiates a 
        /// move operation in move mode, initiates a draw operation in draw mode and performs node operations in 
        /// manipulate mode.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        /// <param name="coords">The coordinates of the MouseLeftButtonDownEvent.</param>
        /// <param name="alternate">A value that indicates whether a modifier key was pressed.</param>
        private void HandleNodeMouseLeftButtonDown(String nodeId, Point coords, bool alternate)
        {
            switch (NodeMode)
            {
                case NodeMode.Movenode:
                    KeepSelected = SelectNode(nodeId, alternate);
                    InitializeNodeMove(nodeId, coords);
                    break;
                case NodeMode.Drawarc:
                    InitializeDrawingArc(nodeId);
                    break;
                case NodeMode.Manipulate:
                    if (Model.IsPlace(nodeId))
                        IncDecTokenCount(nodeId, alternate);
                    else
                        PerformTransition(nodeId);
                    break;
            }
        }

        /// <summary>
        /// Handles the ArcMouseLeftButtonDownCommand. If alternate indicates that a modifier key was pressed, the
        /// arc is either added to or removed from the selection. If alternate indicates that no modifier key was
        /// pressed, the selection state of the arc is changed and the rest of the selection is reset.
        /// </summary>
        /// <param name="arcId">The id of the arc that triggered the command.</param>
        /// <param name="alternate">A value that indicates whether a modifier key was pressed.</param>
        private void HandleArcMouseLeftButtonDown(String id, bool alternate)
        {
            if (!ElementProvider.GetArc(id).Selected)
                SelectArc(id, alternate);
            else
                DeselectElement(id, alternate);
        }

        /// <summary>
        /// Handles the NodeMouseMoveCommand. Performs a move operation in move mode and a draw operation
        /// in draw mode.
        /// </summary>
        /// <param name="pos">The position of the mouse pointer after the move.</param>
        private void HandleNodeMouseMove(Point pos)
        {
            switch (NodeMode)
            {
                case NodeMode.Movenode:
                    if (DragSourceId != null) 
                    { 
                        IVisualNode node = ElementProvider.GetNode(DragSourceId);
                        MoveSelectedItems(pos.X + DrawSize / 2 - node.XPos - DragStart.X,
                                            pos.Y + DrawSize / 2 - node.YPos - DragStart.Y);
                        KeepSelected = true; 
                    }
                    break;
                case NodeMode.Drawarc:
                    if (DrawSourceId != null)
                    {
                        DrawArc(pos); 
                    }
                    break;
                default:
                    break;
            } 
        }

        /// <summary>
        /// Handles the NodeMouseLeftButtonUpCommand. Deselects the VisualNode with the given id if 
        /// required and finalizes the move operation in move mode or finalizes the draw operation in 
        /// draw mode.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        /// <param name="coords">The coordinates of the MouseLeftButtonUpEvent.</param>
        /// <param name="alternate">A value that indicates whether a modifier key was pressed.</param>
        private void HandleNodeMouseLeftButtonUp(String nodeId, Point coords, bool alternate)
        {
            IVisualNode node = ElementProvider.GetNode(nodeId);
            switch (NodeMode)
            {
                case NodeMode.Movenode:
                    if (!KeepSelected && node.Selected)
                        DeselectElement(nodeId, alternate);
                    KeepSelected = false;
                    if (node.IsDragSource)
                    {
                        CommitMove();
                        DragStart = null;
                    }
                    node.IsDragSource = false;
                    break;
                case NodeMode.Drawarc:
                    if (node.IsDragSource)
                        FinishDrawing();
                    node.IsDragSource = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonUpCommand. Ends drawing operation if one was in progress.
        /// </summary>
        /// <param name="coords">The coordinates of the MouseLeftButtonUpEvent.</param>
        private void HandleMouseLeftButtonUp(Point coords)
        {
            if(DrawSourceId != null)
            {
                DrawingArc.Visible = false;
                DrawingArc.IsValid = false;
                DrawSourceId = null;
            }
        }

        /// <summary>
        /// Handles the TokensChangedCommand. Writes the changed token value of the specified node
        /// to the model.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        /// <param name="tokenCount">The new amount of tokens as a String.</param>
        private void HandleTokensChanged(String nodeId, String tokenCount)
        {
            IVisualNode node = ElementProvider.GetNode(nodeId);
            int count = Int32.Parse(tokenCount);
            bool changed = node.TokenCount != count;
            if (node.TokenCount != count)
            {
                UndoManager.AddTokenChangeUndoParams(nodeId, node.TokenCount);
                node.TokenCount = count;
                Model.ChangeTokens(nodeId, node.TokenCount);
                UndoManager.PushTokenChangeUndo();
                UndoManager.ClearRedoStack();
                Modified(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the PerformTransitionCommand. Causes the model to perform a transition on the
        /// node that corresponds to the specified id.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        private void HandlePerformTransition(String nodeId)
        {
            PerformTransition(nodeId);
        }

        /// <summary>
        /// Determines whether the PerformTransitionCommand is enabled or disabled.
        /// </summary>
        /// <param name="nodeId">The id of the node that triggered the command.</param>
        /// <returns>true if this VisualNode is an enabled transition; otherwise false.</returns>
        private bool CanPerformTransition(String nodeId)
        {
            return !Model.IsPlace(nodeId) && Model.GetState(nodeId);
        }

        /// <summary>
        /// Selects the node with the provided id if it is not already selected and clears the rest of the 
        /// selection depending on the value specified by multiselect.
        /// </summary>
        /// <param name="id">The id of the node to select.</param>
        /// <param name="multiselect">A value that indicates whether this VisualNode should be
        /// added to the existing selection or if it should start a new selection.</param>
        /// <returns>true if the VisualNode was not already selected; otherwise false</returns>
        private bool SelectNode(String id, bool multiselect)
        {
            IVisualNode node = ElementProvider.GetNode(id);
            if (!node.Selected)
            {
                UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
                if (!multiselect)
                    SelectionManager.ClearSelectedItems();
                SelectionManager.SelectElement(id);
                UndoManager.PushSelectUndo();
                UndoManager.ClearRedoStack();
                ReevaluateCommandState(this, new EventArgs());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performs initialization for an arc drawing operation.
        /// </summary>
        /// <param name="drawSourceId">The id of the visual node that requested the operation.</param>
        private void InitializeDrawingArc(String drawSourceId)
        {
            IVisualNode node = ElementProvider.GetNode(drawSourceId);
            DrawSourceId = drawSourceId;
            DrawingArc.SourceType = Model.IsPlace(drawSourceId) ? NodeType.Place : NodeType.Transition;
            DrawingArc.TargetNodePos = null;
            DrawingArc.SourceNodePos = Model.GetCoordinates(drawSourceId);
            node.IsDragSource = true;
            DrawingStateChanged(this, new StateChangedEventArgs(true));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Performs an arc draw operation from the current drawsource to the specified coordinates.
        /// </summary>
        /// <param name="pos">The coordinates to which the visual arc is to be drawn.</param>
        private void DrawArc(Point pos)
        {
            bool elementFound = false;
            bool sourceFound = false;
            // check if cursor is over a node of opposite type
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (node.IsContained(pos))
                {
                    if (node.NodeType != ElementProvider.GetNode(DrawSourceId).NodeType
                        && Model.GetArc(DrawSourceId, node.Id) == null)
                    {
                        node.IsDrawTarget = true;
                        DrawTargetId = node.Id;
                        DrawingArc.IsValid = true;
                        DrawingArc.TargetNodePos = Model.GetCoordinates(DrawTargetId);
                        MouseOverTarget = true;
                        elementFound = true;
                    }
                    else if (node.Id.Equals(DrawSourceId))
                        sourceFound = true;
                }
            }
            
            // if cursor has left a node of opposite type
            if (!elementFound && DrawTargetId != null)
            {
                ElementProvider.GetNode(DrawTargetId).IsDrawTarget = false;
                DrawTargetId = null;
                DrawingArc.IsValid = false;
                DrawingArc.TargetNodePos = null;
                MouseOverTarget = false;
            }
            // if cursor has left source node
            if (!sourceFound)
                ElementProvider.GetNode(DrawSourceId).IsHighlighted = false;
            // if not over node of opposite type
            if (DrawTargetId == null)
            {
                Point nodePosition = Model.GetCoordinates(DrawSourceId);

                pos = ApplyClippingCorrection(nodePosition, pos);

                if (Calculations.GetDistance(nodePosition, pos) > DrawSize / 2.0)
                {
                    DrawingArc.IsBeyondEdge = true;
                    DrawingArc.ActualTarget = pos;
                    DrawingArc.Visible = true;
                    DrawingArc.IsBeyondEdge = false;
                }
            }
        }

        /// <summary>
        /// Finalizes a arc draw operation.
        /// </summary>
        private void FinishDrawing()
        {
            if (DrawTargetId != null)
            {
                FinalizeArc();
                ElementProvider.GetNode(DrawTargetId).IsDrawTarget = false;
                DrawTargetId = null;
            }
            ElementProvider.GetNode(DrawSourceId).IsHighlighted = true;
            DrawingArc.Visible = false;
            DrawingArc.IsValid = false;
            MouseOverTarget = false;
            DrawSourceId = null;
            DrawingStateChanged(this, new StateChangedEventArgs(false));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Creates a new visual arc from the drawing arc and adds it to the presentation and model.
        /// </summary>
        private void FinalizeArc()
        {
            String arcId = Guid.NewGuid().ToString();
            Model.AddArc(DrawSourceId, DrawTargetId, arcId);
            VisualArc arc = (VisualArc)DrawingArc.Clone();
            arc.Id = arcId;
            ElementProvider.AddArc(arc);
            TryCreateBrother(arcId);
            SelectionManager.UpdateAutoSelection(arcId);
            UndoManager.AddCreateUndoId(arcId);
            UndoManager.AddCreateUndoViewSize(ViewWidth, ViewHeight);
            UndoManager.PushCreateUndo();
            UndoManager.ClearRedoStack();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Corrects the end point of the drawing arc during an arc draw operation so that the arrowhead
        /// does not leave the presentation area, but still aligns to the position of the mouse pointer
        /// even when it is outside of the presentation area.
        /// </summary>
        /// <param name="nodePos">The center coordinates of the node the visual arc is being drawn from.</param>
        /// <param name="target">The current target coordinates of the visual arc that is being drawn.</param>
        /// <returns>The corrected target coordinates for the visual arc that is being drawn.</returns>
        private Point ApplyClippingCorrection(Point nodePos, Point target)
        {
            // if target lies to the right of the presentation area
            if (target.X > ViewWidth)
            {
                target.Y = target.Y + Calculations.GetClippingCorrectionY(nodePos, target, ViewWidth);
                target.X = ViewWidth;
            }

            // if target lies to the left of the presentation area
            if (target.X < 0)
            {
                target.Y = target.Y + Calculations.GetClippingCorrectionY(nodePos, target, 0);
                target.X = 0;
            }

            // if target lies below the presentation area
            if (target.Y > ViewHeight)
            {
                target.X = target.X + Calculations.GetClippingCorrectionX(nodePos, target, ViewHeight);
                target.Y = ViewHeight;
            }

            // if target lies above the presentation area
            if (target.Y < 0)
            {
                target.X = target.X + Calculations.GetClippingCorrectionX(nodePos, target, 0);
                target.Y = 0;
            }
            return target;
        }

        /// <summary>
        /// Performs initialization for a node move operation.
        /// </summary>
        /// <param name="dragSourceId">The id of the visual node that requested the operation.</param>
        /// <param name="coords">The coordinates at which the node move has been initiated.</param>
        private void InitializeNodeMove(String dragSourceId, Point coords)
        {
            IVisualNode node = ElementProvider.GetNode(dragSourceId);
            DragStart = new NPoint(coords.X, coords.Y);
            node.IsDragSource = true;
            DragSourceId = dragSourceId;
            UndoManager.MoveUndoViewSize = new Point(ViewWidth, ViewHeight);
            UndoManager.MoveUndoStart = new Point(node.XPos, node.YPos);
        }

        /// <summary>
        /// Performs a move operation on all elements in the current selection by the specified offset.
        /// </summary>
        /// <param name="xOffset">The distance to move horizontally.</param>
        /// <param name="yOffset">The distance to move vertically.</param>
        private void MoveSelectedItems(double xOffset, double yOffset)
        {
            foreach (String id in SelectionManager.GetSelectedItems())
            {
                if (Model.IsNode(id))
                {
                    ElementProvider.GetNode(id).XPos += xOffset;
                    ElementProvider.GetNode(id).YPos += yOffset;
                    ElementProvider.GetNameField(id).AdjustNameField(); 
                    AdjustViewSize(id, UndoRedoOps.Move);
                    ScrollIntoView(DragSourceId);
                }
            }
        }

        /// <summary>
        /// Finalizes a move operation and commits the results to the model.
        /// </summary>
        private void CommitMove()
        {
            Point moveStart = Model.GetCoordinates(DragSourceId);
            foreach (String id in SelectionManager.GetSelectedItems())
            {
                if (Model.IsNode(id))
                {
                    IVisualNode node = ElementProvider.GetNode(id);
                    Model.ChangePosition(id, (int)node.XPos, (int)node.YPos);
                    UndoManager.AddMoveUndoId(id);
                }
            }
            Point moveOffset = new Point(Model.GetCoordinates(DragSourceId).X - moveStart.X,
                                         Model.GetCoordinates(DragSourceId).Y - moveStart.Y);
            if (moveOffset.X >= 1 || moveOffset.X <= -1 || moveOffset.Y >= 1 || moveOffset.Y <= -1 ||
                UndoManager.MoveUndoViewSize.X != ViewWidth || UndoManager.MoveUndoViewSize.Y != ViewHeight)
            {
                IVisualNode node = ElementProvider.GetNode(DragSourceId);
                UndoManager.AssembleMoveUndoParams(node.XPos, node.YPos);
                UndoManager.PushMoveUndo();
                UndoManager.ClearRedoStack();
                Modified(this, new EventArgs());
            }
            UndoManager.ClearMoveUndoParams();
            DragSourceId = null;
        }

        /// <summary>
        /// Increments or decrements the amount of tokens on the specified node as indicated by the 
        /// decrease flag and writes the new value to the model.
        /// </summary>
        /// <param name="nodeId">The id of the visual node that requested the operation.</param>
        /// <param name="decrease">A value that indicates whether the token count should be
        /// increased or decreased.</param>
        private void IncDecTokenCount(String nodeId, bool decrease)
        {
            IVisualNode node = ElementProvider.GetNode(nodeId);
            UndoManager.AddTokenChangeUndoParams(nodeId, node.TokenCount);
            if (!decrease)
                node.TokenCount += 1;
            else
                node.TokenCount -= 1;
            Model.ChangeTokens(nodeId, node.TokenCount);
            UndoManager.PushTokenChangeUndo();
            UndoManager.ClearRedoStack();
            Modified(this, new EventArgs()); 
        }

        /// <summary>
        /// Causes the model to perform a transition on the specified node if it is enabled.
        /// </summary>
        private void PerformTransition(String nodeId)
        {
            if (Model.GetState(nodeId))
            {
                UndoManager.AddTransitionUndoId(nodeId);
                Model.PerformTransition(nodeId);
                UndoManager.PushTransitionUndo();
                UndoManager.ClearRedoStack();
                Modified(this, new EventArgs()); 
            }
        }

        /// <summary>
        /// Scrolls the node with the given id back into view if it has been moved outside
        /// of the visible area.
        /// </summary>
        /// <param name="id">The id of the node to be scrolled back into view.</param>
        private void ScrollIntoView(String id)
        {
            if (ElementProvider.GetNameField(id).NameFieldVisible)
            {
                ElementProvider.GetNameField(id).IsBeyondEdge = false;
                ElementProvider.GetNameField(id).IsBeyondEdge = true;
            }
            else
            {
                ElementProvider.GetNode(id).IsBeyondEdge = false;
                ElementProvider.GetNode(id).IsBeyondEdge = true;
            }
        }
        #endregion

        #region public
        /// <summary>
        /// Selects the arc with the provided id if it is not already selected and clears the rest of the 
        /// selection depending on the value specified by multiselect.
        /// </summary>
        /// <param name="id">The id of the arc to select.</param>
        /// <param name="multiselect">A value that indicates whether this VisualNode should be
        /// added to the existing selection or if it should start a new selection.</param>
        /// <returns>true if the VisualNode was not already selected; otherwise false</returns>
        public bool SelectArc(String id, bool multiselect)
        {
            IVisualArc arc = ElementProvider.GetArc(id);
            if (!arc.Selected)
            {
                UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
                if (!multiselect)
                    SelectionManager.ClearSelectedItems();
                SelectionManager.SelectElement(id);
                UndoManager.PushSelectUndo();
                UndoManager.ClearRedoStack();
                ReevaluateCommandState(this, new EventArgs());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deselects the element with the provided id and clears the rest of the selection depending on the value
        /// specified by multiselect.
        /// </summary>
        /// <param name="id">The id of the element to deselect.</param>
        /// <param name="multiselect">A value that indicates whether this VisualNode should be
        /// removed from the existing selection or if the entire selection should be cleared
        /// </param>
        public void DeselectElement(String id, bool multiselect)
        {
            UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
            SelectionManager.RemoveElement(id);
            ReevaluateCommandState(this, new EventArgs());
            if (!multiselect)
                SelectionManager.ClearSelectedItems();
            UndoManager.PushSelectUndo();
            UndoManager.ClearRedoStack();
        }

        /// <summary>
        /// Adjusts the size of the visual presentation of the petrinet to fit the positioning of all
        /// nodes. To be called after the visual presentation of a node has been repositioned.
        /// </summary>
        /// <param name="id">The id of the node that requested the adjustment.</param>
        /// <param name="callType">A value that indicates what kind of operation is responsible for this 
        /// call.</param>
        public void AdjustViewSize(String id, UndoRedoOps callType)
        {
            IVisualNode node = ElementProvider.GetNode(id);
            INameField nField = ElementProvider.GetNameField(id);
            double xNameOverhang = (nField.Width > node.DrawSize ? (nField.Width - node.DrawSize) / 2 : 0);
            double yNameOverhang = nField.Height;
            double spanToRight = node.XPos + node.DrawSize / 2 + xNameOverhang;
            double spanToBottom = node.YPos + node.DrawSize / 2;
            double spanPastLeft = -(node.XPos - node.DrawSize / 2) + xNameOverhang;
            double spanPastTop = -(node.YPos - node.DrawSize / 2) + yNameOverhang;
            // if the node is located past the right edge of the presentation, add width
            if (spanToRight > ViewWidth)
                ViewSizeChanged(this, new ViewSizeChangedEventArgs(spanToRight, ViewHeight));
            // if the node is located past the bottom edge of the presentation, add height
            if (spanToBottom > ViewHeight) 
                ViewSizeChanged(this, new ViewSizeChangedEventArgs(ViewWidth, spanToBottom));
            // if the node is located past the left edge of the presentation, put node on left edge, 
            // reposition all other nodes and add width
            if (spanPastLeft > 0)
            {
                node.XPos = node.DrawSize / 2 + xNameOverhang;
                nField.XPos = 0;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                RightShift(node.Id, spanPastLeft, callType);
            }
            // if the node is located past the top edge of the presentation, put node on top edge, 
            // reposition all other nodes and add height
            if (spanPastTop > 0)
            {
                node.YPos = node.DrawSize / 2 + yNameOverhang;
                nField.YPos = 0;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                DownShift(node.Id, spanPastTop, callType);
            }
            ScrollIntoView(id);
        }

        /// <summary>
        /// Repositions a visual arc to match a visual node at the specified coordinates.
        /// </summary>
        /// <param name="arcId">The id of the visual arc to be repositioned.</param>
        /// <param name="nodeId">The id of the visual node that requests the operation.</param>
        /// <param name="xPos">The new x-coordinate of the visual node.</param>
        /// <param name="yPos">The new y-coordinate of the visual node.</param>
        public void RepositionArc(String arcId, String nodeId, double xPos, double yPos)
        {
            if (Model.IsSource(arcId, nodeId))
                ElementProvider.GetArc(arcId).SourceNodePos = new NPoint(xPos, yPos);
            else
                ElementProvider.GetArc(arcId).TargetNodePos = new NPoint(xPos, yPos);
        }

        /// <summary>
        /// Removes the brother relationship with the brother of the Arc with the specified id 
        /// if there is one.
        /// </summary>
        public void RemoveBrother(String arcId)
        {
            String brotherId = ElementProvider.GetArc(arcId).BrotherId;
            if (brotherId != null)
            {
                IVisualArc brother = ElementProvider.GetArc(brotherId);
                brother.BrotherId = null;
                // remove and readd arc to allow visual template to update
                ElementProvider.RemoveArc(brother);
                ElementProvider.AddArc(brother);
            }
        }

        /// <summary>
        /// Creates a brother relationship with the inverse of the arc with the specified id, 
        /// if there should be one. 
        /// </summary>
        public void TryCreateBrother(String arcId)
        {
            String[] nodes = Model.GetNodesForArc(arcId);
            String sourceId = Model.IsSource(arcId, nodes[0]) ? nodes[0] : nodes[1];
            String targetId = Model.IsSource(arcId, nodes[0]) ? nodes[1] : nodes[0];
            String brotherId = null;
            if ((brotherId = Model.GetArc(targetId, sourceId)) != null)
            {
                IVisualArc brother = ElementProvider.GetArc(brotherId);
                brother.BrotherId = arcId;
                ElementProvider.GetArc(arcId).BrotherId = brotherId;
                // remove and readd arc to allow visual template to update
                ElementProvider.RemoveArc(brother);
                ElementProvider.AddArc(brother);
            }
        }

        /// <summary>
        /// Shifts all visual nodes to the right by the specified amount.
        /// </summary>
        /// <param name="nodeId">The id of the node that is not affected by the shift.</param>
        /// <param name="rightShift">The amount by which to shift to the right.</param>
        /// <param name="moveCall">A value that indicates whether this call belongs to a node
        /// move operation.</param>
        public void RightShift(String nodeId, double rightShift, UndoRedoOps callType)
        {
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (node.Id.Equals(nodeId))
                    continue;
                node.XPos += rightShift;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
            }
            for (int i = 0; i < ElementProvider.NameFieldsCount; i++)
            {
                INameField nField = ElementProvider.GetNameField(i);
                if (nField.Id.Equals(nodeId))
                    continue;
                nField.XPos += rightShift;
            }
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(ViewWidth + rightShift, ViewHeight));
            switch (callType)
            {
                case UndoRedoOps.Move:
                    UndoManager.MoveRightShift += rightShift;
                    break;
                case UndoRedoOps.Create:
                    UndoManager.CreateRightShift += rightShift;
                    break;
                case UndoRedoOps.ChangeSize:
                    UndoManager.SizeChangeRightShift += rightShift;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Shifts all visual nodes down by the specified amount.
        /// </summary>
        /// <param name="nodeId">The id of the node that is not affected by the shift.</param>
        /// <param name="downShift">The amount by which to shift down.</param>
        /// <param name="moveCall">A value that indicates whether this call belongs to a node
        /// move operation.</param>
        public void DownShift(String nodeId, double downShift, UndoRedoOps callType)
        {
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (node.Id.Equals(nodeId))
                    continue;
                node.YPos += downShift;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
            }
            for (int i = 0; i < ElementProvider.NameFieldsCount; i++)
            {
                INameField nField = ElementProvider.GetNameField(i);
                if (nField.Id.Equals(nodeId))
                    continue;
                nField.YPos += downShift;
            }
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(ViewWidth, ViewHeight + downShift));
            switch (callType)
            {
                case UndoRedoOps.Move:
                    UndoManager.MoveDownShift += downShift;
                    break;
                case UndoRedoOps.Create:
                    UndoManager.CreateDownShift += downShift;
                    break;
                case UndoRedoOps.ChangeSize:
                    UndoManager.SizeChangeDownShift += downShift;
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Handles the TokensChangedEvent of the model. Sets the modfied token count to the
        /// respective visual node.
        /// </summary>
        /// <param name="e">The TokensChangedEventArgs containing the event data.</param>
        public void HandleModelTokensChanged(TokensChangedEventArgs e)
        {
            ElementProvider.GetNode(e.PlaceId).TokenCount = e.TokenCount;
        }

        /// <summary>
        /// Handles the TransitionStateChangedEvent of the model. Sets the enabled state of the
        /// respective visual node.
        /// </summary>
        /// <param name="e">The TransitionStateChangedEventArgs containing the event data.</param>
        public void HandleModelTransitionStateChanged(TransitionStateChangedEventArgs e)
        {
            ElementProvider.GetNode(e.TransitionId).Enabled = e.Active;
        }
        #endregion

        #endregion
    }
}
